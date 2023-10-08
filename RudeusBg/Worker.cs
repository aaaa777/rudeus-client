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
            this.Register();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                PostInformation();
                await Task.Delay(1000, stoppingToken);
            }
        }

        private void PostInformation()
        {
            //string accessToken = settings.GetAccessToken();
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();

            string accessToken = settings.Get("AccessToken");
            string username = settings.GetUsername();
            //string hostname = settings.GetHostname();
            string hostname = $"HIU-P{firstNumber}-{secondNumber}";
            
            //_logger.LogInformation($"{accessToken}, {username}");

            UpdateResponse response = RemoteAPI.UpdateDevice(accessToken, hostname, username);

            _logger.LogInformation($"req: changing hostname into `{hostname}` => res: {response.status}");
        }

        /// <summary>
        /// deplicated
        /// </summary>
        public void SetupDummyVal()
        {
            //settings.Set("AccessToken", "123");
            //_logger.LogInformation(settings.Get("AccessToken"));
            this.settings.SetAccessToken("112|laravel_sanctum_BCtYO6nnG7CqlKgPhk5gwTk8bEEXdqIGSfziIthAc73f957a");
            this.settings.SetHostname("HIU-P12-643");
            this.settings.SetDeviceId("123453212");
            this.settings.SetUsername("2000111");
        }

        public void Register()
        {
            // ãNìÆñàÇ…GUIDÇê∂ê¨ÇµÇƒDevideIdÇ∆ÇµÇƒÇ¢ÇÈ
            Guid g = System.Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            string deviceId = $"000000-{guid8}";
            string hostname = "HIU-P12-234";
            string username = "9999999";

            RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);
            string accessToken = response.response_data.access_token;

            RemoteAPI.LoginDevice(accessToken, username);

            settings.SetAccessToken(accessToken);
            settings.SetHostname(hostname);
            settings.SetDeviceId(deviceId);
            settings.SetUsername(username);
        }
    }
}