; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

!define APP "FTP4AFP"
!define VER "0.1.0.0"
!define SVC "FTP4AFP"
!define TTL "FTP for AFP"
!define COM "HIRAOKA HYPERS TOOLS, Inc."

; The name of the installer
Name "${APP} ${VER}"

!searchreplace APV ${VER} "." "_"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\${COM}\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!define DOTNET_VERSION "3.5"

!include "DotNET35.nsh"
!include "LogicLib.nsh"

!include "nsDialogs.nsh"
!include "winmessages.nsh"
!include "nsDialogs_createTextMultiline.nsh"

;--------------------------------

; Pages

Page components
Page directory
Page custom nsDialogsPage nsDialogsPageLeave
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

var Settings

var Dialog
var Label
var Text

Function nsDialogsPage
  nsDialogs::Create 1018
    Pop $Dialog

  ${NSD_CreateLabel} "0" "0" "-5u" "10u" "Commandline:"
    Pop $Label

  ReadRegStr $0 HKLM "Software\${COM}\${APP}" "Settings"

  ${NSD_CreateTextMultiline} "0" "15u" "-5u" "-30u" "$0"
    Pop $Text

  nsDialogs::Show
FunctionEnd

Function nsDialogsPageLeave
  ${NSD_GetText} $Text $0

  StrCpy $Settings $0
FunctionEnd

InstType "Install GUI"
InstType "Install Service"
InstType "Update"
InstType "Uninstall"

Section ""
  SectionIn 1 2 3

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  !insertmacro CheckDotNET ${DOTNET_VERSION}

SectionEnd

Section "Stop nt service"
  SectionIn 2 3 4

  ExecWait 'net.exe stop ${SVC}' $0
  DetailPrint "Exitcode: $0"
SectionEnd

Section "Remove nt service"
  SectionIn 2 4
  
  ExecWait 'sc.exe delete ${SVC}' $0
  DetailPrint "Exitcode: $0"
SectionEnd

; The stuff to install
Section "FTP4AFP"
  SectionIn 1 2 3

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File /r /x "*.vshost.*" "bin\DEBUG\*.*"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM "Software\${COM}\${APP}" "Install_Dir" "$INSTDIR"
  
  WriteRegStr HKLM "Software\${COM}\${APP}" "Settings" $Settings

  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${TTL}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "Create desktop shortcut"
  SectionIn 1

  CreateShortCut "$DESKTOP\FTP4AFP.lnk" "$INSTDIR\${APP}.exe" '$Settings'
SectionEnd

Section "Launch"
  SectionIn 1
  
  Exec '"$INSTDIR\${APP}.exe" $Settings'
SectionEnd

Section "Create nt service"
  SectionIn 2
  
  FileOpen $0 "$INSTDIR\Commandline.txt" "w"
  FileWrite $0 $Settings
  FileClose $0

  ExecWait 'sc.exe create ${SVC} binPath= "\"$INSTDIR\${APP}.exe\" /service \"@$INSTDIR\Commandline.txt\" " start= auto depend= HTTP DisplayName= "${TTL}" ' $0
  DetailPrint "Exitcode: $0"
SectionEnd

Section "Start nt service"
  SectionIn 2 3

  ExecWait 'net.exe start ${SVC}' $0
  DetailPrint "Exitcode: $0"
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  ExecWait 'sc.exe stop ${SVC}' $0
  DetailPrint "Exitcode: $0"

  ExecWait 'sc.exe delete ${SVC}' $0
  DetailPrint "Exitcode: $0"

  ; Remove files and uninstaller
  Delete "$INSTDIR\uninstall.exe"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKLM "Software\${COM}\${APP}"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
