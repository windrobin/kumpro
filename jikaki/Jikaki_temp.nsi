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
  !define VER "1.2.0.0"
  !define APV "1_2_0_0"

  ;Name and file
  Name "${APP} - ${VER}"
  OutFile "Setup_${APP}_${APV}.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" ""
  
  RequestExecutionLevel highest

  !include "LogicLib.nsh"

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
 
  !insertmacro XPUI_LANGUAGE "Japanese"

;--------------------------------
;Installer Sections

Section ""

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...
  File "release\jikaki.exe"

  ;Store installation folder
  
  ;Create uninstaller

SectionEnd

Section "ショートカットを作成：通常"
  CreateShortcut "$SMPROGRAMS\jikaki.lnk" "$INSTDIR\jikaki.exe"
SectionEnd

Section /o "ショートカットを作成：Ctrl+Alt+Qで起動"
  CreateShortcut "$SMPROGRAMS\jikaki.lnk" "$INSTDIR\jikaki.exe" "" "" "" "" "ALT|CONTROL|Q"
SectionEnd

Section "起動する"
  Exec '"$INSTDIR\jikaki.exe"'
SectionEnd
