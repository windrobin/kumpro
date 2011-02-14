;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "SendHostAd"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "1.0"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "Setup_${APP}.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\${APP}"

  ;Get installation folder from registry if available
  InstallDirRegKey HKLM "Software\${COM}\${APP}" ""

  RequestExecutionLevel admin

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
  ${Page} InstFiles
  
;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ;ADD YOUR OWN FILES HERE...
  File "bin\RELEASE\SendHostAd.exe"
  File "bin\RELEASE\SendHostAd.exe.config"
  File "bin\RELEASE\SendHostAd.pdb"
  File "bin\RELEASE\TaskScheduler.dll"

  ;Store installation folder
  WriteRegStr HKLM "Software\${COM}\${APP}" "" $INSTDIR
  
  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  Exec "$INSTDIR\SendHostAd.exe"

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
