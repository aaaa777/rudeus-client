
using Rudeus.Initializer;
using Rudeus.Procedure;
using Test.Procedure;

namespace Test.Initializer
{
    public class ProgramTests
    {
        [Fact]
        public void TestMain()
        {
            IFakeProcedure ri = new FakeProcedure();
            IFakeProcedure ti = new FakeProcedure();
            IFakeProcedure ci = new FakeProcedure();
            IFakeProcedure sr = new FakeProcedure();
            string[] args = Array.Empty<string>();

            Program._registryInitializer = ri;
            Program._taskInitializer = ti;
            Program._certificateInstaller = ci;
            Program._serverRegister = sr;
            Program.CheckLogMessage = false;

            Program.Main(args);

            Assert.Equal(1, ri.RunCount);
            Assert.Equal(1, ti.RunCount);
            Assert.Equal(1, ci.RunCount);
            Assert.Equal(1, sr.RunCount);
        }
    }
}