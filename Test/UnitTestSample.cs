namespace Test
{
    public class UnitTestSample
    {
        // 単体テストのサンプル
        [Fact(DisplayName = "abs(1) -> 1")]
        public void TestAbs1()
        {
            Abs(1).Is(1);
        }

        [Fact(DisplayName = "abs(-12) -> 12")]
        public void TestAbs2()
        {
            Abs(-12).Is(12);
        }

        [Fact(DisplayName = "abs(0) -> 0")]
        public void TestAbs3()
        {
            Abs(0).Is(0);
        }

        private int Abs(int num)
        {
            return num < 0 ? -num : num;
        }
    }
}