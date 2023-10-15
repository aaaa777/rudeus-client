using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Model;

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
        string latestVersionFromUrl = "1.0.0.1";

        // アップデート判定
        if (!ShouldUpdate()) 
        { 
            return;
        }

        // アップデート開始
        try
        {
            StartUpdate();
        }
        catch
        {
            // アップデート失敗、フォールバック処理
            return;
        }

        // アップデート完了、起動先変更
        Settings.SetLatestVersionStatusDownloaded();
    }

    private static void StartUpdate()
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
        string tmpLatestPath = DownloadLatest();


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

    // Latestをダウンロードした一時ディレクトリ名を返す
    private static string DownloadLatest()
    {
        Guid g = Guid.NewGuid();
        string guid8 = g.ToString().Substring(0, 8);
        string tempDir = $"{Path.GetTempPath()}{guid8}";
#if (DEBUG)
        Utils.CopyDirectory(@"C:\Users\a774n\source\repos\Rudeus\RudeusBgForm\bin\Release\net7.0-windows10.0.17763.0", tempDir, true);
#else
    
#endif
        return tempDir;
    }

    //
    private static void MvTempLatest(string tmpDirName)
    {
        Directory.Move(tmpDirName, $"{Settings.LatestVersionDirPath}");
    }

    public static bool ShouldUpdate()
    {
        return true;
    }
}
