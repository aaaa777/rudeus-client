using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Rudeus.Model.Operations;
using Rudeus;
using Rudeus.Model;
using Rudeus.API;
using Rudeus.API.Request;
using Rudeus.API.Response;
using SharedLib.Model.Settings;

namespace Rudeus
{
    /// <summary>
    /// ユーティリティクラス
    /// </summary>
    public class Utils : IUtils
    {
        public static readonly string WebPortalUrl = Constants.WebPortalUrl;
        public static readonly string Polite3Url = Constants.Polite3Url;
        public static readonly string KyoumuUrl = Constants.KyoumuUrl;

        // DI for static class
        public static ILocalMachine LM { get; set; } = LocalMachine.GetInstance();
        public static IRootSettings RootSettings { get; set; } = new RootSettings();

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

            if (studentId.Length != 7)
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

            if (fqdn != "example.com" && fqdn != "s.do-johodai.ac.jp")
            {
                return false;
            }

            if (username.Split("s")[1].Length != 7)
            {
                return false;
            }

            return true;
        }

        // BgInitializerか、BgInitializerが失敗した後のBgの初回起動で実行する
        // Todo: ダミーデータではなく実環境での初期化を行う
        /// <inheritdoc/>
        public static void RegisterDeviceAndSetData()
        {
            string hostname = LM.GetHostname();
            string deviceId = LM.GetDeviceId();

            // デフォルトのレジストリにセット
            RootSettings.HostnameP = hostname;
            RootSettings.FirstHostnameP = hostname;
            RootSettings.DeviceIdP = deviceId;

            // 発行
            try
            {
                RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);

                RootSettings.AccessTokenP = response.response_data.access_token ?? throw new("AccessToken not set");
                Console.WriteLine($"registered device: `{hostname}`: {response.status}");
                return;
            }
            catch
            {
                Console.WriteLine("server connection failed, device is not registered yet");
                throw;
            }
        }

        /// <inheritdoc/>
        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // GetStatic information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // GetStatic the files in the source directory and copy to the destination directory
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

        /// <inheritdoc/>
        public static int CompareVersionString(string a, string b, int depth = 0)
        {
            string[] aSeries = a.Split('.');
            string[] bSeries = b.Split('.');
            int aNum, bNum;
            depth = (depth > 0) ? Math.Min(depth, aSeries.Length) : aSeries.Length;
            for (var i = 0; i < depth; i++)
            {
                if (i >= bSeries.Length) { return 1; }

                if (!int.TryParse(aSeries[i], out aNum))
                {
                    aNum = 0;
                }

                if (!int.TryParse(bSeries[i], out bNum))
                {
                    bNum = 0;
                }

                if (aNum > bNum) { return 1; } else if (aNum < bNum) { return -1; }
            }
            return (aSeries.Length == bSeries.Length) ? 0 : -1;
        }

        /// <inheritdoc/>
        public static Dictionary<string, string> ParseArgs(string[] args)
        {
            var argsDict = args.Select(arg => arg.Split('=')).Where(s => s.Length == 2).ToDictionary(v => v[0], v => v[1]);
            return argsDict;
        }

        public static bool IsArgsAllNullOrAllObject(List<object> args)
        {
            throw new NotImplementedException();
        }
    }
}
