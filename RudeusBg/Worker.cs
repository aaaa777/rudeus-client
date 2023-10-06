using Rudeus.Model;

namespace RudeusBg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Settings settings;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            settings = Settings.Load();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                PostInformation();
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void PostInformation()
        {
            string accessToken = settings.GetAccessToken();
            string username = settings.GetUsername();
            //RemoteAPI.UpdateDevice(accessToken, username);
            _logger.LogInformation($"{accessToken}, {username}");
        }
    }
}