; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "JSOpen"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "${APP}_portable.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

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
  File "release\JSOpen.exe"
  File "B8DA6310-E19B-11D0-933C-00A0C90DCAA9.reg"
  
  Exec '"$INSTDIR\JSOpen.exe"'
  
SectionEnd ; end the section
