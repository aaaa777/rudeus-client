using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Rudeus.Model.Operations;

namespace Rudeus.Model
{
    internal class Utils
    {
        public static readonly string WebPortalUrl = Constants.WebPortalUrl;
        public static readonly string Polite3Url = Constants.Polite3Url;
        public static readonly string KyoumuUrl = Constants.KyoumuUrl;

        //public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application";
        //public static readonly string RudeusBgExe = "RudeusBg.exe";

        //public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager";
        //public static readonly string RudeusBgFormExe = "RudeusBgForm.exe";

        /// <summary>
        /// s9999999@s.do-johodai.ac.jp -> 9999999
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ConcatStudentNumberFromMail(string mailAddress)
        {
            string username = mailAddress.Split("@")[0];
            string studentId = username.Split("s")[1];

            if(studentId.Length != 7)
            {
                throw new Exception();
            }

            return studentId;
        }

        /// <summary>
        /// s9999999@s.do-johodai.ac.jp -> true
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <returns></returns>
        public static bool IsStudentMailAddress(string mailAddress)
        {
            string username = mailAddress.Split('@')[0];
            string fqdn = mailAddress.Split("@")[1];
            
            if(fqdn != "example.com" && fqdn != "s.do-johodai.ac.jp")
            {
                return false;
            }

            if (username.Split("s")[1].Length != 7)
            {
                return false;
            }

            return true;
        }

        public static string GetPhysicalRamInfo()
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

        public static string[] GetStorageDeviceIdList()
        {
            var volumes = new ManagementClass("Win32_DiskDrive").GetInstances();
            string[] volumeIdList = new string[volumes.Count];

            int i = 0;
            foreach (var volume in volumes)
            {
                volumeIdList[i] = (string)volume["SerialNumber"];
                i++;
            }
            return volumeIdList;
        }

        public static string GetDeviceId()
        {
            // ToDo: デバイスIDを実際に取得するコードに置き換え
            Guid g = Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            return $"000000-{guid8}";
        }

        public static string GetHostname()
        {
            // ToDo: ホスト名を実際に取得するコードに置き換え
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            return $"P{firstNumber}-{secondNumber}";
            // System.Net.Dns.GetHostName();
        }

        public static string GetSpec()
        {
            return "Intel Xeon E5 2250;128GB";
        }

        public static string GetWinVersion()
        {
            return "12345.6789";
        }

        // BgInitializerか、BgInitializerが失敗した後のBgの初回起動で実行する
        // Todo: ダミーデータではなく実環境での初期化を行う
        public static void RegisterDeviceAndSetData()
        {
            string hostname = GetHostname();
            string deviceId = GetDeviceId();

            // デフォルトのレジストリにセット
            Settings.UpdateRegistryKey();
            Settings.Hostname = hostname;
            Settings.FirstHostname = hostname;
            Settings.DeviceId = deviceId;

            // 発行
            try
            {
                Response.RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);

                Settings.AccessToken = response.response_data.access_token ?? throw new("AccessToken not set");
                Console.WriteLine($"registered device: `{hostname}`: {response.status}");
                return;
            }
            catch
            {
                Console.WriteLine("server connection failed, device is not registered yet");
                throw;
            }
        }

        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        /// <summary>ドット区切り数字列を比較する</summary>
        /// <param name="a">対象文字列A</param>
        /// <param name="b">対象文字列B</param>
        /// <param name="number">比較する列数 (0なら全て)</param>
        /// <returns>A-Bの符号 (1, 0, -1)</returns>
        public static int CompareVersionString(string a, string b, int number = 0)
        {
            string[] aSeries = a.Split('.');
            string[] bSeries = b.Split('.');
            int aNum, bNum;
            number = (number > 0) ? Math.Min(number, aSeries.Length) : aSeries.Length;
            for (var i = 0; i < number; i++)
            {
                if (i >= bSeries.Length) { return 1; }
                
                if(!int.TryParse(aSeries[i], out aNum))
                {
                    aNum = 0;                    
                }

                if(!int.TryParse(bSeries[i], out bNum))
                {
                    bNum = 0;
                }

                if (aNum > bNum) { return 1; } else if (aNum < bNum) { return -1; }
            }
            return (aSeries.Length == bSeries.Length) ? 0 : -1;
        }
    }
}
