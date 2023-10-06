using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal class Utils
    {
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
    }
}
