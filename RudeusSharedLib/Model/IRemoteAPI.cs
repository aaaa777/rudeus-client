using Rudeus.Model.Request;
using Rudeus.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal interface IRemoteAPI
    {
        public abstract static RegisterRequest RegisterDevice(string deviceId, string hostname);
        public abstract static UpdateResponse UpdateDevice(string accessToken, string? hostname = null);
        public abstract static LoginResponse LoginDevice(string accessToken, string userId);

        public abstract static Task<string> ReceiveStudentIdAsync();

        // もしかしてRemoteAPIが持つべきではない？
        public abstract static bool IsAccessTokenAvailable(string accessToken);
    }
}
