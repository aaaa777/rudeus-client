namespace Rudeus.Bg
{
    class Program
    {
        internal static string[]? commandArgs;
        public static void Main(string[] args)
        {
            commandArgs = args;

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();
                })
                .Build();

            try
            {
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
#if (!RELEASE)
                //Console.ReadLine();
#endif
            }

        }
    }
}