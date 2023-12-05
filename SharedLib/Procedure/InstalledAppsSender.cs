﻿using Rudeus.API;
using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Procedure
{
    /// <summary>
    /// 管理サーバにインストール済みアプリを送信する
    /// </summary>
    internal class InstalledAppsSender : IProcedure
    {
        /// <summary>
        /// 実行する
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            string accessToken = AppSettings.AccessToken;
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
