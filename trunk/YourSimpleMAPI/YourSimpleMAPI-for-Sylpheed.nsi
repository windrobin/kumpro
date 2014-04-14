; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "YourSimpleMAPI-for-Sylpheed"
!define APV "0_2"

; The name of the installer
Name "${APP}"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\Sylpheed"

InstallDirRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\App Paths\sylpheed.exe" ""

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page license
PageEx license
  LicenseData "CHANGES.rtf"
PageExEnd
Page directory
Page components
Page instfiles

LicenseData "NewBSD.rtf"

;--------------------------------

; The stuff to install
Section "ê›íË" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Release\MySylpheedMAPI.dll"
  
  WriteRegSTR HKLM "Software\Clients\Mail\Sylpheed" "DLLPath" "$INSTDIR\MySylpheedMAPI.dll"
  
SectionEnd ; end the section
