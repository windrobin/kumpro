;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "TrackLogging"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "1.0.0"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "Setup_${APP}.exe"

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

  !insertmacro XPUI_PAGE_LICENSE "License.rtf"
  !insertmacro XPUI_PAGE_DIRECTORY
  !insertmacro XPUI_PAGE_INSTFILES

  !insertmacro XPUI_PAGEMODE_UNINST
  !insertmacro XPUI_PAGE_UNINSTCONFIRM_NSIS
  !insertmacro XPUI_PAGE_INSTFILES

;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ;ADD YOUR OWN FILES HERE...
  File "bin\release\TrackLogging.exe"
  File "bin\release\TrackLogging.exe.config"
  File "bin\release\TrackLogging.pdb"
  
  SetOutPath "$INSTDIR"

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR

  ;Create uninstaller
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${APP}"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  Exec "$INSTDIR\TrackLogging.exe"

SectionEnd

Section

  CreateDirectory "$SMPROGRAMS\${APP}"
  CreateShortCut "$SMPROGRAMS\${APP}\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\${APP}\Run ${APP}.lnk" "$INSTDIR\TrackLogging.exe" "" "$INSTDIR\TrackLogging.exe" 0

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  ; Remove registry keys
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKCU "Software\${COM}\${APP}"

  ; Remove files and uninstaller
  Delete "$INSTDIR\TrackLogging.exe"
  Delete "$INSTDIR\TrackLogging.exe.config"
  Delete "$INSTDIR\TrackLogging.pdb"
  Delete "$INSTDIR\Uninstall.exe"

  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\${COM}\${APP}"

SectionEnd
