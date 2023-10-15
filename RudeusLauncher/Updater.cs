using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Model;
using Rudeus.Model.Response;

internal class Updater
{
    private static string? RegistryKey;
    public static string lastVersionDirName = "last";
    public static string latestVersionDirName = "latest";
    public static string tempdir;
    public static void Run(string registryKey)
    {
        RegistryKey = registryKey;

        // アップデート情報取得
        // 最終的にRemoteAPIを利用したい
        UpdateMetadataResponse res = RemoteAPI.GetUpdateMetadata();
        string latestVersionRemote = res.response_data.latest_version;
        string latestVersionZipUrl = res.response_data.latest_raw_zip;

        // アップデート判定
        if (!ShouldUpdate()) 
        { 
            return;
        }

        // アップデート開始
        try
        {
            StartUpdate(latestVersionZipUrl);
        }
        catch
        {
            // アップデート失敗、フォールバック処理
            return;
        }

        // アップデート完了、起動先変更
        Settings.SetLatestVersionStatusDownloaded();
    }

    private static void StartUpdate(string url)
    {
        // レジストリを切り替え
        Settings.UpdateRegistryKey(RegistryKey);

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

            if(Directory.Exists(Settings.LatestVersionDirPath)) 
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

    private static void SwitchLatestDirLast()
    {
        // del, mv latest last
        Settings.UpdateRegistryKey(RegistryKey);
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
    private static string DownloadLatest(string url)
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
    private static void MvTempLatest(string tmpDirName)
    {
        Directory.Move(tmpDirName, $"{Settings.LatestVersionDirPath}");
    }

    public static bool ShouldUpdate()
    {
#if(DEBUG)
        return true;
#else
        return false;
#endif
    }
}
