/*
 * Created by SharpDevelop.
 * User: Jon_Sandstrom
 * Date: 2016-04-22
 * Time: 16:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace AsusZenStates
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
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
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
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelMB = new System.Windows.Forms.Label();
            this.labelCPU = new System.Windows.Forms.Label();
            this.checkBoxApplyOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxGuiOnStart = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxC6Package = new System.Windows.Forms.CheckBox();
            this.checkBoxC6Core = new System.Windows.Forms.CheckBox();
            this.comboBoxPerfbias = new System.Windows.Forms.ComboBox();
            this.labelPerfbias = new System.Windows.Forms.Label();
            this.labelPPT = new System.Windows.Forms.Label();
            this.buttonDefaults = new System.Windows.Forms.Button();
            this.checkBoxP80temp = new System.Windows.Forms.CheckBox();
            this.checkBoxCpb = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPPT = new System.Windows.Forms.TextBox();
            this.labelTDC = new System.Windows.Forms.Label();
            this.labelEDC = new System.Windows.Forms.Label();
            this.textBoxEDC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPerfenh = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTDC = new System.Windows.Forms.TextBox();
            this.textBoxScalar = new System.Windows.Forms.TextBox();
            this.checkBoxSmuPL = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonApply
            // 
            this.buttonApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonApply.Location = new System.Drawing.Point(260, 210);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 8;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.ButtonApplyClick);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxLogo.Image")));
            this.pictureBoxLogo.InitialImage = null;
            this.pictureBoxLogo.Location = new System.Drawing.Point(4, 4);
            this.pictureBoxLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(100, 21);
            this.pictureBoxLogo.TabIndex = 16;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonClose
            // 
            this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(405, 4);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(18, 19);
            this.buttonClose.TabIndex = 17;
            this.buttonClose.Text = "X";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // labelMB
            // 
            this.labelMB.Location = new System.Drawing.Point(110, 2);
            this.labelMB.Margin = new System.Windows.Forms.Padding(0);
            this.labelMB.Name = "labelMB";
            this.labelMB.Size = new System.Drawing.Size(250, 13);
            this.labelMB.TabIndex = 18;
            // 
            // labelCPU
            // 
            this.labelCPU.Location = new System.Drawing.Point(110, 15);
            this.labelCPU.Margin = new System.Windows.Forms.Padding(0);
            this.labelCPU.Name = "labelCPU";
            this.labelCPU.Size = new System.Drawing.Size(250, 13);
            this.labelCPU.TabIndex = 19;
            // 
            // checkBoxApplyOnStart
            // 
            this.checkBoxApplyOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxApplyOnStart.Location = new System.Drawing.Point(292, 182);
            this.checkBoxApplyOnStart.Name = "checkBoxApplyOnStart";
            this.checkBoxApplyOnStart.Size = new System.Drawing.Size(124, 20);
            this.checkBoxApplyOnStart.TabIndex = 20;
            this.checkBoxApplyOnStart.Text = "Apply at start";
            this.checkBoxApplyOnStart.UseVisualStyleBackColor = true;
            this.checkBoxApplyOnStart.CheckedChanged += new System.EventHandler(this.CheckBoxSystemStartupCheckedChanged);
            // 
            // checkBoxGuiOnStart
            // 
            this.checkBoxGuiOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxGuiOnStart.Location = new System.Drawing.Point(292, 158);
            this.checkBoxGuiOnStart.Name = "checkBoxGuiOnStart";
            this.checkBoxGuiOnStart.Size = new System.Drawing.Size(124, 18);
            this.checkBoxGuiOnStart.TabIndex = 21;
            this.checkBoxGuiOnStart.Text = "Start with system";
            this.checkBoxGuiOnStart.UseVisualStyleBackColor = true;
            this.checkBoxGuiOnStart.CheckedChanged += new System.EventHandler(this.CheckBoxStartWithGUICheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 16);
            this.label1.TabIndex = 31;
            // 
            // checkBoxC6Package
            // 
            this.checkBoxC6Package.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Package.Location = new System.Drawing.Point(292, 62);
            this.checkBoxC6Package.Name = "checkBoxC6Package";
            this.checkBoxC6Package.Size = new System.Drawing.Size(124, 18);
            this.checkBoxC6Package.TabIndex = 32;
            this.checkBoxC6Package.Text = "Package C6-state";
            this.checkBoxC6Package.UseVisualStyleBackColor = true;
            // 
            // checkBoxC6Core
            // 
            this.checkBoxC6Core.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxC6Core.Location = new System.Drawing.Point(292, 38);
            this.checkBoxC6Core.Name = "checkBoxC6Core";
            this.checkBoxC6Core.Size = new System.Drawing.Size(124, 18);
            this.checkBoxC6Core.TabIndex = 33;
            this.checkBoxC6Core.Text = "Core C6-state";
            this.checkBoxC6Core.UseVisualStyleBackColor = true;
            // 
            // comboBoxPerfbias
            // 
            this.comboBoxPerfbias.FormattingEnabled = true;
            this.comboBoxPerfbias.Location = new System.Drawing.Point(140, 128);
            this.comboBoxPerfbias.Name = "comboBoxPerfbias";
            this.comboBoxPerfbias.Size = new System.Drawing.Size(130, 21);
            this.comboBoxPerfbias.TabIndex = 35;
            // 
            // labelPerfbias
            // 
            this.labelPerfbias.Location = new System.Drawing.Point(5, 131);
            this.labelPerfbias.Name = "labelPerfbias";
            this.labelPerfbias.Size = new System.Drawing.Size(118, 20);
            this.labelPerfbias.TabIndex = 36;
            this.labelPerfbias.Text = "Performance Bias";
            // 
            // labelPPT
            // 
            this.labelPPT.Location = new System.Drawing.Point(12, 190);
            this.labelPPT.Name = "labelPPT";
            this.labelPPT.Size = new System.Drawing.Size(30, 20);
            this.labelPPT.TabIndex = 37;
            this.labelPPT.Text = "PPT";
            // 
            // buttonDefaults
            // 
            this.buttonDefaults.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonDefaults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.buttonDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDefaults.Location = new System.Drawing.Point(186, 210);
            this.buttonDefaults.Name = "buttonDefaults";
            this.buttonDefaults.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaults.TabIndex = 39;
            this.buttonDefaults.Text = "Restore";
            this.buttonDefaults.UseVisualStyleBackColor = true;
            this.buttonDefaults.Click += new System.EventHandler(this.buttonDefaults_Click);
            // 
            // checkBoxP80temp
            // 
            this.checkBoxP80temp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxP80temp.Location = new System.Drawing.Point(292, 134);
            this.checkBoxP80temp.Name = "checkBoxP80temp";
            this.checkBoxP80temp.Size = new System.Drawing.Size(124, 18);
            this.checkBoxP80temp.TabIndex = 34;
            this.checkBoxP80temp.Text = "Q-Code temp display";
            this.checkBoxP80temp.UseVisualStyleBackColor = true;
            // 
            // checkBoxCpb
            // 
            this.checkBoxCpb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxCpb.Location = new System.Drawing.Point(292, 86);
            this.checkBoxCpb.Name = "checkBoxCpb";
            this.checkBoxCpb.Size = new System.Drawing.Size(124, 18);
            this.checkBoxCpb.TabIndex = 40;
            this.checkBoxCpb.Text = "Core Perf. Boost";
            this.checkBoxCpb.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.buttonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSave.Location = new System.Drawing.Point(341, 210);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 44;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPPT
            // 
            this.textBoxPPT.Location = new System.Drawing.Point(48, 187);
            this.textBoxPPT.MaxLength = 4;
            this.textBoxPPT.Name = "textBoxPPT";
            this.textBoxPPT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxPPT.Size = new System.Drawing.Size(32, 20);
            this.textBoxPPT.TabIndex = 45;
            this.textBoxPPT.Text = "0";
            this.textBoxPPT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTDC
            // 
            this.labelTDC.Location = new System.Drawing.Point(86, 190);
            this.labelTDC.Name = "labelTDC";
            this.labelTDC.Size = new System.Drawing.Size(30, 20);
            this.labelTDC.TabIndex = 46;
            this.labelTDC.Text = "TDC";
            // 
            // labelEDC
            // 
            this.labelEDC.Location = new System.Drawing.Point(12, 215);
            this.labelEDC.Name = "labelEDC";
            this.labelEDC.Size = new System.Drawing.Size(30, 20);
            this.labelEDC.TabIndex = 48;
            this.labelEDC.Text = "EDC";
            // 
            // textBoxEDC
            // 
            this.textBoxEDC.Location = new System.Drawing.Point(48, 212);
            this.textBoxEDC.MaxLength = 4;
            this.textBoxEDC.Name = "textBoxEDC";
            this.textBoxEDC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxEDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxEDC.TabIndex = 49;
            this.textBoxEDC.Text = "0";
            this.textBoxEDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "Performance Enhancer";
            // 
            // comboBoxPerfenh
            // 
            this.comboBoxPerfenh.FormattingEnabled = true;
            this.comboBoxPerfenh.Location = new System.Drawing.Point(140, 155);
            this.comboBoxPerfenh.Name = "comboBoxPerfenh";
            this.comboBoxPerfenh.Size = new System.Drawing.Size(130, 21);
            this.comboBoxPerfenh.TabIndex = 51;
            this.comboBoxPerfenh.SelectedIndexChanged += new System.EventHandler(this.comboBoxPerfenh_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(86, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 52;
            this.label3.Text = "Scalar";
            // 
            // textBoxTDC
            // 
            this.textBoxTDC.Location = new System.Drawing.Point(121, 187);
            this.textBoxTDC.MaxLength = 4;
            this.textBoxTDC.Name = "textBoxTDC";
            this.textBoxTDC.Size = new System.Drawing.Size(32, 20);
            this.textBoxTDC.TabIndex = 47;
            this.textBoxTDC.Text = "0";
            this.textBoxTDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxScalar
            // 
            this.textBoxScalar.Location = new System.Drawing.Point(133, 212);
            this.textBoxScalar.MaxLength = 2;
            this.textBoxScalar.Name = "textBoxScalar";
            this.textBoxScalar.Size = new System.Drawing.Size(20, 20);
            this.textBoxScalar.TabIndex = 53;
            this.textBoxScalar.Text = "0";
            this.textBoxScalar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxSmuPL
            // 
            this.checkBoxSmuPL.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxSmuPL.Location = new System.Drawing.Point(292, 110);
            this.checkBoxSmuPL.Name = "checkBoxSmuPL";
            this.checkBoxSmuPL.Size = new System.Drawing.Size(124, 18);
            this.checkBoxSmuPL.TabIndex = 54;
            this.checkBoxSmuPL.Text = "SMU Power Limits";
            this.checkBoxSmuPL.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(428, 245);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxSmuPL);
            this.Controls.Add(this.textBoxScalar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxPerfenh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxEDC);
            this.Controls.Add(this.labelEDC);
            this.Controls.Add(this.textBoxTDC);
            this.Controls.Add(this.labelTDC);
            this.Controls.Add(this.textBoxPPT);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxCpb);
            this.Controls.Add(this.buttonDefaults);
            this.Controls.Add(this.labelPPT);
            this.Controls.Add(this.labelPerfbias);
            this.Controls.Add(this.comboBoxPerfbias);
            this.Controls.Add(this.checkBoxP80temp);
            this.Controls.Add(this.checkBoxC6Core);
            this.Controls.Add(this.checkBoxC6Package);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxGuiOnStart);
            this.Controls.Add(this.checkBoxApplyOnStart);
            this.Controls.Add(this.labelCPU);
            this.Controls.Add(this.labelMB);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.buttonApply);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASUS ZenStates";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SettingsFormMouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		private System.Windows.Forms.CheckBox checkBoxApplyOnStart;
		private System.Windows.Forms.Label labelCPU;
		private System.Windows.Forms.Label labelMB;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.PictureBox pictureBoxLogo;
		private System.Windows.Forms.Button buttonApply;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxGuiOnStart;
		private System.Windows.Forms.CheckBox checkBoxC6Package;
		private System.Windows.Forms.CheckBox checkBoxC6Core;
		private System.Windows.Forms.ComboBox comboBoxPerfbias;
		private System.Windows.Forms.Label labelPerfbias;
        private System.Windows.Forms.Label labelPPT;
        private System.Windows.Forms.Button buttonDefaults;
        private System.Windows.Forms.CheckBox checkBoxP80temp;
        private System.Windows.Forms.CheckBox checkBoxCpb;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPPT;
        private System.Windows.Forms.Label labelTDC;
        private System.Windows.Forms.Label labelEDC;
        private System.Windows.Forms.TextBox textBoxEDC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPerfenh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTDC;
        private System.Windows.Forms.TextBox textBoxScalar;
        private System.Windows.Forms.CheckBox checkBoxSmuPL;
    }
}
