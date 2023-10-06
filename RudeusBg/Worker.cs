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
            this.SetupDummyVal();
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
            //string accessToken = settings.GetAccessToken();
            string accessToken = settings.Get("AccessToken");
            string username = settings.GetUsername();
            //RemoteAPI.UpdateDevice(accessToken, username);
            _logger.LogInformation($"{accessToken}, {username}");
        }

        public void SetupDummyVal()
        {
            settings.Set("AccessToken", "123");
            _logger.LogInformation(settings.Get("AccessToken"));
            this.settings.SetAccessToken("11|laravel_sanctum_rXQijK2UD5j1coEkpxgEpRo0f00IWELSnuRV6ewE64effe15");
            this.settings.SetHostname("HIU-P12-344");
            this.settings.SetDeviceId("g68y0p7hgu98");
            this.settings.SetUsername("2112018");
        }
    }
}