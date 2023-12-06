using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Rudeus.Model
{
    /// <summary>
    /// ローカルマシンの情報を取得するモデル
    /// </summary>
    public class LocalMachine : ILocalMachine
    {
        private static ILocalMachine? _instance = null;
        public static ILocalMachine GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LocalMachine();
            }
            return _instance;
        }

        private LocalMachine()
        {
            
        }

        public string GetPhysicalRamInfo()
        {
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();

            string ram = "";

            foreach (ManagementObject mo in moc)
            {
                //合計物理メモリ
                ram = (string)mo["TotalVisibleMemorySize"];

                mo.Dispose();
            }

            moc.Dispose();
            mc.Dispose();

            return ram;
        }

        public List<string> GetStorageDeviceIdList()
        {
            var volumes = new ManagementClass("Win32_DiskDrive").GetInstances();
            List<string> volumeIdList = new();

            foreach (var volume in volumes)
            {
                volumeIdList.Add((string)volume["SerialNumber"]);
            }
            return volumeIdList;
        }

        public string GetDeviceId()
        {
            // ToDo: デバイスIDを実際に取得するコードに置き換え
            Guid g = Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            return $"000000-{guid8}";
        }

        public string GetHostname()
        {
            // ToDo: ホスト名を実際に取得するコードに置き換え
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            return $"P{firstNumber}-{secondNumber}";
            // System.Net.Dns.GetHostName();
        }

        public string GetWinVersion()
        {
            return "12345.6789";
        }

        public string GetSpec()
        {
            return "Intel Xeon E5 2250;16GB";
        }

        public string GetCpuName()
        {
            return "Intel Xeon E5";
        }

        public string GetMemory()
        {
            return "16GB";
        }

        public string GetCDrive()
        {
            return "256GB";
        }

        public string GetOS()
        {
            return "windows 10";
        }

        public string GetOSVersion()
        {
            return "10.23456.234";
        }

        public string GetWithSecure()
        {
            return "";
        }

        public string GetLabelId()
        {
            return "P12-345";
        }
    }
}
