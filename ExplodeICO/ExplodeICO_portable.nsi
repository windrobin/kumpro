;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "ExplodeICO"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "0.1'"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "${APP}_portable.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${COM}\${APP}" ""

  RequestExecutionLevel user

  !define DOTNET_VERSION "3.5"

  !include "DotNET35.nsh"
  !include "LogicLib.nsh"

;--------------------------------
;Interface Settings

  !define XPUI_ABORTWARNING

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

Section "" SecDummy

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}
  
  ClearErrors

  ;ADD YOUR OWN FILES HERE...
  File /x "*.vshost.*" "bin\DEBUG\*.*"
  
  SetOutPath "$INSTDIR"

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR
  
  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"

SectionEnd

Section "Add to start menu"
  StrCpy $1 "$STARTMENU\ExplodeICO"
  StrCpy $2 "$INSTDIR\${APP}.exe"
  StrCpy $3 ""
  StrCpy $4 "$INSTDIR\1.ico"
  
  CreateDirectory "$1"
  CreateShortCut  "$1\ExplodeICO.lnk" "$2" "$3" "$4"
  CreateShortCut  "$1\Explode Icons.lnk" "$2" "$3" "$4"
  CreateShortCut  "$1\アイコン解体.lnk" "$2" "$3" "$4"
  CreateShortCut  "$1\アイコン素材.lnk" "$2" "$3" "$4"
SectionEnd

Section "Run ${APP}"
  Exec "$INSTDIR\${APP}.exe"
SectionEnd

Section ""
  IfErrors +2
    SetAutoClose true
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
