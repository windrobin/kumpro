;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !define XPUI_AUTOCLOSE true

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "Check64BITS"
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

Section ""

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR

SectionEnd

Section "Any CPU" Sec_anycpu

  SetOutPath "$INSTDIR"

  ;ADD YOUR OWN FILES HERE...
  File "bin\release\Check64BITS.exe"
  File "bin\release\Check64BITS.pdb"
  
  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"
  
  Exec '"$INSTDIR\Check64BITS.exe"'

SectionEnd

Section "x86" Sec_x86 ; ------

  SetOutPath "$INSTDIR\x86"

  ;ADD YOUR OWN FILES HERE...
  File "bin\x86\release\Check64BITS.exe"
  File "bin\x86\release\Check64BITS.pdb"

  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"

  Exec '"$INSTDIR\x86\Check64BITS.exe"'

SectionEnd

Section "x64" Sec_x64 ; ------

  SetOutPath "$INSTDIR\x64"

  ;ADD YOUR OWN FILES HERE...
  File "bin\x64\release\Check64BITS.exe"
  File "bin\x64\release\Check64BITS.pdb"

  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"

  Exec '"$INSTDIR\x64\Check64BITS.exe"'

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_Sec_anycpu ${LANG_ENGLISH} "processorArchitecture Any CPUで構築した試験体を使用"
  LangString DESC_Sec_x86 ${LANG_ENGLISH} "processorArchitecture x86で構築した試験体を使用"
  LangString DESC_Sec_x64 ${LANG_ENGLISH} "processorArchitecture x64で構築した試験体を使用"

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${Sec_x64} $(DESC_Sec_x64)
    !insertmacro XPUI_DESCRIPTION_TEXT ${Sec_x86} $(DESC_Sec_x86)
    !insertmacro XPUI_DESCRIPTION_TEXT ${Sec_anycpu} $(DESC_Sec_anycpu)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
