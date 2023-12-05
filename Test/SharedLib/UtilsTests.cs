using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus;

namespace Test.SharedLib
{
    public class UtilsTests

    {
        /// <summary>
        /// 学生のメール -> true
        /// </summary>
        [Fact(DisplayName = "IsStudentMailAddress(): s1234567@s.do-johodai.ac.jp -> True")]
        public void TestIsStudentMailAddress1()
        {
            Utils.IsStudentMailAddress("s1234567@s.do-johodai.ac.jp").IsTrue();
        }

        /// <summary>
        /// 教員のメール -> false
        /// </summary>
        [Fact(DisplayName = "IsStudentMailAddress(): samplename@s.do-johodai.ac.jp -> False")]
        public void TestIsStudentMailAddress2()
        {
            Utils.IsStudentMailAddress("samplename@s.do-johodai.ac.jp").IsFalse();
        }

        /// <summary>
        /// 外部のメール -> false
        /// </summary>
        [Fact(DisplayName = "IsStudentMailAddress(): s1234567@www.example.com -> False")]
        public void TestIsStudentMailAddress3()
        {
            Utils.IsStudentMailAddress("s1234567@www.example.com").IsFalse();
        }



        [Fact(DisplayName = "ConcatStudentNumberFromMail(): \"s1234567@s.do-johodai.ac.jp\" -> \"1234567\"")]
        public void TestConcatStudentNumberFromMail()
        {
            Utils.ConcatStudentNumberFromMail("s1234567@s.do-johodai.ac.jp").Is("1234567");
        }



        [Fact(DisplayName = "CompareVersionString(): \"1.0.0\", \"1.0.0\" -> 0")]
        public void TestCompareVersionString1()
        {
            // 引数1 == 引数2 -> 0
            Utils.CompareVersionString("1.0.0", "1.0.0").Is(0);
        }

        [Fact(DisplayName = "CompareVersionString(): \"1.0.0\", \"0.32.0\" -> 1")]
        public void TestCompareVersionString2()
        {

            // 引数1 > 引数2 -> 1
            Utils.CompareVersionString("1.0.0", "0.32.0").Is(1);
        }

        [Fact(DisplayName = "CompareVersionString(): \"1.0.0\", \"2.1.0\" -> -1")]
        public void TestCompareVersionString()
        {
            // 引数1 < 引数2 -> -1
            Utils.CompareVersionString("1.0.0", "2.1.0").Is(-1);
        }
    }
}
