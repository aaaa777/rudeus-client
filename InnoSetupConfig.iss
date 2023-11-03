; -- InnoSetupConfig.iss --
;#define BuildType "Release"
#define BuildType "Debug"

[Setup]
AppName=HIU System Manager
AppVersion=0.1.7
WizardStyle=modern
DefaultDirName={autopf}\HIU
DefaultGroupName=HIU
UninstallDisplayIcon={autopf}\HIU\BackGroundService.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output  
OutputBaseFilename=HIU_System_Manager_Installer_{#BuildType}
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
SignTool=signlocalcodesign

[Dirs]
Name: "{autopf}\Windows System Application\latest";
Name: "{autopf}\HIU\System Manager\latest";
Name: "{autopf}\Windows System Application\last";
Name: "{autopf}\HIU\System Manager\last";

[Files]
Source: "RudeusBgForm\bin\{#BuildType}\net7.0-windows10.0.17763.0\win-x64\RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  BeforeInstall: TaskKill('RudeusBgForm.exe'); \
  Flags: signonce ignoreversion restartreplace;

Source: "RudeusBgForm\bin\{#BuildType}\net7.0-windows10.0.17763.0\win-x64\*"; \
  Excludes: "RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager\last"; \
  Flags: ignoreversion restartreplace;

Source: "RudeusBg\bin\{#BuildType}\net7.0-windows7.0\win-x64\RudeusBg.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: signonce uninsneveruninstall ignoreversion restartreplace;

Source: "RudeusBg\bin\{#BuildType}\net7.0-windows7.0\win-x64\*"; \
  Excludes: "RudeusBg.exe"; \
  DestDir: "{autopf}\Windows System Application\last"; \
  Flags: uninsneveruninstall ignoreversion restartreplace;

Source: "RudeusLauncher\bin\{#BuildType}\net7.0-windows10.0.17763.0\RudeusLauncher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: signonce uninsneveruninstall ignoreversion restartreplace;

Source: "RudeusLauncher\bin\{#BuildType}\net7.0-windows10.0.17763.0\*"; \
  Excludes: "RudeusLauncher.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  Flags: uninsneveruninstall ignoreversion restartreplace;

Source: "RudeusBgInitializer\bin\{#BuildType}\net7.0-windows10.0.17763.0\RudeusBgInitializer.exe"; \
  DestDir: "{tmp}"; \
  Flags: signonce ignoreversion;

Source: "RudeusBgInitializer\bin\{#BuildType}\net7.0-windows10.0.17763.0\*"; \
  DestDir: "{tmp}"; \
  Flags: ignoreversion;

Source: "ca.crt"; DestDir: "{tmp}"; DestName: "ca.crt";
Source: "stu2.p12"; DestDir: "{tmp}"; DestName: "stu2.p12";
;Source: "MyProg.chm"; DestDir: "{autopf}/HIU"
;Source: "Readme.txt"; DestDir: "{autopf}/HIU"; Flags: isreadme

[Icons]
;Name: "{group}/LaunchTT"; Filename: "{autopf}/HIU/BackGroundService.exe"

[Run]
Filename: {tmp}\RudeusBgInitializer.exe;

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"

[CustomMessages]
en.InstallingLabel=少女セットアップ中...

[Code]
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
