using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Rudeus.Model
{
    internal class Utils
    {
        public static readonly string WebPortalUrl = "https://portal.do-johodai.ac.jp";
        public static readonly string Polite3Url = "https://polite3.do-johodai.ac.jp";
        public static readonly string KyoumuUrl = "https://kyoumu.do-johodai.ac.jp";

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
    }
}
