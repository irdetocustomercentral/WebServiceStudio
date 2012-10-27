
	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace WebServiceStudio
{
    public class Wsdl
    {
        static Wsdl()
        {
            schemaValidationFailure =
                "Schema could not be validated. Class generation may fail or may produce incorrect results";
            duplicateService = "Warning: Ignoring duplicate service description with TargetNamespace='{0}' from '{1}'.";
            duplicateSchema = "Warning: Ignoring duplicate schema description with TargetNamespace='{0}' from '{1}'.";
        }

        public Wsdl()
        {
            proxyCode = "<NOT YET>";
            proxyNamespace = "";
            paths = new StringCollection();
            codeProvider = null;
            wsdls = new StringCollection();
            xsds = new StringCollection();
            cancelled = false;
        }

        private void AddDocument(string path, object document, XmlSchemas schemas,
                                 ServiceDescriptionCollection descriptions)
        {
            ServiceDescription description1 = document as ServiceDescription;
            if (description1 != null)
            {
                if (descriptions[description1.TargetNamespace] == null)
                {
                    descriptions.Add(description1);
                    StringWriter writer1 = new StringWriter();
                    XmlTextWriter writer2 = new XmlTextWriter(writer1);
                    writer2.Formatting = Formatting.Indented;
                    description1.Write(writer2);
                    wsdls.Add(writer1.ToString());
                }
                else
                {
                    CheckPoint(MessageType.Warning, string.Format(duplicateService, description1.TargetNamespace, path));
                }
            }
            else
            {
                XmlSchema schema1 = document as XmlSchema;
                if (schema1 != null)
                {
                    if (schemas[schema1.TargetNamespace] == null)
                    {
                        schemas.Add(schema1);
                        StringWriter writer3 = new StringWriter();
                        XmlTextWriter writer4 = new XmlTextWriter(writer3);
                        writer4.Formatting = Formatting.Indented;
                        schema1.Write(writer4);
                        xsds.Add(writer3.ToString());
                    }
                    else
                    {
                        CheckPoint(MessageType.Warning,
                                   string.Format(duplicateSchema, description1.TargetNamespace, path));
                    }
                }
            }
        }

        private static void AddElementAndType(XmlSchema schema, string baseXsdType, string ns)
        {
            XmlSchemaElement element1 = new XmlSchemaElement();
            element1.Name = baseXsdType;
            element1.SchemaTypeName = new XmlQualifiedName(baseXsdType, ns);
            schema.Items.Add(element1);
            XmlSchemaComplexType type1 = new XmlSchemaComplexType();
            type1.Name = baseXsdType;
            XmlSchemaSimpleContent content1 = new XmlSchemaSimpleContent();
            type1.ContentModel = content1;
            XmlSchemaSimpleContentExtension extension1 = new XmlSchemaSimpleContentExtension();
            extension1.BaseTypeName = new XmlQualifiedName(baseXsdType, "http://www.w3.org/2001/XMLSchema");
            content1.Content = extension1;
            schema.Items.Add(type1);
        }

        private static void AddFakeSchemas(XmlSchema parent, XmlSchemas schemas)
        {
            if (schemas["http://www.w3.org/2001/XMLSchema"] == null)
            {
                XmlSchemaImport import1 = new XmlSchemaImport();
                import1.Namespace = "http://www.w3.org/2001/XMLSchema";
                import1.Schema = CreateFakeXsdSchema("http://www.w3.org/2001/XMLSchema", "schema");
                parent.Includes.Add(import1);
            }
            if (schemas["http://schemas.xmlsoap.org/soap/encoding/"] == null)
            {
                XmlSchemaImport import2 = new XmlSchemaImport();
                import2.Namespace = "http://schemas.xmlsoap.org/soap/encoding/";
                import2.Schema = CreateFakeSoapEncodingSchema("http://schemas.xmlsoap.org/soap/encoding/", "Array");
                parent.Includes.Add(import2);
            }
            if (schemas["http://schemas.xmlsoap.org/wsdl/"] == null)
            {
                XmlSchemaImport import3 = new XmlSchemaImport();
                import3.Namespace = "http://schemas.xmlsoap.org/wsdl/";
                import3.Schema = CreateFakeWsdlSchema("http://schemas.xmlsoap.org/wsdl/");
                parent.Includes.Add(import3);
            }
        }

        private static void AddSimpleType(XmlSchema schema, string typeName, string baseXsdType)
        {
            XmlSchemaSimpleType type1 = new XmlSchemaSimpleType();
            type1.Name = typeName;
            XmlSchemaSimpleTypeRestriction restriction1 = new XmlSchemaSimpleTypeRestriction();
            restriction1.BaseTypeName = new XmlQualifiedName(baseXsdType, "http://www.w3.org/2001/XMLSchema");
            type1.Content = restriction1;
            schema.Items.Add(type1);
        }

        public void Cancel()
        {
            cancelled = true;
        }

        private void ChangeBaseType(CodeCompileUnit compileUnit)
        {
            if ((WsdlProperties.ProxyBaseType != null) && (WsdlProperties.ProxyBaseType.Length != 0))
            {
                Type type1 = Type.GetType(WsdlProperties.ProxyBaseType, true);
                foreach (CodeNamespace namespace1 in compileUnit.Namespaces)
                {
                    foreach (CodeTypeDeclaration declaration1 in namespace1.Types)
                    {
                        bool flag1 = false;
                        foreach (CodeAttributeDeclaration declaration2 in declaration1.CustomAttributes)
                        {
                            if (declaration2.Name == typeof (WebServiceBindingAttribute).FullName)
                            {
                                flag1 = true;
                                break;
                            }
                        }
                        if (flag1)
                        {
                            declaration1.BaseTypes[0] = new CodeTypeReference(type1.FullName);
                        }
                    }
                }
            }
        }

        private void CheckPoint(MessageType status, string message)
        {
            if (!cancelled)
            {
                MainForm.ShowMessage(this, status, message);
            }
        }

        private static void CollectIncludes(XmlSchema schema, Hashtable includeSchemas)
        {
            foreach (XmlSchemaExternal external1 in schema.Includes)
            {
                string text1 = external1.SchemaLocation;
                if (external1 is XmlSchemaImport)
                {
                    external1.SchemaLocation = null;
                }
                else if (((external1 is XmlSchemaInclude) && (text1 != null)) && (text1.Length > 0))
                {
                    string text2 = Path.GetFullPath(text1).ToLower();
                    if (includeSchemas[text2] == null)
                    {
                        XmlSchema schema1 = ReadSchema(text1);
                        includeSchemas[text2] = schema1;
                        CollectIncludes(schema1, includeSchemas);
                    }
                    external1.Schema = (XmlSchema) includeSchemas[text2];
                    external1.SchemaLocation = null;
                }
            }
        }

        private void Compile(XmlSchemas userSchemas)
        {
            XmlSchema schema1 = new XmlSchema();
            foreach (XmlSchema schema2 in userSchemas)
            {
                if ((schema2.TargetNamespace != null) && (schema2.TargetNamespace.Length == 0))
                {
                    schema2.TargetNamespace = null;
                }
                if (schema2.TargetNamespace == schema1.TargetNamespace)
                {
                    XmlSchemaInclude include1 = new XmlSchemaInclude();
                    include1.Schema = schema2;
                    schema1.Includes.Add(include1);
                }
                else
                {
                    XmlSchemaImport import1 = new XmlSchemaImport();
                    import1.Namespace = schema2.TargetNamespace;
                    import1.Schema = schema2;
                    schema1.Includes.Add(import1);
                }
            }
            AddFakeSchemas(schema1, userSchemas);
            try
            {
                XmlSchemaCollection collection1 = new XmlSchemaCollection();
                collection1.ValidationEventHandler += new ValidationEventHandler(ValidationCallbackWithErrorCode);
                collection1.Add(schema1);
                if (collection1.Count == 0)
                {
                    CheckPoint(MessageType.Warning, schemaValidationFailure);
                }
            }
            catch (Exception exception1)
            {
                CheckPoint(MessageType.Warning, schemaValidationFailure + "\n" + exception1.Message);
                return;
            }
        }

        private void CreateCodeGenerator(out ICodeGenerator codeGen, out string fileExtension)
        {
            Language language1 = WsdlProperties.Language;
            switch (language1)
            {
                case Language.CS:
                    codeProvider = new CSharpCodeProvider();
                    break;

                case Language.VB:
                    codeProvider = new VBCodeProvider();
                    break;

                default:
                    {
                        if (language1 != Language.Custom)
                        {
                            throw new Exception("Unknown language");
                        }
                        Type type1 = Type.GetType(WsdlProperties.CustomCodeDomProvider);
                        if (type1 == null)
                        {
                            throw new TypeLoadException("Type '" + WsdlProperties.CustomCodeDomProvider +
                                                        "' is not found");
                        }
                        codeProvider = (CodeDomProvider) Activator.CreateInstance(type1);
                        break;
                    }
            }
            if (codeProvider != null)
            {
                codeGen = codeProvider.CreateGenerator();
                fileExtension = codeProvider.FileExtension;
                if (fileExtension == null)
                {
                    fileExtension = string.Empty;
                }
                else if ((fileExtension.Length > 0) && (fileExtension[0] != '.'))
                {
                    fileExtension = "." + fileExtension;
                }
            }
            else
            {
                fileExtension = ".src";
                codeGen = null;
            }
        }

        private DiscoveryClientProtocol CreateDiscoveryClient()
        {
            DiscoveryClientProtocol protocol1 = new DiscoveryClientProtocol();
            protocol1.AllowAutoRedirect = true;
            if ((WsdlProperties.UserName != null) && (WsdlProperties.UserName.Length != 0))
            {
                protocol1.Credentials =
                    new NetworkCredential(WsdlProperties.UserName, WsdlProperties.Password, WsdlProperties.Domain);
            }
            else
            {
                protocol1.Credentials = CredentialCache.DefaultCredentials;
            }
            if ((WsdlProperties.ProxyServer != null) && (WsdlProperties.ProxyServer.Length != 0))
            {
                IWebProxy proxy1 = null;
                proxy1 = new WebProxy(WsdlProperties.ProxyServer);
                proxy1.Credentials =
                    new NetworkCredential(WsdlProperties.ProxyUserName, WsdlProperties.ProxyPassword,
                                          WsdlProperties.ProxyDomain);
                protocol1.Proxy = proxy1;
            }
            return protocol1;
        }

        private static XmlSchema CreateFakeSoapEncodingSchema(string ns, string name)
        {
            XmlSchema schema1 = new XmlSchema();
            schema1.TargetNamespace = ns;
            XmlSchemaGroup group1 = new XmlSchemaGroup();
            group1.Name = "Array";
            XmlSchemaSequence sequence1 = new XmlSchemaSequence();
            XmlSchemaAny any1 = new XmlSchemaAny();
            any1.MinOccurs = new decimal(0);
            any1.MaxOccurs = new decimal(-1, -1, -1, false, 0);
            sequence1.Items.Add(any1);
            any1.Namespace = "##any";
            any1.ProcessContents = XmlSchemaContentProcessing.Lax;
            group1.Particle = sequence1;
            schema1.Items.Add(group1);
            XmlSchemaComplexType type1 = new XmlSchemaComplexType();
            type1.Name = name;
            XmlSchemaGroupRef ref1 = new XmlSchemaGroupRef();
            ref1.RefName = new XmlQualifiedName("Array", ns);
            type1.Particle = ref1;
            XmlSchemaAttribute attribute1 = new XmlSchemaAttribute();
            attribute1.RefName = new XmlQualifiedName("arrayType", ns);
            type1.Attributes.Add(attribute1);
            schema1.Items.Add(type1);
            attribute1 = new XmlSchemaAttribute();
            attribute1.Use = XmlSchemaUse.None;
            attribute1.Name = "arrayType";
            schema1.Items.Add(attribute1);
            AddSimpleType(schema1, "base64", "base64Binary");
            AddElementAndType(schema1, "anyURI", ns);
            AddElementAndType(schema1, "base64Binary", ns);
            AddElementAndType(schema1, "boolean", ns);
            AddElementAndType(schema1, "byte", ns);
            AddElementAndType(schema1, "date", ns);
            AddElementAndType(schema1, "dateTime", ns);
            AddElementAndType(schema1, "decimal", ns);
            AddElementAndType(schema1, "double", ns);
            AddElementAndType(schema1, "duration", ns);
            AddElementAndType(schema1, "ENTITIES", ns);
            AddElementAndType(schema1, "ENTITY", ns);
            AddElementAndType(schema1, "float", ns);
            AddElementAndType(schema1, "gDay", ns);
            AddElementAndType(schema1, "gMonth", ns);
            AddElementAndType(schema1, "gMonthDay", ns);
            AddElementAndType(schema1, "gYear", ns);
            AddElementAndType(schema1, "gYearMonth", ns);
            AddElementAndType(schema1, "hexBinary", ns);
            AddElementAndType(schema1, "ID", ns);
            AddElementAndType(schema1, "IDREF", ns);
            AddElementAndType(schema1, "IDREFS", ns);
            AddElementAndType(schema1, "int", ns);
            AddElementAndType(schema1, "integer", ns);
            AddElementAndType(schema1, "language", ns);
            AddElementAndType(schema1, "long", ns);
            AddElementAndType(schema1, "Name", ns);
            AddElementAndType(schema1, "NCName", ns);
            AddElementAndType(schema1, "negativeInteger", ns);
            AddElementAndType(schema1, "NMTOKEN", ns);
            AddElementAndType(schema1, "NMTOKENS", ns);
            AddElementAndType(schema1, "nonNegativeInteger", ns);
            AddElementAndType(schema1, "nonPositiveInteger", ns);
            AddElementAndType(schema1, "normalizedString", ns);
            AddElementAndType(schema1, "positiveInteger", ns);
            AddElementAndType(schema1, "QName", ns);
            AddElementAndType(schema1, "short", ns);
            AddElementAndType(schema1, "string", ns);
            AddElementAndType(schema1, "time", ns);
            AddElementAndType(schema1, "token", ns);
            AddElementAndType(schema1, "unsignedByte", ns);
            AddElementAndType(schema1, "unsignedInt", ns);
            AddElementAndType(schema1, "unsignedLong", ns);
            AddElementAndType(schema1, "unsignedShort", ns);
            return schema1;
        }

        private static XmlSchema CreateFakeWsdlSchema(string ns)
        {
            XmlSchema schema1 = new XmlSchema();
            schema1.TargetNamespace = ns;
            XmlSchemaAttribute attribute1 = new XmlSchemaAttribute();
            attribute1.Use = XmlSchemaUse.None;
            attribute1.Name = "arrayType";
            attribute1.SchemaTypeName = new XmlQualifiedName("QName", "http://www.w3.org/2001/XMLSchema");
            schema1.Items.Add(attribute1);
            return schema1;
        }

        private static XmlSchema CreateFakeXsdSchema(string ns, string name)
        {
            XmlSchema schema1 = new XmlSchema();
            schema1.TargetNamespace = ns;
            XmlSchemaElement element1 = new XmlSchemaElement();
            element1.Name = name;
            XmlSchemaComplexType type1 = new XmlSchemaComplexType();
            element1.SchemaType = type1;
            schema1.Items.Add(element1);
            return schema1;
        }

        private static bool FileExists(string path)
        {
            if ((path != null) && (path.Length != 0))
            {
                try
                {
                    return ((path.LastIndexOf('?') == -1) && File.Exists(path));
                }
                catch
                {
                }
            }
            return false;
        }

        public void Generate()
        {
            Generate(false);
        }

        public void Generate(bool updateAssembly)
        {
            if (useLocalAssembly&&!updateAssembly)
            {
                if (File.Exists(GetDllOutPutPath()))
                {
                    proxyAssembly = Assembly.LoadFrom(GetDllOutPutPath().ToString());
                    CheckPoint(MessageType.Success, "Local Assembly Path:" + GetDllOutPutPath().ToString());
                    return;
                }
            }

            CheckPoint(MessageType.Begin, "Initializing");
            ServiceDescriptionCollection collection1 = new ServiceDescriptionCollection();
            XmlSchemas schemas1 = new XmlSchemas();
            StringCollection collection2 = new StringCollection();
            StringCollection collection3 = new StringCollection();
            GetPaths(collection3, collection2);
            collection1.Clear();
            schemas1.Clear();
            if ((collection3 != null) && (collection3.Count > 0))
            {
                string text1 = collection3[0];
                string text2 = Path.GetExtension(text1);
                if ((string.Compare(text2, ".exe", true) == 0) || (string.Compare(text2, ".dll", true) == 0))
                {
                    CheckPoint(MessageType.Begin, "Loading Assembly");
                    proxyAssembly = Assembly.LoadFrom(text1);

                    if (proxyAssembly != null)
                    {
                        CheckPoint(MessageType.Success, "Loaded Assembly");
                    }
                    else
                    {
                        CheckPoint(MessageType.Failure, "Failed to load Assembly");
                    }
                    return;
                }
            }
            CheckPoint(MessageType.Begin, "Generating WSDL");
            try
            {
                DiscoveryClientProtocol protocol1 = CreateDiscoveryClient();
                ProcessLocalPaths(protocol1, collection3, schemas1, collection1);
                ProcessRemoteUrls(protocol1, collection2, schemas1, collection1);
            }
            catch (Exception exception1)
            {
                CheckPoint(MessageType.Failure, exception1.ToString());
                return;
            }
            try
            {
                ICodeGenerator generator1;
                string text3;
                CheckPoint(MessageType.Begin, "Generating Proxy");
                CreateCodeGenerator(out generator1, out text3);
                XmlSchemas schemas2 = new XmlSchemas();
                schemas2.Add(schemas1);
                foreach (ServiceDescription description1 in collection1)
                {
                    schemas2.Add(description1.Types.Schemas);
                }
                Hashtable hashtable1 = new Hashtable();
                foreach (XmlSchema schema1 in schemas2)
                {
                    CollectIncludes(schema1, hashtable1);
                }
                Compile(schemas2);
                GenerateCode(collection1, schemas1, "http://tempuri.org", generator1, text3);
                CheckPoint(MessageType.Begin, "Compiling Proxy");
                //if (proxyCode.Length < 1000)
                //{
                //    CheckPoint(MessageType.Failure, "Empty Assembly");
                //    return;
                //}

                if (updateAssembly)
                {
                    GenerateAssembly(true);
                }
                else
                {
                    GenerateAssembly();
                }
                CheckPoint(MessageType.Success, "Generated Assembly");
            }
            catch (Exception exception2)
            {
                CheckPoint(MessageType.Failure, exception2.ToString());
                return;
            }
        }

        public void GenerateAndUpdateAssembly()
        {
            Generate(true);
        }

        private string GetDllOutPutPath()
        {
            StringBuilder retult = new StringBuilder();
            string[] strings = paths[0].Split('/');

            for (int i = 2; i < strings.Length - 1; i++)
            {
                retult.Append(strings[i] + "_");
            }

            retult.Append(strings[strings.Length - 1].Split('.')[0]);

            retult.Remove(retult.Length - 1, 1);

            retult.Append(".dll");

            return retult.ToString();
        }


        private void GenerateAssembly()
        {
            GenerateAssembly(false);
        }

        private void GenerateAssembly(bool overrideFile)
        {
            ICodeCompiler compiler1 = codeProvider.CreateCompiler();
            string text1 = "";
            if ((WsdlProperties.ProxyBaseType != null) && (WsdlProperties.ProxyBaseType.Length > 0))
            {
                Type type1 = Type.GetType(WsdlProperties.ProxyBaseType, true);
                text1 = type1.Assembly.Location;
            }
            string[] textArray1 =
                new string[]
                    {
                        "System.Xml.dll", "System.dll", "System.Web.Services.dll", "System.Data.dll",
                        Assembly.GetExecutingAssembly().Location, text1
                    };

            CompilerParameters parameters1 = null;
            if ((useLocalAssembly && !File.Exists(GetDllOutPutPath())) || overrideFile)
            {
                parameters1 = new CompilerParameters(textArray1, GetDllOutPutPath());
                CheckPoint(MessageType.Success, "Saved Local Assembly:" + GetDllOutPutPath().ToString());
            }
            else
            {
                parameters1 = new CompilerParameters(textArray1);
            }
            parameters1.WarningLevel = 0;
            parameters1.GenerateInMemory = false;
            CompilerResults results1 = compiler1.CompileAssemblyFromSource(parameters1, proxyCode);
            if (results1.Errors.HasErrors)
            {
                foreach (CompilerError error1 in results1.Errors)
                {
                    CheckPoint(MessageType.Error, error1.ToString());
                }
                throw new Exception("CompilationErrors");
            }
            proxyAssembly = results1.CompiledAssembly;
        }

        private void GenerateCode(ServiceDescriptionCollection sources, XmlSchemas schemas, string uriToWSDL,
                                  ICodeGenerator codeGen, string fileExtension)
        {
            proxyCode = " <ERROR> ";
            StringWriter writer1 = null;
            compileUnit = new CodeCompileUnit();
            ServiceDescriptionImporter importer1 = new ServiceDescriptionImporter();
            importer1.Schemas.Add(schemas);
            foreach (ServiceDescription description1 in sources)
            {
                importer1.AddServiceDescription(description1, "", "");
            }
            importer1.Style = ServiceDescriptionImportStyle.Client;
            Protocol protocol1 = WsdlProperties.Protocol;
            importer1.ProtocolName = WsdlProperties.Protocol.ToString();
            CodeNamespace namespace1 = new CodeNamespace(proxyNamespace);
            compileUnit.Namespaces.Add(namespace1);
            ServiceDescriptionImportWarnings warnings1 = importer1.Import(namespace1, compileUnit);
            try
            {
                try
                {
                    writer1 = new StringWriter();
                }
                catch
                {
                    throw;
                }
                MemoryStream stream1 = null;
                if (schemas.Count > 0)
                {
                    compileUnit.ReferencedAssemblies.Add("System.Data.dll");
                    foreach (XmlSchema schema1 in schemas)
                    {
                        string text1 = null;
                        try
                        {
                            text1 = schema1.TargetNamespace;
                            if (XmlSchemas.IsDataSet(schema1))
                            {
                                if (stream1 == null)
                                {
                                    stream1 = new MemoryStream();
                                }
                                stream1.Position = 0;
                                stream1.SetLength((long) 0);
                                schema1.Write(stream1);
                                stream1.Position = 0;
                                DataSet set1 = new DataSet();
                                set1.ReadXmlSchema(stream1);
                                TypedDataSetGenerator.Generate(set1, namespace1, codeGen);
                            }
                            continue;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                try
                {
                    GenerateVersionComment(compileUnit.Namespaces[0]);
                    ChangeBaseType(compileUnit);
                    codeGen.GenerateCodeFromCompileUnit(compileUnit, writer1, null);
                }
                catch (Exception exception1)
                {
                    if (writer1 != null)
                    {
                        writer1.Write("Exception in generating code");
                        writer1.Write(exception1.Message);
                    }
                    throw new InvalidOperationException("Error generating ", exception1);
                }
            }
            finally
            {
                proxyCode = writer1.ToString();
                if (writer1 != null)
                {
                    writer1.Close();
                }
            }
        }

        private static void GenerateVersionComment(CodeNamespace codeNamespace)
        {
            codeNamespace.Comments.Add(new CodeCommentStatement(""));
            AssemblyName name1 = Assembly.GetExecutingAssembly().GetName();
            Version version1 = Environment.Version;
            codeNamespace.Comments.Add(
                new CodeCommentStatement("Assembly " + name1.Name + " Version = " + version1.ToString()));
            codeNamespace.Comments.Add(new CodeCommentStatement(""));
        }

        public ICodeGenerator GetCodeGenerator()
        {
            ICodeGenerator generator1;
            string text1;
            CreateCodeGenerator(out generator1, out text1);
            return generator1;
        }

        private void GetPaths(StringCollection localPaths, StringCollection urls)
        {
            foreach (string text1 in paths)
            {
                if (FileExists(text1))
                {
                    if (!Path.HasExtension(text1))
                    {
                        throw new InvalidOperationException(text1 + " has no extensions");
                    }
                    localPaths.Add(text1);
                }
                else
                {
                    Uri uri1 = null;
                    try
                    {
                        uri1 = new Uri(text1, true);
                    }
                    catch (Exception)
                    {
                        uri1 = null;
                    }
                    if (uri1 == null)
                    {
                        throw new InvalidOperationException(text1 + " is invalid URI");
                    }
                    urls.Add(uri1.AbsoluteUri);
                }
            }
        }

        private void ProcessLocalPaths(DiscoveryClientProtocol client, StringCollection localPaths, XmlSchemas schemas,
                                       ServiceDescriptionCollection descriptions)
        {
            foreach (string text1 in localPaths)
            {
                string text2 = Path.GetExtension(text1);
                if (string.Compare(text2, ".discomap", true) == 0)
                {
                    client.ReadAll(text1);
                }
                else
                {
                    object obj1 = null;
                    if (string.Compare(text2, ".wsdl", true) == 0)
                    {
                        obj1 = ReadLocalDocument(false, text1);
                    }
                    else
                    {
                        if (string.Compare(text2, ".xsd", true) != 0)
                        {
                            throw new InvalidOperationException("Unknown file type " + text1);
                        }
                        obj1 = ReadLocalDocument(true, text1);
                    }
                    if (obj1 != null)
                    {
                        AddDocument(text1, obj1, schemas, descriptions);
                    }
                }
            }
        }

        private void ProcessRemoteUrls(DiscoveryClientProtocol client, StringCollection urls, XmlSchemas schemas,
                                       ServiceDescriptionCollection descriptions)
        {
            foreach (string text1 in urls)
            {
                try
                {
                    DiscoveryDocument document1 = client.DiscoverAny(text1);
                    client.ResolveAll();
                    continue;
                }
                catch (Exception exception1)
                {
                    throw new InvalidOperationException("General Error " + text1, exception1);
                }
            }
            foreach (DictionaryEntry entry1 in client.Documents)
            {
                AddDocument((string) entry1.Key, entry1.Value, schemas, descriptions);
            }
        }

        private object ReadLocalDocument(bool isSchema, string path)
        {
            object obj1 = null;
            StreamReader reader1 = null;
            try
            {
                reader1 = new StreamReader(path);
                if (isSchema)
                {
                    return XmlSchema.Read(new XmlTextReader(reader1), null);
                }
                obj1 = ServiceDescription.Read(reader1.BaseStream);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader1 != null)
                {
                    reader1.Close();
                }
            }
            return obj1;
        }

        public static XmlSchema ReadSchema(string filename)
        {
            XmlTextReader reader1 = new XmlTextReader(filename);
            if (reader1.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
            {
                return XmlSchema.Read(reader1, null);
            }
            return null;
        }

        public void Reset()
        {
            paths.Clear();
            codeProvider = null;
            wsdls.Clear();
            xsds.Clear();
            compileUnit = null;
            proxyAssembly = null;
            proxyCode = null;
            cancelled = false;
        }

        private void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
        {
            CheckPoint(MessageType.Warning, "Schema parsing error " + args.Message);
        }


        public StringCollection Paths
        {
            get { return paths; }
        }

        public Assembly ProxyAssembly
        {
            get { return proxyAssembly; }
        }

        public string ProxyCode
        {
            get { return proxyCode; }
        }

        public CodeCompileUnit ProxyCodeDom
        {
            get { return compileUnit; }
        }

        public string ProxyFileExtension
        {
            get { return WsdlProperties.Language.ToString(); }
        }

        public string ProxyNamespace
        {
            get { return proxyNamespace; }
            set { proxyNamespace = value; }
        }

        public WsdlProperties WsdlProperties
        {
            get { return Configuration.MasterConfig.WsdlSettings; }
        }

        public StringCollection Wsdls
        {
            get { return wsdls; }
        }

        public StringCollection Xsds
        {
            get { return xsds; }
        }


        private bool cancelled;
        private CodeDomProvider codeProvider;
        private CodeCompileUnit compileUnit;
        private static string duplicateSchema;
        private static string duplicateService;
        private StringCollection paths;
        private Assembly proxyAssembly;
        private string proxyCode;
        private string proxyNamespace;
        private static string schemaValidationFailure;
        private StringCollection wsdls;
        private StringCollection xsds;

        public bool UseLocalAssembly
        {
            get { return useLocalAssembly; }
            set { useLocalAssembly = value; }
        }

        private bool useLocalAssembly = false;


        internal class Namespace
        {
            public const string SoapEncoding = "http://schemas.xmlsoap.org/soap/encoding/";
            public const string Wsdl = "http://schemas.xmlsoap.org/wsdl/";
        }
    }
}
