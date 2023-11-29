using Rudeus.Model.Request;
using Rudeus.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal interface IRemoteAPI
    {
        // メソッドは必ず例外処理を行うこと
        // 開発時以外はサーバ起因の例外しか発生しないので、基本はリトライ処理だけでいい
        public abstract static RegisterResponse RegisterDevice(string deviceId, string hostname);
        public abstract static UpdateResponse UpdateDevice(string accessToken, string? hostname = null);
        public abstract static LoginResponse LoginDevice(string accessToken, string userId);

        public abstract static BaseResponse SendInstalledApps(string accessToken, List<InstalledApplications> apps);

        public abstract static Task<string> ReceiveStudentIdAsync();

        // もしかしてRemoteAPIが持つべきではない？
        public abstract static bool IsAccessTokenAvailable(string accessToken);

        public abstract static bool IsRemoteReachable();
    }
}
