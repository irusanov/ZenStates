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
            this.comboBoxMulti = new System.Windows.Forms.ComboBox();
            this.comboBoxCore = new System.Windows.Forms.ComboBox();
            this.comboBoxVid = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxMulti, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCore, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxVid, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxMulti
            // 
            this.comboBoxMulti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMulti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMulti.FormattingEnabled = true;
            this.comboBoxMulti.Location = new System.Drawing.Point(3, 3);
            this.comboBoxMulti.Name = "comboBoxMulti";
            this.comboBoxMulti.Size = new System.Drawing.Size(105, 21);
            this.comboBoxMulti.TabIndex = 0;
            // 
            // comboBoxCore
            // 
            this.comboBoxCore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxCore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCore.FormattingEnabled = true;
            this.comboBoxCore.Location = new System.Drawing.Point(114, 3);
            this.comboBoxCore.Name = "comboBoxCore";
            this.comboBoxCore.Size = new System.Drawing.Size(105, 21);
            this.comboBoxCore.TabIndex = 1;
            // 
            // comboBoxVid
            // 
            this.comboBoxVid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVid.FormattingEnabled = true;
            this.comboBoxVid.Location = new System.Drawing.Point(225, 3);
            this.comboBoxVid.Name = "comboBoxVid";
            this.comboBoxVid.Size = new System.Drawing.Size(107, 21);
            this.comboBoxVid.TabIndex = 2;
            // 
            // ManualOverclockItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManualOverclockItem";
            this.Size = new System.Drawing.Size(335, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxMulti;
        private System.Windows.Forms.ComboBox comboBoxCore;
        private System.Windows.Forms.ComboBox comboBoxVid;
    }
}
