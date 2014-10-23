; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

!define APP "TestDownloadManager"
!define COM "HIRAOKA HYPERS TOOLS, Inc."

; The name of the installer
Name "${APP}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\${COM}\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
Section ""

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Release\TestDownloadManager.dll"
  
  ExecWait 'regsvr32.exe "$INSTDIR\TestDownloadManager.dll"' $0
  DetailPrint "Œ‹‰Ê: $0"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM "Software\${COM}\${APP}" "Install_Dir" "$INSTDIR"
  
  WriteRegStr HKCU "Software\Microsoft\Internet Explorer" "DownloadUI" "{F87D60B2-27E1-48EC-A32D-D4F5AC6E62F8}"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${APP}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  ; Remove files
  ExecWait 'regsvr32.exe /u "$INSTDIR\TestDownloadManager.dll"' $0
  Delete "$INSTDIR\TestDownloadManager.dll"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKLM "Software\${COM}\${APP}"
  DeleteRegValue HKCU "Software\Microsoft\Internet Explorer" "DownloadUI"

  ; Remove uninstaller
  Delete "$INSTDIR\uninstall.exe"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
