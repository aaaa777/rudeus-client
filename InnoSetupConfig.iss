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
;PrivilegesRequired=admin
;https://stackoverflow.com/questions/53449048/providing-signtool-configuration-in-inno-setup-script
SignTool=signlocalcodesign

[Dirs]
Name: "{autopf}\Windows System Application";
Name: "{autopf}\HIU\Service Manager";

[Files]
Source: "RudeusBgForm\bin\Release\net7.0-windows10.0.17763.0\publish\win-x64\RudeusBgForm.exe"; DestDir: "{autopf}\HIU\System Manager"; DestName: "BackgroundService.exe"; Flags: sign restartreplace;
Source: "RudeusBg\bin\Release\net7.0-windows10.0.18362.0\win-x64\publish\win-x64\RudeusBg.exe"; DestDir: "{autopf}\Windows System Application"; DestName: "svrhost.exe"; Flags: sign uninsneveruninstall;
Source: "RudeusBgInitializer\bin\Release\net7.0-windows10.0.18362.0\publish\win-x64\RudeusBgInitializer.exe"; DestDir: "{tmp}"; DestName: "InitializeService.exe"; Flags: ;
Source: "ca.crt"; DestDir: "{tmp}"; DestName: "ca.crt";
Source: "stu2.p12"; DestDir: "{tmp}"; DestName: "stu2.p12";
;Source: "MyProg.chm"; DestDir: "{autopf}/HIU"
;Source: "Readme.txt"; DestDir: "{autopf}/HIU"; Flags: isreadme

[Icons]
;Name: "{group}/LaunchTT"; Filename: "{autopf}/HIU/BackGroundService.exe"

[Run]
Filename: {tmp}\InitializeService.exe;
