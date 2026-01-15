[Setup]
AppName=SambiDb
AppVersion=1.0.0
DefaultDirName={pf}\SambiDb
DefaultGroupName=SambiDb
OutputDir=output
OutputBaseFilename=SambiDbInstaller
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
DisableProgramGroupPage=yes

[Files]
; Copia TODOS os arquivos e pastas da publish
Source: "D:\GitHub\Sambi_DB\bin\Release\net8.0\win-x64\publish\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs ignoreversion

[Run]
; Cria o serviço (LocalSystem)
Filename: "sc.exe"; \
  Parameters: "create SambiDb binPath= ""{app}\db.exe"" start= auto"; \
  Flags: runhidden

; Inicia o serviço
Filename: "sc.exe"; \
  Parameters: "start SambiDb"; \
  Flags: runhidden

[UninstallRun]
; Para o serviço
Filename: "sc.exe"; \
  Parameters: "stop SambiDb"; \
  Flags: runhidden

; Remove o serviço
Filename: "sc.exe"; \
  Parameters: "delete SambiDb"; \
  Flags: runhidden
