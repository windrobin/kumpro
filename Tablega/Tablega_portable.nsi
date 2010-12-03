; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "Tablega"
!define VER "1.0"

; The name of the installer
Name "${APP}  ${VER}"

; The file to write
!ifdef URL
OutFile "${APP}_portable.exe"
!else
OutFile "${APP}_generic_portable.exe"
!endif

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel user

AutoCloseWindow true

;--------------------------------

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include LogicLib.nsh

;--------------------------------

; Pages

Page license
Page directory
Page instfiles

LicenseData "License.rtf"

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File "bin\release\*.*"
  
!ifdef URL
  Exec '"$INSTDIR\${APP}.exe" "${URL}"'
!else
  Exec '"$INSTDIR\${APP}.exe"'
!endif

SectionEnd ; end the section
