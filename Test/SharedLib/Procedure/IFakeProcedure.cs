using Rudeus.Procedure;

namespace Test.Procedure
{
    public interface IFakeProcedure : IProcedure
    {
        int RunCount { get; }

        Task Run();
    }
}