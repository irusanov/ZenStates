namespace ZenStates.Components
{
    partial class PstateItem
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
            this.comboBoxFID = new System.Windows.Forms.ComboBox();
            this.comboBoxDID = new System.Windows.Forms.ComboBox();
            this.comboBoxVID = new System.Windows.Forms.ComboBox();
            this.checkBoxPstateEnabled = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxFID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxDID, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxVID, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxPstateEnabled, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // comboBoxFID
            // 
            this.comboBoxFID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxFID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFID.FormattingEnabled = true;
            this.comboBoxFID.Location = new System.Drawing.Point(48, 4);
            this.comboBoxFID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.comboBoxFID.Name = "comboBoxFID";
            this.comboBoxFID.Size = new System.Drawing.Size(90, 21);
            this.comboBoxFID.TabIndex = 4;
            this.comboBoxFID.SelectedIndexChanged += new System.EventHandler(this.comboBoxFID_SelectedIndexChanged);
            // 
            // comboBoxDID
            // 
            this.comboBoxDID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDID.FormattingEnabled = true;
            this.comboBoxDID.Location = new System.Drawing.Point(144, 4);
            this.comboBoxDID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.comboBoxDID.Name = "comboBoxDID";
            this.comboBoxDID.Size = new System.Drawing.Size(90, 21);
            this.comboBoxDID.TabIndex = 2;
            this.comboBoxDID.SelectedIndexChanged += new System.EventHandler(this.comboBoxDID_SelectedIndexChanged);
            // 
            // comboBoxVID
            // 
            this.comboBoxVID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxVID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVID.FormattingEnabled = true;
            this.comboBoxVID.Location = new System.Drawing.Point(240, 4);
            this.comboBoxVID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.comboBoxVID.Name = "comboBoxVID";
            this.comboBoxVID.Size = new System.Drawing.Size(92, 21);
            this.comboBoxVID.TabIndex = 3;
            this.comboBoxVID.SelectedIndexChanged += new System.EventHandler(this.comboBoxVID_SelectedIndexChanged);
            // 
            // checkBoxPstateEnabled
            // 
            this.checkBoxPstateEnabled.AutoSize = true;
            this.checkBoxPstateEnabled.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxPstateEnabled.Location = new System.Drawing.Point(3, 3);
            this.checkBoxPstateEnabled.Name = "checkBoxPstateEnabled";
            this.checkBoxPstateEnabled.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.checkBoxPstateEnabled.Size = new System.Drawing.Size(39, 21);
            this.checkBoxPstateEnabled.TabIndex = 5;
            this.checkBoxPstateEnabled.Text = "P0";
            this.checkBoxPstateEnabled.UseVisualStyleBackColor = true;
            this.checkBoxPstateEnabled.CheckedChanged += new System.EventHandler(this.checkBoxPstateEnabled_CheckedChanged);
            // 
            // PstateItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Name = "PstateItem";
            this.Size = new System.Drawing.Size(335, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxFID;
        private System.Windows.Forms.ComboBox comboBoxDID;
        private System.Windows.Forms.ComboBox comboBoxVID;
        private System.Windows.Forms.CheckBox checkBoxPstateEnabled;
    }
}
