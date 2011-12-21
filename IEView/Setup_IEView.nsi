;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "IEView"
  !define TTL "IEView"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "0.1"
  !define APV "0_1"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "Setup_${APP}_${APV}.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${COM}\${APP}" "Install_Dir"

  RequestExecutionLevel user

  !define DOTNET_VERSION "2.0"

  !include LogicLib.nsh

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
 
  !insertmacro XPUI_LANGUAGE "Japanese"

;--------------------------------
;Installer Sections

Section "Dynamic link build" dynamic
  SetOutPath "$INSTDIR\Dynamic link"

  ;ADD YOUR OWN FILES HERE...
  File ".\release\IEView.exe"
  File ".\release\IEView.pdb"

SectionEnd

Section "Static link build" static
  SetOutPath "$INSTDIR\Static link"

  ;ADD YOUR OWN FILES HERE...
  File ".\release static\IEView.exe"
  File ".\release static\IEView.pdb"

SectionEnd

Section "表示"
  Exec 'explorer.exe "$INSTDIR"'
SectionEnd

Section ""
  ; Write the installation path into the registry
  WriteRegStr HKCU "Software\${COM}\${APP}" "Install_Dir" "$INSTDIR"
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_dynamic ${LANG_ENGLISH} "ダイナミックリンク版"
  LangString DESC_static ${LANG_ENGLISH} "スタティックリンク版"

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${dynamic} $(DESC_dynamic)
    !insertmacro XPUI_DESCRIPTION_TEXT ${static} $(DESC_static)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
