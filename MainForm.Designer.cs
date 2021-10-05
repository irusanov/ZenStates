using ZenStates.Components;

namespace ZenStates
{
    partial class AppWindow
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
            //hMutexPci.Dispose();
            //ols.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cpuTabOC = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxPstates = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxCpuFreq = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.manualOverclockItem = new ZenStates.Components.ManualOverclockItem();
            this.tabGPU = new System.Windows.Forms.TabPage();
            this.tabPower = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownScalar = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEDC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTDC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPPT = new System.Windows.Forms.NumericUpDown();
            this.labelScalar = new System.Windows.Forms.Label();
            this.labelEDC = new System.Windows.Forms.Label();
            this.labelPPT = new System.Windows.Forms.Label();
            this.labelTDC = new System.Windows.Forms.Label();
            this.checkBoxCPB = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Core = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Package = new System.Windows.Forms.CheckBox();
            this.labelPerfEnh = new System.Windows.Forms.Label();
            this.comboBoxPerfEnh = new System.Windows.Forms.ComboBox();
            this.divider1 = new System.Windows.Forms.Label();
            this.tabTweaks = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxPerfBias = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxPerfBias = new System.Windows.Forms.ComboBox();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.microcodeInfoLabel = new System.Windows.Forms.Label();
            this.smuInfoLabel = new System.Windows.Forms.Label();
            this.biosInfoLabel = new System.Windows.Forms.Label();
            this.mbVendorInfoLabel = new System.Windows.Forms.Label();
            this.labelInfoCpu = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cpuInfoLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mbModelInfoLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cpuIdLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxQcode = new System.Windows.Forms.CheckBox();
            this.checkBoxMinToTray = new System.Windows.Forms.CheckBox();
            this.checkBoxStartMinimized = new System.Windows.Forms.CheckBox();
            this.checkBoxStartOnBoot = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.statusText = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayMenuItemApp = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.cpuTabOC.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxPstates.SuspendLayout();
            this.groupBoxCpuFreq.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabPower.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScalar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEDC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTDC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPPT)).BeginInit();
            this.tabTweaks.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.groupBoxPerfBias.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.trayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.cpuTabOC);
            this.tabControl1.Controls.Add(this.tabGPU);
            this.tabControl1.Controls.Add(this.tabPower);
            this.tabControl1.Controls.Add(this.tabTweaks);
            this.tabControl1.Controls.Add(this.tabInfo);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(299, 260);
            this.tabControl1.TabIndex = 1;
            // 
            // cpuTabOC
            // 
            this.cpuTabOC.Controls.Add(this.tableLayoutPanel2);
            this.cpuTabOC.Location = new System.Drawing.Point(4, 22);
            this.cpuTabOC.Name = "cpuTabOC";
            this.cpuTabOC.Padding = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.cpuTabOC.Size = new System.Drawing.Size(291, 234);
            this.cpuTabOC.TabIndex = 0;
            this.cpuTabOC.Text = "CPU";
            this.cpuTabOC.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBoxPstates, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxCpuFreq, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(283, 218);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBoxPstates
            // 
            this.groupBoxPstates.AutoSize = true;
            this.groupBoxPstates.Controls.Add(this.tableLayoutPanel3);
            this.groupBoxPstates.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPstates.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxPstates.Location = new System.Drawing.Point(3, 100);
            this.groupBoxPstates.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.groupBoxPstates.Name = "groupBoxPstates";
            this.groupBoxPstates.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxPstates.Size = new System.Drawing.Size(277, 21);
            this.groupBoxPstates.TabIndex = 2;
            this.groupBoxPstates.TabStop = false;
            this.groupBoxPstates.Text = "PStates";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 17);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(269, 0);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBoxCpuFreq
            // 
            this.groupBoxCpuFreq.AutoSize = true;
            this.groupBoxCpuFreq.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCpuFreq.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxCpuFreq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCpuFreq.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxCpuFreq.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCpuFreq.Name = "groupBoxCpuFreq";
            this.groupBoxCpuFreq.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxCpuFreq.Size = new System.Drawing.Size(277, 82);
            this.groupBoxCpuFreq.TabIndex = 1;
            this.groupBoxCpuFreq.TabStop = false;
            this.groupBoxCpuFreq.Text = "Manual Overclock";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.manualOverclockItem, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(269, 61);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // manualOverclockItem
            // 
            this.manualOverclockItem.CcxInCcd = 0;
            this.manualOverclockItem.coreDisableMap = ((uint)(0u));
            this.manualOverclockItem.Cores = 0;
            this.manualOverclockItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manualOverclockItem.Location = new System.Drawing.Point(3, 3);
            this.manualOverclockItem.Multi = 4D;
            this.manualOverclockItem.Name = "manualOverclockItem";
            this.manualOverclockItem.OCmode = false;
            this.manualOverclockItem.ProchotEnabled = false;
            this.manualOverclockItem.Size = new System.Drawing.Size(263, 55);
            this.manualOverclockItem.TabIndex = 0;
            this.manualOverclockItem.Vid = ((byte)(232));
            this.manualOverclockItem.SlowModeClicked += new System.EventHandler(this.ManualOverclockItem_SlowModeClicked);
            this.manualOverclockItem.ProchotClicked += new System.EventHandler(this.ManualOverclockItem_ProchotClicked);
            // 
            // tabGPU
            // 
            this.tabGPU.Location = new System.Drawing.Point(4, 22);
            this.tabGPU.Name = "tabGPU";
            this.tabGPU.Padding = new System.Windows.Forms.Padding(3);
            this.tabGPU.Size = new System.Drawing.Size(291, 234);
            this.tabGPU.TabIndex = 1;
            this.tabGPU.Text = "GPU";
            this.tabGPU.UseVisualStyleBackColor = true;
            // 
            // tabPower
            // 
            this.tabPower.Controls.Add(this.tableLayoutPanel9);
            this.tabPower.Location = new System.Drawing.Point(4, 22);
            this.tabPower.Name = "tabPower";
            this.tabPower.Padding = new System.Windows.Forms.Padding(3);
            this.tabPower.Size = new System.Drawing.Size(291, 234);
            this.tabPower.TabIndex = 2;
            this.tabPower.Text = "Power";
            this.tabPower.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.numericUpDownScalar, 1, 10);
            this.tableLayoutPanel9.Controls.Add(this.numericUpDownEDC, 1, 9);
            this.tableLayoutPanel9.Controls.Add(this.numericUpDownTDC, 1, 8);
            this.tableLayoutPanel9.Controls.Add(this.numericUpDownPPT, 1, 7);
            this.tableLayoutPanel9.Controls.Add(this.labelScalar, 0, 10);
            this.tableLayoutPanel9.Controls.Add(this.labelEDC, 0, 9);
            this.tableLayoutPanel9.Controls.Add(this.labelPPT, 0, 7);
            this.tableLayoutPanel9.Controls.Add(this.labelTDC, 0, 8);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxCPB, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxC6Core, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.checkBoxC6Package, 0, 3);
            this.tableLayoutPanel9.Controls.Add(this.labelPerfEnh, 0, 6);
            this.tableLayoutPanel9.Controls.Add(this.comboBoxPerfEnh, 1, 6);
            this.tableLayoutPanel9.Controls.Add(this.divider1, 0, 5);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel9.RowCount = 12;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(285, 228);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // numericUpDownScalar
            // 
            this.numericUpDownScalar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownScalar.DecimalPlaces = 2;
            this.numericUpDownScalar.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericUpDownScalar.Location = new System.Drawing.Point(129, 199);
            this.numericUpDownScalar.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownScalar.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownScalar.Name = "numericUpDownScalar";
            this.numericUpDownScalar.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownScalar.TabIndex = 7;
            this.numericUpDownScalar.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // numericUpDownEDC
            // 
            this.numericUpDownEDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownEDC.Location = new System.Drawing.Point(129, 173);
            this.numericUpDownEDC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownEDC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownEDC.Name = "numericUpDownEDC";
            this.numericUpDownEDC.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownEDC.TabIndex = 6;
            this.numericUpDownEDC.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // numericUpDownTDC
            // 
            this.numericUpDownTDC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownTDC.Location = new System.Drawing.Point(129, 147);
            this.numericUpDownTDC.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTDC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownTDC.Name = "numericUpDownTDC";
            this.numericUpDownTDC.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownTDC.TabIndex = 5;
            this.numericUpDownTDC.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // numericUpDownPPT
            // 
            this.numericUpDownPPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownPPT.Location = new System.Drawing.Point(129, 121);
            this.numericUpDownPPT.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownPPT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownPPT.Name = "numericUpDownPPT";
            this.numericUpDownPPT.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownPPT.TabIndex = 4;
            this.numericUpDownPPT.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // labelScalar
            // 
            this.labelScalar.AutoSize = true;
            this.labelScalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScalar.Location = new System.Drawing.Point(7, 196);
            this.labelScalar.Name = "labelScalar";
            this.labelScalar.Size = new System.Drawing.Size(116, 26);
            this.labelScalar.TabIndex = 3;
            this.labelScalar.Text = "Scalar";
            this.labelScalar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelEDC
            // 
            this.labelEDC.AutoSize = true;
            this.labelEDC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEDC.Location = new System.Drawing.Point(7, 170);
            this.labelEDC.Name = "labelEDC";
            this.labelEDC.Size = new System.Drawing.Size(116, 26);
            this.labelEDC.TabIndex = 1;
            this.labelEDC.Text = "EDC (A)";
            this.labelEDC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPPT
            // 
            this.labelPPT.AutoSize = true;
            this.labelPPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPPT.Location = new System.Drawing.Point(7, 118);
            this.labelPPT.Name = "labelPPT";
            this.labelPPT.Size = new System.Drawing.Size(116, 26);
            this.labelPPT.TabIndex = 0;
            this.labelPPT.Text = "PPT (W)";
            this.labelPPT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTDC
            // 
            this.labelTDC.AutoSize = true;
            this.labelTDC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTDC.Location = new System.Drawing.Point(7, 144);
            this.labelTDC.Name = "labelTDC";
            this.labelTDC.Size = new System.Drawing.Size(116, 26);
            this.labelTDC.TabIndex = 2;
            this.labelTDC.Text = "TDC (A)";
            this.labelTDC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxCPB
            // 
            this.checkBoxCPB.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this.checkBoxCPB, 2);
            this.checkBoxCPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxCPB.Location = new System.Drawing.Point(7, 15);
            this.checkBoxCPB.Name = "checkBoxCPB";
            this.checkBoxCPB.Size = new System.Drawing.Size(271, 17);
            this.checkBoxCPB.TabIndex = 0;
            this.checkBoxCPB.Text = "Core Performance Boost";
            this.checkBoxCPB.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Core
            // 
            this.checkBoxC6Core.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this.checkBoxC6Core, 2);
            this.checkBoxC6Core.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxC6Core.Location = new System.Drawing.Point(7, 38);
            this.checkBoxC6Core.Name = "checkBoxC6Core";
            this.checkBoxC6Core.Size = new System.Drawing.Size(271, 17);
            this.checkBoxC6Core.TabIndex = 2;
            this.checkBoxC6Core.Text = "Core C6-State";
            this.checkBoxC6Core.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Package
            // 
            this.checkBoxC6Package.AutoSize = true;
            this.tableLayoutPanel9.SetColumnSpan(this.checkBoxC6Package, 2);
            this.checkBoxC6Package.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxC6Package.Location = new System.Drawing.Point(7, 61);
            this.checkBoxC6Package.Name = "checkBoxC6Package";
            this.checkBoxC6Package.Size = new System.Drawing.Size(271, 17);
            this.checkBoxC6Package.TabIndex = 3;
            this.checkBoxC6Package.Text = "Package C6-State";
            this.checkBoxC6Package.UseVisualStyleBackColor = true;
            // 
            // labelPerfEnh
            // 
            this.labelPerfEnh.AutoSize = true;
            this.labelPerfEnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPerfEnh.Location = new System.Drawing.Point(7, 91);
            this.labelPerfEnh.Name = "labelPerfEnh";
            this.labelPerfEnh.Size = new System.Drawing.Size(116, 27);
            this.labelPerfEnh.TabIndex = 9;
            this.labelPerfEnh.Text = "Performance Enhancer";
            this.labelPerfEnh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxPerfEnh
            // 
            this.comboBoxPerfEnh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPerfEnh.FormattingEnabled = true;
            this.comboBoxPerfEnh.Location = new System.Drawing.Point(129, 94);
            this.comboBoxPerfEnh.Name = "comboBoxPerfEnh";
            this.comboBoxPerfEnh.Size = new System.Drawing.Size(141, 21);
            this.comboBoxPerfEnh.TabIndex = 8;
            // 
            // divider1
            // 
            this.divider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel9.SetColumnSpan(this.divider1, 2);
            this.divider1.Location = new System.Drawing.Point(8, 86);
            this.divider1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.divider1.Name = "divider1";
            this.divider1.Size = new System.Drawing.Size(265, 2);
            this.divider1.TabIndex = 10;
            // 
            // tabTweaks
            // 
            this.tabTweaks.Controls.Add(this.tableLayoutPanel8);
            this.tabTweaks.Location = new System.Drawing.Point(4, 22);
            this.tabTweaks.Name = "tabTweaks";
            this.tabTweaks.Padding = new System.Windows.Forms.Padding(4, 8, 4, 8);
            this.tabTweaks.Size = new System.Drawing.Size(291, 234);
            this.tabTweaks.TabIndex = 3;
            this.tabTweaks.Text = "Tweaks";
            this.tabTweaks.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.groupBoxPerfBias, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(4, 8);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(283, 218);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // groupBoxPerfBias
            // 
            this.groupBoxPerfBias.AutoSize = true;
            this.groupBoxPerfBias.Controls.Add(this.tableLayoutPanel7);
            this.groupBoxPerfBias.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPerfBias.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPerfBias.Name = "groupBoxPerfBias";
            this.groupBoxPerfBias.Size = new System.Drawing.Size(277, 46);
            this.groupBoxPerfBias.TabIndex = 1;
            this.groupBoxPerfBias.TabStop = false;
            this.groupBoxPerfBias.Text = "Performance Bias";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.comboBoxPerfBias, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(271, 27);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 27);
            this.label8.TabIndex = 0;
            this.label8.Text = "Profile";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxPerfBias
            // 
            this.comboBoxPerfBias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPerfBias.FormattingEnabled = true;
            this.comboBoxPerfBias.Location = new System.Drawing.Point(45, 3);
            this.comboBoxPerfBias.Name = "comboBoxPerfBias";
            this.comboBoxPerfBias.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPerfBias.TabIndex = 1;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tableLayoutPanel5);
            this.tabInfo.Location = new System.Drawing.Point(4, 22);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabInfo.Size = new System.Drawing.Size(291, 234);
            this.tabInfo.TabIndex = 4;
            this.tabInfo.Text = "Info";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.microcodeInfoLabel, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.smuInfoLabel, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.biosInfoLabel, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.mbVendorInfoLabel, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.labelInfoCpu, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.cpuInfoLabel, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.mbModelInfoLabel, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.cpuIdLabel, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.tableLayoutPanel5.RowCount = 8;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(285, 228);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // microcodeInfoLabel
            // 
            this.microcodeInfoLabel.AutoEllipsis = true;
            this.microcodeInfoLabel.AutoSize = true;
            this.microcodeInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.microcodeInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.microcodeInfoLabel.Location = new System.Drawing.Point(71, 143);
            this.microcodeInfoLabel.Name = "microcodeInfoLabel";
            this.microcodeInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.microcodeInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.microcodeInfoLabel.TabIndex = 13;
            this.microcodeInfoLabel.Text = "-";
            this.microcodeInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // smuInfoLabel
            // 
            this.smuInfoLabel.AutoEllipsis = true;
            this.smuInfoLabel.AutoSize = true;
            this.smuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smuInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smuInfoLabel.Location = new System.Drawing.Point(71, 120);
            this.smuInfoLabel.Name = "smuInfoLabel";
            this.smuInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.smuInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.smuInfoLabel.TabIndex = 7;
            this.smuInfoLabel.Text = "-";
            this.smuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // biosInfoLabel
            // 
            this.biosInfoLabel.AutoEllipsis = true;
            this.biosInfoLabel.AutoSize = true;
            this.biosInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biosInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.biosInfoLabel.Location = new System.Drawing.Point(71, 97);
            this.biosInfoLabel.Name = "biosInfoLabel";
            this.biosInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.biosInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.biosInfoLabel.TabIndex = 6;
            this.biosInfoLabel.Text = "-";
            this.biosInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbVendorInfoLabel
            // 
            this.mbVendorInfoLabel.AutoEllipsis = true;
            this.mbVendorInfoLabel.AutoSize = true;
            this.mbVendorInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbVendorInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbVendorInfoLabel.Location = new System.Drawing.Point(71, 51);
            this.mbVendorInfoLabel.Name = "mbVendorInfoLabel";
            this.mbVendorInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbVendorInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.mbVendorInfoLabel.TabIndex = 5;
            this.mbVendorInfoLabel.Text = "-";
            this.mbVendorInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInfoCpu
            // 
            this.labelInfoCpu.AutoSize = true;
            this.labelInfoCpu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfoCpu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoCpu.Location = new System.Drawing.Point(3, 5);
            this.labelInfoCpu.Name = "labelInfoCpu";
            this.labelInfoCpu.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.labelInfoCpu.Size = new System.Drawing.Size(62, 23);
            this.labelInfoCpu.TabIndex = 0;
            this.labelInfoCpu.Text = "CPU";
            this.labelInfoCpu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label2.Size = new System.Drawing.Size(62, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "MB Vendor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 120);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label3.Size = new System.Drawing.Size(62, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Firmware";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 97);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label4.Size = new System.Drawing.Size(62, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "BIOS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cpuInfoLabel
            // 
            this.cpuInfoLabel.AutoEllipsis = true;
            this.cpuInfoLabel.AutoSize = true;
            this.cpuInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuInfoLabel.Location = new System.Drawing.Point(71, 5);
            this.cpuInfoLabel.Name = "cpuInfoLabel";
            this.cpuInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.cpuInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.cpuInfoLabel.TabIndex = 4;
            this.cpuInfoLabel.Text = "-";
            this.cpuInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 74);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label5.Size = new System.Drawing.Size(62, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "MB Model";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mbModelInfoLabel
            // 
            this.mbModelInfoLabel.AutoEllipsis = true;
            this.mbModelInfoLabel.AutoSize = true;
            this.mbModelInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbModelInfoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mbModelInfoLabel.Location = new System.Drawing.Point(71, 74);
            this.mbModelInfoLabel.Name = "mbModelInfoLabel";
            this.mbModelInfoLabel.Padding = new System.Windows.Forms.Padding(5);
            this.mbModelInfoLabel.Size = new System.Drawing.Size(206, 23);
            this.mbModelInfoLabel.TabIndex = 9;
            this.mbModelInfoLabel.Text = "-";
            this.mbModelInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label6.Size = new System.Drawing.Size(62, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "CPUID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cpuIdLabel
            // 
            this.cpuIdLabel.AutoSize = true;
            this.cpuIdLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuIdLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuIdLabel.Location = new System.Drawing.Point(71, 28);
            this.cpuIdLabel.Name = "cpuIdLabel";
            this.cpuIdLabel.Padding = new System.Windows.Forms.Padding(5);
            this.cpuIdLabel.Size = new System.Drawing.Size(206, 23);
            this.cpuIdLabel.TabIndex = 11;
            this.cpuIdLabel.Text = "-";
            this.cpuIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 143);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.label7.Size = new System.Drawing.Size(62, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Microcode";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tableLayoutPanel6);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabSettings.Size = new System.Drawing.Size(291, 234);
            this.tabSettings.TabIndex = 5;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.checkBoxQcode, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.checkBoxMinToTray, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.checkBoxStartMinimized, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.checkBoxStartOnBoot, 0, 5);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(285, 228);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // checkBoxQcode
            // 
            this.checkBoxQcode.AutoSize = true;
            this.checkBoxQcode.Checked = true;
            this.checkBoxQcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxQcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxQcode.Location = new System.Drawing.Point(7, 15);
            this.checkBoxQcode.Name = "checkBoxQcode";
            this.checkBoxQcode.Size = new System.Drawing.Size(271, 17);
            this.checkBoxQcode.TabIndex = 0;
            this.checkBoxQcode.Text = "Q-Code temperature display";
            this.checkBoxQcode.UseVisualStyleBackColor = true;
            // 
            // checkBoxMinToTray
            // 
            this.checkBoxMinToTray.AutoSize = true;
            this.checkBoxMinToTray.Checked = true;
            this.checkBoxMinToTray.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMinToTray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxMinToTray.Location = new System.Drawing.Point(7, 38);
            this.checkBoxMinToTray.Name = "checkBoxMinToTray";
            this.checkBoxMinToTray.Size = new System.Drawing.Size(271, 17);
            this.checkBoxMinToTray.TabIndex = 2;
            this.checkBoxMinToTray.Text = "Minimize to tray";
            this.checkBoxMinToTray.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartMinimized
            // 
            this.checkBoxStartMinimized.AutoSize = true;
            this.checkBoxStartMinimized.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxStartMinimized.Location = new System.Drawing.Point(7, 61);
            this.checkBoxStartMinimized.Name = "checkBoxStartMinimized";
            this.checkBoxStartMinimized.Size = new System.Drawing.Size(271, 17);
            this.checkBoxStartMinimized.TabIndex = 3;
            this.checkBoxStartMinimized.Text = "Start minimized";
            this.checkBoxStartMinimized.UseVisualStyleBackColor = true;
            // 
            // checkBoxStartOnBoot
            // 
            this.checkBoxStartOnBoot.AutoSize = true;
            this.checkBoxStartOnBoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxStartOnBoot.Location = new System.Drawing.Point(7, 84);
            this.checkBoxStartOnBoot.Name = "checkBoxStartOnBoot";
            this.checkBoxStartOnBoot.Size = new System.Drawing.Size(271, 17);
            this.checkBoxStartOnBoot.TabIndex = 1;
            this.checkBoxStartOnBoot.Text = "Start with system";
            this.checkBoxStartOnBoot.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.buttonRefresh, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.statusText, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.buttonApply, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 264);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(299, 29);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(140, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusText.Location = new System.Drawing.Point(3, 0);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(131, 29);
            this.statusText.TabIndex = 2;
            this.statusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(221, 3);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApply_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayIconMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "ZenStates";
            this.trayIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayMenuItemApp,
            this.trayMenuItemExit});
            this.trayIconMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(94, 48);
            // 
            // trayMenuItemApp
            // 
            this.trayMenuItemApp.Enabled = false;
            this.trayMenuItemApp.Name = "trayMenuItemApp";
            this.trayMenuItemApp.Size = new System.Drawing.Size(93, 22);
            // 
            // trayMenuItemExit
            // 
            this.trayMenuItemExit.Name = "trayMenuItemExit";
            this.trayMenuItemExit.Size = new System.Drawing.Size(93, 22);
            this.trayMenuItemExit.Text = "Exit";
            this.trayMenuItemExit.Click += new System.EventHandler(this.TrayMenuItemExit_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Location = new System.Drawing.Point(0, 0);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(75, 23);
            this.buttonUndo.TabIndex = 0;
            // 
            // AppWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(305, 295);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AppWindow";
            this.Padding = new System.Windows.Forms.Padding(4, 4, 2, 2);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.AppWindow_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.cpuTabOC.ResumeLayout(false);
            this.cpuTabOC.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxPstates.ResumeLayout(false);
            this.groupBoxPstates.PerformLayout();
            this.groupBoxCpuFreq.ResumeLayout(false);
            this.groupBoxCpuFreq.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabPower.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownScalar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEDC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTDC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPPT)).EndInit();
            this.tabTweaks.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.groupBoxPerfBias.ResumeLayout(false);
            this.groupBoxPerfBias.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            this.tabInfo.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.trayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage cpuTabOC;
        private System.Windows.Forms.TabPage tabGPU;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxCpuFreq;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxPstates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label statusText;
        private System.Windows.Forms.TabPage tabPower;
        private System.Windows.Forms.TabPage tabTweaks;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label smuInfoLabel;
        private System.Windows.Forms.Label biosInfoLabel;
        private System.Windows.Forms.Label mbVendorInfoLabel;
        private System.Windows.Forms.Label labelInfoCpu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cpuInfoLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label mbModelInfoLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label cpuIdLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label microcodeInfoLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox checkBoxStartOnBoot;
        private System.Windows.Forms.CheckBox checkBoxQcode;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem trayMenuItemExit;
        private System.Windows.Forms.CheckBox checkBoxMinToTray;
        private System.Windows.Forms.ToolStripMenuItem trayMenuItemApp;
        private System.Windows.Forms.CheckBox checkBoxStartMinimized;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxPerfBias;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonUndo;
        private System.Windows.Forms.GroupBox groupBoxPerfBias;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.CheckBox checkBoxCPB;
        private System.Windows.Forms.CheckBox checkBoxC6Core;
        private System.Windows.Forms.CheckBox checkBoxC6Package;
        private System.Windows.Forms.Label labelPPT;
        private System.Windows.Forms.Label labelEDC;
        private System.Windows.Forms.Label labelTDC;
        private System.Windows.Forms.Label labelScalar;
        private System.Windows.Forms.NumericUpDown numericUpDownPPT;
        private System.Windows.Forms.NumericUpDown numericUpDownTDC;
        private System.Windows.Forms.NumericUpDown numericUpDownEDC;
        private System.Windows.Forms.NumericUpDown numericUpDownScalar;
        private System.Windows.Forms.ComboBox comboBoxPerfEnh;
        private System.Windows.Forms.Label labelPerfEnh;
        private System.Windows.Forms.Label divider1;
        private ManualOverclockItem manualOverclockItem;
    }
}

