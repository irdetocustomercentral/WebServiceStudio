	//For licensing details please see the following link
	//http://webservicestudio.codeplex.com/license

	//8-20-2012 Web service Studio has been modified to understand more complex input types
	//such as Irdeto's Customer Care input type of Base Query Request. 
	
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    internal class Script
    {
        static Script()
        {
            usingStatementsCS =
                "\r\nusing System.CodeDom.Compiler;\r\nusing System.CodeDom;\r\nusing System.Collections;\r\nusing System.ComponentModel;\r\nusing System.Data;\r\nusing System.Globalization;\r\nusing System.IO;\r\nusing System.Reflection;\r\nusing System.Web.Services.Protocols;\r\nusing System.Xml.Serialization;\r\nusing System.Xml;\r\nusing System;\r\n";
            dumpClassCS =
                "\r\npublic class Dumper {\r\n    Hashtable objects = new Hashtable();\r\n    TextWriter writer;\r\n    int indent = 0;\r\n\r\n    public Dumper():this(Console.Out) {\r\n    }\r\n    public Dumper(TextWriter writer) {\r\n        this.writer = writer;\r\n    }\r\n    public static void Dump(string name, object o){\r\n        Dumper d = new Dumper();\r\n        d.DumpInternal(name, o);\r\n    }\r\n\r\n    private void DumpInternal(string name, object o) {\r\n        for (int i1 = 0; i1 < indent; i1++)\r\n            writer.Write(\"--- \");\r\n\r\n        if (name == null) name = string.Empty;\r\n\r\n        if (o == null) {\r\n            writer.WriteLine(name + \" = null\");\r\n            return;\r\n        }\r\n\r\n        Type type = o.GetType();\r\n\r\n        writer.Write(type.Name + \" \" + name);\r\n\r\n        if (objects[o] != null) {\r\n            writer.WriteLine(\" = ...\");\r\n            return;\r\n        }\r\n\r\n        if (!type.IsValueType && !type.Equals(typeof(string)))\r\n            objects.Add(o, o);\r\n\r\n        if (type.IsArray) {\r\n            Array a = (Array)o;\r\n            writer.WriteLine();\r\n            indent++;\r\n            for (int j = 0; j < a.Length; j++)\r\n                DumpInternal(\"[\" + j + \"]\", a.GetValue(j));\r\n            indent--;\r\n            return;\r\n        }\r\n        if (o is XmlQualifiedName) {\r\n            DumpInternal(\"Name\", ((XmlQualifiedName) o).Name);\r\n            DumpInternal(\"Namespace\", ((XmlQualifiedName) o).Namespace);\r\n            return;\r\n        }\r\n        if (o is XmlNode) {\r\n            string xml = ((XmlNode)o).OuterXml;\r\n            writer.WriteLine(\" = \" + xml);\r\n            return;\r\n        }\r\n        if (type.IsEnum) {\r\n            writer.WriteLine(\" = \" + ((Enum)o).ToString());\r\n            return;\r\n        }\r\n        if (type.IsPrimitive) {\r\n            writer.WriteLine(\" = \" + o.ToString());\r\n            return;\r\n        }\r\n        if (typeof(Exception).IsAssignableFrom(type)) {                \r\n            writer.WriteLine(\" = \" + ((Exception)o).Message);\r\n            return;\r\n        }\r\n        if (o is DataSet) {\r\n            writer.WriteLine();\r\n            indent++;\r\n            DumpInternal(\"Tables\", ((DataSet)o).Tables);\r\n            indent--;\r\n            return;\r\n        }\r\n        if (o is DateTime) {\r\n            writer.WriteLine(\" = \" + o.ToString());\r\n            return;\r\n        }\r\n        if (o is DataTable) {\r\n            writer.WriteLine();\r\n            indent++;\r\n            DataTable table = (DataTable)o;\r\n            DumpInternal(\"TableName\", table.TableName);\r\n            DumpInternal(\"Rows\", table.Rows);\r\n            indent--;\r\n            return;\r\n        }\r\n        if (o is DataRow) {\r\n            writer.WriteLine();\r\n            indent++;\r\n            DataRow row = (DataRow)o;\r\n            DumpInternal(\"Values\", row.ItemArray);\r\n            indent--;\r\n            return;\r\n        }\r\n        if (o is string) {\r\n            string s = (string)o;\r\n            if (s.Length > 40) {\r\n                writer.WriteLine(\" = \");\r\n                writer.WriteLine(\"\\\"\" + s + \"\\\"\");\r\n            }\r\n            else {\r\n                writer.WriteLine(\" = \\\"\" + s + \"\\\"\");\r\n            }\r\n            return;\r\n        }\r\n        if (o is IEnumerable) {\r\n            IEnumerator e = ((IEnumerable)o).GetEnumerator();\r\n            if (e == null) {\r\n                writer.WriteLine(\" GetEnumerator() == null\");\r\n                return;\r\n            }\r\n            writer.WriteLine();\r\n            int c = 0;\r\n            indent++;\r\n            while (e.MoveNext()) {\r\n                DumpInternal(\"[\" + c + \"]\", e.Current);\r\n                c++;\r\n            }\r\n            indent--;\r\n            return;\r\n        }\r\n        writer.WriteLine();\r\n        indent++;\r\n        FieldInfo[] fields = type.GetFields();\r\n        for (int i2 = 0; i2 < fields.Length; i2++) {\r\n            FieldInfo f = fields[i2];\r\n            if (!f.IsStatic)\r\n                DumpInternal(f.Name, f.GetValue(o));\r\n        }\r\n        PropertyInfo[] props = type.GetProperties();\r\n        for (int i3 = 0; i3 < props.Length; i3++) {\r\n            PropertyInfo p = props[i3];\r\n            if (p.CanRead &&\r\n                  (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) || p.CanWrite) &&\r\n                  !p.PropertyType.Equals(type)){\r\n                object v;\r\n                try {\r\n                    v = p.GetValue(o, null);\r\n                }\r\n                catch (Exception e) {\r\n                    v = e;\r\n                }\r\n                DumpInternal(p.Name, v);\r\n            }\r\n        }\r\n        indent--;\r\n    }\r\n}\r\n";
            usingStatementsVB =
                "\r\nImports System.CodeDom.Compiler\r\nImports System.CodeDom\r\nImports System.Collections\r\nImports System.ComponentModel\r\nImports System.Data\r\nImports System.Globalization\r\nImports System.IO\r\nImports System.Reflection\r\nImports System.Web.Services.Protocols\r\nImports System.Xml.Serialization\r\nImports System.Xml\r\nImports System\r\n";
            dumpClassVB =
                "Public Class Dumper\r\n   Private objects As New Hashtable()\r\n   Private writer As TextWriter\r\n   Private indent As Integer = 0\r\n   \r\n   \r\n   Public Sub New()\r\n      MyClass.New(Console.Out)\r\n   End Sub 'New\r\n   \r\n   Public Sub New(writer As TextWriter)\r\n      Me.writer = writer\r\n   End Sub 'New\r\n   \r\n   Public Shared Sub Dump(name As String, o As Object)\r\n      Dim d As New Dumper()\r\n      d.DumpInternal(name, o)\r\n   End Sub 'Dump\r\n   \r\n   \r\n   Private Sub DumpInternal(name As String, o As Object)\r\n      Dim i1 As Integer\r\n      For i1 = 0 To indent - 1\r\n         writer.Write(\"--- \")\r\n      Next i1 \r\n      If name Is Nothing Then\r\n         name = String.Empty\r\n      End If \r\n      If o Is Nothing Then\r\n         writer.WriteLine((name + \" = null\"))\r\n         Return\r\n      End If\r\n      \r\n      Dim type As Type = o.GetType()\r\n      \r\n      writer.Write((type.Name + \" \" + name))\r\n      \r\n      If Not (objects(o) Is Nothing) Then\r\n         writer.WriteLine(\" = ...\")\r\n         Return\r\n      End If\r\n      \r\n      If Not type.IsValueType And Not type.Equals(GetType(String)) Then\r\n         objects.Add(o, o)\r\n      End If \r\n      If type.IsArray Then\r\n         Dim a As Array = CType(o, Array)\r\n         writer.WriteLine()\r\n         indent += 1\r\n         Dim j As Integer\r\n         For j = 0 To a.Length - 1\r\n            DumpInternal(\"[\" + j + \"]\", a.GetValue(j))\r\n         Next j\r\n         indent -= 1\r\n         Return\r\n      End If\r\n      If TypeOf o Is XmlQualifiedName Then\r\n         DumpInternal(\"Name\", CType(o, XmlQualifiedName).Name)\r\n         DumpInternal(\"Namespace\", CType(o, XmlQualifiedName).Namespace)\r\n         Return\r\n      End If\r\n      If TypeOf o Is XmlNode Then\r\n         Dim xml As String = CType(o, XmlNode).OuterXml\r\n         writer.WriteLine((\" = \" + xml))\r\n         Return\r\n      End If\r\n      If type.IsEnum Then\r\n         writer.WriteLine((\" = \" + CType(o, [Enum]).ToString()))\r\n         Return\r\n      End If\r\n      If type.IsPrimitive Then\r\n         writer.WriteLine((\" = \" + o.ToString()))\r\n         Return\r\n      End If\r\n      If GetType(Exception).IsAssignableFrom(type) Then\r\n         writer.WriteLine((\" = \" + CType(o, Exception).Message))\r\n         Return\r\n      End If\r\n      If TypeOf o Is DataSet Then\r\n         writer.WriteLine()\r\n         indent += 1\r\n         DumpInternal(\"Tables\", CType(o, DataSet).Tables)\r\n         indent -= 1\r\n         Return\r\n      End If\r\n      If TypeOf o Is DateTime Then\r\n         writer.WriteLine((\" = \" + o.ToString()))\r\n         Return\r\n      End If\r\n      If TypeOf o Is DataTable Then\r\n         writer.WriteLine()\r\n         indent += 1\r\n         Dim table As DataTable = CType(o, DataTable)\r\n         DumpInternal(\"TableName\", table.TableName)\r\n         DumpInternal(\"Rows\", table.Rows)\r\n         indent -= 1\r\n         Return\r\n      End If\r\n      If TypeOf o Is DataRow Then\r\n         writer.WriteLine()\r\n         indent += 1\r\n         Dim row As DataRow = CType(o, DataRow)\r\n         DumpInternal(\"Values\", row.ItemArray)\r\n         indent -= 1\r\n         Return\r\n      End If\r\n      If TypeOf o Is String Then\r\n         Dim s As String = CStr(o)\r\n         If s.Length > 40 Then\r\n            writer.WriteLine(\" = \")\r\n            writer.WriteLine((\"\"\"\" + s + \"\"\"\"))\r\n         Else\r\n            writer.WriteLine((\" = \"\"\" + s + \"\"\"\"))\r\n         End If\r\n         Return\r\n      End If\r\n      If TypeOf o Is IEnumerable Then\r\n         Dim e As IEnumerator = CType(o, IEnumerable).GetEnumerator()\r\n         If e Is Nothing Then\r\n            writer.WriteLine(\" GetEnumerator() == null\")\r\n            Return\r\n         End If\r\n         writer.WriteLine()\r\n         Dim c As Integer = 0\r\n         indent += 1\r\n         While e.MoveNext()\r\n            DumpInternal(\"[\" + c + \"]\", e.Current)\r\n            c += 1\r\n         End While\r\n         indent -= 1\r\n         Return\r\n      End If\r\n      writer.WriteLine()\r\n      indent += 1\r\n      Dim fields As FieldInfo() = type.GetFields()\r\n      Dim i2 As Integer\r\n      For i2 = 0 To fields.Length - 1\r\n         Dim f As FieldInfo = fields(i2)\r\n         If Not f.IsStatic Then\r\n            DumpInternal(f.Name, f.GetValue(o))\r\n         End If\r\n      Next i2\r\n      Dim props As PropertyInfo() = type.GetProperties()\r\n      Dim i3 As Integer\r\n      For i3 = 0 To props.Length - 1\r\n         Dim p As PropertyInfo = props(i3)\r\n         If p.CanRead And(GetType(IEnumerable).IsAssignableFrom(p.PropertyType) Or p.CanWrite) And Not p.PropertyType.Equals(type) Then\r\n            Dim v As Object\r\n            Try\r\n               v = p.GetValue(o, Nothing)\r\n            Catch e As Exception\r\n               v = e\r\n            End Try\r\n            DumpInternal(p.Name, v)\r\n         End If\r\n      Next i3\r\n      indent -= 1\r\n   End Sub 'DumpInternal\r\nEnd Class 'Dumper\r\n";
        }

        public Script() : this("IBS.Utilities.ASMWSTester", "MainClass")
        {
        }

        public Script(string namespaceToGen, string className)
        {
            compileUnit = new CodeCompileUnit();
            codeNamespace = new CodeNamespace(namespaceToGen);
            compileUnit.Namespaces.Add(codeNamespace);
            codeClass = new CodeTypeDeclaration(className);
            codeNamespace.Types.Add(codeClass);
            proxySetting = ProxySettings.RequiredHeaders;
            mainMethod = new CodeEntryPointMethod();
            mainMethod.Name = "Main";
            mainMethod.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            codeClass.Members.Add(mainMethod);
            dumpMethodRef = BuildDumper();
        }

        public void AddMethod(MethodInfo method, object[] parameters)
        {
            BuildMethod(codeClass.Members, method, parameters);
        }

        private CodeExpression BuildArray(CodeStatementCollection statements, string name, object value)
        {
            Array array1 = (Array) value;
            Type type1 = value.GetType();
            string text1 = GetUniqueVariableName(name, statements);
            CodeVariableDeclarationStatement statement1 = new CodeVariableDeclarationStatement(type1.FullName, text1);
            statement1.InitExpression = new CodeArrayCreateExpression(type1.GetElementType(), array1.Length);
            statements.Add(statement1);
            CodeVariableReferenceExpression expression1 = new CodeVariableReferenceExpression(text1);
            string text2 = name + "_";
            for (int num1 = 0; num1 < array1.Length; num1++)
            {
                CodeArrayIndexerExpression expression2 =
                    new CodeArrayIndexerExpression(expression1, new CodeExpression[0]);
                expression2.Indices.Add(new CodePrimitiveExpression(num1));
                CodeExpression expression3 = BuildObject(statements, text2 + num1.ToString(), array1.GetValue(num1));
                statements.Add(new CodeAssignStatement(expression2, expression3));
            }
            return expression1;
        }

        private CodeExpression BuildClass(CodeStatementCollection statements, string name, object value)
        {
            Type type1 = value.GetType();
            string text1 = GetUniqueVariableName(name, statements);
            CodeVariableDeclarationStatement statement1 = new CodeVariableDeclarationStatement(type1.FullName, text1);
            statement1.InitExpression = new CodeObjectCreateExpression(type1.FullName, new CodeExpression[0]);
            statements.Add(statement1);
            CodeVariableReferenceExpression expression1 = new CodeVariableReferenceExpression(text1);
            foreach (
                MemberInfo info1 in type1.GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
            {
                object obj1 = null;
                Type type2 = typeof (object);
                CodeExpression expression2 = null;
                if (info1 is FieldInfo)
                {
                    FieldInfo info2 = (FieldInfo) info1;
                    if (info2.IsStatic || info2.IsInitOnly)
                    {
                        goto Label_014B;
                    }
                    type2 = info2.FieldType;
                    obj1 = info2.GetValue(value);
                    expression2 = new CodeFieldReferenceExpression(expression1, info2.Name);
                }
                else if (info1 is PropertyInfo)
                {
                    PropertyInfo info3 = (PropertyInfo) info1;
                    if (!info3.CanWrite)
                    {
                        goto Label_014B;
                    }
                    MethodInfo info4 = info3.GetGetMethod();
                    ParameterInfo[] infoArray2 = info4.GetParameters();
                    if ((infoArray2.Length > 0) || info4.IsStatic)
                    {
                        goto Label_014B;
                    }
                    type2 = info3.PropertyType;
                    obj1 = info3.GetValue(value, null);
                    expression2 = new CodePropertyReferenceExpression(expression1, info3.Name);
                }
                if (expression2 != null)
                {
                    CodeExpression expression3 = BuildObject(statements, info1.Name, obj1);
                    statements.Add(new CodeAssignStatement(expression2, expression3));
                }
                Label_014B:
                ;
            }
            return expression1;
        }

        public CodeMethodReferenceExpression BuildDumper()
        {
            return new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("Dumper"), "Dump");
        }

        private void BuildDumpInvoke(CodeStatementCollection statements, string name, CodeExpression obj)
        {
            CodeMethodInvokeExpression expression1 =
                new CodeMethodInvokeExpression(dumpMethodRef, new CodeExpression[0]);
            expression1.Parameters.Add(new CodePrimitiveExpression(name));
            expression1.Parameters.Add(obj);
            statements.Add(expression1);
        }

        private void BuildMethod(CodeTypeMemberCollection members, MethodInfo method, object[] parameters)
        {
            CodeMemberMethod method1 = new CodeMemberMethod();
            method1.Name = "Invoke" + method.Name;
            mainMethod.Statements.Add(new CodeMethodInvokeExpression(null, method1.Name, new CodeExpression[0]));
            method1.Attributes = MemberAttributes.Public | MemberAttributes.Static;
            members.Add(method1);
            CodeExpression expression1 = BuildProxy(method1.Statements, method);
            CodeMethodInvokeExpression expression2 =
                new CodeMethodInvokeExpression(expression1, method.Name, new CodeExpression[0]);
            BuildParameters(method1.Statements, method, parameters, expression2.Parameters);
            if (method.ReturnType == typeof (void))
            {
                method1.Statements.Add(new CodeExpressionStatement(expression2));
            }
            else
            {
                string text1 = GetUniqueVariableName(method.Name + "Result", method1.Statements);
                method1.Statements.Add(
                    new CodeVariableDeclarationStatement(method.ReturnType.FullName, text1, expression2));
                BuildDumpInvoke(method1.Statements, "result", new CodeVariableReferenceExpression(text1));
            }
            ParameterInfo[] infoArray1 = method.GetParameters();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                ParameterInfo info1 = infoArray1[num1];
                if (info1.IsOut || info1.ParameterType.IsByRef)
                {
                    BuildDumpInvoke(method1.Statements, info1.Name,
                                    ((CodeDirectionExpression) expression2.Parameters[num1]).Expression);
                }
            }
        }

        private CodeExpression BuildObject(CodeStatementCollection statements, string name, object value)
        {
            if (value == null)
            {
                return new CodePrimitiveExpression(null);
            }
            Type type1 = value.GetType();
            if (type1.IsPrimitive || (type1 == typeof(string)) || type1==typeof(decimal))
            {
                return new CodePrimitiveExpression(value);
            }
            if (type1.IsEnum)
            {
                string[] textArray1 = value.ToString().Split(new char[] {','});
                if (textArray1.Length > 1)
                {
                    CodeExpression expression1 =
                        new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(type1.FullName), textArray1[0]);
                    for (int num1 = 1; num1 < textArray1.Length; num1++)
                    {
                        expression1 =
                            new CodeBinaryOperatorExpression(expression1, CodeBinaryOperatorType.BitwiseOr,
                                                             new CodeFieldReferenceExpression(
                                                                 new CodeTypeReferenceExpression(type1.FullName),
                                                                 textArray1[num1]));
                    }
                    return expression1;
                }
                return
                    new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(type1.FullName), value.ToString());
            }
            if (type1 == typeof (DateTime))
            {
                DateTime time1 = (DateTime) value;
                string text1 =
                    ((DateTime) value).ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz", DateTimeFormatInfo.InvariantInfo);
                long num2 = time1.Ticks;
                statements.Add(new CodeCommentStatement("Init DateTime object value = " + text1));
                statements.Add(new CodeCommentStatement("We going to use DateTime ctor that takes Ticks"));
                return
                    new CodeObjectCreateExpression(new CodeTypeReference(typeof (DateTime)),
                                                   new CodeExpression[] {new CodePrimitiveExpression(num2)});
            }
            if (typeof (XmlNode).IsAssignableFrom(type1))
            {
                return BuildXmlNode(statements, name, value);
            }
            if (type1.IsArray)
            {
                return BuildArray(statements, name, value);
            }
            if (type1.IsAbstract || (type1.GetConstructor(new Type[0]) == null))
            {
                statements.Add(
                    new CodeCommentStatement("Can not create object of type " + type1.FullName +
                                             " because it does not have a default ctor"));
                return new CodePrimitiveExpression(null);
            }
            return BuildClass(statements, name, value);
        }

        private void BuildParameters(CodeStatementCollection statements, MethodInfo method, object[] paramValues,
                                     CodeExpressionCollection parameters)
        {
            ParameterInfo[] infoArray1 = method.GetParameters();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                ParameterInfo info1 = infoArray1[num1];
                Type type1 = infoArray1[num1].ParameterType;
                FieldDirection direction1 = FieldDirection.In;
                if (type1.IsByRef)
                {
                    direction1 = FieldDirection.Ref;
                    type1 = type1.GetElementType();
                }
                CodeExpression expression1 = null;
                if (!info1.IsOut)
                {
                    expression1 = BuildObject(statements, info1.Name, paramValues[num1]);
                }
                else
                {
                    direction1 = FieldDirection.Out;
                }
                if (direction1 != FieldDirection.In)
                {
                    if ((expression1 == null) || !(expression1 is CodeVariableReferenceExpression))
                    {
                        CodeVariableDeclarationStatement statement1 =
                            new CodeVariableDeclarationStatement(type1.FullName, info1.Name);
                        if (expression1 != null)
                        {
                            statement1.InitExpression = expression1;
                        }
                        statements.Add(statement1);
                        expression1 = new CodeVariableReferenceExpression(statement1.Name);
                    }
                    expression1 = new CodeDirectionExpression(direction1, expression1);
                }
                parameters.Add(expression1);
            }
        }

        private CodeExpression BuildProxy(CodeStatementCollection statements, MethodInfo method)
        {
            Type type1 = proxy.GetType();
            string text1 = CodeIdentifier.MakeCamel(type1.Name);
            if (proxySetting == ProxySettings.AllProperties)
            {
                return BuildClass(statements, text1, proxy);
            }
            CodeVariableDeclarationStatement statement1 = new CodeVariableDeclarationStatement(type1.Name, text1);
            statement1.InitExpression = new CodeObjectCreateExpression(type1.FullName, new CodeExpression[0]);
            statements.Add(statement1);
            CodeExpression expression1 = new CodeVariableReferenceExpression(text1);
            FieldInfo[] infoArray1 = null;
            if (proxySetting == ProxySettings.RequiredHeaders)
            {
                infoArray1 = MethodProperty.GetSoapHeaders(method, true);
            }
            else
            {
                infoArray1 = type1.GetFields();
            }
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                FieldInfo info1 = infoArray1[num1];
                if (typeof (SoapHeader).IsAssignableFrom(info1.FieldType))
                {
                    CodeExpression expression2 = new CodeFieldReferenceExpression(expression1, info1.Name);
                    CodeExpression expression3 = BuildObject(statements, info1.Name, info1.GetValue(proxy));
                    statements.Add(new CodeAssignStatement(expression2, expression3));
                }
            }
            return expression1;
        }

        private CodeExpression BuildXmlNode(CodeStatementCollection statements, string name, object value)
        {
            Type type1 = value.GetType();
            if (type1 == typeof (XmlElement))
            {
                XmlElement element1 = (XmlElement) value;
                string text1 = GetUniqueVariableName(name + "Doc", statements);
                CodeVariableDeclarationStatement statement1 =
                    new CodeVariableDeclarationStatement(typeof (XmlDocument), text1);
                statement1.InitExpression = new CodeObjectCreateExpression(typeof (XmlDocument), new CodeExpression[0]);
                statements.Add(statement1);
                CodeVariableReferenceExpression expression1 = new CodeVariableReferenceExpression(text1);
                CodeMethodInvokeExpression expression2 =
                    new CodeMethodInvokeExpression(expression1, "LoadXml", new CodeExpression[0]);
                expression2.Parameters.Add(new CodePrimitiveExpression(element1.OuterXml));
                statements.Add(expression2);
                return new CodeFieldReferenceExpression(expression1, "DocumentElement");
            }
            if (type1 == typeof (XmlAttribute))
            {
                XmlAttribute attribute1 = (XmlAttribute) value;
                string text2 = GetUniqueVariableName(name + "Doc", statements);
                CodeVariableDeclarationStatement statement2 =
                    new CodeVariableDeclarationStatement(typeof (XmlDocument), text2);
                statement2.InitExpression = new CodeObjectCreateExpression(typeof (XmlDocument), new CodeExpression[0]);
                statements.Add(statement2);
                CodeVariableReferenceExpression expression3 = new CodeVariableReferenceExpression(text2);
                CodeMethodInvokeExpression expression4 =
                    new CodeMethodInvokeExpression(expression3, "CreateAttribute", new CodeExpression[0]);
                expression4.Parameters.Add(new CodePrimitiveExpression(attribute1.Name));
                expression4.Parameters.Add(new CodePrimitiveExpression(attribute1.NamespaceURI));
                string text3 = GetUniqueVariableName(name + "Attr", statements);
                CodeVariableDeclarationStatement statement3 =
                    new CodeVariableDeclarationStatement(typeof (XmlAttribute), text3);
                statement3.InitExpression = expression4;
                statements.Add(statement3);
                CodeVariableReferenceExpression expression5 = new CodeVariableReferenceExpression(text3);
                statements.Add(
                    new CodeAssignStatement(new CodeFieldReferenceExpression(expression5, "Value"),
                                            new CodePrimitiveExpression(attribute1.Value)));
                return expression5;
            }
            throw new Exception("Unsupported XmlNode type");
        }

        public string Generate(ICodeGenerator codeGen)
        {
            StringWriter writer1 = new StringWriter();
            codeGen.GenerateCodeFromCompileUnit(compileUnit, writer1, null);
            return writer1.ToString();
        }

        public static string GetDumpCode(Language language)
        {
            if (language == Language.CS)
            {
                return dumpClassCS;
            }
            if (language == Language.VB)
            {
                return dumpClassVB;
            }
            return ("*** Dump classes is not generated for " + language + " ***");
        }

        private static string GetUniqueVariableName(string name, CodeStatementCollection statements)
        {
            name = CodeIdentifier.MakeCamel(name);
            foreach (CodeStatement statement1 in statements)
            {
                CodeVariableDeclarationStatement statement2 = statement1 as CodeVariableDeclarationStatement;
                if ((statement2 != null) && (statement2.Name == name))
                {
                    return (name + "_" + statements.Count);
                }
            }
            return name;
        }

        public static string GetUsingCode(Language language)
        {
            if (language == Language.CS)
            {
                return usingStatementsCS;
            }
            if (language == Language.VB)
            {
                return usingStatementsVB;
            }
            return "";
        }

        private bool IsCLSCompliant(Type type)
        {
            CLSCompliantAttribute[] attributeArray1 =
                type.GetCustomAttributes(typeof (CLSCompliantAttribute), true) as CLSCompliantAttribute[];
            if (attributeArray1.Length != 1)
            {
                return false;
            }
            return attributeArray1[0].IsCompliant;
        }


        public HttpWebClientProtocol Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }


        private CodeTypeDeclaration codeClass;
        private CodeNamespace codeNamespace;
        private CodeCompileUnit compileUnit;
        private static string dumpClassCS;
        private static string dumpClassVB;
        private CodeMethodReferenceExpression dumpMethodRef;
        private CodeEntryPointMethod mainMethod;
        private HttpWebClientProtocol proxy;
        private ProxySettings proxySetting;
        private static string usingStatementsCS;
        private static string usingStatementsVB;


        private enum ProxySettings
        {
            RequiredHeaders,
            AllHeaders,
            AllProperties
        }
    }
}
