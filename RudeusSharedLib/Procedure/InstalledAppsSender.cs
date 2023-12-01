using Rudeus.API;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Procedure
{
    internal class InstalledAppsSender : IProcedure
    {
        public static async void Run()
        {
            string accessToken = Settings.AccessToken;
            // インストール済みアプリ送信
            // TODO: WatchDogs
            try
            {
                List<ApplicationData> installedApps = await InstalledApplications.LoadAsync();
                RemoteAPI.SendInstalledApps(accessToken, installedApps);
            }
            catch
            {
                Console.WriteLine("failed to send installed apps");
                throw;
            }
        }
    }
}
