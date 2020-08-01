namespace ZenStates.Components
{
    partial class ManualOverclockItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxOCModeEnabled = new System.Windows.Forms.CheckBox();
            this.comboBoxMulti = new System.Windows.Forms.ComboBox();
            this.comboBoxCore = new System.Windows.Forms.ComboBox();
            this.comboBoxVid = new System.Windows.Forms.ComboBox();
            this.checkBoxSlowMode = new System.Windows.Forms.CheckBox();
            this.checkBoxProchot = new System.Windows.Forms.CheckBox();
            this.comboBoxControlMode = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.checkBoxOCModeEnabled, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxMulti, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCore, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxVid, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxSlowMode, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxProchot, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxControlMode, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // checkBoxOCModeEnabled
            // 
            this.checkBoxOCModeEnabled.AutoSize = true;
            this.checkBoxOCModeEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxOCModeEnabled.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOCModeEnabled.Name = "checkBoxOCModeEnabled";
            this.checkBoxOCModeEnabled.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.checkBoxOCModeEnabled.Size = new System.Drawing.Size(39, 20);
            this.checkBoxOCModeEnabled.TabIndex = 6;
            this.checkBoxOCModeEnabled.Text = "En";
            this.checkBoxOCModeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxOCModeEnabled.CheckedChanged += new System.EventHandler(this.CheckBoxOCModeEnabled_CheckedChanged);
            // 
            // comboBoxMulti
            // 
            this.comboBoxMulti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMulti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMulti.FormattingEnabled = true;
            this.comboBoxMulti.Location = new System.Drawing.Point(48, 3);
            this.comboBoxMulti.Name = "comboBoxMulti";
            this.comboBoxMulti.Size = new System.Drawing.Size(90, 21);
            this.comboBoxMulti.TabIndex = 0;
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCore.FormattingEnabled = true;
            this.comboBoxCore.Location = new System.Drawing.Point(144, 3);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Size = new System.Drawing.Size(90, 21);
            this.comboBoxCore.TabIndex = 1;
            // 
            // comboBoxVid
            // 
            this.comboBoxVid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVid.FormattingEnabled = true;
            this.comboBoxVid.Location = new System.Drawing.Point(240, 3);
            this.comboBoxVid.Name = "comboBoxVid";
            this.comboBoxVid.Size = new System.Drawing.Size(92, 21);
            this.comboBoxVid.TabIndex = 2;
            // 
            // checkBoxSlowMode
            // 
            this.checkBoxSlowMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxSlowMode.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSlowMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxSlowMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxSlowMode.Location = new System.Drawing.Point(239, 29);
            this.checkBoxSlowMode.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBoxSlowMode.Name = "checkBoxSlowMode";
            this.checkBoxSlowMode.Size = new System.Drawing.Size(94, 23);
            this.checkBoxSlowMode.TabIndex = 7;
            this.checkBoxSlowMode.Text = "Slow Mode";
            this.checkBoxSlowMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSlowMode.UseVisualStyleBackColor = true;
            this.checkBoxSlowMode.Click += new System.EventHandler(this.CheckBoxSlowMode_Click);
            // 
            // checkBoxProchot
            // 
            this.checkBoxProchot.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxProchot.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxProchot.Checked = true;
            this.checkBoxProchot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxProchot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxProchot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBoxProchot.Location = new System.Drawing.Point(47, 29);
            this.checkBoxProchot.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBoxProchot.Name = "checkBoxProchot";
            this.checkBoxProchot.Size = new System.Drawing.Size(92, 23);
            this.checkBoxProchot.TabIndex = 8;
            this.checkBoxProchot.Text = "PROCHOT";
            this.checkBoxProchot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxProchot.UseVisualStyleBackColor = true;
            this.checkBoxProchot.Click += new System.EventHandler(this.CheckBoxProchot_Click);
            // 
            // comboBoxControlMode
            // 
            this.comboBoxControlMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxControlMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxControlMode.FormattingEnabled = true;
            this.comboBoxControlMode.Items.AddRange(new object[] {
            "Cores",
            "CCX",
            "CCD"});
            this.comboBoxControlMode.Location = new System.Drawing.Point(144, 30);
            this.comboBoxControlMode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.comboBoxControlMode.Name = "comboBoxControlMode";
            this.comboBoxControlMode.Size = new System.Drawing.Size(90, 21);
            this.comboBoxControlMode.TabIndex = 9;
            this.comboBoxControlMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxControlMode_SelectedIndexChanged);
            // 
            // ManualOverclockItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "ManualOverclockItem";
            this.Size = new System.Drawing.Size(335, 55);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxMulti;
        private System.Windows.Forms.ComboBox comboBoxCore;
        private System.Windows.Forms.ComboBox comboBoxVid;
        private System.Windows.Forms.CheckBox checkBoxOCModeEnabled;
        private System.Windows.Forms.CheckBox checkBoxSlowMode;
        private System.Windows.Forms.CheckBox checkBoxProchot;
        private System.Windows.Forms.ComboBox comboBoxControlMode;
    }
}
