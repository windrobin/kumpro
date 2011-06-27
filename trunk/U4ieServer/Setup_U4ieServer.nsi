; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "U4ieServer"
!define APV "1.0"

; The name of the installer
Name "${APP} ${APV}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include LogicLib.nsh

AutoCloseWindow true

;--------------------------------

; Pages

Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File "REGU\bin\x86\release\REGU.exe"
  File "REGU\bin\x86\release\REGU.exe.config"
  File "REGU\bin\x86\release\REGU.pdb"
  File "REGU\bin\x86\release\TaskScheduler_2_0.dll"
  File "bin\release\U4ieServer.exe"
  File "bin\release\U4ieServer.pdb"
  
  Exec '"$INSTDIR\REGU.exe"'

SectionEnd ; end the section
