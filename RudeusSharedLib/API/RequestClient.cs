// HTTP���N�G�X�g�̃X�^�u�쐬�pREST�N���C�A���g

using Rudeus.API.Request;
using System.Net;
using static Rudeus.API.Exceptions;

namespace Rudeus.API
{
    public class RequestClient : IRequestClient
    {
        HttpClient Client { get; set; }

        public RequestClient(string endpoint)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(endpoint)
            };
        }

        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            HttpResponseMessage response;
            try
            {
                response = Client.SendAsync(message).Result;
            }
            catch (HttpRequestException e)
            {
                throw new ServerUnavailableException(e.Message);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AccessTokenUnavailableException("�A�N�Z�X�g�[�N���������ł�");
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // �o�O�̉\��������
                throw new ServerUnavailableException(response.Content.ReadAsStringAsync().Result);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new UnexpectedResponseException($"`{message.RequestUri.ToString()}`�ŗ\�����Ȃ��X�e�[�^�X�R�[�h`{response.StatusCode}`���Ԃ���܂���");
            }

            return response;
        }

        public string RequestString(HttpRequestMessage message)
        {
            HttpResponseMessage res = Request(message);
            return  res.Content.ReadAsStringAsync().Result;
            
        }
    }
}