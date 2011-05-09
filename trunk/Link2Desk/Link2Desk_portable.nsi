; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "Link2Desk"

!define APV "1_1"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "${APP}_${APV}_portable.exe"

; The default installation directory
InstallDir "$APPDATA\Microsoft\AddIns"

; Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------

; Pages

Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Link2Desk.xla"
  
SectionEnd ; end the section
