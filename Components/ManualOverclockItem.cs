using System;
using System.Windows.Forms;

namespace ZenStates.Components
{
    public partial class ManualOverclockItem : UserControl
    {
        private double multi;
        private int cores;
        private byte vid;
        private int selectedCoreIndex;

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
            for (int core = 0; core < Cores; ++core)
            {
                Console.WriteLine($"ccd: {core / 8}, ccx: {core / 4}, core: {core}");
                l.Add(new CoreListItem(core / 8, core / 4, core));
            }

            l.Add("All Cores");
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
            PopulateFrequencyList(comboBoxMulti.Items);
            PopulateVidItems();

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(comboBoxCore, "All physical cores are listed. The app can't enumerate active cores only.");
        }

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
                comboBoxCore.Items.Clear();
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

        public bool AllCores { get => comboBoxCore.SelectedIndex == cores; }
        public int CoreMask
        {
            get
            {
                CoreListItem i = comboBoxCore.SelectedItem as CoreListItem;
                Console.WriteLine($"SET - ccd: {i.CCD}, ccx: {i.CCX }, core: {i.CORE % 4 }");
                return ((i.CCD << 4 | i.CCX % 2 & 0xF) << 4 | i.CORE % 4 & 0xF) << 20;
            }
        }

        public bool Changed
        {
            get => vid != Vid || multi != Multi || selectedCoreIndex != SelectedCoreIndex;
        }

        public void Reset()
        {
            PopulateFrequencyList(comboBoxMulti.Items);
            PopulateVidItems();

            Vid = vid;
            Multi = multi;
            comboBoxCore.SelectedIndex = selectedCoreIndex;
        }

        public void UpdateState()
        {
            vid = Vid;
            multi = Multi;
            selectedCoreIndex = comboBoxCore.SelectedIndex;
        }
        #endregion
    }
}
