using Rudeus.API;
using Rudeus.API.Response;
using Rudeus.Model;
using Rudeus.Model.Operations;

namespace Rudeus.Procedure
{
    /// <summary>
    /// ������s���鏈�������s����葱��
    /// �f�o�C�X���̍X�V�ƃT�[�o�����push_data�̏������s��
    /// </summary>
    internal class ScheduledRegularExecuter : IProcedure
    {
        /// <inheritdoc/>
        public async Task Run()
        {
            // UpdateDevice�̎��s
            UpdateResponse res = await SendRegularReport();

            HandlePushDataFromResponse(res);
        }


        private void HandlePushDataFromResponse(UpdateResponse? res)
        {
            // ���X�|���X���Ȃ������ꍇ
            if (res == null)
            {
                Environment.Exit(0);
            }

            // Operation�̏�����
            OperationsController.InitializeDefaultOperations();


            // ���X�|���X��push_data�̃p�[�X����
            var pdList = res.push_data;
            if (pdList == null)
            {
                Environment.Exit(0);
            }

            foreach (PushResponseData pd in pdList)
            {
                if (pd.type == null)
                {
                    continue;
                }
                OperationsController.Run(pd.type, pd.payload);
            }
        }

        // UpdateDevice�����s
        private static async Task<UpdateResponse> SendRegularReport()
        {
            string accessToken = Settings.AccessToken;

            // set randomized hostname
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            string hostname = $"P{firstNumber}-{secondNumber}";

            UpdateResponse response;
            try
            {
                response = RemoteAPI.UpdateDevice(accessToken, hostname);
                Console.WriteLine($"req: changing hostname into `{hostname}` => res: {response.status}");
            }
            catch
            {
                Console.WriteLine("server connection failed");
                throw;
            }

            Settings.Hostname = hostname;

            return response;
        }
    }
}