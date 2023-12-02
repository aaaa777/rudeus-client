using Rudeus.API.Request;
using Rudeus.API.Response;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.API
{
    internal interface IRemoteAPI
    {
        public abstract static IRequestClient RequestClient { get; set; }
        //public abstract static IRequestClient NoCertRequestClient { get; set; }
       

        // メソッドは必ず例外処理を行うこと
        // 開発時以外はサーバ起因の例外しか発生しないので、基本はリトライ処理だけでいい
        public abstract static RegisterResponse RegisterDevice(string deviceId, string hostname);
        public abstract static UpdateResponse UpdateDevice(string accessToken, string? hostname = null);
        public abstract static LoginResponse LoginDevice(string accessToken, string userId);

        public abstract static BaseResponse SendInstalledApps(string accessToken, List<InstalledApplications> apps);

        public abstract static Task<string> ReceiveStudentIdAsync();

        public abstract static UpdateMetadataResponse GetUpdateMetadata(string accessToken);

        // もしかしてRemoteAPIが持つべきではない？
        public abstract static bool IsAccessTokenAvailable(string accessToken);

        public abstract static bool IsRemoteReachable();
    }
}
