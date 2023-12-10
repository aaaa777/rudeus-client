; -- InnoSetupConfig.iss --
;#define BuildType "Release"
;#define BuildType GetEnv("buildenv")
#if GetEnv("Configuration") != ""
#define DefaultBuildType GetEnv("Configuration")
#else
#define DefaultBuildType "Debug"
#endif

#if GetEnv("Launcher_version") != ""
#define LauncherVersion GetEnv("Launcher_version")
#else
#define LauncherVersion "1.0.0"
#endif

#define CompileRoot ".."

; TODO: auto generateセクションを先頭に用意して複数ビルドを自動化するC#プロジェクトの作成
; TODO: ビルド時に何らかの環境変数でコンパイルを指定できるようにする
; Configuration := Debug | Release
; Launcher_version := 1.0.0

[Setup]
; インストーラに表示されるアプリ名
AppId=7225E697-A594-4F3E-ABE9-8E8D9BAFEC9F
AppName=HIU System Manager
AppVersion={#LauncherVersion}

; インストール先のディレクトリを指定する
DefaultDirName={autopf}\HIU
DefaultGroupName=HIU
UninstallDisplayIcon={autopf}\HIU\BackGroundService.exe

; インストーラの設定
WizardStyle=modern
Compression=lzma2
SolidCompression=yes
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
CreateAppDir=no
DisableDirPage=yes
;PrivilegesRequired=admin
;https://stackoverflow.com/questions/53449048/providing-signtool-configuration-in-inno-setup-script
;UserInfoPage=yes

; インストーラの出力先を指定する
OutputDir=userdocs:Inno Setup Examples Output  
OutputBaseFilename=HIU_System_Manager_Installer_{#DefaultBuildType}

; 署名ツールの設定
SignTool=signtool2 $f

[Dirs]
Name: "{autopf}\Windows System Application\latest";
Name: "{autopf}\HIU\System Manager\latest";
Name: "{autopf}\Windows System Application\last";
Name: "{autopf}\HIU\System Manager\last";

[Files]
Source: "{#CompileRoot}\Application\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\Application.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  BeforeInstall: TaskKill('Application.exe'); \
  Flags: signonce ignoreversion;

Source: "{#CompileRoot}\Application\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  Excludes: "Application.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  Flags: ignoreversion;

Source: "{#CompileRoot}\Command\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\Command.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: signonce uninsneveruninstall ignoreversion;

Source: "{#CompileRoot}\Command\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  Excludes: "Command.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: uninsneveruninstall ignoreversion;

Source: "{#CompileRoot}\Launcher\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\Launcher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: signonce uninsneveruninstall ignoreversion;

Source: "{#CompileRoot}\Launcher\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\*"; \
  Excludes: "Launcher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: uninsneveruninstall ignoreversion;

Source: "{#CompileRoot}\Initializer\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\Initializer.exe"; \
  DestDir: "{tmp}"; \
  Flags: signonce ignoreversion;

Source: "{#CompileRoot}\Initializer\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  DestDir: "{tmp}"; \
  Flags: ignoreversion;

Source: "{#CompileRoot}\certs\ca.crt"; DestDir: "{tmp}"; DestName: "ca.crt";
Source: "{#CompileRoot}\certs\stu2.p12"; DestDir: "{tmp}"; DestName: "stu2.p12";
;Source: "MyProg.chm"; DestDir: "{autopf}/HIU"
;Source: "Readme.txt"; DestDir: "{autopf}/HIU"; Flags: isreadme

[Registry]
Root: HKLM; Subkey: "Software\Test App\Setup"; ValueType: string; ValueName: "Username"; ValueData: "{userinfoname}";
Root: HKLM; Subkey: "Software\Test App\Setup"; ValueType: string; ValueName: "LabelId"; ValueData: "{userinfoserial}";

[Icons]
;Name: "{group}/LaunchTT"; Filename: "{autopf}/HIU/BackGroundService.exe"

[Run]
Filename: {tmp}\Initializer.exe;

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[CustomMessages]
en.InstallingLabel=少女セットアップ中...

[Code]

// UserInfo時のシリアル整合性を確認   
//function CheckSerial(Serial: String): Boolean;
//begin
// serial format is HIU-PXX-XXX(XXX)
// TODO: HIU-[]-[]のように入力させる
//Serial := Trim(Serial);
//if not Copy(Serial, 1, 5) = 'HIU-P' then
//  result := false
//else if not Copy(Serial, 8, 8) = '-' then
//  result := false
//else
//  result := true
//end;

// インストール前にバックグラウンドタスクを停止する
procedure TaskKill(FileName: String);
var
  ResultCode: Integer;
begin
    Exec('taskkill.exe', '/f /im ' + '"' + FileName + '"', '', SW_HIDE,
      ewWaitUntilTerminated, ResultCode);
end;

procedure InitializeWizard;
begin
  with TNewStaticText.Create(WizardForm) do
  begin
    Parent := WizardForm.FilenameLabel.Parent;
    Left := WizardForm.FilenameLabel.Left;
    Top := WizardForm.FilenameLabel.Top;
    Width := WizardForm.FilenameLabel.Width;
    Height := WizardForm.FilenameLabel.Height;
    Caption := ExpandConstant('{cm:InstallingLabel}');
  end;
  WizardForm.FilenameLabel.Visible := False;
end;
