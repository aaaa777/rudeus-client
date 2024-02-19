# ビルド方法

ビルド方法についての説明ページ

## 手動でビルドする場合

Visual Studio 2022、.NET 7.0 SDK、Inno Setupがインストールされていることを確認してください。

依存パッケージをダウンロードした後、Visual Studio 2022でプロジェクトをビルドします。

Inno Setupを起動して`Installer/InnoSetupConfig.iss`を開き、ビルドします。

実行ファイルに署名する場合はこのときsigntoolを設定してください。

Windowsは実行ファイルに署名がされていない場合、実行が不可能な場合があります。
