using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AsusZenStates
{
    /// <summary>
    /// Description of SettingsForm.
    /// </summary>
    public partial class SettingsForm : Form
    {
        // Window management
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int PSTATES = 1;
        private const int FID_MAX = 0xFF;
        private const int FID_MIN = 0x10;
        private const int VID_MAX = 0xA9;
        private const int VID_MIN = 0x00;

        private const double FREQ_MAX = 70;
        private const double FREQ_MIN = 5.5;

        // CheckBox[] PstateEn = new CheckBox[PSTATES];
        ComboBox[] PstateFid = new ComboBox[PSTATES];
        ComboBox[] PstateDid = new ComboBox[PSTATES];
        ComboBox[] PstateVid = new ComboBox[PSTATES];

        class CustomListItem
        {
            public int id;
            public byte value;
            public string text;

            public CustomListItem(int id, string text)
            {
                this.id = id;
                this.value = (byte)id;
                this.text = text;
            }

            public CustomListItem(int id, byte value, string text)
            {
                this.id = id;
                this.value = value;
                this.text = text;
            }

            public override string ToString() { return text; }
        }

        CustomListItem[] PERFBIAS = {
            new CustomListItem(0, "Auto (BIOS)"),
            new CustomListItem(1, "Disabled"),
            new CustomListItem(2, "Cinebench R15 / AIDA64"),
            new CustomListItem(3, "Cinebench R11.5"),
            new CustomListItem(4, "Geekbench 3")
        };

        CustomListItem[] PERFENH = {
            new CustomListItem(0, "None"),
            new CustomListItem(1, "Default"),
            new CustomListItem(2, "Level 1"),
            new CustomListItem(3, "Level 2"),
            new CustomListItem(4, "Level 3 (OC)")
        };

        CustomListItem[] DIVIDERS = {
            new CustomListItem(0,  0x04,   "/2"),
            new CustomListItem(1,  0x08,   "/4"),
            new CustomListItem(2,  0x0A,   "/5"),
            new CustomListItem(3,  0x0B, "/5.5"),
            new CustomListItem(4,  0x0C,   "/6"),
            new CustomListItem(5,  0x0D, "/6.5"),
            new CustomListItem(6,  0x0E,   "/7"),
            new CustomListItem(7,  0x0F, "/7.5"),
            new CustomListItem(8,  0x10,   "/8"),
            new CustomListItem(9,  0x11, "/8.5"),
            new CustomListItem(10, 0x12,   "/9"),
            new CustomListItem(11, 0x13, "/9.5"),
            new CustomListItem(12, 0x14, "/10")
        };

        public SettingsForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            // Window management
            labelMB.MouseDown += new MouseEventHandler(SettingsFormMouseDown);
            labelCPU.MouseDown += new MouseEventHandler(SettingsFormMouseDown);

            // MB/CPU description
            labelMB.Text = NotificationIcon.mbName;
            labelCPU.Text = NotificationIcon.cpuName;
            label1.Text = "version " + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf("."));

            // Pstate controls
            for (int i = 0; i < PstateFid.Length; i++)
            {
                // Enable checkbox
                /*PstateEn[i] = new CheckBox
                {
                    Text = "P" + i.ToString(),
                    Size = new System.Drawing.Size(40, 20),
                    Location = new System.Drawing.Point(10, 7 + i * 25),
                    Enabled = false
                };
                this.splitContainer1.Panel2.Controls.Add(PstateEn[i]);*/

                // FID combobox
                PstateFid[i] = new ComboBox
                {
                    Size = new System.Drawing.Size(120, 20),
                    Location = new System.Drawing.Point(10, 7 + i * 25)
                };
                this.splitContainer1.Panel2.Controls.Add(PstateFid[i]);

                // DID combobox
                PstateDid[i] = new ComboBox
                {
                    Size = new System.Drawing.Size(50, 20),
                    Location = new System.Drawing.Point(135, 7 + i * 25),
                    Enabled = false
                };
                foreach (CustomListItem item in DIVIDERS)
                {
                    PstateDid[i].Items.Add(item);
                }
                PstateDid[i].SelectedIndexChanged += UpdateFids;
                this.splitContainer1.Panel2.Controls.Add(PstateDid[i]);

                // VID combobox
                PstateVid[i] = new ComboBox
                {
                    Size = new System.Drawing.Size(80, 20),
                    Location = new System.Drawing.Point(190, 7 + i * 25)
                };
                int k = 0;
                for (byte j = VID_MIN; j <= VID_MAX; j++)
                {
                    double voltage = 1.55 - j * 0.00625;
                    CustomListItem item = new CustomListItem(k++, j, voltage.ToString("F3") + "V");
                    PstateVid[i].Items.Add(item);
                }
                this.splitContainer1.Panel2.Controls.Add(PstateVid[i]);
            }

            foreach (CustomListItem item in PERFBIAS)
            {
                comboBoxPerfbias.Items.Add(item);
            }

            foreach (CustomListItem item in PERFENH)
            {
                comboBoxPerfenh.Items.Add(item);
            }

            ResetValues();

            // Disable PPT/TDC/EDC
            /*textBoxPPT.Enabled = false;
            textBoxTDC.Enabled = false;
            textBoxEDC.Enabled = false;*/

            // Save button
            SetSavedButton(false);

            // ToolTip
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(labelPPT, "Socket Power (W)");
            toolTip.SetToolTip(labelEDC, "Electrical Design Current (A)");
            toolTip.SetToolTip(labelTDC, "Thermal Design Current (A)");
        }

        public void ResetValues()
        {
            try
            {
                // PerfBias
                comboBoxPerfbias.SelectedIndex = (int)NotificationIcon.perfBias;
                //comboBoxPerfenh.SelectedIndex = (int)NotificationIcon.PerfEnh.None;

                for (int i = 0; i < PSTATES; i++)
                {

                    // PstateEn
                    // PstateEn[i].Checked = Convert.ToBoolean((NotificationIcon.Pstate[i] >> 63) & 0x1);

                    PstateFid[i].Enabled = Convert.ToBoolean(NotificationIcon.ZenOc);

                    // DID
                    byte did = Convert.ToByte((NotificationIcon.Pstate[i] >> 8) & 0x3F);
                    foreach (CustomListItem item in PstateDid[i].Items)
                    {
                        if (item.value == did) PstateDid[i].SelectedIndex = item.id;
                    }

                    // VID
                    byte vid = Convert.ToByte((NotificationIcon.Pstate[i] >> 14) & 0xFF);
                    foreach (CustomListItem item in PstateVid[i].Items)
                    {
                        if (item.value == vid) PstateVid[i].SelectedIndex = item.id;
                    }
                    PstateVid[i].Enabled = Convert.ToBoolean(NotificationIcon.ZenOc);
                }

                // FID/Ratios
                UpdateFids(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Checkboxes
            checkBoxApplyOnStart.Checked = Convert.ToBoolean(NotificationIcon.ApplyAtStart);
            checkBoxGuiOnStart.Checked = Convert.ToBoolean(NotificationIcon.TrayIconAtStart);
            checkBoxP80temp.Checked = Convert.ToBoolean(NotificationIcon.P80Temp);

            checkBoxC6Core.Checked = Convert.ToBoolean(NotificationIcon.ZenC6Core);
            checkBoxC6Package.Checked = Convert.ToBoolean(NotificationIcon.ZenC6Package);
            checkBoxCpb.Checked = Convert.ToBoolean(NotificationIcon.ZenCorePerfBoost);

            checkBoxOc.Checked = Convert.ToBoolean(NotificationIcon.ZenOc);

            textBoxPPT.Text = NotificationIcon.ZenPPT.ToString();
            textBoxTDC.Text = NotificationIcon.ZenTDC.ToString();
            textBoxEDC.Text = NotificationIcon.ZenEDC.ToString();
            textBoxScalar.Text = NotificationIcon.ZenScalar.ToString();
        }

        public void SetSavedButton(bool state)
        {
            buttonSave.Enabled = state;
        }

        private void UpdateFids(object sender, EventArgs e)
        {

            for (int i = 0; i < PstateFid.Length; i++)
            {
                // Get current FID
                byte fid = Convert.ToByte(NotificationIcon.Pstate[i] & 0xFF);
                try
                {
                    fid = ((CustomListItem)PstateFid[i].SelectedItem).value;
                }
                catch (Exception) { };

                // Get current did
                byte did = Convert.ToByte((NotificationIcon.Pstate[i] >> 8) & 0x3F);
                try
                {
                    CustomListItem item = (CustomListItem)PstateDid[i].SelectedItem;
                    did = item.value;
                }
                catch (Exception ex) { }

                // Calculate old frequency

                PstateFid[i].Items.Clear();

                int select = 0;
                int k = 0;
                for (byte j = FID_MAX; j >= FID_MIN; j--)
                {
                    double freq = (25 * j / (did * 12.5));

                    if (FREQ_MAX >= freq && freq >= FREQ_MIN)
                    {
                        CustomListItem item = new CustomListItem(k++, j, freq.ToString("F2") + "x");
                        PstateFid[i].Items.Add(item);
                        if (item.value == fid) select = item.id;
                        /*double diff = Math.Abs(freq - freq_prev);
                        if (diff < diff_last) select = PstateFid[i].Items.Count - 1;
                        diff_last = diff;*/
                    }
                }

                PstateFid[i].SelectedIndex = select;
            }
        }

        void ButtonApplyClick(object sender, EventArgs e)
        {
            // Apply new settings
            try
            {
                for (int i = 0; i < PstateFid.Length; i++)
                {
                    // UInt64 en = Convert.ToUInt64(PstateEn[i].Checked);
                    UInt64 en = Convert.ToUInt64(checkBoxOc.Checked);
                    UInt64 fid = Convert.ToUInt64(((CustomListItem)PstateFid[i].SelectedItem).value);
                    UInt64 did = Convert.ToUInt64(((CustomListItem)PstateDid[i].SelectedItem).value);
                    UInt64 vid = Convert.ToUInt64(((CustomListItem)PstateVid[i].SelectedItem).value);

                    UInt64 ps = NotificationIcon.di.MemRead(DataInterface.REG_P0 + i);

                    ps &= 0x7FFFFFFFFFC00000;
                    ps |= (en & 1) << 63 | (vid & 0xFF) << 14 | (did & 0xFF) << 8 | fid & 0xFF;

                    NotificationIcon.di.MemWrite(DataInterface.REG_P0 + i, ps);

                    PstateFid[i].Enabled = checkBoxOc.Checked;
                    PstateVid[i].Enabled = checkBoxOc.Checked;
                }

                UInt64 flags = 0;
                if (checkBoxApplyOnStart.Checked) flags |= DataInterface.FLAG_APPLY_AT_START;
                if (checkBoxGuiOnStart.Checked) flags |= DataInterface.FLAG_TRAY_ICON_AT_START;
                if (checkBoxP80temp.Checked) flags |= DataInterface.FLAG_P80_TEMP_EN;
                if (checkBoxC6Core.Checked) flags |= DataInterface.FLAG_C6CORE;
                if (checkBoxC6Package.Checked) flags |= DataInterface.FLAG_C6PACKAGE;
                if (checkBoxCpb.Checked) flags |= DataInterface.FLAG_CPB;
                if (checkBoxOc.Checked) flags |= DataInterface.FLAG_OC;

                if (!int.TryParse(textBoxPPT.Text, out int ppt))
                {
                    MessageBox.Show("Bad PPT value.");
                    return;
                }
                if (!int.TryParse(textBoxTDC.Text, out int tdc))
                {
                    MessageBox.Show("Bad TDC value.");
                    return;
                }
                if (!int.TryParse(textBoxEDC.Text, out int edc))
                {
                    MessageBox.Show("Bad EDC value.");
                    return;
                }
                if (!int.TryParse(textBoxScalar.Text, out int scalar) || scalar < 1 || scalar > 10)
                {
                    MessageBox.Show("Bad Scalar value.");
                    return;
                }

                NotificationIcon.di.MemWrite(DataInterface.REG_PPT, (UInt64)ppt);
                NotificationIcon.di.MemWrite(DataInterface.REG_TDC, (UInt64)tdc);
                NotificationIcon.di.MemWrite(DataInterface.REG_EDC, (UInt64)edc);
                NotificationIcon.di.MemWrite(DataInterface.REG_SCALAR, (UInt64)scalar);

                NotificationIcon.di.MemWrite(DataInterface.REG_PERF_BIAS, (UInt64)comboBoxPerfbias.SelectedIndex);

                NotificationIcon.di.MemWrite(DataInterface.REG_CLIENT_FLAGS, flags);

                // Send update flag command
                NotificationIcon.Execute(DataInterface.NOTIFY_CLIENT_FLAGS, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SettingsFormMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void CheckBoxSystemStartupCheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxApplyOnStart.Checked) checkBoxGuiOnStart.Checked = true;
        }
        void CheckBoxStartWithGUICheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxGuiOnStart.Checked) checkBoxApplyOnStart.Checked = false;
        }

        private void buttonDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                // Send restore command
                NotificationIcon.Execute(DataInterface.NOTIFY_RESTORE, true);

                buttonSave.Enabled = !NotificationIcon.SettingsSaved;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Send restore command
                NotificationIcon.Execute(DataInterface.NOTIFY_SAVE, false);

                buttonSave.Enabled = !NotificationIcon.SettingsSaved;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxPerfenh_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxPerfenh.SelectedIndex)
            {
                case (int)NotificationIcon.PerfEnh.None:
                    textBoxPPT.Text = "0";
                    textBoxTDC.Text = "0";
                    textBoxEDC.Text = "0";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = true;
                    break;
                case (int)NotificationIcon.PerfEnh.Default:
                    textBoxPPT.Text = "142";
                    textBoxTDC.Text = "95";
                    textBoxEDC.Text = "140";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = true;
                    break;
                case (int)NotificationIcon.PerfEnh.Level1:
                    textBoxPPT.Text = "1000";
                    textBoxTDC.Text = "1000";
                    textBoxEDC.Text = "150";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = true;
                    break;
                case (int)NotificationIcon.PerfEnh.Level2:
                    textBoxPPT.Text = "1000";
                    textBoxTDC.Text = "1000";
                    textBoxEDC.Text = "1000";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = true;
                    break;
                case (int)NotificationIcon.PerfEnh.Level3_OC:
                    textBoxPPT.Text = "1000";
                    textBoxTDC.Text = "1000";
                    textBoxEDC.Text = "150";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = false;
                    break;
                default:
                    break;
            }
        }
    }
}
