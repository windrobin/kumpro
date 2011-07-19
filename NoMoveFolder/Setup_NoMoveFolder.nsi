; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "NoMoveFolder"
!define APV "1.0.0"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

SetOverwrite ifdiff

;--------------------------------

; Pages

Page directory
Page components
Page instfiles

;--------------------------------

; The stuff to install
Section "32ビット版" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR\${APV}_x86"
  
  ; Put file there
  File "Release\NoMoveFolder.dll"
  File "Release\NoMoveFolder.pdb"

  ExecWait 'REGSVR32 /s "$OUTDIR\NoMoveFolder.dll"' $0
  DetailPrint "32ビット版、登録結果: $0"
  
SectionEnd ; end the section

Section "64ビット版" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath "$INSTDIR\${APV}_x64"

  ; Put file there
  File "x64\Release\NoMoveFolder.dll"
  File "x64\Release\NoMoveFolder.pdb"

  ExecWait 'REGSVR32 /s "$OUTDIR\NoMoveFolder.dll"' $0
  DetailPrint "64ビット版、登録結果: $0"

SectionEnd ; end the section
