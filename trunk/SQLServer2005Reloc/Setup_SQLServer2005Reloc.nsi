; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "SQLServer2005Reloc"
!define VER "0.1.0.0"
!define APV "0_1_0_0"

; bin\x86\DEBUG

; The name of the installer
Name "${APP}"

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
Page directory
Page instfiles

LicenseData "NewBSD.rtf"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File /x "*.vshost.*" "bin\x86\DEBUG\*.*"
  
  Exec '"$INSTDIR\${APP}.exe"'
  
SectionEnd ; end the section

Section
  IfErrors Noop
  SetAutoClose true
Noop:
SectionEnd
