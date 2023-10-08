using Rudeus.Model;
using Rudeus.Model.Response;

namespace RudeusBg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Settings settings;
        private string endpoint = "http://10.10.2.11/";
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            settings = Settings.Load();
            client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
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
            string hostname = settings.GetHostname();
            //RemoteAPI.UpdateDevice(accessToken, username);
            _logger.LogInformation($"{accessToken}, {username}");

            UpdateResponse response = RemoteAPI.UpdateDevice(accessToken, hostname, username);

            _logger.LogInformation($"res: {response.response_data}");
        }

        public void SetupDummyVal()
        {
            //settings.Set("AccessToken", "123");
            //_logger.LogInformation(settings.Get("AccessToken"));
            this.settings.SetAccessToken("112|laravel_sanctum_BCtYO6nnG7CqlKgPhk5gwTk8bEEXdqIGSfziIthAc73f957a");
            this.settings.SetHostname("HIU-P12-643");
            this.settings.SetDeviceId("123453212");
            this.settings.SetUsername("2000111");
        }

        public void SetupInitialize()
        {
            // ãNìÆñàÇ…GUIDÇê∂ê¨ÇµÇƒDevideIdÇ∆ÇµÇƒÇ¢ÇÈ
            Guid g = System.Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);

            RegisterResponse response = RemoteAPI.RegisterDevice($"000000-{guid8}", "HIU-P12-234");
            RemoteAPI.LoginDevice(response.response_data.access_token, "9999999");
        }
    }
}