/*
 * Created by SharpDevelop.
 * User: Jon_Sandstrom
 * Date: 2016-04-22
 * Time: 16:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ZenStates
{
    partial class SettingsForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.smuVersionLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelMB = new System.Windows.Forms.Label();
            this.labelCPU = new System.Windows.Forms.Label();
            this.checkBoxOc = new System.Windows.Forms.CheckBox();
            this.checkBoxSmuPL = new System.Windows.Forms.CheckBox();
            this.textBoxScalar = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPerfenh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEDC = new System.Windows.Forms.TextBox();
            this.labelEDC = new System.Windows.Forms.Label();
            this.textBoxTDC = new System.Windows.Forms.TextBox();
            this.labelTDC = new System.Windows.Forms.Label();
            this.textBoxPPT = new System.Windows.Forms.TextBox();
            this.checkBoxCpb = new System.Windows.Forms.CheckBox();
            this.labelPPT = new System.Windows.Forms.Label();
            this.labelPerfbias = new System.Windows.Forms.Label();
            this.comboBoxPerfbias = new System.Windows.Forms.ComboBox();
            this.checkBoxP80temp = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Core = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Package = new System.Windows.Forms.CheckBox();
            this.checkBoxGuiOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxApplyOnStart = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.biosVersionLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonApply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonApply.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.ForeColor = System.Drawing.Color.White;
            this.buttonApply.Location = new System.Drawing.Point(260, 9);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 8;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = false;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonDefaults.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonDefaults.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDefaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDefaults.ForeColor = System.Drawing.Color.White;
            this.buttonDefaults.Location = new System.Drawing.Point(179, 9);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaults.TabIndex = 39;
            this.buttonDefaults.Text = "Restore";
            this.buttonDefaults.UseVisualStyleBackColor = false;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(185)))));
            this.buttonSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(341, 9);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 44;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonDefaults);
            this.panel1.Controls.Add(this.buttonApply);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(52)))), ((int)(((byte)(72)))));
            this.panel1.Location = new System.Drawing.Point(0, 267);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(14);
            this.panel1.Size = new System.Drawing.Size(428, 40);
            this.panel1.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "version";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.biosVersionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.smuVersionLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.labelMB);
            this.splitContainer1.Panel1.Controls.Add(this.labelCPU);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(52)))), ((int)(((byte)(72)))));
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(14);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxOc);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxSmuPL);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxScalar);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxPerfenh);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxEDC);
            this.splitContainer1.Panel2.Controls.Add(this.labelEDC);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxTDC);
            this.splitContainer1.Panel2.Controls.Add(this.labelTDC);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxPPT);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxCpb);
            this.splitContainer1.Panel2.Controls.Add(this.labelPPT);
            this.splitContainer1.Panel2.Controls.Add(this.labelPerfbias);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxPerfbias);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxP80temp);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxC6Core);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxC6Package);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxGuiOnStart);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxApplyOnStart);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(52)))), ((int)(((byte)(72)))));
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(14);
            this.splitContainer1.Size = new System.Drawing.Size(428, 266);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 59;
            // 
            // smuVersionLabel
            // 
            this.smuVersionLabel.AutoSize = true;
            this.smuVersionLabel.Location = new System.Drawing.Point(371, 31);
            this.smuVersionLabel.Name = "smuVersionLabel";
            this.smuVersionLabel.Size = new System.Drawing.Size(49, 13);
            this.smuVersionLabel.TabIndex = 78;
            this.smuVersionLabel.Text = "00.00.00";
            this.smuVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(338, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "SMU";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.panel3.Location = new System.Drawing.Point(-1, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(428, 1);
            this.panel3.TabIndex = 76;
            // 
            // labelMB
            // 
            this.labelMB.Location = new System.Drawing.Point(7, 31);
            this.labelMB.Margin = new System.Windows.Forms.Padding(0);
            this.labelMB.Name = "labelMB";
            this.labelMB.Size = new System.Drawing.Size(250, 13);
            this.labelMB.TabIndex = 21;
            this.labelMB.Text = "Motherboard";
            // 
            // labelCPU
            // 
            this.labelCPU.Location = new System.Drawing.Point(7, 10);
            this.labelCPU.Margin = new System.Windows.Forms.Padding(0);
            this.labelCPU.Name = "labelCPU";
            this.labelCPU.Size = new System.Drawing.Size(250, 13);
            this.labelCPU.TabIndex = 20;
            this.labelCPU.Text = "CPU";
            // 
            // checkBoxOc
            // 
            this.checkBoxOc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxOc.Location = new System.Drawing.Point(292, 11);
            this.checkBoxOc.Name = "checkBoxOc";
            this.checkBoxOc.Size = new System.Drawing.Size(124, 17);
            this.checkBoxOc.TabIndex = 75;
            this.checkBoxOc.Text = "Manual Overclock";
            this.checkBoxOc.UseVisualStyleBackColor = true;
            // 
            // checkBoxSmuPL
            // 
            this.checkBoxSmuPL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSmuPL.Enabled = false;
            this.checkBoxSmuPL.Location = new System.Drawing.Point(292, 106);
            this.checkBoxSmuPL.Name = "checkBoxSmuPL";
            this.checkBoxSmuPL.Size = new System.Drawing.Size(124, 18);
            this.checkBoxSmuPL.TabIndex = 74;
            this.checkBoxSmuPL.Text = "SMU Power Limits";
            this.checkBoxSmuPL.UseVisualStyleBackColor = false;
            // 
            // textBoxScalar
            // 
            this.textBoxScalar.Location = new System.Drawing.Point(136, 176);
            this.textBoxScalar.MaxLength = 2;
            this.textBoxScalar.Name = "textBoxScalar";
            this.textBoxScalar.Size = new System.Drawing.Size(32, 20);
            this.textBoxScalar.TabIndex = 73;
            this.textBoxScalar.Text = "0";
            this.textBoxScalar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(94, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 72;
            this.label3.Text = "Scalar";
            // 
            // comboBoxPerfenh
            // 
            this.comboBoxPerfenh.Enabled = false;
            this.comboBoxPerfenh.FormattingEnabled = true;
            this.comboBoxPerfenh.Location = new System.Drawing.Point(136, 119);
            this.comboBoxPerfenh.Name = "comboBoxPerfenh";
            this.comboBoxPerfenh.Size = new System.Drawing.Size(135, 21);
            this.comboBoxPerfenh.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(6, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 70;
            this.label2.Text = "Performance Enhancer";
            // 
            // textBoxEDC
            // 
            this.textBoxEDC.Enabled = false;
            this.textBoxEDC.Location = new System.Drawing.Point(42, 176);
            this.textBoxEDC.MaxLength = 4;
            this.textBoxEDC.Name = "textBoxEDC";
            this.textBoxEDC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxEDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxEDC.TabIndex = 69;
            this.textBoxEDC.Text = "0";
            this.textBoxEDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelEDC
            // 
            this.labelEDC.Enabled = false;
            this.labelEDC.Location = new System.Drawing.Point(6, 179);
            this.labelEDC.Name = "labelEDC";
            this.labelEDC.Size = new System.Drawing.Size(30, 20);
            this.labelEDC.TabIndex = 68;
            this.labelEDC.Text = "EDC";
            // 
            // textBoxTDC
            // 
            this.textBoxTDC.Enabled = false;
            this.textBoxTDC.Location = new System.Drawing.Point(136, 151);
            this.textBoxTDC.MaxLength = 4;
            this.textBoxTDC.Name = "textBoxTDC";
            this.textBoxTDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxTDC.TabIndex = 67;
            this.textBoxTDC.Text = "0";
            this.textBoxTDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTDC
            // 
            this.labelTDC.Enabled = false;
            this.labelTDC.Location = new System.Drawing.Point(94, 154);
            this.labelTDC.Name = "labelTDC";
            this.labelTDC.Size = new System.Drawing.Size(30, 20);
            this.labelTDC.TabIndex = 66;
            this.labelTDC.Text = "TDC";
            // 
            // textBoxPPT
            // 
            this.textBoxPPT.Enabled = false;
            this.textBoxPPT.Location = new System.Drawing.Point(42, 151);
            this.textBoxPPT.MaxLength = 4;
            this.textBoxPPT.Name = "textBoxPPT";
            this.textBoxPPT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPPT.Size = new System.Drawing.Size(32, 20);
            this.textBoxPPT.TabIndex = 65;
            this.textBoxPPT.Text = "0";
            this.textBoxPPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxCpb
            // 
            this.checkBoxCpb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxCpb.Location = new System.Drawing.Point(292, 82);
            this.checkBoxCpb.Name = "checkBoxCpb";
            this.checkBoxCpb.Size = new System.Drawing.Size(124, 18);
            this.checkBoxCpb.TabIndex = 64;
            this.checkBoxCpb.Text = "Core Perf. Boost";
            this.checkBoxCpb.UseVisualStyleBackColor = true;
            // 
            // labelPPT
            // 
            this.labelPPT.Enabled = false;
            this.labelPPT.Location = new System.Drawing.Point(6, 154);
            this.labelPPT.Name = "labelPPT";
            this.labelPPT.Size = new System.Drawing.Size(30, 20);
            this.labelPPT.TabIndex = 63;
            this.labelPPT.Text = "PPT";
            // 
            // labelPerfbias
            // 
            this.labelPerfbias.Location = new System.Drawing.Point(6, 95);
            this.labelPerfbias.Name = "labelPerfbias";
            this.labelPerfbias.Size = new System.Drawing.Size(118, 20);
            this.labelPerfbias.TabIndex = 62;
            this.labelPerfbias.Text = "Performance Bias";
            // 
            // comboBoxPerfbias
            // 
            this.comboBoxPerfbias.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPerfbias.FormattingEnabled = true;
            this.comboBoxPerfbias.Location = new System.Drawing.Point(136, 91);
            this.comboBoxPerfbias.Name = "comboBoxPerfbias";
            this.comboBoxPerfbias.Size = new System.Drawing.Size(135, 21);
            this.comboBoxPerfbias.TabIndex = 61;
            // 
            // checkBoxP80temp
            // 
            this.checkBoxP80temp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxP80temp.Location = new System.Drawing.Point(292, 130);
            this.checkBoxP80temp.Name = "checkBoxP80temp";
            this.checkBoxP80temp.Size = new System.Drawing.Size(124, 18);
            this.checkBoxP80temp.TabIndex = 60;
            this.checkBoxP80temp.Text = "Q-Code temp display";
            this.checkBoxP80temp.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Core
            // 
            this.checkBoxC6Core.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Core.Location = new System.Drawing.Point(292, 34);
            this.checkBoxC6Core.Name = "checkBoxC6Core";
            this.checkBoxC6Core.Size = new System.Drawing.Size(124, 18);
            this.checkBoxC6Core.TabIndex = 59;
            this.checkBoxC6Core.Text = "Core C6-state";
            this.checkBoxC6Core.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Package
            // 
            this.checkBoxC6Package.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Package.Location = new System.Drawing.Point(292, 58);
            this.checkBoxC6Package.Name = "checkBoxC6Package";
            this.checkBoxC6Package.Size = new System.Drawing.Size(124, 18);
            this.checkBoxC6Package.TabIndex = 58;
            this.checkBoxC6Package.Text = "Package C6-state";
            this.checkBoxC6Package.UseVisualStyleBackColor = true;
            // 
            // checkBoxGuiOnStart
            // 
            this.checkBoxGuiOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxGuiOnStart.Location = new System.Drawing.Point(292, 154);
            this.checkBoxGuiOnStart.Name = "checkBoxGuiOnStart";
            this.checkBoxGuiOnStart.Size = new System.Drawing.Size(124, 18);
            this.checkBoxGuiOnStart.TabIndex = 57;
            this.checkBoxGuiOnStart.Text = "Start with system";
            this.checkBoxGuiOnStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplyOnStart
            // 
            this.checkBoxApplyOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxApplyOnStart.Location = new System.Drawing.Point(292, 178);
            this.checkBoxApplyOnStart.Name = "checkBoxApplyOnStart";
            this.checkBoxApplyOnStart.Size = new System.Drawing.Size(124, 20);
            this.checkBoxApplyOnStart.TabIndex = 56;
            this.checkBoxApplyOnStart.Text = "Apply at start";
            this.checkBoxApplyOnStart.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 266);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 1);
            this.panel2.TabIndex = 60;
            // 
            // biosVersionLabel
            // 
            this.biosVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.biosVersionLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.biosVersionLabel.Location = new System.Drawing.Point(371, 10);
            this.biosVersionLabel.Name = "biosVersionLabel";
            this.biosVersionLabel.Size = new System.Drawing.Size(49, 13);
            this.biosVersionLabel.TabIndex = 79;
            this.biosVersionLabel.Text = "0000";
            this.biosVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(338, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "BIOS";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(428, 307);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZenStates";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsFormMouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelMB;
        private System.Windows.Forms.Label labelCPU;
        private System.Windows.Forms.CheckBox checkBoxOc;
        private System.Windows.Forms.CheckBox checkBoxSmuPL;
        private System.Windows.Forms.TextBox textBoxScalar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPerfenh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEDC;
        private System.Windows.Forms.Label labelEDC;
        private System.Windows.Forms.TextBox textBoxTDC;
        private System.Windows.Forms.Label labelTDC;
        private System.Windows.Forms.TextBox textBoxPPT;
        private System.Windows.Forms.CheckBox checkBoxCpb;
        private System.Windows.Forms.Label labelPPT;
        private System.Windows.Forms.Label labelPerfbias;
        private System.Windows.Forms.ComboBox comboBoxPerfbias;
        private System.Windows.Forms.CheckBox checkBoxP80temp;
        private System.Windows.Forms.CheckBox checkBoxC6Core;
        private System.Windows.Forms.CheckBox checkBoxC6Package;
        private System.Windows.Forms.CheckBox checkBoxGuiOnStart;
        private System.Windows.Forms.CheckBox checkBoxApplyOnStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label smuVersionLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label biosVersionLabel;
        private System.Windows.Forms.Label label5;
    }
}
