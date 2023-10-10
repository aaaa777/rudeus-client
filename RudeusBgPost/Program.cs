// See https://aka.ms/new-console-template for more information
using Rudeus.Model;
using Rudeus.Model.Response;
using Rudeus.Model.Operations;

class Program
{
    private static Settings settings;

    static void Main(string[] args)
    {
        // 初期化
        settings = Settings.Load();
        Operation.InitializeOperations();


        // 使用可能なアクセストークンがない場合
        if (IsFirstRun() || RemoteAPI.IsAccessTokenAvailable(settings.AccessToken))
        {
            // デバイスIDを発行
            Register();
        }


        // POSTを実行
        bool isOneShot = true;
        do
        {
            //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            UpdateResponse res = PostInformation();
            PushResponseData[] pdList = res.push_data;

            foreach (PushResponseData pd in pdList)
            {
                Operation.Run(pd.opcode);
            }

        }
        while (!isOneShot);

        Environment.Exit(0);
    }

    private static UpdateResponse PostInformation()
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
            Console.WriteLine($"req: changing hostname into `{hostname}` => res: {response.status}");
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine("server connection failed");
        }
        return null;
    }

    public static void Register()
    {
        // 起動毎にGUIDを生成してDevideIdとしている
        Guid g = System.Guid.NewGuid();
        string guid8 = g.ToString().Substring(0, 8);
        string deviceId = $"000000-{guid8}";
        string hostname = "HIU-P12-234";
        string username = "9999999";

        RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);
        string accessToken = response.response_data.access_token;

        RemoteAPI.LoginDevice(accessToken, username);

        settings.AccessToken = accessToken;
        settings.FirstHostname = hostname;
        settings.Hostname = hostname;
        settings.DeviceId = deviceId;
        settings.Username = username;
    }

    public static bool IsFirstRun()
    {
        string accessToken = settings.AccessToken;
        if (accessToken != null && accessToken != "")
        {
            return true;
        }
        return false;
    }

    private static void CallOperation(string opcode)
    {
        Operation.Run(opcode);
    }
}
