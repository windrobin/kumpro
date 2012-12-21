; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "FindBrokenTasks"
!define VER "0.2.0.0"
!define APV "0_2_0_0"

; The name of the installer
Name "${APP} ${VER}"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include LogicLib.nsh

;--------------------------------

; Pages

Page license
PageEx license
   LicenseData "Task Scheduler Managed Wrapper.rtf"
 PageExEnd
Page directory
Page instfiles

LicenseData "FindBrokenTasks.rtf"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File /x "*.vshost.*" "bin\DEBUG\*.*"
  
  Exec "$INSTDIR\${APP}.exe"

  IfErrors +2
    SetAutoClose true
  
SectionEnd ; end the section
