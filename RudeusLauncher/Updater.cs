using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Model;

internal class Updater
{
    public string lastVersionDirName = "last";
    public string latestVersionDirName = "latest";
    public string tempdir;
    public static void Run(string registryKey)
    {
        // レジストリを切り替え
        Settings.UpdateRegistryKey(registryKey);

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
        // latestが起動確認できている場合のみlastにコピーさせる
        if (Settings.IsLatestVersionStatusOk())
        {
            SwitchLatestDirLast();
        }


        // 一時ディレクトリにダウンロード開始
        string tmpLatestPath = DownloadLatest();

        // latestを使用不能にマーク
        Settings.SetLatestVersionStatusUnlaunchable();

        // 一時ディレクトリを移動
        MvTempLatest(tmpLatestPath);

        //throw new NotImplementedException();
        
        // 何もなければ
    }

    private static void SwitchLatestDirLast()
    {
        // del, mv latest last
        Directory.Delete(Settings.LastVersionDirPath);
        Directory.Move(Settings.LatestVersionDirPath, Settings.LastVersionDirPath);
    }

    // Latestをダウンロードした一時ディレクトリ名を返す
    private static string DownloadLatest()
    {

        return $"temp\\";
    }

    //
    private static void MvTempLatest(string tmpDirName)
    {
        Directory.Move(tmpDirName, Settings.LatestVersionDirPath);
    }

    public static bool ShouldUpdate()
    {
        return true;
    }
}
