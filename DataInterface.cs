using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ZenStates
{
    /// <summary>
    /// Description of DataInterface.
    /// </summary>
    public class DataInterface
    {

        // Constants

        public const UInt64 ServiceVersion = 10; // 0 = 0.1/b, 1 = 0.2, 2 = 0.2.x, 3 = 0.3, 4 = 0.3.xxxx, 5 = 0.4/0.5, 6 = 0.5, 7 = 0.6, 8 = 0.7, 9 = 0.7.1. 10 = 0.8.0
        private const int bufferSize = 16;
        private const string mmf_path = @"Global\ZenStates";

        // Memory register map

        public const int REG_P0 = 0x00;
        public const int REG_P1 = 0x01;
        public const int REG_P2 = 0x02;
        /*public const int REG_P3 = 0x03;
        public const int REG_P4 = 0x04;
        public const int REG_P5 = 0x05;
        public const int REG_P6 = 0x06;
        public const int REG_P7 = 0x07;*/

        public const int REG_PERF_BIAS = 0x03;
        public const int REG_TEMP = 0x04;
        public const int REG_PPT = 0x05;
        public const int REG_TDC = 0x06;
        public const int REG_EDC = 0x07;

        public const int REG_SERVER_FLAGS = 0x08;
        public const int REG_CLIENT_FLAGS = 0x09;
        public const int REG_PING_PONG = 0x0A;
        public const int REG_NOTIFY_STATUS = 0x0B;
        public const int REG_SERVER_VERSION = 0x0C;
        public const int REG_SMU_VERSION = 0x0E;

        public const int REG_SCALAR = 0x0D;

        public const UInt64 FLAG_IS_AVAILABLE = 1 << 0;
        public const UInt64 FLAG_SUPPORTED_CPU = 1 << 1;
        public const UInt64 FLAG_SETTINGS_RESET = 1 << 2;
        public const UInt64 FLAG_TRAY_ICON_AT_START = 1 << 3;
        public const UInt64 FLAG_APPLY_AT_START = 1 << 4;
        public const UInt64 FLAG_SETTINGS_SAVED = 1 << 5;
        public const UInt64 FLAG_C6CORE = 1 << 6;
        public const UInt64 FLAG_C6PACKAGE = 1 << 7;
        public const UInt64 FLAG_SHUTDOWN_UNCLEAN = 1 << 8;
        public const UInt64 FLAG_P80_TEMP_EN = 1 << 9;
        public const UInt64 FLAG_CPB = 1 << 10;
        public const UInt64 FLAG_OC = 1 << 11;

        public const UInt64 NOTIFY_CLEAR = 0x00;
        public const UInt64 NOTIFY_CLIENT_FLAGS = 0x01;
        public const UInt64 NOTIFY_RESTORE = 0x02;
        public const UInt64 NOTIFY_APPLY = 0x03;
        public const UInt64 NOTIFY_SAVE = 0x04;
        public const UInt64 NOTIFY_DONE = 0x80;

        // Local vars
        private MemoryMappedFile mmf;
        private UInt64[] buffer;

        public DataInterface(bool server)
        {
            try
            {
                buffer = new UInt64[bufferSize];

                if (server)
                {
                    SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                    var security = new MemoryMappedFileSecurity();
                    security.AddAccessRule(new AccessRule<MemoryMappedFileRights>(everyone, MemoryMappedFileRights.FullControl, AccessControlType.Allow));
                    mmf = MemoryMappedFile.CreateOrOpen(mmf_path, bufferSize * 8,
                                     MemoryMappedFileAccess.ReadWrite,
                                     MemoryMappedFileOptions.DelayAllocatePages, security,
                                     HandleInheritability.Inheritable);

                }
                else
                {
                    mmf = MemoryMappedFile.OpenExisting(mmf_path);
                }
            }
            catch (Exception ex)
            {
                throw new System.ApplicationException("Error initializing memory interface " + ex.Message);
            }

        }

        public bool MemWrite(int offset, UInt64 data)
        {
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                try
                {
                    stream.Seek(offset * 8, SeekOrigin.Begin);
                    BinaryWriter writer = new BinaryWriter(stream);
                    writer.Write(data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool MemWriteAll()
        {
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    BinaryWriter writer = new BinaryWriter(stream);
                    for (int i = 0; i < bufferSize; i++) writer.Write(buffer[i]);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        }
        public UInt64 MemRead(int offset)
        {
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                try
                {
                    stream.Seek(offset * 8, SeekOrigin.Begin);
                    BinaryReader reader = new BinaryReader(stream);
                    return reader.ReadUInt64();
                }
                catch (Exception ex)
                {
                    return 0;
                }

            }
        }

        public UInt64[] MemReadAll()
        {
            using (MemoryMappedViewStream stream = mmf.CreateViewStream())
            {
                try
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    BinaryReader reader = new BinaryReader(stream);
                    for (int i = 0; i < bufferSize; i++) buffer[i] = reader.ReadUInt64();
                    return buffer;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public void BufferWrite(int offset, UInt64 data)
        {
            buffer[offset] = data;
        }

        public UInt64 BufferRead(int offset)
        {
            return buffer[offset];
        }
    }
}
