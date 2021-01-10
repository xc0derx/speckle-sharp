; ↓↓↓ MAKE SURE TO UPDATE THESE ↓↓↓
#define Name "Dynamo"
#define AppName "Speckle for " + Name
#define AppVersion GetFileVersion("ConnectorDynamo\bin\ReleaseRevit\SpeckleConnectorDynamo.dll")
#define AppPublisher "Speckle"
#define AppURL "https://speckle.systems"
#define SpeckleFolder "{localappdata}\Speckle\" + Name

[Setup]
; ↓↓↓ MAKE SURE TO UPDATE THIS ↓↓↓
AppId={{BE8FB58E-6EC4-42E4-A2D8-B23FC816346D} 
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}
DefaultDirName={#SpeckleFolder}
DisableDirPage=yes
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
DisableWelcomePage=no
OutputDir="..\Builds\Installers"
OutputBaseFilename={#AppName} Setup {#AppVersion}
SetupIconFile=..\Build\InnoSetup\speckle.ico
Compression=lzma
SolidCompression=yes
WizardImageFile=..\Build\InnoSetup\installer.bmp
ChangesAssociations=yes
PrivilegesRequired=lowest
VersionInfoVersion={#AppVersion}


[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{app}"; Permissions: everyone-full 

[Files]
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.1\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.2\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.3\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.4\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.5\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.6\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.7\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.8\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Revit\*"; DestDir: "{userappdata}\Dynamo\Dynamo Revit\2.9\packages\"; Flags: ignoreversion recursesubdirs; 

Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.1\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.2\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.3\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.4\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.5\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.6\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.7\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.8\packages\"; Flags: ignoreversion recursesubdirs; 
Source: "ConnectorDynamo\dist\Sandbox\*"; DestDir: "{userappdata}\Dynamo\Dynamo Core\2.9\packages\"; Flags: ignoreversion recursesubdirs; 