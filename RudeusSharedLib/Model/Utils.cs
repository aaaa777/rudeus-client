﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Rudeus.Model.Operations;

namespace Rudeus.Model
{
    internal class Utils
    {
        public static readonly string WebPortalUrl = "https://portal.do-johodai.ac.jp";
        public static readonly string Polite3Url = "https://polite3.do-johodai.ac.jp";
        public static readonly string KyoumuUrl = "https://eduweb.do-johodai.ac.jp";

        public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application\\";
        public static readonly string RudeusBgExe = "svrhost.exe";

        public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager\\";
        public static readonly string RudeusBgFormExe = "RudeusBgForm.exe";

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
            return $"HIU-P{firstNumber}-{secondNumber}";
        }

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
            }
        }
    }
}
