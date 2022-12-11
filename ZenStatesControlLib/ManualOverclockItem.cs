using System;
using System.Windows.Forms;
using ZenStates.Core;

namespace ZenStates.Components
{
    public partial class ManualOverclockItem : UserControl
    {
        private double multi = Constants.MULTI_MIN;
        private int cores = 0;
        private int coresInCcx = 4;
        private byte vid = Constants.VID_MAX;
        private int selectedCoreIndex = -1;
        private bool ocmode = false;
        private bool prochot = false;

        #region Private Methods
        private void PopulateFrequencyList(ComboBox.ObjectCollection l)
        {
            for (double m = Constants.MULTI_MAX; m >= Constants.MULTI_MIN; m -= Constants.MULTI_STEP)
            {
                l.Add(new FrequencyListItem(m, string.Format("x{0:0.00}", m)));
            }
        }

        private void PopulateCoreList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            int coreNum = 0;

            for (int i = 0; i < Cores; ++i)
            {
                bool enabled = ((~coreDisableMap >> i) & 1) == 1;
                if (enabled)
                {
                    int ccd = i / Constants.CCD_SIZE;
                    int ccx = i / coresInCcx - CcxInCcd * ccd;
                    int core = i % coresInCcx;

                    Console.WriteLine($"ccd: {ccd}, ccx: {ccx}, core: {core}");
                    l.Add(new CoreListItem(ccd, ccx, core, string.Format("Core {0}", coreNum++)));
                }
            }

            l.Add("All Cores");
        }

        private void PopulateCCXList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            for (int i = 0; i < Cores; i += coresInCcx)
            {
                int ccd = i / Constants.CCD_SIZE;
                int ccx = i / coresInCcx - CcxInCcd * ccd;

                Console.WriteLine($"ccd: {ccd}, ccx: {ccx}");
                l.Add(new CoreListItem(ccd, ccx, 0, string.Format("CCX {0}", i / coresInCcx)));
            }

            l.Add("All CCX");
        }

        private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            for (int i = 0; i < Cores; i += Constants.CCD_SIZE)
            {
                int ccd = i / Constants.CCD_SIZE;

                Console.WriteLine($"ccd: {ccd}");
                l.Add(new CoreListItem(ccd, 0, 0, string.Format("CCD {0}", ccd)));
            }

            l.Add("All CCD");
        }

        private void PopulateVidItems()
        {
            for (uint i = Constants.VID_MIN; i <= Constants.VID_MAX; i++)
            {
                double voltage = SVIVersion == 3 ? Utils.VidToVoltageSVI3(i) : Utils.VidToVoltage(i);
                CustomListItem item = new CustomListItem(i, string.Format("{0:0.000}V", voltage));
                comboBoxVid.Items.Add(item);
                // if (i == CpuVid) comboBoxVoltage.SelectedItem = item;
            }
        }

        #endregion

        public ManualOverclockItem()
        {
            InitializeComponent();
            Reset();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(comboBoxCore, "All physical cores are listed. The app can't enumerate active cores only.");
        }

        public event EventHandler SlowModeClicked;
        public event EventHandler ProchotClicked;

        #region Properties
        public uint coreDisableMap { get; set; }
        public int CcxInCcd { get; set; }

        public double Multi
        {
            get => (comboBoxMulti.SelectedItem as FrequencyListItem).Multi;
            set
            {
                multi = value;
                foreach (FrequencyListItem item in comboBoxMulti.Items)
                {
                    if (item.Multi == value)
                        comboBoxMulti.SelectedItem = item;
                }
            }
        }

        public int Cores
        {
            get => cores;
            set
            {
                cores = value;
                if (CcxInCcd > 0)
                {
                    coresInCcx = Constants.CCD_SIZE / CcxInCcd;
                }
                PopulateCoreList(comboBoxCore.Items);
                comboBoxCore.SelectedIndex = comboBoxCore.Items.Count - 1;

                if (value > 0)
                {
                    selectedCoreIndex = value;
                }
            }
        }

        public int SelectedCoreIndex
        {
            get => comboBoxCore.SelectedIndex;
        }

        public byte Vid
        {
            get => (byte)(comboBoxVid.SelectedItem as CustomListItem).Value;
            set
            {
                vid = value;
                foreach (CustomListItem item in comboBoxVid.Items)
                {
                    if (item.Value == value)
                        comboBoxVid.SelectedItem = item;
                }
            }
        }

        public bool AllCores { get => comboBoxCore.SelectedIndex == comboBoxCore.Items.Count - 1; }
        public uint CoreMask
        {
            get
            {
                CoreListItem i = comboBoxCore.SelectedItem as CoreListItem;
                // Console.WriteLine($"SET - ccd: {i.CCD}, ccx: {i.CCX }, core: {i.CORE % 4 }");
                return Convert.ToUInt32(((i.CCD << 4 | i.CCX % CcxInCcd & 0xF) << 4 | i.CORE % coresInCcx & 0xF) << 20);
            }
        }

        public bool OCmode
        {
            get => checkBoxOCModeEnabled.Checked;
            set
            {
                checkBoxOCModeEnabled.Checked = value;
                ocmode = value;
            }
        }

        public int ControlMode => comboBoxControlMode.SelectedIndex;

        public bool Changed => vid != Vid || multi != Multi || selectedCoreIndex != SelectedCoreIndex;

        public bool ModeChanged => ocmode != OCmode;

        public bool VidChanged => vid != Vid;

        public bool ProchotEnabled
        {
            get => prochot;
            set
            {
                prochot = value;
                checkBoxProchot.Checked = value;
            }
        }

        public int SVIVersion { get; set; } = 2;
        public void Reset()
        {
            PopulateFrequencyList(comboBoxMulti.Items);
            PopulateVidItems();

            Vid = vid;
            Multi = multi;
            OCmode = ocmode;
            comboBoxCore.SelectedIndex = selectedCoreIndex;

            comboBoxCore.Enabled = OCmode;
            comboBoxMulti.Enabled = OCmode;
            comboBoxVid.Enabled = OCmode;

            comboBoxControlMode.Enabled = OCmode;
            comboBoxControlMode.SelectedIndex = 0;
            checkBoxProchot.Enabled = OCmode;
            checkBoxSlowMode.Enabled = OCmode;

            checkBoxProchot.Checked = ProchotEnabled;
        }

        public void UpdateState()
        {
            vid = Vid;
            multi = Multi;
            ocmode = OCmode;
            selectedCoreIndex = comboBoxCore.SelectedIndex;
        }
        #endregion

        private void CheckBoxOCModeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxCore.Enabled = OCmode;
            comboBoxMulti.Enabled = OCmode;
            comboBoxVid.Enabled = OCmode;

            comboBoxControlMode.Enabled = OCmode;
            checkBoxProchot.Enabled = OCmode;
            checkBoxSlowMode.Enabled = OCmode;
        }

        private void CheckBoxSlowMode_Click(object sender, EventArgs e)
        {
            SlowModeClicked?.Invoke(checkBoxSlowMode, e);
        }

        private void CheckBoxProchot_Click(object sender, EventArgs e)
        {
            ProchotClicked?.Invoke(checkBoxProchot, e);
        }

        private void ComboBoxControlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = comboBoxCore.Items;
            switch (comboBoxControlMode.SelectedIndex)
            {
                case 0:
                    PopulateCoreList(items);
                    break;
                case 1:
                    PopulateCCXList(items);
                    break;
                case 2:
                    PopulateCCDList(items);
                    break;
                default:
                    break;
            }

            comboBoxCore.SelectedIndex = items.Count - 1;
        }
    }
}
