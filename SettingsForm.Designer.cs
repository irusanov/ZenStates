namespace ZenStates
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.topSection = new System.Windows.Forms.TableLayoutPanel();
            this.labelCPU = new System.Windows.Forms.Label();
            this.labelMB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.smuVersionLabel = new System.Windows.Forms.Label();
            this.biosVersionLabel = new System.Windows.Forms.Label();
            this.dividerTop = new System.Windows.Forms.Panel();
            this.dividerBottom = new System.Windows.Forms.Panel();
            this.bottomSection = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mainSection = new System.Windows.Forms.TableLayoutPanel();
            this.checkboxesTable = new System.Windows.Forms.TableLayoutPanel();
            this.checkboxGroupCPU = new System.Windows.Forms.GroupBox();
            this.checkBoxSmuPL = new System.Windows.Forms.CheckBox();
            this.checkBoxCpb = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Package = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Core = new System.Windows.Forms.CheckBox();
            this.checkboxGroupApp = new System.Windows.Forms.GroupBox();
            this.checkBoxApplyOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxGuiOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxP80temp = new System.Windows.Forms.CheckBox();
            this.centerPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioAutoControl = new System.Windows.Forms.RadioButton();
            this.radioManualControl = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxPerfenh = new System.Windows.Forms.ComboBox();
            this.comboBoxPerfbias = new System.Windows.Forms.ComboBox();
            this.labelPerfbias = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxScalar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxEDC = new System.Windows.Forms.TextBox();
            this.labelEDC = new System.Windows.Forms.Label();
            this.textBoxTDC = new System.Windows.Forms.TextBox();
            this.labelTDC = new System.Windows.Forms.Label();
            this.textBoxPPT = new System.Windows.Forms.TextBox();
            this.labelPPT = new System.Windows.Forms.Label();
            this.panelModes = new System.Windows.Forms.Panel();
            this.panelManualMode = new System.Windows.Forms.Panel();
            this.panelAutoMode = new System.Windows.Forms.Panel();
            this.topSection.SuspendLayout();
            this.bottomSection.SuspendLayout();
            this.mainSection.SuspendLayout();
            this.checkboxesTable.SuspendLayout();
            this.checkboxGroupCPU.SuspendLayout();
            this.checkboxGroupApp.SuspendLayout();
            this.centerPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panelModes.SuspendLayout();
            this.SuspendLayout();
            // 
            // topSection
            // 
            this.topSection.AutoSize = true;
            this.topSection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.topSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.topSection.ColumnCount = 3;
            this.topSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.topSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.topSection.Controls.Add(this.labelCPU, 0, 0);
            this.topSection.Controls.Add(this.labelMB, 0, 1);
            this.topSection.Controls.Add(this.label3, 1, 0);
            this.topSection.Controls.Add(this.label4, 1, 1);
            this.topSection.Controls.Add(this.smuVersionLabel, 2, 0);
            this.topSection.Controls.Add(this.biosVersionLabel, 2, 1);
            this.topSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.topSection.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.topSection.Location = new System.Drawing.Point(0, 0);
            this.topSection.Name = "topSection";
            this.topSection.Padding = new System.Windows.Forms.Padding(7);
            this.topSection.RowCount = 2;
            this.topSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.topSection.Size = new System.Drawing.Size(428, 52);
            this.topSection.TabIndex = 0;
            // 
            // labelCPU
            // 
            this.labelCPU.AutoSize = true;
            this.labelCPU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCPU.Location = new System.Drawing.Point(10, 7);
            this.labelCPU.Name = "labelCPU";
            this.labelCPU.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelCPU.Size = new System.Drawing.Size(314, 19);
            this.labelCPU.TabIndex = 0;
            this.labelCPU.Text = "CPU";
            this.labelCPU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelMB
            // 
            this.labelMB.AutoSize = true;
            this.labelMB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMB.Location = new System.Drawing.Point(10, 26);
            this.labelMB.Name = "labelMB";
            this.labelMB.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.labelMB.Size = new System.Drawing.Size(314, 19);
            this.labelMB.TabIndex = 1;
            this.labelMB.Text = "Motherboard";
            this.labelMB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(330, 7);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label3.Size = new System.Drawing.Size(32, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "SMU";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(330, 26);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.label4.Size = new System.Drawing.Size(32, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "BIOS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // smuVersionLabel
            // 
            this.smuVersionLabel.AutoSize = true;
            this.smuVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smuVersionLabel.Location = new System.Drawing.Point(368, 7);
            this.smuVersionLabel.Name = "smuVersionLabel";
            this.smuVersionLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.smuVersionLabel.Size = new System.Drawing.Size(50, 19);
            this.smuVersionLabel.TabIndex = 4;
            this.smuVersionLabel.Text = "00.00.00";
            this.smuVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // biosVersionLabel
            // 
            this.biosVersionLabel.AutoSize = true;
            this.biosVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biosVersionLabel.Location = new System.Drawing.Point(368, 26);
            this.biosVersionLabel.Name = "biosVersionLabel";
            this.biosVersionLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.biosVersionLabel.Size = new System.Drawing.Size(50, 19);
            this.biosVersionLabel.TabIndex = 5;
            this.biosVersionLabel.Text = "0000";
            this.biosVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dividerTop
            // 
            this.dividerTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.dividerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.dividerTop.Location = new System.Drawing.Point(0, 52);
            this.dividerTop.Name = "dividerTop";
            this.dividerTop.Size = new System.Drawing.Size(428, 1);
            this.dividerTop.TabIndex = 1;
            // 
            // dividerBottom
            // 
            this.dividerBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dividerBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.dividerBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.dividerBottom.Location = new System.Drawing.Point(0, 294);
            this.dividerBottom.Name = "dividerBottom";
            this.dividerBottom.Size = new System.Drawing.Size(428, 1);
            this.dividerBottom.TabIndex = 3;
            // 
            // bottomSection
            // 
            this.bottomSection.AutoSize = true;
            this.bottomSection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bottomSection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.bottomSection.ColumnCount = 4;
            this.bottomSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.bottomSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.bottomSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.bottomSection.Controls.Add(this.buttonSave, 3, 0);
            this.bottomSection.Controls.Add(this.buttonApply, 2, 0);
            this.bottomSection.Controls.Add(this.buttonDefaults, 1, 0);
            this.bottomSection.Controls.Add(this.label1, 0, 0);
            this.bottomSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.bottomSection.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.bottomSection.Location = new System.Drawing.Point(0, 295);
            this.bottomSection.Name = "bottomSection";
            this.bottomSection.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.bottomSection.RowCount = 1;
            this.bottomSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.bottomSection.Size = new System.Drawing.Size(428, 33);
            this.bottomSection.TabIndex = 4;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(346, 5);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(70, 23);
            this.buttonSave.TabIndex = 49;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonApply.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.ForeColor = System.Drawing.Color.White;
            this.buttonApply.Location = new System.Drawing.Point(266, 5);
            this.buttonApply.Margin = new System.Windows.Forms.Padding(5);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(70, 23);
            this.buttonApply.TabIndex = 48;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDefaults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonDefaults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDefaults.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonDefaults.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDefaults.ForeColor = System.Drawing.Color.White;
            this.buttonDefaults.Location = new System.Drawing.Point(186, 5);
            this.buttonDefaults.Margin = new System.Windows.Forms.Padding(5);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(70, 23);
            this.buttonDefaults.TabIndex = 47;
            this.buttonDefaults.Text = "Restore";
            this.buttonDefaults.UseVisualStyleBackColor = false;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(175, 33);
            this.label1.TabIndex = 46;
            this.label1.Text = "version";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainSection
            // 
            this.mainSection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainSection.BackColor = System.Drawing.Color.White;
            this.mainSection.ColumnCount = 2;
            this.mainSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainSection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainSection.Controls.Add(this.checkboxesTable, 1, 0);
            this.mainSection.Controls.Add(this.centerPanel, 0, 0);
            this.mainSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainSection.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.mainSection.Location = new System.Drawing.Point(0, 53);
            this.mainSection.Name = "mainSection";
            this.mainSection.RowCount = 1;
            this.mainSection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainSection.Size = new System.Drawing.Size(428, 241);
            this.mainSection.TabIndex = 5;
            // 
            // checkboxesTable
            // 
            this.checkboxesTable.ColumnCount = 1;
            this.checkboxesTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.checkboxesTable.Controls.Add(this.checkboxGroupCPU, 0, 1);
            this.checkboxesTable.Controls.Add(this.checkboxGroupApp, 0, 2);
            this.checkboxesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkboxesTable.Location = new System.Drawing.Point(281, 3);
            this.checkboxesTable.Name = "checkboxesTable";
            this.checkboxesTable.RowCount = 3;
            this.checkboxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.checkboxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.checkboxesTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.checkboxesTable.Size = new System.Drawing.Size(144, 235);
            this.checkboxesTable.TabIndex = 0;
            // 
            // checkboxGroupCPU
            // 
            this.checkboxGroupCPU.AutoSize = true;
            this.checkboxGroupCPU.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.checkboxGroupCPU.Controls.Add(this.checkBoxSmuPL);
            this.checkboxGroupCPU.Controls.Add(this.checkBoxCpb);
            this.checkboxGroupCPU.Controls.Add(this.checkBoxC6Package);
            this.checkboxGroupCPU.Controls.Add(this.checkBoxC6Core);
            this.checkboxGroupCPU.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkboxGroupCPU.Location = new System.Drawing.Point(3, 29);
            this.checkboxGroupCPU.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.checkboxGroupCPU.Name = "checkboxGroupCPU";
            this.checkboxGroupCPU.Padding = new System.Windows.Forms.Padding(7, 3, 7, 7);
            this.checkboxGroupCPU.Size = new System.Drawing.Size(133, 107);
            this.checkboxGroupCPU.TabIndex = 0;
            this.checkboxGroupCPU.TabStop = false;
            // 
            // checkBoxSmuPL
            // 
            this.checkBoxSmuPL.AutoSize = true;
            this.checkBoxSmuPL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSmuPL.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxSmuPL.Enabled = false;
            this.checkBoxSmuPL.Location = new System.Drawing.Point(7, 79);
            this.checkBoxSmuPL.Name = "checkBoxSmuPL";
            this.checkBoxSmuPL.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxSmuPL.Size = new System.Drawing.Size(119, 21);
            this.checkBoxSmuPL.TabIndex = 75;
            this.checkBoxSmuPL.Text = "SMU Power Limits";
            this.checkBoxSmuPL.UseVisualStyleBackColor = false;
            // 
            // checkBoxCpb
            // 
            this.checkBoxCpb.AutoSize = true;
            this.checkBoxCpb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxCpb.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxCpb.Location = new System.Drawing.Point(7, 58);
            this.checkBoxCpb.Name = "checkBoxCpb";
            this.checkBoxCpb.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxCpb.Size = new System.Drawing.Size(119, 21);
            this.checkBoxCpb.TabIndex = 65;
            this.checkBoxCpb.Text = "Core Perf. Boost";
            this.checkBoxCpb.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Package
            // 
            this.checkBoxC6Package.AutoSize = true;
            this.checkBoxC6Package.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Package.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxC6Package.Location = new System.Drawing.Point(7, 37);
            this.checkBoxC6Package.Name = "checkBoxC6Package";
            this.checkBoxC6Package.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxC6Package.Size = new System.Drawing.Size(119, 21);
            this.checkBoxC6Package.TabIndex = 61;
            this.checkBoxC6Package.Text = "Package C6-state";
            this.checkBoxC6Package.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Core
            // 
            this.checkBoxC6Core.AutoSize = true;
            this.checkBoxC6Core.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Core.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxC6Core.Location = new System.Drawing.Point(7, 16);
            this.checkBoxC6Core.Name = "checkBoxC6Core";
            this.checkBoxC6Core.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxC6Core.Size = new System.Drawing.Size(119, 21);
            this.checkBoxC6Core.TabIndex = 60;
            this.checkBoxC6Core.Text = "Core C6-state";
            this.checkBoxC6Core.UseVisualStyleBackColor = true;
            // 
            // checkboxGroupApp
            // 
            this.checkboxGroupApp.AutoSize = true;
            this.checkboxGroupApp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.checkboxGroupApp.Controls.Add(this.checkBoxApplyOnStart);
            this.checkboxGroupApp.Controls.Add(this.checkBoxGuiOnStart);
            this.checkboxGroupApp.Controls.Add(this.checkBoxP80temp);
            this.checkboxGroupApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkboxGroupApp.Location = new System.Drawing.Point(3, 142);
            this.checkboxGroupApp.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.checkboxGroupApp.Name = "checkboxGroupApp";
            this.checkboxGroupApp.Padding = new System.Windows.Forms.Padding(7, 3, 7, 7);
            this.checkboxGroupApp.Size = new System.Drawing.Size(133, 86);
            this.checkboxGroupApp.TabIndex = 1;
            this.checkboxGroupApp.TabStop = false;
            // 
            // checkBoxApplyOnStart
            // 
            this.checkBoxApplyOnStart.AutoSize = true;
            this.checkBoxApplyOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxApplyOnStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxApplyOnStart.Location = new System.Drawing.Point(7, 58);
            this.checkBoxApplyOnStart.Name = "checkBoxApplyOnStart";
            this.checkBoxApplyOnStart.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxApplyOnStart.Size = new System.Drawing.Size(119, 21);
            this.checkBoxApplyOnStart.TabIndex = 63;
            this.checkBoxApplyOnStart.Text = "Apply at start";
            this.checkBoxApplyOnStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxGuiOnStart
            // 
            this.checkBoxGuiOnStart.AutoSize = true;
            this.checkBoxGuiOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxGuiOnStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxGuiOnStart.Location = new System.Drawing.Point(7, 37);
            this.checkBoxGuiOnStart.Name = "checkBoxGuiOnStart";
            this.checkBoxGuiOnStart.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxGuiOnStart.Size = new System.Drawing.Size(119, 21);
            this.checkBoxGuiOnStart.TabIndex = 62;
            this.checkBoxGuiOnStart.Text = "Start with system";
            this.checkBoxGuiOnStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxP80temp
            // 
            this.checkBoxP80temp.AutoSize = true;
            this.checkBoxP80temp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxP80temp.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxP80temp.Location = new System.Drawing.Point(7, 16);
            this.checkBoxP80temp.Margin = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.checkBoxP80temp.Name = "checkBoxP80temp";
            this.checkBoxP80temp.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.checkBoxP80temp.Size = new System.Drawing.Size(119, 21);
            this.checkBoxP80temp.TabIndex = 61;
            this.checkBoxP80temp.Text = "Q-Code temp display";
            this.checkBoxP80temp.UseVisualStyleBackColor = true;
            // 
            // centerPanel
            // 
            this.centerPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.centerPanel.ColumnCount = 1;
            this.centerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.centerPanel.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.centerPanel.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.centerPanel.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.centerPanel.Controls.Add(this.panelModes, 0, 1);
            this.centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanel.Location = new System.Drawing.Point(3, 3);
            this.centerPanel.Name = "centerPanel";
            this.centerPanel.RowCount = 5;
            this.centerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.centerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.centerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.centerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.centerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.centerPanel.Size = new System.Drawing.Size(272, 235);
            this.centerPanel.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.radioAutoControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioManualControl, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 23);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radioAutoControl
            // 
            this.radioAutoControl.AutoSize = true;
            this.radioAutoControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioAutoControl.Location = new System.Drawing.Point(6, 3);
            this.radioAutoControl.Name = "radioAutoControl";
            this.radioAutoControl.Size = new System.Drawing.Size(47, 17);
            this.radioAutoControl.TabIndex = 0;
            this.radioAutoControl.Text = "Auto";
            this.radioAutoControl.UseVisualStyleBackColor = true;
            this.radioAutoControl.CheckedChanged += new System.EventHandler(this.RadioAutoControl_CheckedChanged);
            // 
            // radioManualControl
            // 
            this.radioManualControl.AutoSize = true;
            this.radioManualControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.radioManualControl.Location = new System.Drawing.Point(59, 3);
            this.radioManualControl.Name = "radioManualControl";
            this.radioManualControl.Size = new System.Drawing.Size(60, 17);
            this.radioManualControl.TabIndex = 1;
            this.radioManualControl.Text = "Manual";
            this.radioManualControl.UseVisualStyleBackColor = true;
            this.radioManualControl.CheckedChanged += new System.EventHandler(this.RadioManualControl_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.comboBoxPerfenh, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxPerfbias, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelPerfbias, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(266, 54);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // comboBoxPerfenh
            // 
            this.comboBoxPerfenh.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxPerfenh.Enabled = false;
            this.comboBoxPerfenh.FormattingEnabled = true;
            this.comboBoxPerfenh.Location = new System.Drawing.Point(127, 30);
            this.comboBoxPerfenh.Name = "comboBoxPerfenh";
            this.comboBoxPerfenh.Size = new System.Drawing.Size(135, 21);
            this.comboBoxPerfenh.TabIndex = 73;
            // 
            // comboBoxPerfbias
            // 
            this.comboBoxPerfbias.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPerfbias.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxPerfbias.FormattingEnabled = true;
            this.comboBoxPerfbias.Location = new System.Drawing.Point(127, 3);
            this.comboBoxPerfbias.Name = "comboBoxPerfbias";
            this.comboBoxPerfbias.Size = new System.Drawing.Size(135, 21);
            this.comboBoxPerfbias.TabIndex = 72;
            // 
            // labelPerfbias
            // 
            this.labelPerfbias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPerfbias.Location = new System.Drawing.Point(3, 0);
            this.labelPerfbias.Name = "labelPerfbias";
            this.labelPerfbias.Size = new System.Drawing.Size(118, 27);
            this.labelPerfbias.TabIndex = 63;
            this.labelPerfbias.Text = "Performance Bias";
            this.labelPerfbias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(3, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 27);
            this.label2.TabIndex = 71;
            this.label2.Text = "Performance Enhancer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.textBoxScalar, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxEDC, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelEDC, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxTDC, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelTDC, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBoxPPT, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelPPT, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 92);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(266, 54);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // textBoxScalar
            // 
            this.textBoxScalar.Location = new System.Drawing.Point(127, 30);
            this.textBoxScalar.MaxLength = 2;
            this.textBoxScalar.Name = "textBoxScalar";
            this.textBoxScalar.Size = new System.Drawing.Size(32, 20);
            this.textBoxScalar.TabIndex = 74;
            this.textBoxScalar.Text = "0";
            this.textBoxScalar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(77, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 27);
            this.label5.TabIndex = 73;
            this.label5.Text = "Scalar";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxEDC
            // 
            this.textBoxEDC.Enabled = false;
            this.textBoxEDC.Location = new System.Drawing.Point(39, 30);
            this.textBoxEDC.MaxLength = 4;
            this.textBoxEDC.Name = "textBoxEDC";
            this.textBoxEDC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxEDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxEDC.TabIndex = 70;
            this.textBoxEDC.Text = "0";
            this.textBoxEDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelEDC
            // 
            this.labelEDC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEDC.Enabled = false;
            this.labelEDC.Location = new System.Drawing.Point(3, 27);
            this.labelEDC.Name = "labelEDC";
            this.labelEDC.Size = new System.Drawing.Size(30, 27);
            this.labelEDC.TabIndex = 69;
            this.labelEDC.Text = "EDC";
            this.labelEDC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxTDC
            // 
            this.textBoxTDC.Enabled = false;
            this.textBoxTDC.Location = new System.Drawing.Point(127, 3);
            this.textBoxTDC.MaxLength = 4;
            this.textBoxTDC.Name = "textBoxTDC";
            this.textBoxTDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxTDC.TabIndex = 68;
            this.textBoxTDC.Text = "0";
            this.textBoxTDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTDC
            // 
            this.labelTDC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTDC.Enabled = false;
            this.labelTDC.Location = new System.Drawing.Point(77, 0);
            this.labelTDC.Name = "labelTDC";
            this.labelTDC.Size = new System.Drawing.Size(41, 27);
            this.labelTDC.TabIndex = 67;
            this.labelTDC.Text = "TDC";
            this.labelTDC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPPT
            // 
            this.textBoxPPT.Enabled = false;
            this.textBoxPPT.Location = new System.Drawing.Point(39, 3);
            this.textBoxPPT.MaxLength = 4;
            this.textBoxPPT.Name = "textBoxPPT";
            this.textBoxPPT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPPT.Size = new System.Drawing.Size(32, 20);
            this.textBoxPPT.TabIndex = 66;
            this.textBoxPPT.Text = "0";
            this.textBoxPPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelPPT
            // 
            this.labelPPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPPT.Enabled = false;
            this.labelPPT.Location = new System.Drawing.Point(3, 0);
            this.labelPPT.Name = "labelPPT";
            this.labelPPT.Size = new System.Drawing.Size(30, 27);
            this.labelPPT.TabIndex = 64;
            this.labelPPT.Text = "PPT";
            this.labelPPT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelModes
            // 
            this.panelModes.AutoSize = true;
            this.panelModes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelModes.Controls.Add(this.panelManualMode);
            this.panelModes.Controls.Add(this.panelAutoMode);
            this.panelModes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelModes.Location = new System.Drawing.Point(0, 29);
            this.panelModes.Margin = new System.Windows.Forms.Padding(0);
            this.panelModes.Name = "panelModes";
            this.panelModes.Size = new System.Drawing.Size(272, 0);
            this.panelModes.TabIndex = 3;
            // 
            // panelManualMode
            // 
            this.panelManualMode.AutoSize = true;
            this.panelManualMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelManualMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelManualMode.Location = new System.Drawing.Point(0, 0);
            this.panelManualMode.Margin = new System.Windows.Forms.Padding(0);
            this.panelManualMode.Name = "panelManualMode";
            this.panelManualMode.Size = new System.Drawing.Size(272, 0);
            this.panelManualMode.TabIndex = 0;
            this.panelManualMode.Visible = false;
            // 
            // panelAutoMode
            // 
            this.panelAutoMode.AutoSize = true;
            this.panelAutoMode.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelAutoMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutoMode.Location = new System.Drawing.Point(0, 0);
            this.panelAutoMode.Margin = new System.Windows.Forms.Padding(0);
            this.panelAutoMode.Name = "panelAutoMode";
            this.panelAutoMode.Size = new System.Drawing.Size(272, 0);
            this.panelAutoMode.TabIndex = 0;
            this.panelAutoMode.Visible = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(428, 329);
            this.Controls.Add(this.bottomSection);
            this.Controls.Add(this.dividerBottom);
            this.Controls.Add(this.mainSection);
            this.Controls.Add(this.dividerTop);
            this.Controls.Add(this.topSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(444, 500);
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZenStates";
            this.topSection.ResumeLayout(false);
            this.topSection.PerformLayout();
            this.bottomSection.ResumeLayout(false);
            this.bottomSection.PerformLayout();
            this.mainSection.ResumeLayout(false);
            this.checkboxesTable.ResumeLayout(false);
            this.checkboxesTable.PerformLayout();
            this.checkboxGroupCPU.ResumeLayout(false);
            this.checkboxGroupCPU.PerformLayout();
            this.checkboxGroupApp.ResumeLayout(false);
            this.checkboxGroupApp.PerformLayout();
            this.centerPanel.ResumeLayout(false);
            this.centerPanel.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panelModes.ResumeLayout(false);
            this.panelModes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel topSection;
        private System.Windows.Forms.Label labelCPU;
        private System.Windows.Forms.Label labelMB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label smuVersionLabel;
        private System.Windows.Forms.Label biosVersionLabel;
        private System.Windows.Forms.Panel dividerTop;
        private System.Windows.Forms.Panel dividerBottom;
        private System.Windows.Forms.TableLayoutPanel bottomSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TableLayoutPanel mainSection;
        private System.Windows.Forms.TableLayoutPanel checkboxesTable;
        private System.Windows.Forms.GroupBox checkboxGroupCPU;
        private System.Windows.Forms.GroupBox checkboxGroupApp;
        private System.Windows.Forms.CheckBox checkBoxC6Core;
        private System.Windows.Forms.CheckBox checkBoxC6Package;
        private System.Windows.Forms.CheckBox checkBoxCpb;
        private System.Windows.Forms.CheckBox checkBoxSmuPL;
        private System.Windows.Forms.CheckBox checkBoxP80temp;
        private System.Windows.Forms.CheckBox checkBoxGuiOnStart;
        private System.Windows.Forms.CheckBox checkBoxApplyOnStart;
        private System.Windows.Forms.TableLayoutPanel centerPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton radioAutoControl;
        private System.Windows.Forms.RadioButton radioManualControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelPerfbias;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPerfbias;
        private System.Windows.Forms.ComboBox comboBoxPerfenh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelPPT;
        private System.Windows.Forms.TextBox textBoxPPT;
        private System.Windows.Forms.Label labelTDC;
        private System.Windows.Forms.TextBox textBoxTDC;
        private System.Windows.Forms.Label labelEDC;
        private System.Windows.Forms.TextBox textBoxEDC;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxScalar;
        private System.Windows.Forms.Panel panelModes;
        private System.Windows.Forms.Panel panelManualMode;
        private System.Windows.Forms.Panel panelAutoMode;
    }
}