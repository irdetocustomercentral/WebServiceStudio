using System.Windows.Forms;
using System.Drawing;
using System;

namespace IBS.Utilities.ASMWSTester
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem itemCopyAsmIssue;
            System.Windows.Forms.ToolStripMenuItem itemFind2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textEndPointUri = new System.Windows.Forms.ComboBox();
            this.buttonGet = new System.Windows.Forms.Button();
            this.labelEndPointUrl = new System.Windows.Forms.Label();
            this.menuItemTreeOutputCopy = new System.Windows.Forms.MenuItem();
            this.menuItemTreeInputCopy = new System.Windows.Forms.MenuItem();
            this.menuItemTreeInputPaste = new System.Windows.Forms.MenuItem();
            this.openWsdlDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.saveAllDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabPageInvoke = new System.Windows.Forms.TabPage();
            this.splitterInvoke = new System.Windows.Forms.Splitter();
            this.panelRightInvoke = new System.Windows.Forms.Panel();
            this.splitterInvokeMain = new System.Windows.Forms.SplitContainer();
            this.splitterInput = new System.Windows.Forms.SplitContainer();
            this.chkAutoSetSpecified = new System.Windows.Forms.CheckBox();
            this.treeInput = new System.Windows.Forms.TreeView();
            this.cmenuInputTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSaveInput = new System.Windows.Forms.ToolStripMenuItem();
            this.itemLoadInput = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFind1 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemLoadValueField = new System.Windows.Forms.ToolStripMenuItem();
            this.labelInput = new System.Windows.Forms.Label();
            this.buttonInvoke = new System.Windows.Forms.Button();
            this.txtInvokeTimes = new System.Windows.Forms.TextBox();
            this.labelInputValue = new System.Windows.Forms.Label();
            this.propInput = new System.Windows.Forms.PropertyGrid();
            this.splitterOutput = new System.Windows.Forms.SplitContainer();
            this.chkCollapse = new System.Windows.Forms.CheckBox();
            this.labelOutput = new System.Windows.Forms.Label();
            this.treeOutput = new System.Windows.Forms.TreeView();
            this.cmenuOutputTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemSaveVauleField = new System.Windows.Forms.ToolStripMenuItem();
            this.itemSaveOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleExpandCollapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propOutput = new System.Windows.Forms.PropertyGrid();
            this.labelOutputValue = new System.Windows.Forms.Label();
            this.panelLeftInvoke = new System.Windows.Forms.Panel();
            this.treeMethods = new System.Windows.Forms.TreeView();
            this.cmenuMethodTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabPageWsdl = new System.Windows.Forms.TabPage();
            this.splitterWsdl = new System.Windows.Forms.Splitter();
            this.panelRightWsdl = new System.Windows.Forms.Panel();
            this.richWsdl = new System.Windows.Forms.RichTextBox();
            this.cmenuRichWsdl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelLeftWsdl = new System.Windows.Forms.Panel();
            this.treeWsdl = new System.Windows.Forms.TreeView();
            this.tabPageMessage = new System.Windows.Forms.TabPage();
            this.richMessage = new System.Windows.Forms.RichTextBox();
            this.tabPageRaw = new System.Windows.Forms.TabPage();
            this.splitterRaw = new System.Windows.Forms.Splitter();
            this.panelRightRaw = new System.Windows.Forms.Panel();
            this.buttonSend = new System.Windows.Forms.Button();
            this.richRequest = new System.Windows.Forms.RichTextBox();
            this.richResponse = new System.Windows.Forms.RichTextBox();
            this.labelRequest = new System.Windows.Forms.Label();
            this.labelResponse = new System.Windows.Forms.Label();
            this.panelLeftRaw = new System.Windows.Forms.Panel();
            this.propRequest = new System.Windows.Forms.PropertyGrid();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageBatchConfig = new System.Windows.Forms.TabPage();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSave1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelTopMain = new System.Windows.Forms.Panel();
            this.pnlSimplifiedView = new System.Windows.Forms.Panel();
            this.pnlAdvancedView = new System.Windows.Forms.GroupBox();
            this.AutoPopulateChk = new System.Windows.Forms.CheckBox();
            this.ServerCBox = new System.Windows.Forms.ComboBox();
            this.AutoGetChk = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBatchUpdateLocalAssembly = new System.Windows.Forms.Button();
            this.btnAddBatchUpdateAssemblyUri = new System.Windows.Forms.Button();
            this.btnUpdateLocalAssembly = new System.Windows.Forms.Button();
            this.chkLocalAssembly = new System.Windows.Forms.CheckBox();
            this.btnPopulate = new System.Windows.Forms.Button();
            this.cmbModule = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simplifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.hideSpecifiedFieldForOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottomMain = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            itemCopyAsmIssue = new System.Windows.Forms.ToolStripMenuItem();
            itemFind2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageInvoke.SuspendLayout();
            this.panelRightInvoke.SuspendLayout();
            this.splitterInvokeMain.Panel1.SuspendLayout();
            this.splitterInvokeMain.Panel2.SuspendLayout();
            this.splitterInvokeMain.SuspendLayout();
            this.splitterInput.Panel1.SuspendLayout();
            this.splitterInput.Panel2.SuspendLayout();
            this.splitterInput.SuspendLayout();
            this.cmenuInputTree.SuspendLayout();
            this.splitterOutput.Panel1.SuspendLayout();
            this.splitterOutput.Panel2.SuspendLayout();
            this.splitterOutput.SuspendLayout();
            this.cmenuOutputTree.SuspendLayout();
            this.panelLeftInvoke.SuspendLayout();
            this.cmenuMethodTree.SuspendLayout();
            this.tabPageWsdl.SuspendLayout();
            this.panelRightWsdl.SuspendLayout();
            this.cmenuRichWsdl.SuspendLayout();
            this.panelLeftWsdl.SuspendLayout();
            this.tabPageMessage.SuspendLayout();
            this.tabPageRaw.SuspendLayout();
            this.panelRightRaw.SuspendLayout();
            this.panelLeftRaw.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPageBatchConfig.SuspendLayout();
            this.panelTopMain.SuspendLayout();
            this.pnlSimplifiedView.SuspendLayout();
            this.pnlAdvancedView.SuspendLayout();
            this.msMain.SuspendLayout();
            this.panelBottomMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemCopyAsmIssue
            // 
            itemCopyAsmIssue.Name = "itemCopyAsmIssue";
            itemCopyAsmIssue.Size = new System.Drawing.Size(145, 22);
            itemCopyAsmIssue.Text = "CopyAsmIssue";
            // 
            // itemFind2
            // 
            itemFind2.Name = "itemFind2";
            itemFind2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            itemFind2.Size = new System.Drawing.Size(132, 22);
            itemFind2.Text = "Find";
            // 
            // textEndPointUri
            // 
            this.textEndPointUri.Location = new System.Drawing.Point(109, 6);
            this.textEndPointUri.Name = "textEndPointUri";
            this.textEndPointUri.Size = new System.Drawing.Size(474, 21);
            this.textEndPointUri.TabIndex = 4;
            this.textEndPointUri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEndPointUri_KeyPress);
            // 
            // buttonGet
            // 
            this.buttonGet.Location = new System.Drawing.Point(609, 3);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(70, 24);
            this.buttonGet.TabIndex = 7;
            this.buttonGet.Text = "Get";
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // labelEndPointUrl
            // 
            this.labelEndPointUrl.Location = new System.Drawing.Point(15, 9);
            this.labelEndPointUrl.Name = "labelEndPointUrl";
            this.labelEndPointUrl.Size = new System.Drawing.Size(90, 16);
            this.labelEndPointUrl.TabIndex = 0;
            this.labelEndPointUrl.Text = "WSDL EndPoint:";
            // 
            // menuItemTreeOutputCopy
            // 
            this.menuItemTreeOutputCopy.Index = -1;
            this.menuItemTreeOutputCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItemTreeOutputCopy.Text = "Copy";
            this.menuItemTreeOutputCopy.Click += new System.EventHandler(this.treeOutputMenuCopy_Click);
            // 
            // menuItemTreeInputCopy
            // 
            this.menuItemTreeInputCopy.Index = -1;
            this.menuItemTreeInputCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItemTreeInputCopy.Text = "Copy";
            this.menuItemTreeInputCopy.Click += new System.EventHandler(this.treeInputMenuCopy_Click);
            // 
            // menuItemTreeInputPaste
            // 
            this.menuItemTreeInputPaste.Index = -1;
            this.menuItemTreeInputPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.menuItemTreeInputPaste.Text = "Paste";
            this.menuItemTreeInputPaste.Click += new System.EventHandler(this.treeInputMenuPaste_Click);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Text = "Open Wsdl...";
            this.toolBarButton1.ToolTipText = "Open WSDL file(s)";
            // 
            // tabPageInvoke
            // 
            this.tabPageInvoke.Controls.Add(this.splitterInvoke);
            this.tabPageInvoke.Controls.Add(this.panelRightInvoke);
            this.tabPageInvoke.Controls.Add(this.panelLeftInvoke);
            this.tabPageInvoke.Location = new System.Drawing.Point(4, 22);
            this.tabPageInvoke.Name = "tabPageInvoke";
            this.tabPageInvoke.Size = new System.Drawing.Size(944, 482);
            this.tabPageInvoke.TabIndex = 0;
            this.tabPageInvoke.Tag = "";
            this.tabPageInvoke.Text = "Invoke";
            this.tabPageInvoke.UseVisualStyleBackColor = true;
            // 
            // splitterInvoke
            // 
            this.splitterInvoke.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterInvoke.Location = new System.Drawing.Point(208, 0);
            this.splitterInvoke.Name = "splitterInvoke";
            this.splitterInvoke.Size = new System.Drawing.Size(3, 482);
            this.splitterInvoke.TabIndex = 0;
            this.splitterInvoke.TabStop = false;
            // 
            // panelRightInvoke
            // 
            this.panelRightInvoke.Controls.Add(this.splitterInvokeMain);
            this.panelRightInvoke.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightInvoke.Location = new System.Drawing.Point(208, 0);
            this.panelRightInvoke.Name = "panelRightInvoke";
            this.panelRightInvoke.Size = new System.Drawing.Size(736, 482);
            this.panelRightInvoke.TabIndex = 1;
            // 
            // splitterInvokeMain
            // 
            this.splitterInvokeMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterInvokeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterInvokeMain.Location = new System.Drawing.Point(0, 0);
            this.splitterInvokeMain.Name = "splitterInvokeMain";
            this.splitterInvokeMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitterInvokeMain.Panel1
            // 
            this.splitterInvokeMain.Panel1.Controls.Add(this.splitterInput);
            // 
            // splitterInvokeMain.Panel2
            // 
            this.splitterInvokeMain.Panel2.Controls.Add(this.splitterOutput);
            this.splitterInvokeMain.Size = new System.Drawing.Size(736, 482);
            this.splitterInvokeMain.SplitterDistance = 236;
            this.splitterInvokeMain.TabIndex = 6;
            // 
            // splitterInput
            // 
            this.splitterInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitterInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterInput.Location = new System.Drawing.Point(0, 0);
            this.splitterInput.Name = "splitterInput";
            // 
            // splitterInput.Panel1
            // 
            this.splitterInput.Panel1.Controls.Add(this.chkAutoSetSpecified);
            this.splitterInput.Panel1.Controls.Add(this.treeInput);
            this.splitterInput.Panel1.Controls.Add(this.labelInput);
            // 
            // splitterInput.Panel2
            // 
            this.splitterInput.Panel2.Controls.Add(this.buttonInvoke);
            this.splitterInput.Panel2.Controls.Add(this.txtInvokeTimes);
            this.splitterInput.Panel2.Controls.Add(this.labelInputValue);
            this.splitterInput.Panel2.Controls.Add(this.propInput);
            this.splitterInput.Size = new System.Drawing.Size(732, 232);
            this.splitterInput.SplitterDistance = 372;
            this.splitterInput.TabIndex = 0;
            // 
            // chkAutoSetSpecified
            // 
            this.chkAutoSetSpecified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoSetSpecified.AutoSize = true;
            this.chkAutoSetSpecified.Location = new System.Drawing.Point(250, 8);
            this.chkAutoSetSpecified.Name = "chkAutoSetSpecified";
            this.chkAutoSetSpecified.Size = new System.Drawing.Size(114, 17);
            this.chkAutoSetSpecified.TabIndex = 4;
            this.chkAutoSetSpecified.Text = "Auto Set Specified";
            this.chkAutoSetSpecified.UseVisualStyleBackColor = true;
            // 
            // treeInput
            // 
            this.treeInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeInput.ContextMenuStrip = this.cmenuInputTree;
            this.treeInput.HideSelection = false;
            this.treeInput.Location = new System.Drawing.Point(0, 29);
            this.treeInput.Name = "treeInput";
            this.treeInput.Size = new System.Drawing.Size(365, 196);
            this.treeInput.TabIndex = 0;
            this.treeInput.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeInput_AfterSelect);
            // 
            // cmenuInputTree
            // 
            this.cmenuInputTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSaveInput,
            this.itemLoadInput,
            this.itemFind1,
            this.itemLoadValueField});
            this.cmenuInputTree.Name = "cmenuLoad";
            this.cmenuInputTree.Size = new System.Drawing.Size(163, 92);
            this.cmenuInputTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuInputTree_Opening);
            this.cmenuInputTree.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmenuInuptTree_ItemClicked);
            // 
            // itemSaveInput
            // 
            this.itemSaveInput.Name = "itemSaveInput";
            this.itemSaveInput.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itemSaveInput.Size = new System.Drawing.Size(162, 22);
            this.itemSaveInput.Text = "SaveInput";
            // 
            // itemLoadInput
            // 
            this.itemLoadInput.Name = "itemLoadInput";
            this.itemLoadInput.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.itemLoadInput.Size = new System.Drawing.Size(162, 22);
            this.itemLoadInput.Text = "LoadInput";
            // 
            // itemFind1
            // 
            this.itemFind1.Name = "itemFind1";
            this.itemFind1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itemFind1.Size = new System.Drawing.Size(162, 22);
            this.itemFind1.Text = "Find";
            // 
            // itemLoadValueField
            // 
            this.itemLoadValueField.Name = "itemLoadValueField";
            this.itemLoadValueField.Size = new System.Drawing.Size(162, 22);
            this.itemLoadValueField.Text = "LoadValueField";
            // 
            // labelInput
            // 
            this.labelInput.Location = new System.Drawing.Point(5, 8);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(112, 16);
            this.labelInput.TabIndex = 3;
            this.labelInput.Text = "Input";
            // 
            // buttonInvoke
            // 
            this.buttonInvoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInvoke.Location = new System.Drawing.Point(287, 200);
            this.buttonInvoke.Name = "buttonInvoke";
            this.buttonInvoke.Size = new System.Drawing.Size(56, 20);
            this.buttonInvoke.TabIndex = 4;
            this.buttonInvoke.Text = "Invoke";
            this.buttonInvoke.Click += new System.EventHandler(this.buttonInvoke_Click);
            // 
            // txtInvokeTimes
            // 
            this.txtInvokeTimes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInvokeTimes.Location = new System.Drawing.Point(227, 201);
            this.txtInvokeTimes.Name = "txtInvokeTimes";
            this.txtInvokeTimes.Size = new System.Drawing.Size(43, 20);
            this.txtInvokeTimes.TabIndex = 5;
            this.txtInvokeTimes.Text = "1";
            // 
            // labelInputValue
            // 
            this.labelInputValue.Location = new System.Drawing.Point(3, 7);
            this.labelInputValue.Name = "labelInputValue";
            this.labelInputValue.Size = new System.Drawing.Size(56, 16);
            this.labelInputValue.TabIndex = 1;
            this.labelInputValue.Text = "Value";
            // 
            // propInput
            // 
            this.propInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propInput.HelpVisible = false;
            this.propInput.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propInput.Location = new System.Drawing.Point(3, 29);
            this.propInput.Name = "propInput";
            this.propInput.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propInput.Size = new System.Drawing.Size(349, 196);
            this.propInput.TabIndex = 1;
            this.propInput.ToolbarVisible = false;
            this.propInput.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propInput_PropertyValueChanged);
            // 
            // splitterOutput
            // 
            this.splitterOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitterOutput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterOutput.Location = new System.Drawing.Point(0, 0);
            this.splitterOutput.Name = "splitterOutput";
            // 
            // splitterOutput.Panel1
            // 
            this.splitterOutput.Panel1.Controls.Add(this.chkCollapse);
            this.splitterOutput.Panel1.Controls.Add(this.labelOutput);
            this.splitterOutput.Panel1.Controls.Add(this.treeOutput);
            // 
            // splitterOutput.Panel2
            // 
            this.splitterOutput.Panel2.Controls.Add(this.propOutput);
            this.splitterOutput.Panel2.Controls.Add(this.labelOutputValue);
            this.splitterOutput.Size = new System.Drawing.Size(732, 238);
            this.splitterOutput.SplitterDistance = 372;
            this.splitterOutput.TabIndex = 0;
            // 
            // chkCollapse
            // 
            this.chkCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCollapse.AutoSize = true;
            this.chkCollapse.Location = new System.Drawing.Point(250, 7);
            this.chkCollapse.Name = "chkCollapse";
            this.chkCollapse.Size = new System.Drawing.Size(80, 17);
            this.chkCollapse.TabIndex = 3;
            this.chkCollapse.Text = "Collapse All";
            this.chkCollapse.UseVisualStyleBackColor = true;
            this.chkCollapse.CheckedChanged += new System.EventHandler(this.chkCollapse_CheckedChanged);
            // 
            // labelOutput
            // 
            this.labelOutput.Location = new System.Drawing.Point(2, 7);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(64, 16);
            this.labelOutput.TabIndex = 2;
            this.labelOutput.Text = "Output";
            // 
            // treeOutput
            // 
            this.treeOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeOutput.ContextMenuStrip = this.cmenuOutputTree;
            this.treeOutput.Location = new System.Drawing.Point(0, 26);
            this.treeOutput.Name = "treeOutput";
            this.treeOutput.Size = new System.Drawing.Size(365, 205);
            this.treeOutput.TabIndex = 2;
            this.treeOutput.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeOutput_AfterSelect);
            this.treeOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeOutput_MouseClick);
            // 
            // cmenuOutputTree
            // 
            this.cmenuOutputTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSaveVauleField,
            this.itemSaveOutput,
            this.itemFind,
            this.toggleExpandCollapseToolStripMenuItem});
            this.cmenuOutputTree.Name = "cmenuSave";
            this.cmenuOutputTree.Size = new System.Drawing.Size(190, 92);
            this.cmenuOutputTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuOutputTree_Opening);
            this.cmenuOutputTree.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmenuOutputTree_ItemClicked);
            // 
            // itemSaveVauleField
            // 
            this.itemSaveVauleField.Name = "itemSaveVauleField";
            this.itemSaveVauleField.Size = new System.Drawing.Size(189, 22);
            this.itemSaveVauleField.Text = "SaveValueField";
            // 
            // itemSaveOutput
            // 
            this.itemSaveOutput.Name = "itemSaveOutput";
            this.itemSaveOutput.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itemSaveOutput.Size = new System.Drawing.Size(189, 22);
            this.itemSaveOutput.Text = "SaveOutput";
            // 
            // itemFind
            // 
            this.itemFind.Name = "itemFind";
            this.itemFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.itemFind.Size = new System.Drawing.Size(189, 22);
            this.itemFind.Text = "Find";
            // 
            // toggleExpandCollapseToolStripMenuItem
            // 
            this.toggleExpandCollapseToolStripMenuItem.Name = "toggleExpandCollapseToolStripMenuItem";
            this.toggleExpandCollapseToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.toggleExpandCollapseToolStripMenuItem.Text = "Toggle Expand/Collapse";
            this.toggleExpandCollapseToolStripMenuItem.Click += new System.EventHandler(this.toggleExpandCollapseToolStripMenuItem_Click);
            // 
            // propOutput
            // 
            this.propOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propOutput.HelpVisible = false;
            this.propOutput.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propOutput.Location = new System.Drawing.Point(3, 25);
            this.propOutput.Name = "propOutput";
            this.propOutput.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propOutput.Size = new System.Drawing.Size(346, 206);
            this.propOutput.TabIndex = 3;
            this.propOutput.ToolbarVisible = false;
            // 
            // labelOutputValue
            // 
            this.labelOutputValue.Location = new System.Drawing.Point(2, 6);
            this.labelOutputValue.Name = "labelOutputValue";
            this.labelOutputValue.Size = new System.Drawing.Size(56, 16);
            this.labelOutputValue.TabIndex = 0;
            this.labelOutputValue.Text = "Value";
            // 
            // panelLeftInvoke
            // 
            this.panelLeftInvoke.Controls.Add(this.treeMethods);
            this.panelLeftInvoke.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftInvoke.Location = new System.Drawing.Point(0, 0);
            this.panelLeftInvoke.Name = "panelLeftInvoke";
            this.panelLeftInvoke.Size = new System.Drawing.Size(208, 482);
            this.panelLeftInvoke.TabIndex = 2;
            // 
            // treeMethods
            // 
            this.treeMethods.ContextMenuStrip = this.cmenuMethodTree;
            this.treeMethods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMethods.HideSelection = false;
            this.treeMethods.Location = new System.Drawing.Point(0, 0);
            this.treeMethods.Name = "treeMethods";
            this.treeMethods.Size = new System.Drawing.Size(208, 482);
            this.treeMethods.TabIndex = 0;
            this.treeMethods.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMethods_AfterSelect);
            // 
            // cmenuMethodTree
            // 
            this.cmenuMethodTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            itemFind2});
            this.cmenuMethodTree.Name = "cmenuSave";
            this.cmenuMethodTree.Size = new System.Drawing.Size(133, 26);
            this.cmenuMethodTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmenuMethodTree_Opening);
            this.cmenuMethodTree.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmenuMethodTree_ItemClicked);
            // 
            // tabPageWsdl
            // 
            this.tabPageWsdl.Controls.Add(this.splitterWsdl);
            this.tabPageWsdl.Controls.Add(this.panelRightWsdl);
            this.tabPageWsdl.Controls.Add(this.panelLeftWsdl);
            this.tabPageWsdl.Location = new System.Drawing.Point(4, 22);
            this.tabPageWsdl.Name = "tabPageWsdl";
            this.tabPageWsdl.Size = new System.Drawing.Size(944, 482);
            this.tabPageWsdl.TabIndex = 2;
            this.tabPageWsdl.Tag = "";
            this.tabPageWsdl.Text = "WSDLs & Proxy";
            this.tabPageWsdl.UseVisualStyleBackColor = true;
            // 
            // splitterWsdl
            // 
            this.splitterWsdl.Location = new System.Drawing.Point(208, 0);
            this.splitterWsdl.Name = "splitterWsdl";
            this.splitterWsdl.Size = new System.Drawing.Size(3, 482);
            this.splitterWsdl.TabIndex = 0;
            this.splitterWsdl.TabStop = false;
            // 
            // panelRightWsdl
            // 
            this.panelRightWsdl.Controls.Add(this.richWsdl);
            this.panelRightWsdl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightWsdl.Location = new System.Drawing.Point(208, 0);
            this.panelRightWsdl.Name = "panelRightWsdl";
            this.panelRightWsdl.Size = new System.Drawing.Size(736, 482);
            this.panelRightWsdl.TabIndex = 1;
            // 
            // richWsdl
            // 
            this.richWsdl.ContextMenuStrip = this.cmenuRichWsdl;
            this.richWsdl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richWsdl.HideSelection = false;
            this.richWsdl.Location = new System.Drawing.Point(0, 0);
            this.richWsdl.Name = "richWsdl";
            this.richWsdl.ReadOnly = true;
            this.richWsdl.Size = new System.Drawing.Size(736, 482);
            this.richWsdl.TabIndex = 0;
            this.richWsdl.Text = "";
            this.richWsdl.WordWrap = false;
            // 
            // cmenuRichWsdl
            // 
            this.cmenuRichWsdl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            itemCopyAsmIssue});
            this.cmenuRichWsdl.Name = "cmenuSave";
            this.cmenuRichWsdl.Size = new System.Drawing.Size(146, 26);
            this.cmenuRichWsdl.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmenuRichWsdl_ItemClicked);
            // 
            // panelLeftWsdl
            // 
            this.panelLeftWsdl.Controls.Add(this.treeWsdl);
            this.panelLeftWsdl.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftWsdl.Location = new System.Drawing.Point(0, 0);
            this.panelLeftWsdl.Name = "panelLeftWsdl";
            this.panelLeftWsdl.Size = new System.Drawing.Size(208, 482);
            this.panelLeftWsdl.TabIndex = 2;
            // 
            // treeWsdl
            // 
            this.treeWsdl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeWsdl.Location = new System.Drawing.Point(0, 0);
            this.treeWsdl.Name = "treeWsdl";
            this.treeWsdl.Size = new System.Drawing.Size(208, 482);
            this.treeWsdl.TabIndex = 0;
            this.treeWsdl.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeWsdl_AfterSelect);
            // 
            // tabPageMessage
            // 
            this.tabPageMessage.Controls.Add(this.richMessage);
            this.tabPageMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessage.Name = "tabPageMessage";
            this.tabPageMessage.Size = new System.Drawing.Size(944, 482);
            this.tabPageMessage.TabIndex = 3;
            this.tabPageMessage.Tag = "";
            this.tabPageMessage.Text = "Messages";
            this.tabPageMessage.UseVisualStyleBackColor = true;
            // 
            // richMessage
            // 
            this.richMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richMessage.Location = new System.Drawing.Point(0, 0);
            this.richMessage.Name = "richMessage";
            this.richMessage.ReadOnly = true;
            this.richMessage.Size = new System.Drawing.Size(944, 482);
            this.richMessage.TabIndex = 0;
            this.richMessage.Text = "";
            // 
            // tabPageRaw
            // 
            this.tabPageRaw.Controls.Add(this.splitterRaw);
            this.tabPageRaw.Controls.Add(this.panelRightRaw);
            this.tabPageRaw.Controls.Add(this.panelLeftRaw);
            this.tabPageRaw.Location = new System.Drawing.Point(4, 22);
            this.tabPageRaw.Name = "tabPageRaw";
            this.tabPageRaw.Size = new System.Drawing.Size(944, 482);
            this.tabPageRaw.TabIndex = 1;
            this.tabPageRaw.Text = "Request/Response";
            this.tabPageRaw.UseVisualStyleBackColor = true;
            // 
            // splitterRaw
            // 
            this.splitterRaw.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterRaw.Location = new System.Drawing.Point(208, 0);
            this.splitterRaw.Name = "splitterRaw";
            this.splitterRaw.Size = new System.Drawing.Size(3, 482);
            this.splitterRaw.TabIndex = 0;
            this.splitterRaw.TabStop = false;
            // 
            // panelRightRaw
            // 
            this.panelRightRaw.Controls.Add(this.buttonSend);
            this.panelRightRaw.Controls.Add(this.richRequest);
            this.panelRightRaw.Controls.Add(this.richResponse);
            this.panelRightRaw.Controls.Add(this.labelRequest);
            this.panelRightRaw.Controls.Add(this.labelResponse);
            this.panelRightRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightRaw.Location = new System.Drawing.Point(208, 0);
            this.panelRightRaw.Name = "panelRightRaw";
            this.panelRightRaw.Size = new System.Drawing.Size(736, 482);
            this.panelRightRaw.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSend.Location = new System.Drawing.Point(843, 314);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(56, 24);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "Send";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // richRequest
            // 
            this.richRequest.Location = new System.Drawing.Point(9, 24);
            this.richRequest.Name = "richRequest";
            this.richRequest.Size = new System.Drawing.Size(890, 272);
            this.richRequest.TabIndex = 0;
            this.richRequest.Text = "";
            this.richRequest.WordWrap = false;
            // 
            // richResponse
            // 
            this.richResponse.Location = new System.Drawing.Point(12, 344);
            this.richResponse.Name = "richResponse";
            this.richResponse.ReadOnly = true;
            this.richResponse.Size = new System.Drawing.Size(887, 312);
            this.richResponse.TabIndex = 1;
            this.richResponse.Text = "";
            this.richResponse.WordWrap = false;
            // 
            // labelRequest
            // 
            this.labelRequest.Location = new System.Drawing.Point(9, 5);
            this.labelRequest.Name = "labelRequest";
            this.labelRequest.Size = new System.Drawing.Size(144, 16);
            this.labelRequest.TabIndex = 3;
            this.labelRequest.Text = "Request";
            // 
            // labelResponse
            // 
            this.labelResponse.Location = new System.Drawing.Point(9, 325);
            this.labelResponse.Name = "labelResponse";
            this.labelResponse.Size = new System.Drawing.Size(112, 16);
            this.labelResponse.TabIndex = 4;
            this.labelResponse.Text = "Response";
            // 
            // panelLeftRaw
            // 
            this.panelLeftRaw.Controls.Add(this.propRequest);
            this.panelLeftRaw.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftRaw.Location = new System.Drawing.Point(0, 0);
            this.panelLeftRaw.Name = "panelLeftRaw";
            this.panelLeftRaw.Size = new System.Drawing.Size(208, 482);
            this.panelLeftRaw.TabIndex = 2;
            this.panelLeftRaw.SizeChanged += new System.EventHandler(this.PanelLeftRaw_SizeChanged);
            // 
            // propRequest
            // 
            this.propRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propRequest.HelpVisible = false;
            this.propRequest.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propRequest.Location = new System.Drawing.Point(0, 0);
            this.propRequest.Name = "propRequest";
            this.propRequest.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propRequest.Size = new System.Drawing.Size(208, 482);
            this.propRequest.TabIndex = 0;
            this.propRequest.ToolbarVisible = false;
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabMain.Controls.Add(this.tabPageInvoke);
            this.tabMain.Controls.Add(this.tabPageRaw);
            this.tabMain.Controls.Add(this.tabPageWsdl);
            this.tabMain.Controls.Add(this.tabPageMessage);
            this.tabMain.Controls.Add(this.tabPageBatchConfig);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabMain.ItemSize = new System.Drawing.Size(42, 18);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(952, 508);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tabPageBatchConfig
            // 
            this.tabPageBatchConfig.Controls.Add(this.btnStart);
            this.tabPageBatchConfig.Controls.Add(this.btnSave1);
            this.tabPageBatchConfig.Controls.Add(this.textBox1);
            this.tabPageBatchConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageBatchConfig.Name = "tabPageBatchConfig";
            this.tabPageBatchConfig.Size = new System.Drawing.Size(944, 482);
            this.tabPageBatchConfig.TabIndex = 5;
            this.tabPageBatchConfig.Text = "BatchConfig";
            this.tabPageBatchConfig.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Location = new System.Drawing.Point(323, 612);
            this.btnStart.Name = "btnStart";
            this.btnStart.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnStart.Size = new System.Drawing.Size(60, 24);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSave1
            // 
            this.btnSave1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave1.Location = new System.Drawing.Point(675, 612);
            this.btnSave1.Name = "btnSave1";
            this.btnSave1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSave1.Size = new System.Drawing.Size(60, 24);
            this.btnSave1.TabIndex = 14;
            this.btnSave1.Text = "Save";
            this.btnSave1.Click += new System.EventHandler(this.btnSave1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1099, 587);
            this.textBox1.TabIndex = 1;
            // 
            // panelTopMain
            // 
            this.panelTopMain.Controls.Add(this.pnlSimplifiedView);
            this.panelTopMain.Controls.Add(this.pnlAdvancedView);
            this.panelTopMain.Controls.Add(this.msMain);
            this.panelTopMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopMain.Location = new System.Drawing.Point(0, 0);
            this.panelTopMain.Name = "panelTopMain";
            this.panelTopMain.Size = new System.Drawing.Size(952, 158);
            this.panelTopMain.TabIndex = 0;
            // 
            // pnlSimplifiedView
            // 
            this.pnlSimplifiedView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSimplifiedView.Controls.Add(this.buttonGet);
            this.pnlSimplifiedView.Controls.Add(this.textEndPointUri);
            this.pnlSimplifiedView.Controls.Add(this.labelEndPointUrl);
            this.pnlSimplifiedView.Location = new System.Drawing.Point(6, 116);
            this.pnlSimplifiedView.Name = "pnlSimplifiedView";
            this.pnlSimplifiedView.Size = new System.Drawing.Size(705, 34);
            this.pnlSimplifiedView.TabIndex = 19;
            // 
            // pnlAdvancedView
            // 
            this.pnlAdvancedView.Controls.Add(this.AutoPopulateChk);
            this.pnlAdvancedView.Controls.Add(this.ServerCBox);
            this.pnlAdvancedView.Controls.Add(this.AutoGetChk);
            this.pnlAdvancedView.Controls.Add(this.label2);
            this.pnlAdvancedView.Controls.Add(this.btnBatchUpdateLocalAssembly);
            this.pnlAdvancedView.Controls.Add(this.btnAddBatchUpdateAssemblyUri);
            this.pnlAdvancedView.Controls.Add(this.btnUpdateLocalAssembly);
            this.pnlAdvancedView.Controls.Add(this.chkLocalAssembly);
            this.pnlAdvancedView.Controls.Add(this.btnPopulate);
            this.pnlAdvancedView.Controls.Add(this.cmbModule);
            this.pnlAdvancedView.Controls.Add(this.label3);
            this.pnlAdvancedView.Controls.Add(this.cmbVersion);
            this.pnlAdvancedView.Controls.Add(this.label1);
            this.pnlAdvancedView.Location = new System.Drawing.Point(4, 25);
            this.pnlAdvancedView.Name = "pnlAdvancedView";
            this.pnlAdvancedView.Size = new System.Drawing.Size(942, 127);
            this.pnlAdvancedView.TabIndex = 18;
            this.pnlAdvancedView.TabStop = false;
            // 
            // AutoPopulateChk
            // 
            this.AutoPopulateChk.AutoSize = true;
            this.AutoPopulateChk.Location = new System.Drawing.Point(612, 45);
            this.AutoPopulateChk.Name = "AutoPopulateChk";
            this.AutoPopulateChk.Size = new System.Drawing.Size(93, 17);
            this.AutoPopulateChk.TabIndex = 29;
            this.AutoPopulateChk.Text = "Auto Populate";
            this.AutoPopulateChk.UseVisualStyleBackColor = true;
            // 
            // ServerCBox
            // 
            this.ServerCBox.FormattingEnabled = true;
            this.ServerCBox.Location = new System.Drawing.Point(112, 12);
            this.ServerCBox.Name = "ServerCBox";
            this.ServerCBox.Size = new System.Drawing.Size(474, 21);
            this.ServerCBox.TabIndex = 17;
            this.ServerCBox.SelectedIndexChanged += new System.EventHandler(this.ServerCBox_SelectedIndexChanged);
            // 
            // AutoGetChk
            // 
            this.AutoGetChk.AutoSize = true;
            this.AutoGetChk.Location = new System.Drawing.Point(612, 71);
            this.AutoGetChk.Name = "AutoGetChk";
            this.AutoGetChk.Size = new System.Drawing.Size(68, 17);
            this.AutoGetChk.TabIndex = 22;
            this.AutoGetChk.Text = "Auto Get";
            this.AutoGetChk.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "ASM Server:";
            // 
            // btnBatchUpdateLocalAssembly
            // 
            this.btnBatchUpdateLocalAssembly.Location = new System.Drawing.Point(720, 93);
            this.btnBatchUpdateLocalAssembly.Name = "btnBatchUpdateLocalAssembly";
            this.btnBatchUpdateLocalAssembly.Size = new System.Drawing.Size(207, 24);
            this.btnBatchUpdateLocalAssembly.TabIndex = 27;
            this.btnBatchUpdateLocalAssembly.Text = "Batch Update Local Assembly";
            this.btnBatchUpdateLocalAssembly.Click += new System.EventHandler(this.btnBatchUpdateLocalAssembly_Click);
            // 
            // btnAddBatchUpdateAssemblyUri
            // 
            this.btnAddBatchUpdateAssemblyUri.Location = new System.Drawing.Point(720, 61);
            this.btnAddBatchUpdateAssemblyUri.Name = "btnAddBatchUpdateAssemblyUri";
            this.btnAddBatchUpdateAssemblyUri.Size = new System.Drawing.Size(207, 24);
            this.btnAddBatchUpdateAssemblyUri.TabIndex = 26;
            this.btnAddBatchUpdateAssemblyUri.Text = "Add Batch Update Assembly Module";
            this.btnAddBatchUpdateAssemblyUri.Click += new System.EventHandler(this.btnAddBatchUpdateAssemblyUri_Click);
            // 
            // btnUpdateLocalAssembly
            // 
            this.btnUpdateLocalAssembly.Location = new System.Drawing.Point(720, 30);
            this.btnUpdateLocalAssembly.Name = "btnUpdateLocalAssembly";
            this.btnUpdateLocalAssembly.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnUpdateLocalAssembly.Size = new System.Drawing.Size(207, 24);
            this.btnUpdateLocalAssembly.TabIndex = 25;
            this.btnUpdateLocalAssembly.Text = "Update Local Assembly";
            this.btnUpdateLocalAssembly.Click += new System.EventHandler(this.btnUpdateLocalAssembly_Click);
            // 
            // chkLocalAssembly
            // 
            this.chkLocalAssembly.AutoSize = true;
            this.chkLocalAssembly.Location = new System.Drawing.Point(721, 12);
            this.chkLocalAssembly.Name = "chkLocalAssembly";
            this.chkLocalAssembly.Size = new System.Drawing.Size(121, 17);
            this.chkLocalAssembly.TabIndex = 23;
            this.chkLocalAssembly.Text = "Use Local Assembly";
            this.chkLocalAssembly.UseVisualStyleBackColor = true;
            // 
            // btnPopulate
            // 
            this.btnPopulate.Location = new System.Drawing.Point(612, 12);
            this.btnPopulate.Name = "btnPopulate";
            this.btnPopulate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPopulate.Size = new System.Drawing.Size(70, 24);
            this.btnPopulate.TabIndex = 18;
            this.btnPopulate.Text = "Populate";
            this.btnPopulate.Click += new System.EventHandler(this.btnPopulate_Click);
            // 
            // cmbModule
            // 
            this.cmbModule.FormattingEnabled = true;
            this.cmbModule.Location = new System.Drawing.Point(112, 68);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Size = new System.Drawing.Size(474, 21);
            this.cmbModule.TabIndex = 20;
            this.cmbModule.SelectedIndexChanged += new System.EventHandler(this.cmbModule_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Module:";
            // 
            // cmbVersion
            // 
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(112, 41);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(474, 21);
            this.cmbVersion.TabIndex = 19;
            this.cmbVersion.SelectedIndexChanged += new System.EventHandler(this.cmbVersion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Version:";
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(952, 24);
            this.msMain.TabIndex = 17;
            this.msMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAllFilesToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveAllFilesToolStripMenuItem
            // 
            this.saveAllFilesToolStripMenuItem.Name = "saveAllFilesToolStripMenuItem";
            this.saveAllFilesToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveAllFilesToolStripMenuItem.Text = "&Save All Files";
            this.saveAllFilesToolStripMenuItem.Click += new System.EventHandler(this.menuItemSaveAll_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(138, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simplifiedToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.toolStripMenuItem2,
            this.hideSpecifiedFieldForOutputToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // simplifiedToolStripMenuItem
            // 
            this.simplifiedToolStripMenuItem.CheckOnClick = true;
            this.simplifiedToolStripMenuItem.Name = "simplifiedToolStripMenuItem";
            this.simplifiedToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.simplifiedToolStripMenuItem.Text = "&Simplified";
            this.simplifiedToolStripMenuItem.Click += new System.EventHandler(this.simplifiedToolStripMenuItem_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.CheckOnClick = true;
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.advancedToolStripMenuItem.Text = "&Advanced";
            this.advancedToolStripMenuItem.Click += new System.EventHandler(this.advancedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(236, 6);
            // 
            // hideSpecifiedFieldForOutputToolStripMenuItem
            // 
            this.hideSpecifiedFieldForOutputToolStripMenuItem.CheckOnClick = true;
            this.hideSpecifiedFieldForOutputToolStripMenuItem.Name = "hideSpecifiedFieldForOutputToolStripMenuItem";
            this.hideSpecifiedFieldForOutputToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.hideSpecifiedFieldForOutputToolStripMenuItem.Text = "&Hide Specified Field For Output";
            this.hideSpecifiedFieldForOutputToolStripMenuItem.Click += new System.EventHandler(this.hideSpefifiedFieldForOutputToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.toolStripMenuItem3,
            this.optionsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.menuItemFind_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.findNextToolStripMenuItem.Text = "Find Next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.menuItemFindNext_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.menuItemHelp_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(104, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // panelBottomMain
            // 
            this.panelBottomMain.Controls.Add(this.tabMain);
            this.panelBottomMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomMain.Location = new System.Drawing.Point(0, 158);
            this.panelBottomMain.Name = "panelBottomMain";
            this.panelBottomMain.Size = new System.Drawing.Size(952, 508);
            this.panelBottomMain.TabIndex = 1;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "class.png");
            this.imgList.Images.SetKeyName(1, "method.png");
            this.imgList.Images.SetKeyName(2, "Property.png");
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(952, 666);
            this.Controls.Add(this.panelBottomMain);
            this.Controls.Add(this.panelTopMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Web Service Studio ";
            this.tabPageInvoke.ResumeLayout(false);
            this.panelRightInvoke.ResumeLayout(false);
            this.splitterInvokeMain.Panel1.ResumeLayout(false);
            this.splitterInvokeMain.Panel2.ResumeLayout(false);
            this.splitterInvokeMain.ResumeLayout(false);
            this.splitterInput.Panel1.ResumeLayout(false);
            this.splitterInput.Panel1.PerformLayout();
            this.splitterInput.Panel2.ResumeLayout(false);
            this.splitterInput.Panel2.PerformLayout();
            this.splitterInput.ResumeLayout(false);
            this.cmenuInputTree.ResumeLayout(false);
            this.splitterOutput.Panel1.ResumeLayout(false);
            this.splitterOutput.Panel1.PerformLayout();
            this.splitterOutput.Panel2.ResumeLayout(false);
            this.splitterOutput.ResumeLayout(false);
            this.cmenuOutputTree.ResumeLayout(false);
            this.panelLeftInvoke.ResumeLayout(false);
            this.cmenuMethodTree.ResumeLayout(false);
            this.tabPageWsdl.ResumeLayout(false);
            this.panelRightWsdl.ResumeLayout(false);
            this.cmenuRichWsdl.ResumeLayout(false);
            this.panelLeftWsdl.ResumeLayout(false);
            this.tabPageMessage.ResumeLayout(false);
            this.tabPageRaw.ResumeLayout(false);
            this.panelRightRaw.ResumeLayout(false);
            this.panelLeftRaw.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabPageBatchConfig.ResumeLayout(false);
            this.tabPageBatchConfig.PerformLayout();
            this.panelTopMain.ResumeLayout(false);
            this.panelTopMain.PerformLayout();
            this.pnlSimplifiedView.ResumeLayout(false);
            this.pnlAdvancedView.ResumeLayout(false);
            this.pnlAdvancedView.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.panelBottomMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private TabPage tabPageBatchConfig;
        private TextBox textBox1;
        private ContextMenuStrip cmenuOutputTree;
        private ToolStripMenuItem itemSaveVauleField;
        private ContextMenuStrip cmenuInputTree;
        private ToolStripMenuItem itemLoadValueField;
        private Button btnStart;
        private Button btnSave1;
        private ToolStripMenuItem itemSaveOutput;
        private ToolStripMenuItem itemSaveInput;
        private ToolStripMenuItem itemLoadInput;
        private ContextMenuStrip cmenuRichWsdl;
        private ToolStripMenuItem itemFind;
        private ToolStripMenuItem itemFind1;
        private ContextMenuStrip cmenuMethodTree;
        private TextBox txtInvokeTimes;
        private SplitContainer splitterInvokeMain;
        private SplitContainer splitterOutput;
        private SplitContainer splitterInput;
        private CheckBox chkCollapse;
        private ImageList imgList;
        private ToolStripMenuItem toggleExpandCollapseToolStripMenuItem;
        private CheckBox chkAutoSetSpecified;
        private MenuStrip msMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveAllFilesToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripMenuItem findNextToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox pnlAdvancedView;
        private CheckBox AutoPopulateChk;
        private ComboBox ServerCBox;
        private CheckBox AutoGetChk;
        private Label label2;
        private Button btnBatchUpdateLocalAssembly;
        internal Button btnAddBatchUpdateAssemblyUri;
        private Button btnUpdateLocalAssembly;
        internal CheckBox chkLocalAssembly;
        private Button btnPopulate;
        private ComboBox cmbModule;
        private Label label3;
        private ComboBox cmbVersion;
        private Label label1;
        private Panel pnlSimplifiedView;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem simplifiedToolStripMenuItem;
        private ToolStripMenuItem advancedToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem hideSpecifiedFieldForOutputToolStripMenuItem;
    }
}