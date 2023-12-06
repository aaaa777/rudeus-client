// See https://aka.ms/new-console-template for more information

using Rudeus.Procedure;

namespace Rudeus.Uninstaller
{
    class Program
    {
        static IProcedure RegistryRemover;
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            RegistryRemover = new RegistryRemover();

            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            await RegistryRemover.Run();
        }
    }
}