using Rudeus.Model;
using Rudeus.Model.Response;
using Rudeus.Model.Operations;

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

            // 使用可能なアクセストークンがない場合
            if (IsFirstRun() || RemoteAPI.IsAccessTokenAvailable(settings.AccessToken))
            {
                // デバイスIDを発行
                Register();
            }

            Operation.InitializeDefaultOperations();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var res = PostInformation();
            if(res == null)
            {
                Environment.Exit(0);
            }

            var pdList = res.push_data;
            if(pdList == null)
            {
                Environment.Exit(0);
            }

            foreach (PushResponseData pd in pdList)
            {
                if (pd.opcode == null)
                {
                    continue;
                }
                Operation.Run(pd.opcode);
            }

            await Task.Delay(5000, stoppingToken);
            
            Environment.Exit(0);
        }

        private UpdateResponse? PostInformation()
        {
            //string accessToken = settings.GetAccessToken();
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();

            // set randomized hostname
            string accessToken = settings.AccessToken;
            string username = settings.Username;

            //string hostname = settings.GetHostname();
            string hostname = $"HIU-P{firstNumber}-{secondNumber}";
            settings.Hostname = hostname;

            //_logger.LogInformation($"{accessToken}, {username}");

            try
            { 
                UpdateResponse response = RemoteAPI.UpdateDevice(accessToken, hostname, username);
                _logger.LogInformation($"req: changing hostname into `{hostname}` => res: {response.status}");
                return response;
            } 
            catch
            {
                _logger.LogInformation("server connection failed");
            }
            return null;
        }

        /// <summary>
        /// deplicated
        /// </summary>
        public void SetupDummyVal()
        {
            //settings.Set("AccessToken", "123");
            //_logger.LogInformation(settings.Get("AccessToken"));
            this.settings.AccessToken = "112|laravel_sanctum_BCtYO6nnG7CqlKgPhk5gwTk8bEEXdqIGSfziIthAc73f957a";
            this.settings.Hostname = "HIU-P12-643";
            this.settings.DeviceId = "123453212";
            this.settings.Username = "2000111";
        }

        public void Register()
        {
            // 起動毎にGUIDを生成してDevideIdとしている
            Guid g = System.Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            string deviceId = $"000000-{guid8}";
            string hostname = "HIU-P12-234";
            string username = "9999999";

            RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);
            string accessToken = response.response_data?.access_token ?? throw new Exception("Access Token not found in response");

            RemoteAPI.LoginDevice(accessToken, username);

            settings.AccessToken = accessToken;
            settings.FirstHostname = hostname;
            settings.Hostname = hostname;
            settings.DeviceId = deviceId;
            settings.Username = username;
        }

        public bool IsFirstRun()
        {
            string accessToken = settings.AccessToken;
            if(accessToken != null && accessToken != "")
            {
                return true;
            }
            return false;
        }

        private void CallOperation(string opcode)
        {
            //Operation.Run(opcode);
        }
    }
}