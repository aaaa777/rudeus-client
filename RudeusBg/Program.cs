using RudeusBg;

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
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
#if (DEBUG)
    //Console.ReadLine();
#endif
}