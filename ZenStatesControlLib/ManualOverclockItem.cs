using System;
using System.Windows.Forms;

namespace ZenStates.Components
{
    public partial class ManualOverclockItem : UserControl
    {
        private double multi = Constants.MULTI_MIN;
        private int cores = 0;
        private byte vid = Constants.VID_MAX;
        private int selectedCoreIndex = -1;
        private bool ocmode = false;
        private bool prochot = false;

        #region Private Methods
        private void PopulateFrequencyList(ComboBox.ObjectCollection l)
        {
            for (double multi = Constants.MULTI_MAX; multi >= Constants.MULTI_MIN; multi -= Constants.MULTI_STEP)
            {
                l.Add(new FrequencyListItem(multi, string.Format("x{0:0.00}", multi)));
            }
        }

        private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            for (int core = 0; core < Cores; ++core)
            {
                Console.WriteLine($"ccd: {core / 8}, ccx: {core / 4}, core: {core}");
                l.Add(new CoreListItem(core / 8, core / 4, core, string.Format("Core {0}", core)));
            }

            l.Add("All Cores");
        }

        private void PopulateCCXList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            for (int core = 0; core < Cores; core += 4)
            {
                l.Add(new CoreListItem(core / 8, core / 4, 0, string.Format("CCX {0}", core / 4)));
            }

            l.Add("All CCX");
        }

        private void PopulateVidItems()
        {
            for (uint i = Constants.VID_MIN; i <= Constants.VID_MAX; i++)
            {
                double voltage = 1.55 - i * 0.00625;
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
                PopulateCCDList(comboBoxCore.Items);
                comboBoxCore.SelectedIndex = value;

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
        public int CoreMask
        {
            get
            {
                CoreListItem i = comboBoxCore.SelectedItem as CoreListItem;
                // Console.WriteLine($"SET - ccd: {i.CCD}, ccx: {i.CCX }, core: {i.CORE % 4 }");
                return ((i.CCD << 4 | i.CCX % 2 & 0xF) << 4 | i.CORE % 4 & 0xF) << 20;
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

        public bool CCXMode => checkBoxCCX.Checked;

        public bool Changed => vid != Vid || multi != Multi || selectedCoreIndex != SelectedCoreIndex;

        public bool ModeChanged => ocmode != OCmode;

        public bool VidChanged => vid != Vid;

        public bool ProchotEnabled {
            get => prochot;
            set
            {
                prochot = value; 
                checkBoxProchot.Checked = value;
            } 
        }

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

            checkBoxCCX.Enabled = OCmode;
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

            checkBoxCCX.Enabled = OCmode;
            checkBoxProchot.Enabled = OCmode;
            checkBoxSlowMode.Enabled = OCmode;
        }

        private void CheckBoxCCX_Click(object sender, EventArgs e)
        {
            if (checkBoxCCX.Checked)
            {
                PopulateCCXList(comboBoxCore.Items);
            }
            else
            {
                PopulateCCDList(comboBoxCore.Items);
            }

            comboBoxCore.SelectedIndex = comboBoxCore.Items.Count - 1;
        }

        private void CheckBoxSlowMode_Click(object sender, EventArgs e)
        {
            SlowModeClicked?.Invoke(checkBoxSlowMode, e);
        }

        private void CheckBoxProchot_Click(object sender, EventArgs e)
        {
            ProchotClicked?.Invoke(checkBoxProchot, e);
        }
    }
}
