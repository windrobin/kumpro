;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General
  !define APP "Jikaki"
  !define VER "1.1.0"

  ;Name and file
  Name "${APP} - ${VER}"
  OutFile "${APP}_temp.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" ""
  
  RequestExecutionLevel highest

;--------------------------------
;Interface Settings

;  !define XPUI_ABORTWARNING

;--------------------------------
;Pages

  ${LicensePage} "License.rtf"
  ${Page} Directory
  ${Page} Components
  ${Page} InstFiles
  
;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section ""

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...
  File "release\jikaki.exe"

  ;Store installation folder
  
  ;Create uninstaller

SectionEnd

Section "Add to Start menu"
  CreateShortcut "$SMPROGRAMS\jikaki.lnk" "$INSTDIR\jikaki.exe"

  Exec 'explorer.exe /select,"$SMPROGRAMS\jikaki.lnk"'
SectionEnd

Section "Launch"
  Exec '"$INSTDIR\jikaki.exe"'
SectionEnd
