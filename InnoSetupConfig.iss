; -- InnoSetupConfig.iss --

[Setup]
AppName=HIU System Manager
AppVersion=0.1.1
WizardStyle=modern
DefaultDirName={autopf}\HIU
DefaultGroupName=HIU
UninstallDisplayIcon={autopf}\HIU\BackGroundService.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output  
OutputBaseFilename=HIU_System_Manager_Installer
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
Name: "{autopf}\Windows System Application";
Name: "{autopf}\HIU\Service Manager";

[Files]
Source: "RudeusBgForm\bin\publish\RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager"; \
  BeforeInstall: TaskKill('RudeusBgForm.exe'); \
  Flags: signonce ignoreversion;

Source: "RudeusBgForm\bin\publish\*"; \
  Excludes: "RudeusBgForm.exe"; \
  DestDir: "{autopf}\HIU\System Manager"; \
  Flags: ignoreversion;

Source: "RudeusBg\bin\publish\RudeusBg.exe"; \
  DestDir: "{autopf}\Windows System Application"; \
  DestName: "svrhost.exe"; \
  Flags: signonce uninsneveruninstall ignoreversion;

Source: "RudeusBgInitializer\bin\publish\RudeusBgInitializer.exe"; \
  DestDir: "{tmp}"; \
  Flags: signonce ignoreversion;

Source: "RudeusBgInitializer\bin\publish\*"; \
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
