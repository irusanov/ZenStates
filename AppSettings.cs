using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZenStates.Core;

namespace ZenStates
{
    public sealed class AppSettings
    {
        private const int VERSION_MAJOR = 1;
        private const int VERSION_MINOR = 1;

        private const string filename = "ZenStates-settings.xml";

        public AppSettings()
        {
            Cpu cpu = CpuSingleton.Instance;
            this.CpuId = cpu.info.cpuid;
        }

        public AppSettings Create()
        {
            Version = $"{VERSION_MAJOR}.{VERSION_MINOR}";

            CpuId = this.CpuId;

            Save();

            return this;
        }

        public AppSettings Reset() => Create();

        public AppSettings Load()
        {
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    try
                    {
                        XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                        return xmls.Deserialize(sr) as AppSettings;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        sr.Close();
                        MessageBox.Show(
                            "Invalid settings file!\nSettings will be reset to defaults.",
                            "Error",
                            MessageBoxButtons.OK);
                        return Create();
                    }
                }
            }
            else
            {
                return Create();
            }
        }

        public void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                    xmls.Serialize(sw, this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save settings to file!",
                    "Error",
                    MessageBoxButtons.OK);
            }
        }

        public uint CpuId { get; set; }
        public string Version { get; set; } = $"{VERSION_MAJOR}.{VERSION_MINOR}";
        public bool IsFirstLaunchAfterBoot { get; set; } = true;
        public DateTime LastBootTime { get; set; }
        public int WindowLeft { get; set; } = -1;
        public int WindowTop { get; set; } = -1;

        public double Zen5VoltageLimit { get; set; } = 1.35;

        public bool Zen5VoltageLimitWarning { get; set; } = true;
    }
}
