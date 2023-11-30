using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Model;

namespace RudeusTest.RudeusSharedLib
{
    public class TestUtils
    {
        [Fact(DisplayName = "IsStudentMailAddress")]
        public void TestIsStudentMailAddress()
        {
            // 学生のメール
            Utils.IsStudentMailAddress("s1234567@s.do-johodai.ac.jp").Is(true);

            // 教員のメール
            Utils.IsStudentMailAddress("samplename@s.do-johodai.ac.jp").Is(false);

            // 外部のメール
            Utils.IsStudentMailAddress("s1234567@www.example.com").Is(false);
        }

        [Fact]
        public void TestConcatStudentNumberFromMail()
        {
            Utils.ConcatStudentNumberFromMail("s1234567@s.do-johodai.ac.jp").Is("1234567");
        }
    }
}
