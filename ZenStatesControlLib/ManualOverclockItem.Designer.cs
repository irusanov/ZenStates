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
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // checkBoxOCModeEnabled
            // 
            this.checkBoxOCModeEnabled.AutoSize = true;
            this.checkBoxOCModeEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxOCModeEnabled.Location = new System.Drawing.Point(3, 3);
            this.checkBoxOCModeEnabled.Name = "checkBoxOCModeEnabled";
            this.checkBoxOCModeEnabled.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.checkBoxOCModeEnabled.Size = new System.Drawing.Size(39, 21);
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
            // ManualOverclockItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "ManualOverclockItem";
            this.Size = new System.Drawing.Size(335, 28);
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
    }
}
