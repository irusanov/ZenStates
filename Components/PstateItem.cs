using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZenStates.Components
{
    public partial class PstateItem : UserControl
    {
        private const byte FID_MAX = 0xFF;
        private const byte FID_MIN = 0x10;
        private const byte VID_MAX = 0xE8;
        private const byte VID_MIN = 0x00;

        private uint eax = default;
        private uint edx = default;
        private uint IddDiv = 0x0;
        private uint IddVal = 0x0;
        private uint CpuVid = 0x0;
        private uint CpuDid = 0x0;
        private uint CpuFid = 0x0;

        private uint SelectedCpuVid = 0x0;
        private uint SelectedCpuDid = 0x0;
        private uint SelectedCpuFid = 0x0;

        private uint calculateFidFromMultiDid(double targetMulti = 5.5, uint did = 0)
        {
            uint targetFid = Convert.ToUInt32(Math.Floor(targetMulti * did * 12.5 / 25));

            if (targetFid < FID_MIN) targetFid = FID_MIN;
            if (targetFid > FID_MAX) targetFid = FID_MAX;

            return targetFid;
        }

        private void PopulateVidItems()
        {
            comboBoxVID.Items.Clear();

            for (uint i = VID_MIN; i <= VID_MAX; i++)
            {
                double voltage = 1.55 - i * 0.00625;
                CustomListItem item = new CustomListItem(i, string.Format("{0:0.000}V", voltage));
                comboBoxVID.Items.Add(item);
            }

            foreach (CustomListItem item in comboBoxVID.Items)
            {
                if (item.Value == SelectedCpuVid) comboBoxVID.SelectedItem = item;
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
                if (item.Value == SelectedCpuDid) comboBoxDID.SelectedItem = item;
            }
        }

        private void PopulateFidItems()
        {
            comboBoxFID.Items.Clear();

            for (uint i = FID_MAX; i >= FID_MIN; i--)
            {
                double multi = (25 * i) / (SelectedCpuDid * 12.5);
                CustomListItem item = new CustomListItem(i, string.Format("x{0:0.00}", multi));
                comboBoxFID.Items.Add(item);
            }

            foreach (CustomListItem item in comboBoxFID.Items)
            {
                if (item.Value == SelectedCpuFid) comboBoxFID.SelectedItem = item;
            }
        }

        public PstateItem()
        {
            InitializeComponent();

            comboBoxFID.Enabled = Checked;
            comboBoxDID.Enabled = Checked;
            comboBoxVID.Enabled = Checked;
        }

        public string Label
        {
            get { return checkBoxPstateEnabled.Text; }
            set { checkBoxPstateEnabled.Text = value; }
        }

        public uint EAX
        {
            get { return (IddDiv & 0xFF) << 30 | (IddVal & 0xFF) << 22 | (SelectedCpuVid & 0xFF) << 14 | (SelectedCpuDid & 0xFF) << 8 | SelectedCpuFid & 0xFF; }
            set {
                eax = value;

                IddDiv = eax >> 30;
                IddVal = eax >> 22 & 0xFF;
                CpuVid = eax >> 14 & 0xFF;
                CpuDid = eax >> 8 & 0x3F;
                CpuFid = eax & 0xFF;

                SelectedCpuVid = CpuVid;
                SelectedCpuDid = CpuDid;
                SelectedCpuFid = CpuFid;

                PopulateVidItems();
                PopulateDidItems();
                PopulateFidItems();
            }
        }

        public bool Checked
        {
            get { return checkBoxPstateEnabled.Checked; }
            set { checkBoxPstateEnabled.Checked = value; }
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
            double targetMulti = (25 * CpuFid) / (CpuDid * 12.5);
            SelectedCpuDid = (comboBoxDID.SelectedItem as CustomListItem).Value;
            SelectedCpuFid = calculateFidFromMultiDid(targetMulti, SelectedCpuDid);
            
            PopulateFidItems();
        }
    }
}
