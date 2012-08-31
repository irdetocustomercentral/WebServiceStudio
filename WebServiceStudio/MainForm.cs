using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using IBS.Utilities.ASMWSTester.BatchRun;
using IBS.Utilities.ASMWSTester.Http;
using IBS.Utilities.ASMWSTester.XmlConfig;

namespace IBS.Utilities.ASMWSTester
{
    public partial class MainForm : Form
    {
        private delegate void WsdlGenerationDoneCallback(bool genDone);

        internal Button buttonGet;
        private Button buttonInvoke;
        private Button buttonSend;
        private RichTextBoxFinds findOption;
        private static bool isV1;
        private Label labelEndPointUrl;
        private Label labelInput;
        private Label labelInputValue;
        private Label labelOutput;
        private Label labelOutputValue;
        private Label labelRequest;
        private Label labelResponse;
        internal static MainForm mainForm;
        private MenuItem menuItemTreeInputCopy;
        private MenuItem menuItemTreeInputPaste;
        private MenuItem menuItemTreeOutputCopy;
        private static string MiniHelpText;
        private OpenFileDialog openWsdlDialog;
        private Panel panelBottomMain;
        private Panel panelLeftInvoke;
        private Panel panelLeftRaw;
        private Panel panelLeftWsdl;
        private Panel panelRightInvoke;
        private Panel panelRightRaw;
        private Panel panelRightWsdl;
        private Panel panelTopMain;
        private PropertyGrid propInput;
        private PropertyGrid propOutput;
        private PropertyGrid propRequest;

        public String Message
        {
            get { return richMessage.Text; }
        }

        private RichTextBox richMessage;
        private RichTextBox richRequest;
        private RichTextBox richResponse;
        private RichTextBox richWsdl;
        private SaveFileDialog saveAllDialog;
        private string searchStr;
        private Splitter splitterInvoke;
        private Splitter splitterRaw;
        private Splitter splitterWsdl;
        internal TabControl tabMain;
        private TabPage tabPageInvoke;
        private TabPage tabPageMessage;
        private TabPage tabPageRaw;
        private TabPage tabPageWsdl;
        internal ComboBox textEndPointUri;
        private ToolBarButton toolBarButton1;
        internal TreeView treeInput;
        internal TreeView treeMethods;
        internal TreeView treeOutput;
        private TreeView treeWsdl;
        internal Wsdl wsdl;

        private string lastInvokeError;
        private string currentDsn;
        private string currentUserName;

        //Is Call it form Out
        public bool OutCall
        {
            get { return outCall; }
            set { outCall = value; }
        }

        private bool outCall = false;

        static MainForm()
        {
            isV1 = false;
            MiniHelpText =
                "";
            CheckForIllegalCrossThreadCalls = false;
        }

        public MainForm()
        {
            components = null;
            wsdl = null;
            findOption = RichTextBoxFinds.None;
            searchStr = "";
            InitializeComponent();

            chkLocalAssembly.Checked = Configuration.MasterConfig.OtherSettings.DefaultUseLocalAssembly;
            AutoGetChk.Checked = Configuration.MasterConfig.OtherSettings.DefaultAutoGet;
            AutoPopulateChk.Checked = Configuration.MasterConfig.OtherSettings.DefaultAutoPopulate;

            ServerCBox.Items.AddRange(Configuration.MasterConfig.ServerHistory.CalledServer);

            //ServerCBox.Items.Insert(0, "");

            //    this.ServerCBox.SelectedIndex = 0;


            textEndPointUri.Items.AddRange(Configuration.MasterConfig.InvokeSettings.RecentlyUsedUris);
            if (textEndPointUri.Items.Count > 0)
            {
                textEndPointUri.SelectedIndex = 0;
            }
            else
            {
                textEndPointUri.Text = "";
            }

            try
            {
                openWsdlDialog.DefaultExt = "wsdl";
                openWsdlDialog.Multiselect = true;
                openWsdlDialog.Title = "Open WSDL";
                openWsdlDialog.CheckFileExists = false;
                openWsdlDialog.CheckPathExists = false;
                saveAllDialog.FileName = "doc1";
            }
            catch (Exception exception1)
            {
                Console.WriteLine(exception1.ToString());
            }

            richWsdl.Font = Configuration.MasterConfig.UiSettings.WsdlFont;
            richMessage.Font = Configuration.MasterConfig.UiSettings.MessageFont;
            richRequest.Font = Configuration.MasterConfig.UiSettings.ReqRespFont;
            richResponse.Font = Configuration.MasterConfig.UiSettings.ReqRespFont;

            wsdl = new Wsdl();

            //this.treeOutput.BeforeExpand += new TreeViewCancelEventHandler(treeOutput_BeforeExpand);
            //this.treeOutput.AfterExpand += new TreeViewEventHandler(treeOutput_AfterExpand);          
            this.treeInput.ImageList = this.imgList;
            this.treeOutput.ImageList = this.imgList;
            this.treeMethods.ImageList = this.imgList;

            chkAutoSetSpecified.Checked = Configuration.MasterConfig.OtherSettings.AutoSetSpecified;

            if (Configuration.MasterConfig.OtherSettings.SimplifiedView)
                simplifiedToolStripMenuItem.PerformClick();
            else
                advancedToolStripMenuItem.PerformClick();

            hideSpecifiedFieldForOutputToolStripMenuItem.Checked = Configuration.MasterConfig.OtherSettings.HideSpecifiedFieldForOutput;

            this.msMain.Renderer = new RadioCheckRenderer();    //To show Radio button when menu item is checked
        }

        public class RadioCheckRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
            {
                if (e.Item.Name != "hideSpecifiedFieldForOutputToolStripMenuItem")
                    RadioButtonRenderer.DrawRadioButton(e.Graphics, e.ImageRectangle.Location, System.Windows.Forms.VisualStyles.RadioButtonState.CheckedNormal);
                else
                    base.OnRenderItemCheck(e);
            }
        }

        //void treeOutput_AfterExpand(object sender, TreeViewEventArgs e)
        //{
        //    if (e.Node.Tag != null)
        //    {
        //        switch (e.Node.Tag.GetType().ToString())
        //        {
        //            case "IBS.Utilities.ASMWSTester.ClassProperty": e.Node.ImageIndex = 0; e.Node.SelectedImageIndex = 0; break;
        //            case "IBS.Utilities.ASMWSTester.MethodProperty": e.Node.ImageIndex = 1; e.Node.SelectedImageIndex = 1; break;
        //            case "IBS.Utilities.ASMWSTester.PrimitiveProperty":
        //            case "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty":
        //                e.Node.ImageIndex = 2; e.Node.SelectedImageIndex = 2; break;
        //            default:
        //                e.Node.ImageIndex = -1; e.Node.SelectedImageIndex = -1; break;

        //        }
        //    }
        //}    

        public void buttonGet_Click(object sender, EventArgs e)
        {
            try
            {
                GetAssembly(false);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), Ex.Message);
            }
        }

        private void GetAssembly(bool useThread)
        {
            //if (buttonGet.Text == "Get")
            //{
            ClearAllTabs();
            //TabPage page1 = tabMain.SelectedTab;
            tabMain.SelectedTab = tabPageMessage;
            string text1 = textEndPointUri.Text;
            wsdl.Reset();
            wsdl.Paths.Add(text1);

            wsdl.UseLocalAssembly = chkLocalAssembly.Checked;

            if (OutCall || !useThread)
            {
                wsdl.Generate();
            }
            else
            {
                new Thread(new ThreadStart(wsdl.Generate)).Start();
            }

            //    buttonGet.Text = "Cancel";
            //}
            //else
            //{
            //    buttonGet.Text = "Get";
            //    ShowMessageInternal(this, MessageType.Failure, "Cancelled");
            //    wsdl.Reset();
            //    wsdl = new Wsdl();
            //}
        }

        private XmlDocument lastDocument = new XmlDocument();

        public void buttonInvoke_Click(object sender, EventArgs e)
        {
            //if (treeMethods.Nodes.Count == 0)
            //{
            //    return;
            //}

            //if (treeMethods.SelectedNode == null)
            //{
            //    return;
            //}

            if (wsdl.Paths.Count > 0)
            {
                if (wsdl.Paths[0] != textEndPointUri.Text)
                {
                    DialogResult result = MessageBox.Show("Use new WSDL EndPoint and get assembly ?", "Confirm",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        GetAssembly(false);
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Use new WSDL EndPoint and get assembly ?", "Confirm",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    GetAssembly(false);
                }
            }

            int invokeTimes;

            if (!int.TryParse(txtInvokeTimes.Text, out invokeTimes))
            {
                MessageBox.Show("Invoke Times must be a int!");
                return;
            }


            for (int i = 0; i < invokeTimes; i++)
            {
                lastInvokeError = String.Empty;
                Cursor cursor1 = Cursor;
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (lastDocument.ChildNodes.Count > 0 && treeInput.Nodes.Count == 0)
                    {
                        OuterRunHelper.ClickNode(mainForm.treeMethods, mainForm.treeMethods.Nodes[0], lastDocument.ChildNodes[0].Name, mainForm);

                        if (treeInput.Nodes.Count > 0)
                        {
                            XmlCongifManager.ApplyConfig(treeInput.Nodes[0], String.Empty, lastDocument, wsdl.ProxyAssembly);
                        }

                    }

                    if (treeInput.Nodes.Count > 0)
                    {

                        propOutput.SelectedObject = null;
                        treeOutput.Nodes.Clear();

                        SetCurrentValue(treeInput.Nodes[0]);

                        InvokeWebMethod();

                        lastDocument = new XmlDocument();
                        XmlCongifManager.ReadConfig(treeInput.Nodes[0], null, ref lastDocument);

                        if (btnStart.Text == "Stop")
                        {
                            BatchRunCongifFileHelper.CreateConfig(treeMethods.SelectedNode.Text, treeInput.Nodes[0]);
                            BatchRunCongifFileHelper.CreateRun(treeMethods.SelectedNode.Text,
                                                               treeMethods.SelectedNode.Text + ".xml");
                        }
                    }
                }
                catch (Exception ex)
                {
                    lastInvokeError = ex.ToString();
                    throw ex;
                }
                finally
                {
                    Cursor = cursor1;
                }
            }
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendWebRequest();
        }

        private void ClearAllTabs()
        {
            richWsdl.Clear();
            richWsdl.Font = Configuration.MasterConfig.UiSettings.WsdlFont;
            treeWsdl.Nodes.Clear();
            richMessage.Clear();
            richMessage.Font = Configuration.MasterConfig.UiSettings.MessageFont;
            richRequest.Clear();
            richRequest.Font = Configuration.MasterConfig.UiSettings.ReqRespFont;
            richResponse.Clear();
            richResponse.Font = Configuration.MasterConfig.UiSettings.ReqRespFont;
            treeMethods.Nodes.Clear();
            TreeNodeProperty.ClearIncludedTypes();
            treeInput.Nodes.Clear();
            treeOutput.Nodes.Clear();
            propInput.SelectedObject = null;
            propOutput.SelectedObject = null;
        }

        private void CopyToClipboard(TreeNodeProperty tnp)
        {
            if (!IsValidCopyNode(tnp))
            {
                throw new Exception("Cannot copy from here");
            }
            object obj1 = tnp.ReadChildren();
            if (obj1 != null)
            {
                StringWriter writer1 = new StringWriter();
                Type[] typeArray1 = new Type[] { obj1.GetType() };
                Type type1 = (obj1 is DataSet) ? typeof(DataSet) : typeof(object);
                new XmlSerializer(type1, typeArray1).Serialize(writer1, obj1);
                Clipboard.SetDataObject(writer1.ToString());
            }
        }

        private void DumpResponse(HttpWebResponse response)
        {
            richResponse.Text = WSSWebResponse.DumpResponse(response);
        }

        private void FillInvokeTab()
        {
            Assembly assembly1 = wsdl.ProxyAssembly;
            if (assembly1 != null)
            {
                treeMethods.Nodes.Clear();

                foreach (Type type1 in assembly1.GetTypes())
                {
                    if (TreeNodeProperty.IsWebService(type1))
                    {
                        TreeNode node1 = treeMethods.Nodes.Add(type1.Name);
                        HttpWebClientProtocol protocol1 = (HttpWebClientProtocol)Activator.CreateInstance(type1);
                        ProxyProperty property1 = new ProxyProperty(protocol1);
                        property1.RecreateSubtree(null);
                        node1.Tag = property1.TreeNode;
                        protocol1.Credentials = CredentialCache.DefaultCredentials;
                        SoapHttpClientProtocol protocol2 = protocol1 as SoapHttpClientProtocol;
                        if (protocol2 != null)
                        {
                            protocol2.CookieContainer = new CookieContainer();
                            protocol2.AllowAutoRedirect = true;
                        }

                        List<MethodInfo> methods = new List<MethodInfo>(type1.GetMethods());
                        methods.Sort(new Comparison<MethodInfo>(delegate(MethodInfo a, MethodInfo b)
                            {
                                return a.Name.CompareTo(b.Name);
                            }
                        ));

                        foreach (MethodInfo info1 in methods)  //type1.GetMethods())
                        {
                            if (TreeNodeProperty.IsWebMethod(info1))
                            {
                                TreeNode node2 = node1.Nodes.Add(info1.Name);
                                node2.Tag = info1;
                            }
                        }
                    }
                }
                treeMethods.ExpandAll();

                if (treeMethods.Nodes.Count > 0)
                {
                    this.AddIconRecursively(treeMethods.Nodes[0]);
                }
            }
        }

        private void FillWsdlTab()
        {
            if ((wsdl.Wsdls != null) && (wsdl.Wsdls.Count != 0))
            {
                int num3;
                richWsdl.Text = wsdl.Wsdls[0];
                treeWsdl.Nodes.Clear();
                TreeNode node1 = treeWsdl.Nodes.Add("WSDLs");
                XmlTreeWriter writer1 = new XmlTreeWriter();
                for (int num1 = 0; num1 < wsdl.Wsdls.Count; num1++)
                {
                    num3 = num1 + 1;
                    TreeNode node2 = node1.Nodes.Add("WSDL#" + num3.ToString());
                    node2.Tag = wsdl.Wsdls[num1];
                    writer1.FillTree(wsdl.Wsdls[num1], node2);
                }
                TreeNode node3 = treeWsdl.Nodes.Add("Schemas");
                for (int num2 = 0; num2 < wsdl.Xsds.Count; num2++)
                {
                    num3 = num2 + 1;
                    TreeNode node4 = node3.Nodes.Add("Schema#" + num3.ToString());
                    node4.Tag = wsdl.Xsds[num2];
                    writer1.FillTree(wsdl.Xsds[num2], node4);
                }
                TreeNode node5 = treeWsdl.Nodes.Add("Proxy");
                node5.Tag = wsdl.ProxyCode;
                TreeNode node6 = treeWsdl.Nodes.Add("ClientCode");
                node6.Tag = "Shows client code for all methods accessed in the invoke tab";
                node1.Expand();
            }
        }

        private void Find()
        {
            tabMain.SelectedTab = tabPageWsdl;
            richWsdl.Find(searchStr, richWsdl.SelectionStart + richWsdl.SelectionLength, findOption);
        }

        private string GenerateClientCode()
        {
            Script script1 = new Script(wsdl.ProxyNamespace, "MainClass");
            foreach (TreeNode node1 in treeMethods.Nodes)
            {
                script1.Proxy = GetProxyPropertyFromNode(node1).GetProxy();
                //foreach (TreeNode node2 in node1.Nodes)
                //{

                if (treeMethods.SelectedNode != null)
                {
                    TreeNode node2 = treeMethods.SelectedNode;
                    TreeNode node3 = node2.Tag as TreeNode;
                    if (node3 != null)
                    {
                        MethodProperty property1 = node3.Tag as MethodProperty;
                        if (property1 != null)
                        {
                            MethodInfo info1 = property1.GetMethod();
                            object[] objArray1 = property1.ReadChildren() as object[];
                            script1.AddMethod(info1, objArray1);
                        }
                    }
                }
                //}
            }
            return script1.Generate(wsdl.GetCodeGenerator());
        }

        private MethodProperty GetCurrentMethodProperty()
        {
            if ((treeInput.Nodes == null) || (treeInput.Nodes.Count == 0))
            {
                MessageBox.Show(this, "Select a web method to execute");
                return null;
            }
            TreeNode node1 = treeInput.Nodes[0];
            MethodProperty property1 = node1.Tag as MethodProperty;
            if (property1 == null)
            {
                MessageBox.Show(this, "Select a method to execute");
                return null;
            }
            return property1;
        }

        private ProxyProperty GetProxyPropertyFromNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            TreeNode node1 = treeNode.Tag as TreeNode;
            if (node1 != null)
            {
                return (node1.Tag as ProxyProperty);
            }
            return null;
        }

        private void InvokeWebMethod()
        {
            MethodProperty property1 = GetCurrentMethodProperty();
            if (property1 != null)
            {
                HttpWebClientProtocol protocol1 = property1.GetProxyProperty().GetProxy();
                RequestProperties properties1 = new RequestProperties(protocol1);
                try
                {
                    MethodInfo info1 = property1.GetMethod();
                    //Type type1 = info1.DeclaringType;
                    WSSWebRequest.RequestTrace = properties1;
                    object[] objArray1 = property1.ReadChildren() as object[];
                    object obj1 = info1.Invoke(protocol1, BindingFlags.Public, null, objArray1, null);
                    treeOutput.Nodes.Clear();
                    MethodProperty property2 = new MethodProperty(property1.GetProxyProperty(), info1, obj1, objArray1);
                    property2.RecreateSubtree(null);
                    treeOutput.Nodes.Add(property2.TreeNode);

                    if (!chkCollapse.Checked)
                        treeOutput.ExpandAll();

                    if (treeOutput.Nodes.Count > 0)
                    {
                        this.RemoveSpecifiedFieldsRecursively(treeOutput.Nodes[0]);
                        this.AddIconRecursively(treeOutput.Nodes[0]);
                    }

                }
                finally
                {
                    WSSWebRequest.RequestTrace = null;
                    propRequest.SelectedObject = properties1;
                    richRequest.Text = properties1.requestPayLoad;
                    richResponse.Text = properties1.responsePayLoad;
                }
            }
        }

        private void AddIconRecursively(TreeNode tn)
        {
            if (tn != null && tn.Tag != null)
            {
                switch (tn.Tag.GetType().ToString())
                {
                    case "IBS.Utilities.ASMWSTester.ClassProperty": tn.ImageIndex = 0; tn.SelectedImageIndex = 0; break;
                    case "System.Reflection.RuntimeMethodInfo":
                    case "IBS.Utilities.ASMWSTester.MethodProperty": tn.ImageIndex = 1; tn.SelectedImageIndex = 1; break;
                    case "IBS.Utilities.ASMWSTester.PrimitiveProperty":
                    case "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty":
                        tn.ImageIndex = 2; tn.SelectedImageIndex = 2; break;
                    default:
                        tn.ImageIndex = -1; tn.SelectedImageIndex = -1; break;
                }
            }

            foreach (TreeNode child in tn.Nodes)
            {
                this.AddIconRecursively(child);
            }
        }

        private void RemoveSpecifiedFieldsRecursively(TreeNode tn)
        {
            if (!this.hideSpecifiedFieldForOutputToolStripMenuItem.Checked)
                return;

            if (tn != null && tn.Tag != null)
            {
                TreeNodeProperty currentProp = tn.Tag as TreeNodeProperty;

                if (currentProp != null && tn.NextNode != null)
                {
                    TreeNodeProperty tnp = tn.NextNode.Tag as TreeNodeProperty;
                    if (tnp != null)
                    {
                        PrimitiveProperty pp = tnp as PrimitiveProperty;
                        if (pp != null && pp.Type == typeof(Boolean) &&
                            pp.Name.Equals(currentProp.Name + "Specified", StringComparison.InvariantCultureIgnoreCase))
                        {
                            tn.NextNode.Remove();
                        }
                    }
                }
            }

            foreach (TreeNode child in tn.Nodes)
            {
                this.RemoveSpecifiedFieldsRecursively(child);
            }
        }

        private bool IsValidCopyNode(TreeNodeProperty tnp)
        {
            return (((tnp != null) && (tnp.TreeNode.Parent != null)) && (tnp.GetType() != typeof(TreeNodeProperty)));
        }

        private bool IsValidPasteNode(TreeNodeProperty tnp)
        {
            IDataObject obj1 = Clipboard.GetDataObject();
            if ((obj1 == null) || (obj1.GetData(DataFormats.Text) == null))
            {
                return false;
            }
            return IsValidCopyNode(tnp);
        }

        [STAThread]
        private static void Main()
        {
            Version version1 = typeof(string).Assembly.GetName().Version;
            isV1 = ((version1.Major == 1) && (version1.Minor == 0)) && (version1.Build == 0xce4);
            mainForm = new MainForm();
            WSSWebRequestCreate.RegisterPrefixes();
            try
            {
                mainForm.SetupAssemblyResolver();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(null, exception1.ToString(), "Error Setting up Assembly Resolver");
            }
            Application.Run(mainForm);
        }

        //private void MainForm_SizeChanged(object sender, EventArgs e)
        //{
        //    tabMain.Width = (base.Location.X + base.Width) - tabMain.Location.X;
        //    tabMain.Height = (base.Location.Y + base.Height) - tabMain.Location.Y;
        //}

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, String.Format("ASMWSTester {0}", Application.ProductVersion), "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void menuItemFind_Click(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageInvoke)
            {
                if (treeMethods.SelectedNode == null && treeInput.SelectedNode == null && treeOutput.SelectedNode == null)
                {
                    MessageBox.Show(this, "Please select a TreeView first and click Ctrl+F or using context menu!", "Help",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                return;
            }

            SearchDialog dialog1 = new SearchDialog();
            dialog1.ShowDialog();
            if (dialog1.DialogResult == DialogResult.OK)
            {
                tabMain.SelectedTab = tabPageWsdl;
                findOption = RichTextBoxFinds.None;
                if (dialog1.MatchCase)
                {
                    findOption |= RichTextBoxFinds.MatchCase;
                }
                if (dialog1.WholeWord)
                {
                    findOption |= RichTextBoxFinds.WholeWord;
                }
                searchStr = dialog1.SearchStr;
                Find();
            }
        }

        private void menuItemFindNext_Click(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageInvoke)
            {
                if (treeSearchStatus.CanDoFindNext)
                {
                    SelectNode(treeSearchStatus.LatestTree, treeSearchStatus.LastestMatchNode, treeSearchStatus.LatestSearchText);
                }
                else
                    MessageBox.Show(this, "No further match or end!         ", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Find();
            }
        }

        private void menuItemHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "ASMWSTester - Powered by BSS Shanghai 2008"); //MiniHelpText);
        }

        //private void menuItemOpen_Click(object sender, EventArgs e)
        //{
        //    openWsdlDialog.ShowDialog();
        //    string text1 = openWsdlDialog.FileName;
        //    Cursor cursor1 = Cursor;
        //    Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        wsdl.Reset();
        //        wsdl.Paths.Add(text1);
        //        wsdl.Generate();
        //        FillWsdlTab();
        //        FillInvokeTab();
        //    }
        //    finally
        //    {
        //        Cursor = cursor1;
        //    }
        //}

        private void menuItemOptions_Click(object sender, EventArgs e)
        {
            new OptionDialog(this).ShowDialog();
        }

        private void menuItemSaveAll_Click(object sender, EventArgs e)
        {
            if ((saveAllDialog.ShowDialog() == DialogResult.OK) &&
                ((((wsdl.Wsdls != null) && (wsdl.Wsdls.Count != 0)) || ((wsdl.Xsds != null) && (wsdl.Xsds.Count != 0))) ||
                 (wsdl.ProxyCode != null)))
            {
                int num1 = saveAllDialog.FileName.LastIndexOf('.');
                string text1 = (num1 >= 0) ? saveAllDialog.FileName.Substring(0, num1) : saveAllDialog.FileName;
                if (wsdl.Wsdls.Count == 1)
                {
                    SaveFile(text1 + ".wsdl", wsdl.Wsdls[0]);
                }
                else
                {
                    for (int num2 = 0; num2 < wsdl.Wsdls.Count; num2++)
                    {
                        SaveFile(text1 + num2.ToString() + ".wsdl", wsdl.Wsdls[num2]);
                    }
                }
                if (wsdl.Xsds.Count == 1)
                {
                    SaveFile(text1 + ".xsd", wsdl.Xsds[0]);
                }
                else
                {
                    for (int num3 = 0; num3 < wsdl.Xsds.Count; num3++)
                    {
                        SaveFile(text1 + num3.ToString() + ".xsd", wsdl.Xsds[num3]);
                    }
                }
                SaveFile(text1 + "." + wsdl.ProxyFileExtension, wsdl.ProxyCode);
                SaveFile(text1 + "Client." + wsdl.ProxyFileExtension,
                         Script.GetUsingCode(wsdl.WsdlProperties.Language) + "\n" + GenerateClientCode() + "\n" +
                         Script.GetDumpCode(wsdl.WsdlProperties.Language));
            }
        }

        public Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly1 = wsdl.ProxyAssembly;
            if ((assembly1 != null) && (assembly1.GetName().ToString() == args.Name))
            {
                return assembly1;
            }
            return null;
        }

        private void PanelLeftRaw_SizeChanged(object sender, EventArgs e)
        {
            propRequest.SetBounds(0, 0, panelLeftRaw.Width, panelLeftRaw.Height, BoundsSpecified.Size);
        }

        private void propInput_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            TreeNodeProperty currentProp = propInput.SelectedObject as TreeNodeProperty;
            if ((currentProp != null) && ((e.ChangedItem.Label == "Type") && (e.OldValue != currentProp.Type)))
            {
                TreeNodeProperty property2 = TreeNodeProperty.CreateTreeNodeProperty(currentProp);
                property2.TreeNode = currentProp.TreeNode;
                property2.RecreateSubtree(null);
                treeInput.SelectedNode = property2.TreeNode;
            }

            // To auto set the Specified boolean node when value changed
            if (!this.chkAutoSetSpecified.Checked)
                return;

            if (currentProp != null && (e.ChangedItem.Label == "Value")) // || e.ChangedItem.Label == "IsNull"))
            {
                TreeNode tn = this.treeInput.SelectedNode;
                if (tn != null && tn.NextNode != null)
                {
                    TreeNodeProperty tnp = tn.NextNode.Tag as TreeNodeProperty;
                    if (tnp != null)
                    {
                        PrimitiveProperty pp = tnp as PrimitiveProperty;
                        if (pp != null && pp.Type == typeof(Boolean) &&
                            pp.Name.Equals(currentProp.Name + "Specified", StringComparison.InvariantCultureIgnoreCase))
                        {
                            pp.Value = true;
                        }
                    }
                }
            }
        }

        private bool SaveFile(string fileName, string contents)
        {
            if (File.Exists(fileName) &&
                (MessageBox.Show(this, "File " + fileName + " already exists. Overwrite?", "Warning",
                                 MessageBoxButtons.YesNo) != DialogResult.Yes))
            {
                return false;
            }
            FileStream stream1 = File.OpenWrite(fileName);
            StreamWriter writer1 = new StreamWriter(stream1);
            writer1.Write(contents);
            writer1.Flush();
            stream1.SetLength(stream1.Position);
            stream1.Close();
            return true;
        }

        private void SendWebRequest()
        {
            Encoding encoding1 = new UTF8Encoding(true);
            RequestProperties properties1 = propRequest.SelectedObject as RequestProperties;
            if (properties1 != null)
            {
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.CreateDefault(new Uri(properties1.Url));
                if ((properties1.HttpProxy != null) && (properties1.HttpProxy.Length != 0))
                {
                    request1.Proxy = new WebProxy(properties1.HttpProxy);
                }
                request1.Method = properties1.Method.ToString();
                request1.ContentType = properties1.ContentType;
                request1.Headers["SOAPAction"] = properties1.SOAPAction;
                request1.SendChunked = properties1.SendChunked;
                request1.AllowAutoRedirect = properties1.AllowAutoRedirect;
                request1.AllowWriteStreamBuffering = properties1.AllowWriteStreamBuffering;
                request1.KeepAlive = properties1.KeepAlive;
                request1.Pipelined = properties1.Pipelined;
                request1.PreAuthenticate = properties1.PreAuthenticate;
                request1.Timeout = properties1.Timeout;
                MethodProperty property1 = GetCurrentMethodProperty();
                HttpWebClientProtocol protocol1 = property1.GetProxyProperty().GetProxy();
                if (properties1.UseCookieContainer)
                {
                    if (protocol1.CookieContainer != null)
                    {
                        request1.CookieContainer = protocol1.CookieContainer;
                    }
                    else
                    {
                        request1.CookieContainer = new CookieContainer();
                    }
                }
                CredentialCache cache1 = new CredentialCache();
                bool flag1 = false;
                if ((properties1.BasicAuthUserName != null) && (properties1.BasicAuthUserName.Length != 0))
                {
                    cache1.Add(new Uri(properties1.Url), "Basic",
                               new NetworkCredential(properties1.BasicAuthUserName, properties1.BasicAuthPassword));
                    flag1 = true;
                }
                if (properties1.UseDefaultCredential)
                {
                    cache1.Add(new Uri(properties1.Url), "NTLM", (NetworkCredential)CredentialCache.DefaultCredentials);
                    flag1 = true;
                }
                if (flag1)
                {
                    request1.Credentials = cache1;
                }
                if (properties1.Method == RequestProperties.HttpMethod.POST)
                {
                    request1.ContentLength = richRequest.Text.Length + encoding1.GetPreamble().Length;
                    StreamWriter writer1 = new StreamWriter(request1.GetRequestStream(), encoding1);
                    writer1.Write(richRequest.Text);
                    writer1.Close();
                }
                try
                {
                    HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                    DumpResponse(response1);
                    response1.Close();
                }
                catch (WebException exception1)
                {
                    if (exception1.Response != null)
                    {
                        DumpResponse((HttpWebResponse)exception1.Response);
                        return;
                    }
                    richResponse.Text = exception1.ToString();
                }
                catch (Exception exception2)
                {
                    richResponse.Text = exception2.ToString();
                }
            }
        }

        public void SetupAssemblyResolver()
        {
            ResolveEventHandler handler1 = new ResolveEventHandler(OnAssemblyResolve);
            AppDomain.CurrentDomain.AssemblyResolve += handler1;
        }

        public static void ShowMessage(object sender, MessageType status, string message)
        {
            if (mainForm != null)
            {
                mainForm.ShowMessageInternal(sender, status, message);
            }
        }

        private void ShowMessageInternal(object sender, MessageType status, string message)
        {
            if (message == null)
            {
                message = status.ToString();
            }
            switch (status)
            {
                case MessageType.Begin:
                    richMessage.SelectionColor = Color.Blue;
                    richMessage.AppendText(message + "\n");
                    richMessage.Update();
                    return;

                case MessageType.Success:
                    richMessage.SelectionColor = Color.Green;
                    richMessage.AppendText(message + "\n");
                    richMessage.Update();
                    if (sender == wsdl)
                    {
                        //if (OutCall)
                        //{
                        WsdlGenerationDone(true);
                        //}
                        //else
                        //{
                        //    base.BeginInvoke(new WsdlGenerationDoneCallback(WsdlGenerationDone), new object[] { true });
                        //}
                    }
                    return;

                case MessageType.Failure:
                    richMessage.SelectionColor = Color.Red;
                    richMessage.AppendText(message + "\n");
                    richMessage.Update();
                    if (sender == wsdl)
                    {
                        //if (OutCall)
                        //{
                        WsdlGenerationDone(false);
                        //}
                        //else
                        //{
                        //    base.BeginInvoke(new WsdlGenerationDoneCallback(WsdlGenerationDone), new object[] { false });
                        //}
                    }
                    return;

                case MessageType.Warning:
                    richMessage.SelectionColor = Color.DarkRed;
                    richMessage.AppendText(message + "\n");
                    richMessage.Update();
                    return;

                case MessageType.Error:
                    richMessage.SelectionColor = Color.Red;
                    richMessage.AppendText(message + "\n");
                    richMessage.Update();
                    return;
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedTab == tabPageRaw)
            {
                if (propRequest.SelectedObject == null)
                {
                    propRequest.SelectedObject = new RequestProperties(null);
                }
            }
            else if (((tabMain.SelectedTab == tabPageWsdl) && (treeWsdl.Nodes != null)) && (treeWsdl.Nodes.Count != 0) &&
                     (!chkLocalAssembly.Checked))
            {
                TreeNode node1 = treeWsdl.Nodes[3];
                node1.Tag = GenerateClientCode();
                if (treeWsdl.SelectedNode == node1)
                {
                    richWsdl.Text = node1.Tag.ToString();
                }
            }
            else if (tabMain.SelectedTab == tabPageBatchConfig)
            {
                if (BatchRunCongifFileHelper.testConfigNode != null)
                {
                    textBox1.Text =
                        XmlCongifManager.FormatXmlDocumentToString(BatchRunCongifFileHelper.testConfigNode.ToXML());
                }
            }
        }

        private void textEndPointUri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == '\n'))
            {
                buttonGet_Click(sender, null);
                e.Handled = true;
            }
            else if (!char.IsControl(e.KeyChar))
            {
                if (!isV1)
                {
                    textEndPointUri.SelectedText = e.KeyChar.ToString();
                }
                e.Handled = true;
                string text1 = textEndPointUri.Text;
                if ((text1 != null) && (text1.Length != 0))
                {
                    for (int num1 = 0; num1 < textEndPointUri.Items.Count; num1++)
                    {
                        if (((string)textEndPointUri.Items[num1]).StartsWith(text1))
                        {
                            textEndPointUri.SelectedIndex = num1;
                            textEndPointUri.Select(text1.Length, textEndPointUri.Text.Length);
                            return;
                        }
                    }
                }
            }
        }

        private void treeInput_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propInput.SelectedObject = e.Node.Tag;
            menuItemTreeInputCopy.Enabled = IsValidCopyNode(e.Node.Tag as TreeNodeProperty);
            menuItemTreeInputPaste.Enabled = IsValidPasteNode(e.Node.Tag as TreeNodeProperty);
        }

        private void treeInputMenuCopy_Click(object sender, EventArgs e)
        {
            CopyToClipboard(treeInput.SelectedNode.Tag as TreeNodeProperty);
        }

        private void treeInputMenuPaste_Click(object sender, EventArgs e)
        {
            TreeNodeProperty property1 = treeInput.SelectedNode.Tag as TreeNodeProperty;
            if (property1 is MethodProperty)
            {
                throw new Exception("Paste not valid on method");
            }
            if (property1 != null)
            {
                Type[] typeArray1 = property1.GetTypeList();
                Type type1 = typeof(DataSet).IsAssignableFrom(typeArray1[0]) ? typeof(DataSet) : typeof(object);
                XmlSerializer serializer1 = new XmlSerializer(type1, typeArray1);
                IDataObject obj1 = Clipboard.GetDataObject();
                StringReader reader1 = new StringReader((string)obj1.GetData(DataFormats.Text));
                object obj2 = serializer1.Deserialize(reader1);
                if ((obj2 == null) || !typeArray1[0].IsAssignableFrom(obj2.GetType()))
                {
                    throw new Exception("Invalid Type pasted");
                }
                TreeNodeProperty property2 = TreeNodeProperty.CreateTreeNodeProperty(property1, obj2);
                property2.TreeNode = property1.TreeNode;
                property2.RecreateSubtree(null);
                treeInput.SelectedNode = property2.TreeNode;
            }
        }

        public void treeMethods_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is MethodInfo)
            {
                MethodInfo info1 = e.Node.Tag as MethodInfo;
                treeInput.Nodes.Clear();
                MethodProperty property1 = new MethodProperty(GetProxyPropertyFromNode(e.Node), info1);
                property1.RecreateSubtree(null);
                treeInput.Nodes.Add(property1.TreeNode);
                if (!OutCall)
                {
                    e.Node.Tag = property1.TreeNode;
                }

                XmlCongifManager.ApplyDefaultConfig(treeInput.Nodes[0]);
            }
            else if (e.Node.Tag is TreeNode)
            {
                treeInput.Nodes.Clear();
                treeInput.Nodes.Add((TreeNode)e.Node.Tag);
            }
            treeInput.ExpandAll();
            treeInput.SelectedNode = treeInput.Nodes[0];

            if (treeInput.Nodes.Count > 0)
                this.AddIconRecursively(treeInput.Nodes[0]);
        }

        private void treeOutput_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propOutput.SelectedObject = e.Node.Tag;
            menuItemTreeOutputCopy.Enabled = IsValidCopyNode(e.Node.Tag as TreeNodeProperty);
        }

        private void treeOutputMenuCopy_Click(object sender, EventArgs e)
        {
            CopyToClipboard(treeOutput.SelectedNode.Tag as TreeNodeProperty);
        }

        private void treeWsdl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((e.Node.Tag != null) && (richWsdl.Tag != e.Node.Tag))
            {
                richWsdl.Text = e.Node.Tag.ToString();
                richWsdl.Tag = e.Node.Tag;
            }
            XmlTreeNode node1 = e.Node as XmlTreeNode;
            if (node1 != null)
            {
                richWsdl.Select(node1.StartPosition, node1.EndPosition - node1.StartPosition);
            }
        }

        private void WsdlGenerationDone(bool genDone)
        {
            buttonGet.Text = "Get";
            FillWsdlTab();
            if (genDone)
            {
                ShowMessageInternal(this, MessageType.Begin, "Reflecting Proxy Assembly");
                FillInvokeTab();
                tabMain.SelectedTab = tabPageInvoke;
                ShowMessageInternal(this, MessageType.Success, "Ready To Invoke");
                Configuration.MasterConfig.InvokeSettings.AddUri(textEndPointUri.Text);
                textEndPointUri.Items.Clear();
                textEndPointUri.Items.AddRange(Configuration.MasterConfig.InvokeSettings.RecentlyUsedUris);
            }
        }

        #region Version & Module

        private IDictionary<string, VersionUriCollection> uriRepository = new Dictionary<string, VersionUriCollection>();

        private VersionUri SelectedVersionUri
        {
            get
            {
                string serverName = ServerCBox.Text;
                string selectedVersion = cmbVersion.SelectedItem as string;
                if (selectedVersion != null)
                {
                    return uriRepository[serverName][selectedVersion];
                }
                else
                {
                    return null;
                }
            }
        }

        private ModuleUri SelectedServiceUri
        {
            get
            {
                string selectedModule = cmbModule.SelectedItem as string;
                if (selectedModule != null)
                {
                    return SelectedVersionUri.ServiceUris[selectedModule];
                }
                else
                {
                    return null;
                }
            }
        }

        private void InitializeDropdowns()
        {
            string buildUri = ServerCBox.Text;

            if (buildUri != "")
            {
                if (!uriRepository.ContainsKey(buildUri))
                {
                    UriResolver uriResolver = new UriResolver();
                    VersionUriCollection uris = uriResolver.GetVersionUriCollection(buildUri);
                    uriRepository.Add(buildUri, uris);
                }

                // cmbVersion.DataSource = uriRepository[buildUri].Versions;
                cmbVersion.Items.Clear();
                foreach (string s in uriRepository[buildUri].Versions)
                {
                    cmbVersion.Items.Add(s);
                }
            }
        }

        private void cmbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedVersionUri != null)
            {
                cmbModule.Items.Clear();
                foreach (string s in SelectedVersionUri.ServiceUris.Modules)
                {
                    cmbModule.Items.Add(s);
                }


                //cmbModule.DataSource = SelectedVersionUri.ServiceUris.Modules;
                //cmbModule.Refresh();
            }
        }

        private void cmbModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedServiceUri != null)
            {
                textEndPointUri.Text = SelectedServiceUri.Uri;
                if (AutoGetChk.Checked)
                {
                    GetAssembly(false);

                    //MessageBox.Show(textEndPointUri.Text);
                }
            }
        }

        //private void SetWSDL()
        //{
        //    string wsdlStr = "";

        //    if (this.ServerCBox.Text.Trim() != "")
        //    {
        //        if (!this.ServerCBox.Text.ToUpper().StartsWith("HTTP:")) wsdlStr = "http://";
        //        wsdlStr += this.ServerCBox.Text.Trim();
        //        if (!wsdlStr.EndsWith("/")) wsdlStr += "/";
        //        wsdlStr += this.cmbVersion.Text + "/";
        //        wsdlStr += this.cmbModule.Text;

        //        this.textEndPointUri.Text = wsdlStr;
        //    }

        //}

        public delegate void AsyncMethodCaller();

        private void btnPopulate_Click(object sender, EventArgs e)
        {
            if (ServerCBox.Text == "")
            {
                MessageBox.Show("Please select a server");
                return;
            }

            AsyncInitializeDropdowns();
        }

        private void AsyncInitializeDropdowns()
        {
            btnPopulate.Enabled = false;
            cmbModule.Enabled = false;
            cmbVersion.Enabled = false;
            ServerCBox.Enabled = false;
            buttonGet.Enabled = false;

            AsyncMethodCaller caller = new AsyncMethodCaller(InitializeDropdowns);
            caller.BeginInvoke(
                new AsyncCallback(InitializeDropdownsCallBack),
                caller);
        }

        private void InitializeDropdownsCallBack(IAsyncResult ar)
        {
            Configuration.MasterConfig.ServerHistory.AddHistory(ServerCBox.Text);

            btnPopulate.Enabled = true;
            btnPopulate.Enabled = true;
            cmbModule.Enabled = true;
            cmbVersion.Enabled = true;
            ServerCBox.Enabled = true;
            buttonGet.Enabled = true;
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                btnStart.Text = "Stop";
                BatchRunCongifFileHelper.CreateTest("Test1");
            }
            else
            {
                btnStart.Text = "Start";
            }
        }


        private void cmenuInuptTree_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "SaveInput")
            {
                XmlDocument xmlDocument = new XmlDocument();
                MethodProperty property1 = GetCurrentMethodProperty();
                if (property1 != null)
                {
                    HttpWebClientProtocol protocol1 = property1.GetProxyProperty().GetProxy();
                    RequestProperties properties1 = new RequestProperties(protocol1);
                    try
                    {
                        WSSWebRequest.RequestTrace = properties1;
                        XmlCongifManager.ReadConfig(treeInput.Nodes[0], null, ref xmlDocument);
                    }
                    finally
                    {
                        WSSWebRequest.RequestTrace = null;
                        propRequest.SelectedObject = properties1;
                        richRequest.Text = properties1.requestPayLoad;
                        richResponse.Text = properties1.responsePayLoad;
                    }
                }

                if (property1 != null)
                {
                    Stream myStream = null;

                    saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;

                    if (Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath != String.Empty &&
                        Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath != null)
                    {
                        saveFileDialog1.InitialDirectory =
                            Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath;
                    }

                    saveFileDialog1.FileName = treeInput.Nodes[0].Tag.ToString();

                    cmenuInputTree.Close();

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if ((myStream = saveFileDialog1.OpenFile()) != null)
                        {
                            StreamWriter wText = new StreamWriter(myStream);

                            wText.Write(XmlCongifManager.FormatXmlDocumentToString(xmlDocument));
                            wText.Flush();
                            myStream.Close();
                        }
                    }
                }
            }
            else if (e.ClickedItem.Text == "LoadInput")
            {
                Stream myStream = null;

                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath != String.Empty &&
                    Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath != null)
                {
                    openFileDialog1.InitialDirectory = Configuration.MasterConfig.OtherSettings.DefaultConfigFilePath;
                }

                cmenuInputTree.Close();

                XmlDocument xmlDocument = new XmlDocument();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        xmlDocument.Load(myStream);
                        myStream.Close();
                    }
                }

                if (treeMethods.Nodes.Count > 0)
                {
                    if (xmlDocument.ChildNodes.Count > 0)
                    {
                        OuterRunHelper.ClickNode(mainForm.treeMethods, mainForm.treeMethods.Nodes[0],
                                                 xmlDocument.ChildNodes[0].Name, mainForm);
                    }
                }

                if (treeInput.Nodes.Count > 0)
                {
                    XmlCongifManager.ApplyConfig(treeInput.Nodes[0], String.Empty, xmlDocument, wsdl.ProxyAssembly);
                }
            }
            else if (e.ClickedItem.Text == "Find")
            {
                FindForm findForm = new FindForm();
                findForm.ShowDialog();

                if (findForm.DialogResult == DialogResult.OK)
                {
                    if (treeInput.Nodes != null && treeInput.Nodes.Count > 0)
                        SelectNode(treeInput, treeInput.Nodes[0], findForm.FindValue);
                }
            }
            else if (e.ClickedItem.Text == "LoadValueField")
            {
                if (btnStart.Text == "Stop")
                {
                    TreeNode node = treeInput.SelectedNode;
                    ValueFieldsForm valueFieldsForm = new ValueFieldsForm();
                    valueFieldsForm.ShowDialog();
                    BatchRunCongifFileHelper.RunParameter(valueFieldsForm.SelectVauleField,
                                                          TreeNodeToXPath(node.FullPath),
                                                          treeMethods.SelectedNode.Text,
                                                          treeMethods.SelectedNode.Text + ".xml");
                }
            }
        }

        private void cmenuOutputTree_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "SaveOutput")
            {
                if (treeOutput.Nodes.Count < 1)
                {
                    return;
                }


                XmlDocument xmlDocument1 = new XmlDocument();

                if (mainForm.treeOutput.Nodes[0].Nodes[1].Nodes.Count > 0)
                {
                    TreeNode outPutNode = mainForm.treeOutput.Nodes[0].Nodes[1].Nodes[0];
                    XmlCongifManager.ReadOutput(outPutNode, null, ref xmlDocument1);
                }

                Stream myStream = null;

                saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (Configuration.MasterConfig.OtherSettings.DefaultOutputFilePath != String.Empty &&
                    Configuration.MasterConfig.OtherSettings.DefaultOutputFilePath != null)
                {
                    saveFileDialog1.InitialDirectory = Configuration.MasterConfig.OtherSettings.DefaultOutputFilePath;
                }

                cmenuOutputTree.Close();

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        StreamWriter wText = new StreamWriter(myStream);

                        wText.Write(XmlCongifManager.FormatXmlDocumentToString(xmlDocument1));
                        wText.Flush();
                        myStream.Close();
                    }
                }
            }
            else if (e.ClickedItem.Text == "Find")
            {
                FindForm findForm = new FindForm();
                findForm.ShowDialog();

                if (findForm.DialogResult == DialogResult.OK)
                {
                    if (treeOutput.Nodes != null && treeOutput.Nodes.Count > 0)
                        SelectNode(treeOutput, treeOutput.Nodes[0], findForm.FindValue);
                }
            }
            else if (e.ClickedItem.Text == "SaveValueField")
            {
                if (btnStart.Text == "Stop")
                {
                    TreeNode node = treeOutput.SelectedNode;
                    ValueFieldsForm valueFieldsForm = new ValueFieldsForm();
                    valueFieldsForm.ShowDialog();
                    BatchRunCongifFileHelper.RunSetValueField(valueFieldsForm.SelectVauleField,
                                                              TreeNodeToXPath(node.FullPath),
                                                              treeMethods.SelectedNode.Text,
                                                              treeMethods.SelectedNode.Text + ".xml");
                }
            }
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Stop")
            {
                BatchRunCongifFileHelper.Save();
            }
        }

        private void cmenuOutputTree_Opening(object sender, CancelEventArgs e)
        {
            cmenuOutputTree.Items["itemSaveVauleField"].Enabled = true;
            cmenuOutputTree.Items["itemSaveOutput"].Enabled = true;
            cmenuOutputTree.Items["itemFind"].Enabled = true;

            if (btnStart.Text == "Start")
            {
                cmenuOutputTree.Items["itemSaveVauleField"].Enabled = false;
            }

            if (treeOutput.Nodes.Count < 1)
            {
                cmenuOutputTree.Items["itemSaveOutput"].Enabled = false;
                cmenuOutputTree.Items["itemFind"].Enabled = false;
            }
        }

        private void cmenuInputTree_Opening(object sender, CancelEventArgs e)
        {
            cmenuInputTree.Items["itemLoadValueField"].Enabled = true;
            cmenuInputTree.Items["itemSaveInput"].Enabled = true;
            cmenuInputTree.Items["itemLoadInput"].Enabled = true;
            cmenuInputTree.Items["itemFind1"].Enabled = true;

            if (btnStart.Text == "Start")
            {
                cmenuInputTree.Items["itemLoadValueField"].Enabled = false;
            }

            if (treeInput.Nodes.Count < 1)
            {
                cmenuInputTree.Items["itemSaveInput"].Enabled = false;
                cmenuInputTree.Items["itemFind1"].Enabled = false;
            }

            if (treeMethods.Nodes.Count < 1)
            {
                cmenuInputTree.Items["itemLoadInput"].Enabled = false;
            }
        }

        private void btnUpdateLocalAssembly_Click(object sender, EventArgs e)
        {
            ClearAllTabs();
            //TabPage page1 = tabMain.SelectedTab;
            tabMain.SelectedTab = tabPageMessage;
            string text1 = textEndPointUri.Text;
            wsdl.Reset();
            wsdl.Paths.Add(text1);

            wsdl.UseLocalAssembly = chkLocalAssembly.Checked;

            new Thread(new ThreadStart(wsdl.GenerateAndUpdateAssembly)).Start();
        }

        private IList<String> updateUrls = new List<String>();

        private void btnBatchUpdateLocalAssembly_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPageMessage;

            if (Configuration.MasterConfig.BatchUpdateAssemblySettings.AutoUpdate)
            {
                AsyncMethodCaller caller = new AsyncMethodCaller(InitializeDropdowns);
                IAsyncResult result = caller.BeginInvoke(
                    null,
                    caller);

                caller.EndInvoke(result);

                for (int i = 0; i < cmbVersion.Items.Count; i++)
                {
                    cmbVersion.SelectedIndex = i;
                    for (int j = 1; j < cmbModule.Items.Count; j++)
                    {
                        cmbModule.SelectedIndex = j;

                        foreach (String uri in Configuration.MasterConfig.BatchUpdateAssemblySettings.UpdateModuleNames)
                        {
                            if (uri.ToLower() == cmbModule.Text.ToLower())
                            {
                                string updateUrl = textEndPointUri.Text.ToLower();
                                if (!updateUrls.Contains(updateUrl))
                                {
                                    updateUrls.Add(updateUrl);
                                }
                            }
                        }
                    }
                }
            }

            string[] updateVersions = Configuration.MasterConfig.BatchUpdateAssemblySettings.UpdateVersions;
            string[] updateModuleNames = Configuration.MasterConfig.BatchUpdateAssemblySettings.UpdateModuleNames;

            if (updateVersions != null && updateModuleNames != null)
            {
                for (int i = 0; i < updateVersions.Length; i++)
                {
                    for (int j = 0; j < updateModuleNames.Length; j++)
                    {
                        string updateUrl = updateVersions[i] + updateModuleNames[j];
                        if (!updateUrls.Contains(updateUrl))
                        {
                            updateUrls.Add(updateUrl);
                        }
                    }
                }
            }


            beginUpdateLocalAssembly = 0;
            finishedUpdateLocalAssembly = 0;
            btnBatchUpdateLocalAssembly.Enabled = false;

            foreach (string updateUrl in updateUrls)
            {
                beginUpdateLocalAssembly++;
                Wsdl wsdl1 = new Wsdl();
                wsdl1.Paths.Add(updateUrl);

                AsyncMethodCaller caller1 = new AsyncMethodCaller(wsdl1.GenerateAndUpdateAssembly);
                caller1.BeginInvoke(
                    new AsyncCallback(BatchUpdateLocalAssemblyCallBack),
                    caller1);
                ShowMessage(this, MessageType.Begin,
                            "Begin Create Local Assembly Use EndPointUri: " + updateUrl);
            }
        }


        private int beginUpdateLocalAssembly = 0;
        private int finishedUpdateLocalAssembly = 0;

        private void BatchUpdateLocalAssemblyCallBack(IAsyncResult ar)
        {
            AsyncMethodCaller caller = (AsyncMethodCaller)ar.AsyncState;

            finishedUpdateLocalAssembly++;
            bool batchUpdateLocalAssemblyFinished = (beginUpdateLocalAssembly == finishedUpdateLocalAssembly);
            if (batchUpdateLocalAssemblyFinished)
            {
                btnBatchUpdateLocalAssembly.Enabled = true;
                ShowMessage(this, MessageType.Success,
                            "Batch Update Local Assembly Done.");
            }

            caller.EndInvoke(ar);
        }

        private void btnAddBatchUpdateAssemblyUri_Click(object sender, EventArgs e)
        {
            if (textEndPointUri.Text != null)
            {
                string[] strs = textEndPointUri.Text.Split('/');
                if (strs.Length > 2)
                {
                    string moduleName = strs[strs.Length - 2] +
                                        "/" +
                                        strs[strs.Length - 1];
                    Configuration.MasterConfig.BatchUpdateAssemblySettings.AddUpdateModuleName(moduleName);
                }
            }
        }

        private void cmenuRichWsdl_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (treeWsdl.Nodes.Count != 0)
            {
                if (treeWsdl.Nodes[3].Tag != null)
                {
                    string filepath = Configuration.MasterConfig.OtherSettings.TemplateFileName;
                    if (filepath != null && filepath != String.Empty)
                    {
                        using (StreamReader sr = File.OpenText(filepath))
                        {
                            string s = sr.ReadToEnd();
                            s = s.Replace("[[InputParameter]]", treeWsdl.Nodes[3].Tag.ToString());
                            s = s.Replace("[[EndPointUri]]", textEndPointUri.Text);
                            s = s.Replace("[[ActualResult]]", lastInvokeError);
                            s = s.Replace("[[Login]]", currentUserName);
                            s = s.Replace("[[Database]]", currentDsn);
                            s = s.Replace("[[MethodName]]", GetCurrentMethodProperty().Name);
                            Clipboard.SetText(s);
                        }
                    }
                }
            }
        }

        private void SetCurrentValue(TreeNode treeNode)
        {
            if (treeNode.Tag != null)
            {
                string nodeType = treeNode.Tag.GetType().ToString();

                if (nodeType == "IBS.Utilities.ASMWSTester.NullablePrimitiveProperty")
                {
                    if (((NullablePrimitiveProperty)treeNode.Tag).Name == "Dsn")
                    {
                        if (Configuration.MasterConfig.AuthenticationHeaderSettings.Dsn != "")
                        {
                            currentDsn = ((NullablePrimitiveProperty)treeNode.Tag).Value.ToString();
                        }
                    }
                    else if (((NullablePrimitiveProperty)treeNode.Tag).Name == "UserName")
                    {
                        if (Configuration.MasterConfig.AuthenticationHeaderSettings.UserName != "")
                        {
                            currentUserName = ((NullablePrimitiveProperty)treeNode.Tag).Value.ToString();
                        }
                    }
                }
            }

            foreach (TreeNode childTreeNode in treeNode.Nodes)
            {
                SetCurrentValue(childTreeNode);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //XmlDocument xmlDocument = new XmlDocument();
        //    //MethodProperty property1 = GetCurrentMethodProperty();
        //    //if (property1 != null)
        //    //{
        //    //    HttpWebClientProtocol protocol1 = property1.GetProxyProperty().GetProxy();
        //    //    RequestProperties properties1 = new RequestProperties(protocol1);
        //    //    try
        //    //    {
        //    //        WSSWebRequest.RequestTrace = properties1;
        //    //        XmlCongifManager.ReadConfig(treeInput.Nodes[0], null, ref xmlDocument);
        //    //    }
        //    //    finally
        //    //    {
        //    //        WSSWebRequest.RequestTrace = null;
        //    //        propRequest.SelectedObject = properties1;
        //    //        richRequest.Text = properties1.requestPayLoad;
        //    //        richResponse.Text = properties1.responsePayLoad;
        //    //    }
        //    //}

        //    //XmlDocument xmlDocument1 = new XmlDocument();

        //    //xmlDocument1.Load("C:\\Documents and Settings\\user1\\Desktop\\PSMLog.xml");


        //    //string xpath = String.Empty;

        //    //Test1(treeInput.Nodes[0], xmlDocument1);
        //    //Test(xmlDocument1.ChildNodes[0], xpath, ref xmlDocument);


        //    //XmlCongifManager.ApplyConfig(treeInput.Nodes[0], String.Empty, xmlDocument, wsdl.ProxyAssembly);
        //}

        //private void Test(XmlNode node, string parentXPath, ref XmlDocument vvv)
        //{
        //    if (node.Name.IndexOf('#') > -1)
        //    {
        //        return;
        //    }

        //    if (parentXPath != String.Empty)
        //    {
        //        parentXPath += "/" + node.Name;
        //    }
        //    else
        //    {
        //        parentXPath = node.Name + "/Body";
        //    }

        //    XmlNode ggg = vvv.SelectSingleNode(parentXPath);

        //    if (ggg != null)
        //    {
        //        if (node.ChildNodes.Count == 1)
        //        {
        //            if (node.ChildNodes[0].Name == "#text")
        //            {
        //                //if (node.InnerText != null)
        //                //{
        //                if (ggg.Attributes["value"] != null)
        //                {
        //                    ggg.Attributes["value"].Value = node.ChildNodes[0].Value;
        //                }
        //                else
        //                {
        //                    XmlAttribute xxx = vvv.CreateAttribute("value");
        //                    ggg.Attributes.Append(xxx);
        //                    ggg.Attributes["value"].Value = node.ChildNodes[0].Value;

        //                }


        //                // }
        //            }
        //        }
        //    }


        //    foreach (XmlNode node1 in node.ChildNodes)
        //    {
        //        Test(node1, parentXPath, ref vvv);
        //    }
        //}

        //private object locker=new object();


        //private void Test1(TreeNode treeNode, XmlDocument xmlDocument)
        //{
        //    lock (typeof(ArrayProperty))
        //    {
        //        //XmlElement node1 = null;

        //        if (treeNode.Tag != null)
        //        {
        //            string nodeType = treeNode.Tag.GetType().ToString();

        //            switch (nodeType)
        //            {
        //                case "IBS.Utilities.ASMWSTester.ArrayProperty":
        //                    //node1 = xmlDocument.CreateElement(((ArrayProperty)treeNode.Tag).Name);

        //                    string aa = XpathToPSMLogXpath(TreeNodeToXPath(treeNode.FullPath));
        //                    XmlNodeList nodes = xmlDocument.SelectNodes(aa);

        //                    int xxx = nodes.Count;


        //                    treeInput.SelectedNode = treeNode;


        //                    ArrayProperty arrayProperty = treeNode.Tag as ArrayProperty;

        //                    if (arrayProperty != null)
        //                    {
        //                        if (xxx == 0)
        //                        {
        //                            //arrayProperty.IsNull = true;
        //                            arrayProperty.Length = xxx;
        //                            return;
        //                        }
        //                        else
        //                        {
        //                            //arrayProperty.IsNull = false;


        //                            if (arrayProperty.Length != xxx)
        //                            {
        //                                arrayProperty.Length = xxx;
        //                                Test1(treeInput.Nodes[0], xmlDocument);

        //                                //propInput.SelectedGridItem.Value = xxx;
        //                                return;
        //                            }
        //                        }
        //                    }

        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            //node1 = xmlDocument.CreateElement(treeNode.Text);

        //            //if (treeNode.Text != null)
        //            //{
        //            //    xmlNode.AppendChild(node1);
        //            //}
        //        }

        //        foreach (TreeNode childnode in treeNode.Nodes)
        //        {
        //            Test1(childnode, xmlDocument);
        //        }
        //    }
        //}

        private string TreeNodeToXPath(string nodePath)
        {
            StringBuilder result = new StringBuilder();

            if (nodePath != null)
            {
                if (nodePath != String.Empty)
                {
                    string[] strTemp = nodePath.Split('\\');

                    for (int i = 0; i < strTemp.Length; i++)
                    {
                        if (strTemp[i].IndexOf(" ") > -1)
                        {
                            strTemp[i] = strTemp[i].Split(' ')[1];
                        }
                    }

                    for (int i = 0; i < strTemp.Length; i++)
                    {
                        result.Append(strTemp[i] + "/");
                    }
                }
            }

            return result.ToString().Substring(0, result.ToString().Length - 1);
        }

        //private string XpathToPSMLogXpath(string xpath)
        //{
        //    return xpath.Replace("Body/", "");
        //}

        private void cmenuMethodTree_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Find")
            {
                FindForm findForm = new FindForm();
                findForm.ShowDialog();

                if (findForm.DialogResult == DialogResult.OK)
                {
                    if (treeMethods.Nodes != null && treeMethods.Nodes.Count > 0)
                        SelectNode(treeMethods, treeMethods.Nodes[0], findForm.FindValue);
                }
            }
        }

        private void cmenuMethodTree_Opening(object sender, CancelEventArgs e)
        {
            cmenuMethodTree.Items["itemFind2"].Enabled = true;

            if (treeMethods.Nodes.Count < 1)
            {
                cmenuMethodTree.Items["itemFind2"].Enabled = false;
            }
        }

        private void ServerCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.SetWSDL();
            cmbVersion.Text = "";
            cmbVersion.Items.Clear();
            cmbModule.Text = "";
            cmbModule.Items.Clear();

            if (AutoPopulateChk.Checked)
            {
                AsyncInitializeDropdowns();
            }
        }

        private void chkCollapse_CheckedChanged(object sender, EventArgs e)
        {
            if (this.treeOutput.Nodes != null && this.treeOutput.Nodes.Count > 0)
            {
                if (this.chkCollapse.Checked)
                    this.treeOutput.CollapseAll();
                else
                    this.treeOutput.ExpandAll();
            }
        }


        private void toggleExpandCollapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeOutput.SelectedNode != null)
            {
                if (treeOutput.SelectedNode.IsExpanded)
                    treeOutput.SelectedNode.Collapse();
                else
                    treeOutput.SelectedNode.ExpandAll();
            }
        }

        private void treeOutput_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = treeOutput.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    treeOutput.SelectedNode = tn;
                }
            }
        }

        #region Search

        static SearchStatus treeSearchStatus = new SearchStatus();

        private static TreeNode SelectNode(TreeView tv, TreeNode tn, string searchText)
        {
            if (tv == null || tn == null || string.IsNullOrEmpty(searchText))
                return null;

            TreeNode tnFound = null;

            if (tn != treeSearchStatus.LastestMatchNode)
            {
                if (tn.Text.IndexOf(searchText, StringComparison.InvariantCultureIgnoreCase) != -1)
                    tnFound = tn;
            }

            if (tnFound == null)
            {
                // Find the decedant
                foreach (TreeNode child in tn.Nodes)
                {
                    tnFound = FindNode(child, searchText);
                    if (tnFound != null)
                        break;
                }
            }

            if (tnFound == null)
            {
                // Find the Sibling Node's decedant        
                TreeNode current = tn.NextNode;
                while (current != null)
                {
                    tnFound = FindNode(current, searchText);
                    if (tnFound != null)
                        break;

                    if (current != null && current.Nodes != null)
                    {
                        tnFound = FindNode(current, searchText);
                    }

                    current = current.NextNode;
                }
            }

            // Find parent's Sibling
            if (tnFound == null && tn.Parent != null && tn.Parent.NextNode != null)
            {
                tnFound = FindNode(tn.Parent.NextNode, searchText);
            }

            if (tnFound != null)
            {
                tv.SelectedNode = tnFound;
            }

            treeSearchStatus.UpdateStatus(tv, tnFound, searchText, tnFound != null);

            return tnFound;
        }

        #region old
        //private static TreeNode tnLatestMatchNode = null;
        //private static bool nextSearchFind = false;
        //private static TreeView tvLatestTreeViewToSearch = null;
        //private static String latestSearchText;

        //private static bool NewSearchForTree(TreeView treeview, TreeNode node, string name)
        //{
        //    tvLatestTreeViewToSearch = treeview;
        //    latestSearchText = name;

        //    tnLatestMatchNode = null;

        //    SelectNode(treeview, node, name);

        //    return tnLatestMatchNode != null;
        //}

        //private static void SelectNode(TreeView treeview, TreeNode node, string name)
        //{
        //    if (String.IsNullOrEmpty(name))
        //        return;

        //    if (node.Text.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1)
        //    {
        //        treeview.SelectedNode = node;
        //        tnLatestMatchNode = node;
        //        return;
        //    }

        //    foreach (TreeNode childNode in node.Nodes)
        //    {
        //        if (tnLatestMatchNode != null)
        //            break;
        //        SelectNode(treeview, childNode, name);
        //    }
        //}

        //private static bool NextSearchForTree(TreeView treeview, TreeNode node, string name)
        //{
        //    //tnLatestMatchNode = null;

        //    nextSearchFind = false;
        //    NextSearch(treeview, tnLatestMatchNode, name);

        //    return tnLatestMatchNode != null;
        //}

        //private static void NextSearch(TreeView treeView, TreeNode currentFindNode, string name)
        //{
        //    if (String.IsNullOrEmpty(name) || currentFindNode == null) //|| currentFindNode.NextNode == null)
        //        return;


        //    foreach (TreeNode child in currentFindNode.Nodes)
        //    {
        //        if (child.Text.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1)
        //        {
        //            treeView.SelectedNode = child;
        //            tnLatestMatchNode = child;
        //            nextSearchFind = true;
        //            return;
        //        }

        //        NextSearch(treeView, child, name);
        //    }

        //    TreeNode tnStart = currentFindNode.NextNode;
        //    if (tnStart == null)
        //        return;

        //    if (tnStart.Text.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) > -1)
        //    {
        //        treeView.SelectedNode = tnStart;
        //        tnLatestMatchNode = tnStart;
        //        nextSearchFind = true;
        //        return;
        //    }

        //    foreach (TreeNode childNode in tnStart.Nodes)
        //    {
        //        if (tnLatestMatchNode != null && nextSearchFind)
        //            break;

        //        NextSearch(treeView, childNode, name);
        //    }

        //    NextSearch(treeView, tnStart, name);

        //}
        #endregion

        private static TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;

            if (tnParent.Text.IndexOf(strValue, StringComparison.InvariantCultureIgnoreCase) != -1)
                return tnParent;
            else if (tnParent.Nodes.Count == 0)
                return null;

            TreeNode tnCurrent = null, tnCurrentPar = null;
            //Init node
            tnCurrentPar = tnParent;
            tnCurrent = tnCurrentPar.FirstNode;

            while (tnCurrent != null && tnCurrent != tnParent)
            {
                while (tnCurrent != null)
                {
                    if (tnCurrent.Text.IndexOf(strValue, StringComparison.InvariantCultureIgnoreCase) != -1)
                    {
                        return tnCurrent;
                    }
                    else if (tnCurrent.Nodes.Count > 0)
                    {
                        //Go into the deepest node in current sub-path
                        tnCurrentPar = tnCurrent;
                        tnCurrent = tnCurrent.FirstNode;
                    }
                    else if (tnCurrent != tnCurrentPar.LastNode)
                    {
                        //Goto next sible node 
                        tnCurrent = tnCurrent.NextNode;
                    }
                    else
                        break;
                }
                //Go back to parent node till its has next sible node
                while (tnCurrent != tnParent && tnCurrent == tnCurrentPar.LastNode)
                {
                    tnCurrent = tnCurrentPar;
                    tnCurrentPar = tnCurrentPar.Parent;
                }

                //Goto next sible node
                if (tnCurrent != tnParent)
                    tnCurrent = tnCurrent.NextNode;
            }

            return null;
        }

        #endregion

        private void simplifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.simplifiedToolStripMenuItem.Checked = true;
            this.advancedToolStripMenuItem.Checked = !this.simplifiedToolStripMenuItem.Checked;

            this.pnlAdvancedView.Visible = false;
            this.pnlSimplifiedView.Visible = true;
            this.pnlSimplifiedView.Parent = this.panelTopMain;
            this.pnlSimplifiedView.Location = new Point(3, 27);
            this.pnlSimplifiedView.BorderStyle = BorderStyle.FixedSingle;
            this.panelTopMain.Height = 65;
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.advancedToolStripMenuItem.Checked = true;
            this.simplifiedToolStripMenuItem.Checked = !this.advancedToolStripMenuItem.Checked;

            this.pnlAdvancedView.Visible = true;
            this.pnlSimplifiedView.Visible = true;
            this.pnlSimplifiedView.Parent = pnlAdvancedView;
            this.pnlSimplifiedView.Location = new Point(3, 90);
            this.pnlSimplifiedView.BorderStyle = BorderStyle.None;

            this.panelTopMain.Height = 155;
        }

        private void hideSpefifiedFieldForOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


    }

    class SearchStatus
    {
        private TreeView latestTree;
        public TreeView LatestTree
        {
            get { return latestTree; }
            set { latestTree = value; }
        }

        private TreeNode lastestMatchNode;
        public TreeNode LastestMatchNode
        {
            get { return lastestMatchNode; }
            set { lastestMatchNode = value; }
        }

        private String latestSearchText;
        public String LatestSearchText
        {
            get { return latestSearchText; }
            set { latestSearchText = value; }
        }

        private bool isFound;
        public bool IsFound
        {
            get { return isFound; }
            set { isFound = value; }
        }

        public bool CanDoFindNext
        {
            get
            {
                return latestTree != null && lastestMatchNode != null && !String.IsNullOrEmpty(latestSearchText) && isFound;
            }
        }

        public void UpdateStatus(TreeView tv, TreeNode tnLatestMatch, String latestSearchText, bool isFound)
        {
            this.latestTree = tv;
            this.lastestMatchNode = tnLatestMatch;
            this.latestSearchText = latestSearchText;
            this.isFound = isFound;
        }
    }
}