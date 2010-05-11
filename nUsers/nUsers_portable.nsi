; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "nUsers"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "${APP}_portable.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

XPStyle on

;--------------------------------

; Pages

Page directory
Page instfiles

ShowInstDetails show

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "release\${APP}.exe"
  
  ExecWait "$INSTDIR\${APP}.exe" $0
  
  DetailPrint "åãâ (ç≈ëÂÉÜÅ[ÉUÅ[êî): $0"

SectionEnd ; end the section
