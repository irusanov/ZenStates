using System;
using System.Windows.Forms;

namespace ZenStates.Components
{
    public partial class PstateItem : UserControl
    {
        private ulong IddDiv = 0;
        private ulong IddVal = 0;
        private ulong CpuVid = 0;
        private ulong CpuDid = 0;
        private ulong CpuFid = 0;

        private ulong InitialPstate = 0;
        //private ulong InitialCpuVid = 0;
        private ulong InitialCpuDid = 0;
        private ulong InitialCpuFid = 0;

        #region Private Methods
        private uint CalculateFidFromMultiDid(double targetMulti = 5.5, ulong did = 0)
        {
            uint targetFid = Convert.ToUInt32(Math.Floor(targetMulti * did * 12.5 / 25));

            if (targetFid < Constants.FID_MIN) targetFid = Constants.FID_MIN;
            if (targetFid > Constants.FID_MAX) targetFid = Constants.FID_MAX;

            return targetFid;
        }

        private double CalculateMulti(ulong did = 0, ulong fid = 1)
        {
            return 25 * did / (fid * 12.5);
        }

        private void PopulateVidItems()
        {
            comboBoxVID.Items.Clear();

            for (uint i = Constants.VID_MIN; i <= Constants.VID_MAX; i++)
            {
                double voltage = 1.55 - i * 0.00625;
                CustomListItem item = new CustomListItem(i, string.Format("{0:0.000}V", voltage));
                comboBoxVID.Items.Add(item);
                if (i == CpuVid) comboBoxVID.SelectedItem = item;
            }
        }

        private void PopulateDidItems()
        {
            comboBoxDID.Items.Clear();
            comboBoxDID.Items.Add(new CustomListItem(0x04, "/2"));
            comboBoxDID.Items.Add(new CustomListItem(0x08, "/4"));
            comboBoxDID.Items.Add(new CustomListItem(0x0A, "/5"));
            comboBoxDID.Items.Add(new CustomListItem(0x0B, "/5.5"));
            comboBoxDID.Items.Add(new CustomListItem(0x0C, "/6"));
            comboBoxDID.Items.Add(new CustomListItem(0x0D, "/6.5"));
            comboBoxDID.Items.Add(new CustomListItem(0x0E, "/7"));
            comboBoxDID.Items.Add(new CustomListItem(0x0F, "/7.5"));
            comboBoxDID.Items.Add(new CustomListItem(0x10, "/8"));
            comboBoxDID.Items.Add(new CustomListItem(0x11, "/8.5"));
            comboBoxDID.Items.Add(new CustomListItem(0x12, "/9"));
            comboBoxDID.Items.Add(new CustomListItem(0x13, "/9.5"));
            comboBoxDID.Items.Add(new CustomListItem(0x14, "/10"));

            foreach (CustomListItem item in comboBoxDID.Items)
            {
                if (item.Value == CpuDid) comboBoxDID.SelectedItem = item;
            }
        }

        private void PopulateFidItems()
        {
            comboBoxFID.Items.Clear();

            for (uint i = Constants.FID_MAX; i >= Constants.FID_MIN; i--)
            {
                double maxMulti = Constants.MULTI_MAX;
                double multi = CalculateMulti(i, CpuDid);
                if (multi <= maxMulti)
                {
                    CustomListItem item = new CustomListItem(i, string.Format("x{0:0.00}", multi));
                    comboBoxFID.Items.Add(item);
                    if (i == CpuFid) comboBoxFID.SelectedItem = item;
                }
            }
        }

        private uint GetSelectedItemValue(ComboBox comboBox)
        {
            if (comboBox != null)
                return (comboBox.SelectedItem as CustomListItem).Value;
            return 0;
        }
        #endregion

        public PstateItem()
        {
            InitializeComponent();

            comboBoxFID.Enabled = Checked;
            comboBoxDID.Enabled = Checked;
            comboBoxVID.Enabled = Checked;

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(checkBoxPstateEnabled, "Toggle this state.\nUsually has no effect with newer CPUs and AGESA.");
            toolTip.SetToolTip(comboBoxFID, "Frequency multiplier.\nDepends on DID.");
            toolTip.SetToolTip(comboBoxDID, "DID - divider identifier.\nChanging the value auto-adjusts the multiplier to the closest possible one.");
            toolTip.SetToolTip(comboBoxVID, "VID - voltage identifier.\nSets the voltage which the CPU requests when in this state.\nReal voltage output depends on OC mode and voltage offset.");
        }

        #region Poperties
        public string Label
        {
            get { return checkBoxPstateEnabled.Text; }
            set { checkBoxPstateEnabled.Text = value; }
        }

        public ulong Pstate
        {
            get
            {
                ulong edx = InitialPstate & 0xFFFFFFFF00000000;
                ulong eax = (IddDiv & 0xFF) << 30 | (IddVal & 0xFF) << 22 | (CpuVid & 0xFF) << 14 | (CpuDid & 0x3F) << 8 | CpuFid & 0xFF;

                return edx & ~(1UL << 63) | Convert.ToUInt64(Checked) << 63 | eax;
            }
            set
            {
                IddDiv = value >> 30 & 0xFF;
                IddVal = value >> 22 & 0xFF;
                CpuVid = value >> 14 & 0xFF;
                CpuDid = InitialCpuDid = value >> 8 & 0x3F;
                CpuFid = InitialCpuFid = value & 0xFF;
                Checked = Convert.ToBoolean((value >> 63) & 0x1);
                InitialPstate = value;

                PopulateVidItems();
                PopulateDidItems();
                PopulateFidItems();
            }
        }

        public bool Checked
        {
            get => checkBoxPstateEnabled.Checked;
            private set => checkBoxPstateEnabled.Checked = value;
        }

        public bool Changed => Pstate != InitialPstate;

        public void Reset() => Pstate = InitialPstate;

        public void UpdateState() => Pstate = Pstate;
        #endregion

        #region Event Handlers

        public EventHandler ValueChanged;

        private void NotifyValueChanged()
        {
            ValueChanged?.Invoke(new object(), new EventArgs());
        }

        private void checkBoxPstateEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Checked = checkBoxPstateEnabled.Checked;
            comboBoxFID.Enabled = Checked;
            comboBoxDID.Enabled = Checked;
            comboBoxVID.Enabled = Checked;
        }

        private void comboBoxDID_SelectedIndexChanged(object sender, EventArgs e)
        {
            double targetMulti = CalculateMulti(InitialCpuFid, InitialCpuDid);
            CpuDid = GetSelectedItemValue(comboBoxDID);
            CpuFid = CalculateFidFromMultiDid(targetMulti, CpuDid);

            PopulateFidItems();
        }

        private void comboBoxFID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CpuFid = GetSelectedItemValue(comboBoxFID);
        }

        private void comboBoxVID_SelectedIndexChanged(object sender, EventArgs e)
        {
            CpuVid = GetSelectedItemValue(comboBoxVID);
        }
        #endregion
    }
}
