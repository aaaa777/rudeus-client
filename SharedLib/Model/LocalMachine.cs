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
            OperatingSystem os = Environment.OSVersion;
            // TODO: 10と11の判別はビルド番号からするしかないらしい
            // https://ja.wikipedia.org/wiki/Microsoft_Windows_10%E3%81%AE%E3%83%90%E3%83%BC%E3%82%B8%E3%83%A7%E3%83%B3%E5%B1%A5%E6%AD%B4
            if (os.Version.Major == 10 && os.Version.Minor == 0)
            {
                if (os.Version.Build >= 22000)
                {
                    return "11";
                }
                return "10";
            }

            return "unknown";
        }

        public string GetSpec()
        {
            return "Dummy: Intel Xeon E5 2250;16GB";
        }

        public string GetCpuName()
        {
            string cpuName = "unknown";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                Console.WriteLine(mo["Name"]);
                // TODO: 30文字以上にリクエスト変更要求
                cpuName = (mo["Name"].ToString() ?? "null set").Substring(0, 30);
                mo.Dispose();
            }
            mos.Dispose();
            return cpuName;
        }

        public string GetMemory()
        {
            int nVal = 0;
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                // メモリー情報
                // 合計物理メモリー
                Console.WriteLine("TotalVisibleMemorySize = " + mo["TotalVisibleMemorySize"]);
                // 1024 * 1024で割ると誤差で16GBが15GBになるので、1000 * 1000で割る
                nVal = Convert.ToInt32(mo["TotalVisibleMemorySize"]) / (1_000_000);    // 単位 KB -> MB
                mo.Dispose();
            }
            moc.Dispose();
            return nVal.ToString();
        }

        public string GetCDrive()
        {
            return "256";
        }

        // https://learn.microsoft.com/en-us/windows/win32/sysinfo/operating-system-version
        public string GetOS()
        {
            return $"Windows {GetWinVersion()}";
        }

        public string GetOSVersion()
        {
            System.OperatingSystem os = System.Environment.OSVersion;
            return os.Version.ToString();
        }

        public string GetWithSecure()
        {
            return "";
        }

        public string GetLabelId()
        {
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            return $"P{firstNumber}-{secondNumber}";
        }
    }
}
