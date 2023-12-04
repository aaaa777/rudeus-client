using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rudeus.API;
using Rudeus.API.Response;
using Rudeus.Model;
using Rudeus;
using Rudeus.Procedure;

namespace Rudeus.Launcher
{
    /// <summary>
    /// 起動アプリのアップデートを行う手続き
    /// </summary>
    public class Updater : IProcedure
    {
        private string? RegistryKey;
        public ISettings _settings { get; set; }
        private string lastVersionDirName = "last";
        private string latestVersionDirName = "latest";
        private string tempdir;

        public Updater(ISettings? aps = null)
        {
            _settings = aps ?? new Settings();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // Todo: Bgの方のアップデート機能を追加すること
            //if(registryKey != Constants.RudeusBgFormRegKey)
            //{
            //    return;
            //}

            //RegistryKey = registryKey;

            // アップデート情報取得
            // 最終的にRemoteAPIを利用したい
            UpdateMetadataResponse? res = null;
            try
            {
                res = RemoteAPI.GetUpdateMetadata(Settings.AccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (res == null)
            {
                return;
            }

            string latestVersionLocal = _settings.CurrentVersionP;

            string latestVersionRemote = res.request_data.stable_version;
            string latestVersionZipUrl = res.request_data.stable_zip_url;

            // アップデート判定
            if (!ShouldUpdate(latestVersionLocal, latestVersionRemote))
            {
                Console.WriteLine("Nothing to update");
                return;
            }
            Console.WriteLine("Update found");

            // アップデート開始
            try
            {
                Console.WriteLine($"Updating `{latestVersionLocal}` ->`{latestVersionRemote}` ...");
                StartUpdate(latestVersionZipUrl);

                // アップデート成功後、バージョンを変更
                _settings.CurrentVersionP = latestVersionRemote;
            }
            catch
            {
                // アップデート失敗、フォールバック処理
                Console.WriteLine("Update failed");
                Console.WriteLine("Updating app will be retryed on next launching");
                return;
            }

            // アップデート完了
            Console.WriteLine("Update is completed");

            //起動先変更
            Settings.SetLatestVersionStatusDownloaded();
        }

        private void StartUpdate(string url)
        {
            // レジストリを切り替え
            //Settings.UpdateRegistryKey(RegistryKey);

            // latestが起動確認できている場合のみlastにコピーさせる
            if (Settings.IsLatestVersionStatusOk())
            {
                // latestを使用不能にマーク
                Settings.SetLatestVersionStatusUnlaunchable();

                SwitchLatestDirLast();
            }
            else
            {
                // latestを使用不能にマーク
                Settings.SetLatestVersionStatusUnlaunchable();

                if (Directory.Exists(Settings.LatestVersionDirPath))
                {
                    Directory.Delete(Settings.LatestVersionDirPath, true);
                }
            }

            // 一時ディレクトリにダウンロード開始
            string tmpLatestPath = DownloadLatest(url);


            // 一時ディレクトリを移動
            MvTempLatest(tmpLatestPath);


            // 何もなければ終了
        }

        private void SwitchLatestDirLast()
        {
            // del, mv latest last
            //Settings.UpdateRegistryKey(RegistryKey);
            if (Directory.Exists(Settings.LastVersionDirPath))
            {
                Directory.Delete(Settings.LastVersionDirPath, true);
            }

            if (Directory.Exists(Settings.LatestVersionDirPath))
            {
                Directory.Move(Settings.LatestVersionDirPath, Settings.LastVersionDirPath);
            }
        }

        // Latestをダウンロード
        // 返りはダウンロードした一時ディレクトリ名
        private string DownloadLatest(string url)
        {
            Guid g = Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            string tempDir = $"{Path.GetTempPath()}{guid8}";

            Directory.CreateDirectory(tempDir);

            //Utils.CopyDirectory(@"C:\Users\a774n\source\repos\Rudeus\RudeusBgForm\bin\Release\net7.0-windows10.0.17763.0", tempDir, true);

            WebClient wc = new();
            wc.DownloadFile(url, $"{tempDir}\\RudeusBg_release.zip");
            ZipFile.ExtractToDirectory($"{tempDir}\\RudeusBg_release.zip", tempDir);
            File.Delete($"{tempDir}\\RudeusBg_release.zip");

            return tempDir;
        }

        // 一時フォルダをそのままlatestに移動
        private void MvTempLatest(string tmpDirName)
        {
            Directory.Move(tmpDirName, $"{Settings.LatestVersionDirPath}");
        }

        public bool ShouldUpdate(string localVersion, string remoteVersion)
        {
            int conStat = Utils.CompareVersionString(localVersion, remoteVersion);
            return conStat == -1;

        }
    }
}