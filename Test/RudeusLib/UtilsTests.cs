using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus;

namespace RudeusLibTest
{
    public class UtilsTests

    {
        [Fact(DisplayName = "IsStudentMailAddress")]
        public void TestIsStudentMailAddress()
        {

            // 学生のメール -> true
            Utils.IsStudentMailAddress("s1234567@s.do-johodai.ac.jp").Is(true);

            // 教員のメール -> false
            Utils.IsStudentMailAddress("samplename@s.do-johodai.ac.jp").Is(false);

            // 外部のメール -> false
            Utils.IsStudentMailAddress("s1234567@www.example.com").Is(false);
        }

        [Fact(DisplayName = "ConcatStudentNumberFromMail")]
        public void TestConcatStudentNumberFromMail()
        {
            Utils.ConcatStudentNumberFromMail("s1234567@s.do-johodai.ac.jp").Is("1234567");
        }

        [Fact(DisplayName = "CompareVersionString")]
        public void TestCompareVersionString()
        {
            // 引数1 == 引数2 -> 0
            Utils.CompareVersionString("1.0.0", "1.0.0").Is(0);

            // 引数1 > 引数2 -> 1
            Utils.CompareVersionString("1.0.0", "0.32.0").Is(1);

            // 引数1 < 引数2 -> -1
            Utils.CompareVersionString("1.0.0", "2.1.0").Is(-1);
        }
    }
}
