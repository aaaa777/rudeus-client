namespace Rudeus.Procedure
{
    public interface IFakeProcedure : IProcedure
    {
        int RunCount { get; }

        Task Run();
    }
}