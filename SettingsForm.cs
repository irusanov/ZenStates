using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZenStates
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

        private static bool isZen2 = NotificationIcon.di.MemRead(DataInterface.REG_CPU_TYPE) >= 7; // CPUType.Matisse = 7

        private const int PSTATES = 3;
        private const int FID_MAX = 0xFF;
        private const int FID_MIN = 0x10;
        private const int VID_MAX = 0xA9;
        private const int VID_MIN = 0x00;

        private const double FREQ_MAX = 70;
        private const double FREQ_MIN = 5.5;

        // All auto / pre-Matisse
        CheckBox[] PstateEn = new CheckBox[PSTATES];
        ComboBox[] PstateFid = new ComboBox[PSTATES];
        ComboBox[] PstateDid = new ComboBox[PSTATES];
        ComboBox[] PstateVid = new ComboBox[PSTATES];

        // All auto / Matisse
        Label[] BoostFreqLabel = new Label[3];
        ComboBox[] BoostFreqFid = new ComboBox[3];
        ComboBox[] BoostFreqDid = new ComboBox[3];

        // Manual control
        ComboBox PstateOcFid;
        ComboBox PstateOcDid;
        ComboBox PstateOcVid;

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
            new CustomListItem(2, "Cinebench R11.5"),
            new CustomListItem(3, "Cinebench R15 / R20"),
            new CustomListItem(4, "Geekbench 3 / AIDA64")
        };

        CustomListItem[] PERFENH = {
            new CustomListItem(0, "None"),
            new CustomListItem(1, "Default"),
            new CustomListItem(2, "Level 1"),
            new CustomListItem(3, "Level 2"),
            new CustomListItem(4, "Level 3 (OC)"),
            new CustomListItem(5, "Level 4 (OC)")
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
            biosVersionLabel.Text = NotificationIcon.biosVersion;
            smuVersionLabel.Text = NotificationIcon.smuVersion;
            label1.Text = "version " + Application.ProductVersion; //.Substring(0, Application.ProductVersion.LastIndexOf("."));

            // Auto Pstate controls
            PopulateAutoControls();
            PopulateManualControls();

            foreach (CustomListItem item in PERFBIAS)
            {
                comboBoxPerfbias.Items.Add(item);
            }

            foreach (CustomListItem item in PERFENH)
            {
                comboBoxPerfenh.Items.Add(item);
            }

            ResetValues();

            MessageBox.Show(Convert.ToString(NotificationIcon.smuVersionInt));

            // if (isZen2)
            if ((NotificationIcon.smuVersionInt > 2583 && NotificationIcon.cpuType <= 4)
                 || (NotificationIcon.smuVersionInt > 4316 && NotificationIcon.cpuType > 4 && NotificationIcon.cpuType <= 6))

            {
                textBoxPPT.Enabled = false;
                textBoxTDC.Enabled = false;
                textBoxEDC.Enabled = false;
                labelPPT.Enabled = false;
                labelTDC.Enabled = false;
                labelEDC.Enabled = false;
                checkBoxSmuPL.Enabled = false;
                comboBoxPerfenh.Enabled = false;
                labelPerfEnh.Enabled = false;
            }

            // Save button
            SetSavedButton(false);
            //SwitchControlMode(NotificationIcon.ZenOc);

            // ToolTip
            ToolTip toolTip = new ToolTip();

            toolTip.SetToolTip(labelPPT, "Socket Power (W)");
            toolTip.SetToolTip(labelEDC, "Electrical Design Current (A)");
            toolTip.SetToolTip(labelTDC, "Thermal Design Current (A)");

            //if (isZen2)
            if ((NotificationIcon.smuVersionInt > 2583 && NotificationIcon.cpuType <= 4)
                 || (NotificationIcon.smuVersionInt > 4316 && NotificationIcon.cpuType > 4 && NotificationIcon.cpuType <= 6))

            {
                toolTip.SetToolTip(checkBoxSmuPL, "It's currently not working with Zen2 and new AGESA");
                toolTip.SetToolTip(comboBoxPerfenh, "It's currently not working with Zen2 and new AGESA");
                toolTip.SetToolTip(checkBoxSmuPL, "It's currently not working with Zen2 and new AGESA");
            }
        }

        public void PopulateAutoControls()
        {
            if (isZen2)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Enable checkbox
                    BoostFreqLabel[i] = new Label
                    {
                        Size = new System.Drawing.Size(120, 27),
                        Location = new System.Drawing.Point(7, 7 + i * 25),
                    };

                    // FID combobox
                    BoostFreqFid[i] = new ComboBox
                    {
                        Size = new System.Drawing.Size(62, 21),
                        Location = new System.Drawing.Point(130, 7 + i * 25)
                    };

                    // DID combobox
                    BoostFreqDid[i] = new ComboBox
                    {
                        Size = new System.Drawing.Size(62, 21),
                        Location = new System.Drawing.Point(202, 7 + i * 25),
                    };
                    foreach (CustomListItem item in DIVIDERS)
                    {
                        BoostFreqDid[i].Items.Add(item);
                    }
                    BoostFreqDid[i].SelectedIndexChanged += UpdateFids;

                    this.panelAutoMode.Controls.Add(BoostFreqLabel[i]);
                    this.panelAutoMode.Controls.Add(BoostFreqFid[i]);
                    this.panelAutoMode.Controls.Add(BoostFreqDid[i]);
                }

                BoostFreqLabel[0].Text = "Max Boost Single Core";
                BoostFreqLabel[1].Text = "Max Boost All Cores";
                BoostFreqLabel[2].Text = "Idle Frequency";
            }
            else
            {
                for (int i = 0; i < PSTATES; i++)
                {
                    // Enable checkbox
                    PstateEn[i] = new CheckBox
                    {
                        Text = "P" + i.ToString(),
                        Size = new System.Drawing.Size(40, 21),
                        Location = new System.Drawing.Point(10, 7 + i * 25),
                    };

                    // FID combobox
                    PstateFid[i] = new ComboBox
                    {
                        Size = new System.Drawing.Size(70, 21),
                        Location = new System.Drawing.Point(50, 7 + i * 25)
                    };

                    // DID combobox
                    PstateDid[i] = new ComboBox
                    {
                        Size = new System.Drawing.Size(62, 21),
                        Location = new System.Drawing.Point(130, 7 + i * 25),
                    };
                    foreach (CustomListItem item in DIVIDERS)
                    {
                        PstateDid[i].Items.Add(item);
                    }
                    PstateDid[i].SelectedIndexChanged += UpdateFids;

                    // VID combobox
                    PstateVid[i] = new ComboBox
                    {
                        Size = new System.Drawing.Size(62, 21),
                        Location = new System.Drawing.Point(202, 7 + i * 25)
                    };
                    int k = 0;
                    for (byte j = VID_MIN; j <= VID_MAX; j++)
                    {
                        double voltage = 1.55 - j * 0.00625;
                        CustomListItem item = new CustomListItem(k++, j, voltage.ToString("F3") + "V");
                        PstateVid[i].Items.Add(item);
                    }

                    this.panelAutoMode.Controls.Add(PstateEn[i]);
                    this.panelAutoMode.Controls.Add(PstateFid[i]);
                    this.panelAutoMode.Controls.Add(PstateDid[i]);
                    this.panelAutoMode.Controls.Add(PstateVid[i]);
                }
            }
        }

        public void PopulateManualControls()
        {
            // FID combobox
            PstateOcFid = new ComboBox
            {
                Size = new System.Drawing.Size(113, 21),
                Location = new System.Drawing.Point(7, 7)
            };

            // DID combobox
            PstateOcDid = new ComboBox
            {
                Size = new System.Drawing.Size(55, 21),
                Location = new System.Drawing.Point(130, 7),
            };
            foreach (CustomListItem item in DIVIDERS)
            {
                PstateOcDid.Items.Add(item);
            }
            PstateOcDid.SelectedIndexChanged += UpdateOcFid;

            // VID combobox
            PstateOcVid = new ComboBox
            {
                Size = new System.Drawing.Size(70, 21),
                Location = new System.Drawing.Point(195, 7)
            };
            int k = 0;
            for (byte j = VID_MIN; j <= VID_MAX; j++)
            {
                double voltage = 1.55 - j * 0.00625;
                CustomListItem item = new CustomListItem(k++, j, voltage.ToString("F3") + "V");
                PstateOcVid.Items.Add(item);
            }

            this.panelManualMode.Controls.Add(PstateOcFid);
            this.panelManualMode.Controls.Add(PstateOcDid);
            this.panelManualMode.Controls.Add(PstateOcVid);
        }

        public void SwitchControlMode(bool manual)
        {
            radioAutoControl.Checked = !manual;
            radioManualControl.Checked = manual;
        }

        public void ResetValues()
        {
            try
            {
                // PerfBias
                comboBoxPerfbias.SelectedIndex = (int)NotificationIcon.perfBias;
                //comboBoxPerfenh.SelectedIndex = (int)NotificationIcon.PerfEnh.None;

                if (isZen2)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // DID
                        byte did = Convert.ToByte((NotificationIcon.BoostFreq[i] >> 8) & 0x3F);
                        foreach (CustomListItem item in BoostFreqDid[i].Items)
                        {
                            if (item.value == did) BoostFreqDid[i].SelectedIndex = item.id;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < PSTATES; i++)
                    {
                        // PstateEn
                        PstateEn[i].Checked = Convert.ToBoolean((NotificationIcon.Pstate[i] >> 63) & 0x1);

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
                    }
                }

                // Populate OC FID, VID and DID from Pstate0
                // OC DID
                byte ocDid = Convert.ToByte((NotificationIcon.PstateOc >> 8) & 0x3F);
                foreach (CustomListItem item in PstateOcDid.Items)
                {
                    if (item.value == ocDid) PstateOcDid.SelectedIndex = item.id;
                }

                // OC VID
                byte ocVid = Convert.ToByte((NotificationIcon.PstateOc >> 14) & 0xFF);
                foreach (CustomListItem item in PstateOcVid.Items)
                {
                    if (item.value == ocVid) PstateOcVid.SelectedIndex = item.id;
                }

                // FID/Ratios
                UpdateFids(null, null);
                UpdateOcFid(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Checkboxes
            checkBoxApplyOnStart.Checked = NotificationIcon.ApplyAtStart;
            checkBoxGuiOnStart.Checked = NotificationIcon.TrayIconAtStart;
            checkBoxP80temp.Checked = NotificationIcon.P80Temp;

            checkBoxC6Core.Checked = NotificationIcon.ZenC6Core;
            checkBoxC6Package.Checked = NotificationIcon.ZenC6Package;
            checkBoxCpb.Checked = NotificationIcon.ZenCorePerfBoost;

            bool isManual = NotificationIcon.ZenOc;
            radioAutoControl.Checked = !isManual;
            radioManualControl.Checked = isManual;

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
            if (isZen2)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Get current FID
                    byte fid = Convert.ToByte(NotificationIcon.BoostFreq[i] & 0xFF);
                    try
                    {
                        if (!NotificationIcon.SettingsReset) fid = ((CustomListItem)BoostFreqFid[i].SelectedItem).value;
                    }
                    catch (Exception) { };

                    // Get current did
                    byte did = Convert.ToByte((NotificationIcon.BoostFreq[i] >> 8) & 0x3F);
                    try
                    {
                        if (!NotificationIcon.SettingsReset) did = ((CustomListItem)BoostFreqDid[i].SelectedItem).value;
                    }
                    catch (Exception ex) { }

                    BoostFreqFid[i].Items.Clear();

                    int select = 0;
                    int k = 0;
                    for (byte j = FID_MAX; j >= FID_MIN; j--)
                    {
                        double freq = (25 * j / (did * 12.5));

                        if (FREQ_MAX >= freq && freq >= FREQ_MIN)
                        {
                            CustomListItem item = new CustomListItem(k++, j, freq.ToString("F2") + "x");
                            BoostFreqFid[i].Items.Add(item);
                            if (item.value == fid) select = item.id;
                        }
                    }

                    BoostFreqFid[i].SelectedIndex = select;
                }
            }
            else
            {
                for (int i = 0; i < PSTATES; i++)
                {
                    // Get current FID
                    byte fid = Convert.ToByte(NotificationIcon.Pstate[i] & 0xFF);
                    try
                    {
                        if (!NotificationIcon.SettingsReset) fid = ((CustomListItem)PstateFid[i].SelectedItem).value;
                    }
                    catch (Exception) { };

                    // Get current did
                    byte did = Convert.ToByte((NotificationIcon.Pstate[i] >> 8) & 0x3F);
                    try
                    {
                        if (!NotificationIcon.SettingsReset) did = ((CustomListItem)PstateDid[i].SelectedItem).value;
                    }
                    catch (Exception ex) { }

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
        }

        private void UpdateOcFid(object sender, EventArgs e)
        {
            // Get current FID
            byte fid = Convert.ToByte(NotificationIcon.PstateOc & 0xFF);
            try
            {
                if (!NotificationIcon.SettingsReset) fid = ((CustomListItem)PstateOcFid.SelectedItem).value;
            }
            catch (Exception) { };

            // Get current did
            byte did = Convert.ToByte((NotificationIcon.PstateOc >> 8) & 0x3F);
            try
            {
                if (!NotificationIcon.SettingsReset) did = ((CustomListItem)PstateOcDid.SelectedItem).value;
            }
            catch (Exception ex) { }

            // Calculate old frequency

            PstateOcFid.Items.Clear();

            int select = 0;
            int k = 0;
            for (byte j = FID_MAX; j >= FID_MIN; j--)
            {
                double freq = (25 * j / (did * 12.5));

                if (FREQ_MAX >= freq && freq >= FREQ_MIN)
                {
                    CustomListItem item = new CustomListItem(k++, j, freq.ToString("F2") + "x");
                    PstateOcFid.Items.Add(item);
                    if (item.value == fid) select = item.id;
                    /*double diff = Math.Abs(freq - freq_prev);
                    if (diff < diff_last) select = PstateFid[i].Items.Count - 1;
                    diff_last = diff;*/
                }
            }

            PstateOcFid.SelectedIndex = select;
        }

        void ButtonApplyClick(object sender, EventArgs e)
        {
            // Apply new settings
            try
            {
                if (radioManualControl.Checked)
                {
                    UInt64 ocFid = Convert.ToUInt64(((CustomListItem)PstateOcFid.SelectedItem).value);
                    UInt64 ocDid = Convert.ToUInt64(((CustomListItem)PstateOcDid.SelectedItem).value);
                    UInt64 ocVid = Convert.ToUInt64(((CustomListItem)PstateOcVid.SelectedItem).value);
                    UInt64 ocPs = NotificationIcon.di.MemRead(DataInterface.REG_PSTATE_OC);

                    ocPs &= 0xFFFFFFFFFFC00000;
                    ocPs |= (ocVid & 0xFF) << 14 | (ocDid & 0xFF) << 8 | ocFid & 0xFF;

                    NotificationIcon.di.MemWrite(DataInterface.REG_PSTATE_OC, ocPs);
                    if (!isZen2)
                    {
                        for (int i = 0; i < PSTATES - 1; i++)
                        {
                            UInt64 en = Convert.ToUInt64(PstateEn[i].Checked);
                            ocPs |= (en & 1) << 63;
                            NotificationIcon.di.MemWrite(DataInterface.REG_P0 + i, ocPs);
                            PstateFid[i].SelectedItem = PstateOcFid.SelectedItem;
                            PstateDid[i].SelectedItem = PstateOcDid.SelectedItem;
                            PstateVid[i].SelectedItem = PstateOcVid.SelectedItem;
                        }
                    }
                }
                else
                {
                    if (isZen2)
                    {
                        // Pstate2
                        UInt64 fid = Convert.ToUInt64(((CustomListItem)BoostFreqFid[2].SelectedItem).value);
                        UInt64 did = Convert.ToUInt64(((CustomListItem)BoostFreqDid[2].SelectedItem).value);

                        UInt64 ps = NotificationIcon.di.MemRead(DataInterface.REG_P2);

                        ps &= 0xFFFFFFFFFFFF0000;
                        ps |= (did & 0xFF) << 8 | fid & 0xFF;

                        NotificationIcon.di.MemWrite(DataInterface.REG_P2, ps);

                        // Max boost frequencies
                        for (int i = 0; i < 2; i++)
                        {
                            fid = Convert.ToUInt64(((CustomListItem)BoostFreqFid[i].SelectedItem).value);
                            did = Convert.ToUInt64(((CustomListItem)BoostFreqDid[i].SelectedItem).value);
                            // double freq = (25 * boostFid / (boostDid * 12.5)) * 100;
                            ps = NotificationIcon.di.MemRead(DataInterface.REG_BOOST_FREQ_0 + i);

                            ps &= 0xFFFFFFFFFFFF0000;
                            ps |= (did & 0xFF) << 8 | fid & 0xFF;

                            NotificationIcon.di.MemWrite(DataInterface.REG_BOOST_FREQ_0 + i, ps);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < PSTATES; i++)
                        {
                            UInt64 en = Convert.ToUInt64(PstateEn[i].Checked);

                            UInt64 fid = Convert.ToUInt64(((CustomListItem)PstateFid[i].SelectedItem).value);
                            UInt64 did = Convert.ToUInt64(((CustomListItem)PstateDid[i].SelectedItem).value);
                            UInt64 vid = Convert.ToUInt64(((CustomListItem)PstateVid[i].SelectedItem).value);

                            UInt64 ps = NotificationIcon.di.MemRead(DataInterface.REG_P0 + i);

                            ps &= 0x7FFFFFFFFFC00000;
                            ps |= (en & 1) << 63 | (vid & 0xFF) << 14 | (did & 0xFF) << 8 | fid & 0xFF;

                            NotificationIcon.di.MemWrite(DataInterface.REG_P0 + i, ps);
                        }

                        NotificationIcon.di.MemWrite(DataInterface.REG_PSTATE_OC , NotificationIcon.di.MemRead(DataInterface.REG_P0));
                        PstateOcFid.SelectedItem = PstateFid[0].SelectedItem;
                        PstateOcDid.SelectedItem = PstateDid[0].SelectedItem;
                        PstateOcVid.SelectedItem = PstateVid[0].SelectedItem;
                    }
                }
                
                UInt64 flags = 0;
                if (checkBoxApplyOnStart.Checked) flags |= DataInterface.FLAG_APPLY_AT_START;
                if (checkBoxGuiOnStart.Checked) flags |= DataInterface.FLAG_TRAY_ICON_AT_START;
                if (checkBoxP80temp.Checked) flags |= DataInterface.FLAG_P80_TEMP_EN;
                if (checkBoxC6Core.Checked) flags |= DataInterface.FLAG_C6CORE;
                if (checkBoxC6Package.Checked) flags |= DataInterface.FLAG_C6PACKAGE;
                if (checkBoxCpb.Checked) flags |= DataInterface.FLAG_CPB;
                if (radioManualControl.Checked) flags |= DataInterface.FLAG_OC;

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
                case (int)NotificationIcon.PerfEnh.Level4_OC:
                    textBoxPPT.Text = "1000";
                    textBoxTDC.Text = "1000";
                    textBoxEDC.Text = "1000";
                    textBoxScalar.Text = "1";
                    checkBoxSmuPL.Checked = false;
                    break;
                default:
                    break;
            }
        }

        private void RadioAutoControl_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAutoControl.Checked)
            {
                panelAutoMode.Show();
                panelManualMode.Hide();
            }
        }

        private void RadioManualControl_CheckedChanged(object sender, EventArgs e)
        {
            if (radioManualControl.Checked)
            {
                panelManualMode.Show();
                panelAutoMode.Hide();
            }
        }
    }
}
