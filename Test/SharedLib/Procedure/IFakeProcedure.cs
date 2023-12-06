using Rudeus.Procedure;

namespace Test.SharedLib.Procedure
{
    public interface IFakeProcedure : IProcedure
    {
        int RunCount { get; set; }

        Task Run();
    }
}