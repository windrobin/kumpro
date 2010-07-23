;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General
  !define APP "SetKeepAliveTime"
  
  !define MAJ 1
  !define MIN 2

  ;Name and file
  Name "${APP} ${MAJ}.${MIN}"
  OutFile "${APP}_${MAJ}_${MIN}_portable.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" ""
  
  RequestExecutionLevel admin

;--------------------------------
;Interface Settings

;  !define XPUI_ABORTWARNING

;--------------------------------
;Pages

  ${LicensePage} "License.rtf"
  ${Page} Directory
  ${Page} InstFiles
  
;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"
  
  ;ADD YOUR OWN FILES HERE...
  File "release\SetKeepAliveTime.exe"
  
  ;Store installation folder
  
  ;Create uninstaller
  
  Exec "$INSTDIR\SetKeepAliveTime.exe"

SectionEnd
