using iRacing.Enums;
using iRacing.Exceptions;
using iRacing.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace iRacing
{
    public class IRClient
    {
        // IRInstance \\
        private IRClient() 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(1252);
        }
        private static IRClient IRInstance = null;

        public static IRClient GetInstance()
        {
            if (IRInstance == null) IRInstance = new IRClient();
            return IRInstance;
        }

        public static bool GetSimStatus()
        {
            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(Constants.IRStatusAddress);
                bool running = (response.Split('\n')[1].Split(':')[1].Trim().Equals("1"));
                return running;
            }
        }
        public static MemoryMappedViewAccessor GetFileMapView()
        {
            return GetInstance().FileMapView;
        }

        // IRClient \\

        private readonly Encoding _encoding;

        public const int VarOffsetOffset = 4;
        public const int VarCountOffset = 8;
        public const int VarNameOffset = 16;
        public const int VarDescOffset = 48;
        public const int VarUnitOffset = 112;

        public bool IsInitialized = false;

        MemoryMappedFile iRacingFile;
        protected MemoryMappedViewAccessor FileMapView;
        
        public IRacingSdkHeader Header = null;

        public List<VarHeader> VarHeaders = new List<VarHeader>();

        public bool Startup()
        {
            if (IsInitialized) return true;

            if (GetSimStatus())
            {
                try
                {
                    iRacingFile = MemoryMappedFile.OpenExisting(Constants.MemMapFileName);
                    FileMapView = iRacingFile.CreateViewAccessor();

                    var hEvent = OpenEvent(Constants.DesiredAccess, false, Constants.DataValidEventName);
                    var are = new AutoResetEvent(false)
                    {
                        // This is deprecated, need better option
                        SafeWaitHandle = new SafeWaitHandle(hEvent, true)
                    };

                    var wh = new WaitHandle[1];
                    wh[0] = are;

                    WaitHandle.WaitAny(wh);

                    Header = new IRacingSdkHeader(FileMapView);
                    GetVarHeaders();

                    IsInitialized = true;
                }
                catch (FileNotFoundException)
                {
                    throw new IRNotFoundException();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else
            {
                throw new IRNotRunningException();
            }
        }

        private void GetVarHeaders()
        {
            VarHeaders.Clear();
            for (int i = 0; i < Header.VarCount; i++)
            {
                int type = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size)));
                int offset = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarOffsetOffset));
                int count = FileMapView.ReadInt32(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarCountOffset));
                byte[] name = new byte[Constants.MaxString];
                byte[] desc = new byte[Constants.MaxDesc];
                byte[] unit = new byte[Constants.MaxString];
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarNameOffset), name, 0, Constants.MaxString);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarDescOffset), desc, 0, Constants.MaxDesc);
                FileMapView.ReadArray<byte>(Header.VarHeaderOffset + ((i * VarHeader.Size) + VarUnitOffset), unit, 0, Constants.MaxString);
                string nameStr = _encoding.GetString(name).TrimEnd(new char[] { '\0' });
                string descStr = _encoding.GetString(desc).TrimEnd(new char[] { '\0' });
                string unitStr = _encoding.GetString(unit).TrimEnd(new char[] { '\0' });
                VarHeaders.Add(new VarHeader(type, offset, count, nameStr, descStr, unitStr));
            }
        }

        public object GetData(string name)
        {
            if (!IsInitialized || Header == null) return null;

            var requestedHeader = VarHeaders.FirstOrDefault(h => h.Name == name);

            if (requestedHeader == null) return null;

            int varOffset = requestedHeader.Offset;
            int count = requestedHeader.Count;

            switch (requestedHeader.Type)
            {
                case VarType.irChar:
                    {
                        byte[] data = new byte[count];
                        FileMapView.ReadArray<byte>(Header.Buffer + varOffset, data, 0, count);
                        return _encoding.GetString(data).TrimEnd(new char[] { '\0' });
                    }
                case VarType.irBool:
                    {
                        if (count > 1)
                        {
                            bool[] data = new bool[count];
                            FileMapView.ReadArray<bool>(Header.Buffer + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadBoolean(Header.Buffer + varOffset);
                        }
                    }
                case VarType.irInt:
                case VarType.irBitField:
                    {
                        if (count > 1)
                        {
                            int[] data = new int[count];
                            FileMapView.ReadArray<int>(Header.Buffer + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadInt32(Header.Buffer + varOffset);
                        }
                    }
                case VarType.irFloat:
                    {
                        if (count > 1)
                        {
                            float[] data = new float[count];
                            FileMapView.ReadArray<float>(Header.Buffer + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadSingle(Header.Buffer + varOffset);
                        }
                    }
                case VarType.irDouble:
                    {
                        if (count > 1)
                        {
                            double[] data = new double[count];
                            FileMapView.ReadArray<double>(Header.Buffer + varOffset, data, 0, count);
                            return data;
                        }
                        else
                        {
                            return FileMapView.ReadDouble(Header.Buffer + varOffset);
                        }
                    }
                default: return null;
            }
        }

        public string GetSessionInfo()
        {
            if (IsInitialized && Header != null)
            {
                byte[] data = new byte[Header.SessionInfoLength];
                FileMapView.ReadArray(Header.SessionInfoOffset, data, 0, Header.SessionInfoLength);
                return _encoding.GetString(data).TrimEnd(new char[] { '\0' });
            }
            return null;
        }

        public bool IsConnected()
        {
            if (IsInitialized && Header != null)
            {
                return (Header.Status & 1) > 0;
            }
            return false;
        }

        public void Shutdown()
        {
            IsInitialized = false;
            Header = null;
        }

        IntPtr GetBroadcastMessageID()
        {
            return RegisterWindowMessage(Constants.BroadcastMessageName);
        }

        IntPtr GetPadCarNumID()
        {
            return RegisterWindowMessage(Constants.PadCarNumName);
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2, int var3)
        {
            return BroadcastMessage(msg, var1, MakeLong((short)var2, (short)var3));
        }

        public int BroadcastMessage(BroadcastMessageTypes msg, int var1, int var2)
        {
            IntPtr msgId = GetBroadcastMessageID();
            IntPtr hwndBroadcast = IntPtr.Add(IntPtr.Zero, 0xffff);
            IntPtr result = IntPtr.Zero;
            if (msgId != IntPtr.Zero)
            {
                result = PostMessage(hwndBroadcast, msgId.ToInt32(), MakeLong((short)msg, (short)var1), var2);
            }
            return result.ToInt32();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr RegisterWindowMessage(string lpProcName);

        //[DllImport("user32.dll")]
        //private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenEvent(UInt32 dwDesiredAccess, Boolean bInheritHandle, String lpName);

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        public static short HiWord(int dword)
        {
            return (short)(dword >> 16);
        }

        public static short LoWord(int dword)
        {
            return (short)dword;
        }
    }
}