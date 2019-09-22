using ZenStates;
using System;
using System.IO;
using System.Xml;

namespace ZenStatesSrv
{
    public class Settings
    {

        private const string FileName = "ZenStatesSettings.xml";
        private string FilePath;
        private string FullFilePath;

        public bool SettingsReset = true;

        // Application info
        public byte ServiceVersion = 0;
        public byte LastState = 0;

        // Settings
        public UInt64[] Pstate = new UInt64[CPUHandler.NumPstates];
        public UInt64[] BoostFreq = new UInt64[3];
        public UInt64 PstateOc = new UInt64();

        public bool TrayIconAtStart = false;
        public bool ApplyAtStart = false;
        public bool P80Temp = false;

        public bool ZenC6Core = false;
        public bool ZenC6Package = false;
        public bool ZenCorePerfBoost = false;
        public bool ZenOc = false;
        public int ZenPPT = 0;
        public int ZenTDC = 0;
        public int ZenEDC = 0;
        public int ZenScalar = 1;

        //public CPUHandler.PerfEnh PerformanceEnhancer = 0;
        public CPUHandler.PerfBias PerformanceBias = CPUHandler.PerfBias.Auto;

        public Settings()
        {

            // Path
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ZenStates");
            FullFilePath = Path.Combine(FilePath, FileName);

            // Check if directory exists
            if (!Directory.Exists(FilePath))
            {
                // Create directory
                Directory.CreateDirectory(FilePath);
            }

            bool res = false;

            // Check if file exists
            if (File.Exists(FullFilePath))
            {
                // Read file
                res = ReadSettingsFromFile();
                if (Pstate[0] == 0 && Pstate[1] == 0 && Pstate[2] == 0) SettingsReset = true;
                if (ServiceVersion != DataInterface.ServiceVersion) res = false;
            }

            if (!res)
            {
                // Create/overwrite file
                WriteSettingsToFile();
            }
        }

        public void Save()
        {
            WriteSettingsToFile();
        }

        private void WriteSettingsToFile()
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true
            };

            try
            {
                using (FileStream fs = new FileStream(FullFilePath, FileMode.Create))
                {
                    using (XmlWriter writer = XmlWriter.Create(fs, xmlWriterSettings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteComment("ZenStates Settings file");
                        writer.WriteStartElement("ZenStates");
                        writer.WriteStartElement("Application");
                        writer.WriteElementString("ServiceVersion", DataInterface.ServiceVersion.ToString());
                        writer.WriteElementString("SettingsReset", SettingsReset.ToString());
                        writer.WriteElementString("LastState", LastState.ToString());
                        writer.WriteEndElement();
                        writer.WriteStartElement("Settings");
                        writer.WriteElementString("TrayIconAtStart", TrayIconAtStart.ToString());
                        writer.WriteElementString("ApplyAtStart", ApplyAtStart.ToString());
                        writer.WriteElementString("P80Temp", P80Temp.ToString());
                        writer.WriteElementString("P0", Pstate[0].ToString("X16"));
                        writer.WriteElementString("P1", Pstate[1].ToString("X16"));
                        writer.WriteElementString("P2", Pstate[2].ToString("X16"));
                        writer.WriteElementString("Boost0", BoostFreq[0].ToString("X16"));
                        writer.WriteElementString("Boost1", BoostFreq[1].ToString("X16"));
                        writer.WriteElementString("Boost2", BoostFreq[2].ToString("X16"));
                        writer.WriteElementString("PstateOc", PstateOc.ToString("X16"));
                        writer.WriteElementString("ZenC6Core", ZenC6Core.ToString());
                        writer.WriteElementString("ZenC6Package", ZenC6Package.ToString());
                        writer.WriteElementString("ZenCorePerfBoost", ZenCorePerfBoost.ToString());
                        writer.WriteElementString("ZenOc", ZenOc.ToString());
                        writer.WriteElementString("ZenPPT", ZenPPT.ToString());
                        writer.WriteElementString("ZenTDC", ZenTDC.ToString());
                        writer.WriteElementString("ZenEDC", ZenEDC.ToString());
                        writer.WriteElementString("ZenScalar", ZenScalar.ToString());
                        writer.WriteElementString("PerformanceBias", ((byte)PerformanceBias).ToString());
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();
                    }
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Settings: " + ex.Message);
            }

        }

        private bool ReadSettingsFromFile()
        {
            try
            {
                using (FileStream fs = new FileStream(FullFilePath, FileMode.Open))
                {
                    using (XmlReader reader = XmlReader.Create(fs))
                    {

                        // Find ZenStates Element
                        while (reader.Read() && reader.NodeType == XmlNodeType.Element && reader.Name == "ZenStates") { }

                        // Find Application Element
                        while (reader.Read() && reader.NodeType == XmlNodeType.Element && reader.Name == "Application") { }

                        // Read Application Element variables
                        while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "ServiceVersion": ServiceVersion = (byte)reader.ReadElementContentAsInt(); break;
                                    case "SettingsReset": SettingsReset = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "LastState": LastState = (byte)reader.ReadElementContentAsInt(); break;
                                }
                            }
                        }

                        // Find Settings Element
                        while (reader.Read() && reader.NodeType == XmlNodeType.Element && reader.Name == "Settings") { }

                        // Read Settings Element variables
                        while (reader.Read() && reader.NodeType != XmlNodeType.EndElement)
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "TrayIconAtStart": TrayIconAtStart = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "ApplyAtStart": ApplyAtStart = reader.ReadElementContentAsString() == "True" ? true : false; ; break;
                                    case "P80Temp": P80Temp = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "P0": Pstate[0] = Convert.ToUInt64(reader.ReadElementContentAsString(), 16); break;
                                    case "P1": Pstate[1] = Convert.ToUInt64(reader.ReadElementContentAsString(),16); break;
                                    case "P2": Pstate[2] = Convert.ToUInt64(reader.ReadElementContentAsString(),16); break;
                                    case "Boost0": BoostFreq[0] = Convert.ToUInt64(reader.ReadElementContentAsString(), 16); break;
                                    case "Boost1": BoostFreq[1] = Convert.ToUInt64(reader.ReadElementContentAsString(), 16); break;
                                    case "Boost2": BoostFreq[2] = Convert.ToUInt64(reader.ReadElementContentAsString(), 16); break;
                                    case "PstateOc": PstateOc = Convert.ToUInt64(reader.ReadElementContentAsString(), 16); break;
                                    case "ZenC6Core": ZenC6Core = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "ZenC6Package": ZenC6Package = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "ZenCorePerfBoost": ZenCorePerfBoost = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "ZenOc": ZenOc = reader.ReadElementContentAsString() == "True" ? true : false; break;
                                    case "ZenPPT": ZenPPT = reader.ReadElementContentAsInt(); break;
                                    case "ZenTDC": ZenTDC = reader.ReadElementContentAsInt(); break;
                                    case "ZenEDC": ZenEDC = reader.ReadElementContentAsInt(); break;
                                    case "ZenScalar": ZenScalar = reader.ReadElementContentAsInt(); break;
                                    case "PerformanceBias": PerformanceBias = (CPUHandler.PerfBias)reader.ReadElementContentAsInt(); break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("Settings: " + ex.Message);
                return false;
            }

            return true;
        }

    }
}
