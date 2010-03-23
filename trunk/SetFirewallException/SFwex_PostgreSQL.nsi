;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

  !define APP "SFwex_PostgreSQL"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."

;--------------------------------
;General

  ;Name and file
  Name "PostgreSQL ファイアウォール 例外の追加"
  OutFile "${APP}.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${COM}\${APP}" ""
  
  RequestExecutionLevel admin

;--------------------------------
;Interface Settings

  !define XPUI_ABORTWARNING

;--------------------------------
;Pages

  ${Page} Welcome
  ${LicensePage} "License.rtf"
  ${Page} Directory
  ${Page} Components
  ${Page} InstFiles
  
;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "PostgreSQL (TCP port 5432) 許可" SecDummy

  SetOutPath "$INSTDIR"
  
  ;ADD YOUR OWN FILES HERE...
  File "release\SetFirewallException.exe"
  
  ExecWait '"$INSTDIR\SetFirewallException.exe" set port tcp 5432 "PostgreSQL tcp port"' $0
  DetailPrint "結果: $0"
  
  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR
  
  ;Create uninstaller

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} ""

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section
