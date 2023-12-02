; -- InnoSetupConfig.iss --
;#define BuildType "Release"
;#define BuildType GetEnv("buildenv")
#if GetEnv("RUDEUS_BUILD_TYPE") != ""
#define DefaultBuildType GetEnv("RUDEUS_BUILD_TYPE")
#else
#define DefaultBuildType "Debug"
#endif

; TODO: auto generateセクションを先頭に用意して複数ビルドを自動化するC#プロジェクトの作成
; TODO: ビルド時に何らかの環境変数でコンパイルを指定できるようにする
; RUDEUS_BUILD_TYPE := Debug | Release

[Setup]
AppId=7225E697-A594-4F3E-ABE9-8E8D9BAFEC9F
AppName=HIU System Manager
AppVersion=0.1.7
WizardStyle=modern
DefaultDirName={autopf}\HIU
DefaultGroupName=HIU
UninstallDisplayIcon={autopf}\HIU\BackGroundService.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output  
OutputBaseFilename=HIU_System_Manager_Installer_{#DefaultBuildType}
; "ArchitecturesAllowed=x64" specifies that Setup cannot run on
; anything but x64.
ArchitecturesAllowed=x64
; "ArchitecturesInstallIn64BitMode=x64" requests that the install be
; done in "64-bit mode" on x64, meaning it should use the native
; 64-bit Program Files directory and the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64
CreateAppDir=no
DisableDirPage=yes
;PrivilegesRequired=admin
;https://stackoverflow.com/questions/53449048/providing-signtool-configuration-in-inno-setup-script
;UserInfoPage=yes
;SignTool=signtoolexe sign /f $qC:\Users\a774n\source\repos\Rudeus\codesign2.p12$q /p exampleexampleexample /t http://timestamp.comodoca.com/authenticode /d $qSystem Program$q /fd SHA256 $f
SignTool=signtool2

[Dirs]
Name: "{autopf}\Windows System Application\latest";
Name: "{autopf}\HIU\System Manager\latest";
Name: "{autopf}\Windows System Application\last";
Name: "{autopf}\HIU\System Manager\last";

[Files]
Source: "RudeusBgForm\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  BeforeInstall: TaskKill('RudeusBgForm.exe'); \
  Flags: signonce ignoreversion;

Source: "RudeusBgForm\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  Excludes: "RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  Flags: ignoreversion;

Source: "RudeusBg\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\RudeusBg.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: signonce uninsneveruninstall ignoreversion;

Source: "RudeusBg\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  Excludes: "RudeusBg.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: uninsneveruninstall ignoreversion;

Source: "RudeusLauncher\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\RudeusLauncher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: signonce uninsneveruninstall ignoreversion;

Source: "RudeusLauncher\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\*"; \
  Excludes: "RudeusLauncher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: uninsneveruninstall ignoreversion;

Source: "RudeusBgInitializer\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\RudeusBgInitializer.exe"; \
  DestDir: "{tmp}"; \
  Flags: signonce ignoreversion;

Source: "RudeusBgInitializer\bin\{#DefaultBuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  DestDir: "{tmp}"; \
  Flags: ignoreversion;

Source: "ca.crt"; DestDir: "{tmp}"; DestName: "ca.crt";
Source: "stu2.p12"; DestDir: "{tmp}"; DestName: "stu2.p12";
;Source: "MyProg.chm"; DestDir: "{autopf}/HIU"
;Source: "Readme.txt"; DestDir: "{autopf}/HIU"; Flags: isreadme

[Registry]
Root: HKLM; Subkey: "Software\Test App\Setup"; ValueType: string; ValueName: "Username"; ValueData: "{userinfoname}";
Root: HKLM; Subkey: "Software\Test App\Setup"; ValueType: string; ValueName: "LabelId"; ValueData: "{userinfoserial}";

[Icons]
;Name: "{group}/LaunchTT"; Filename: "{autopf}/HIU/BackGroundService.exe"

[Run]
Filename: {tmp}\RudeusBgInitializer.exe;

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
