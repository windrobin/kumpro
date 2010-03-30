;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "StatFilesbyExt"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "1.0.0"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "${APP}_portable.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${COM}\${APP}" ""

  RequestExecutionLevel user

  !define DOTNET_VERSION "2.0"

  !include "DotNET.nsh"
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
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "" SecDummy

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ;ADD YOUR OWN FILES HERE...
  File "bin\release\StatFilesbyExt.exe"
  File "bin\release\StatFilesbyExt.pdb"
  
  SetOutPath "$INSTDIR"

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR
  
  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"
  
SectionEnd

Section "‹N“®‚·‚é"
  Exec "$INSTDIR\StatFilesbyExt.exe"
SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
