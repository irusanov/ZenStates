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

        private void PopulateCoreList(ComboBox.ObjectCollection l)
        {
            l.Clear();

            for (int i = 0; i < Cores; ++i)
            {
                int ccd = i / 8;
                int ccx = i / 4 - 2 * ccd;
                int core = i % 4;

                Console.WriteLine($"ccd: {ccd}, ccx: {ccx}, core: {core}");
                l.Add(new CoreListItem(ccd, ccx, core, string.Format("Core {0}", i)));
            }

            l.Add("All Cores");
        }

        private void PopulateCCXList(ComboBox.ObjectCollection l)
        {
            l.Clear();
            Console.WriteLine("PopulateCCXList");
            for (int i = 0; i < Cores; i += 4)
            {
                int ccd = i / 8;
                int ccx = i / 4 - 2 * ccd;

                Console.WriteLine($"ccd: {ccd}, ccx: {ccx}");
                l.Add(new CoreListItem(ccd, ccx, 0, string.Format("CCX {0}", i / 4)));
            }

            l.Add("All CCX");
        }

        private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            l.Clear();
            Console.WriteLine("PopulateCCDList");
            for (int i = 0; i < Cores; i += 8)
            {
                int ccd = i / 8;

                Console.WriteLine($"ccd: {ccd}");
                l.Add(new CoreListItem(ccd, 0, 0, string.Format("CCD {0}", i / 8)));
            }

            l.Add("All CCD");
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
                PopulateCoreList(comboBoxCore.Items);
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

        public int ControlMode => comboBoxControlMode.SelectedIndex;

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
