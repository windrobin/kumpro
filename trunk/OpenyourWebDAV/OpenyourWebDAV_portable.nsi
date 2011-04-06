;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

  !include "XPUI.nsh"

;--------------------------------
;General

  !define APP "OpenyourWebDAV"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "1.0"

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
  ${Page} InstFiles
  
;--------------------------------
;Languages
 
  !insertmacro XPUI_LANGUAGE "English"

;--------------------------------

!ifdef SHCNE_ASSOCCHANGED
!undef SHCNE_ASSOCCHANGED
!endif
!define SHCNE_ASSOCCHANGED 0x08000000

!ifdef SHCNF_FLUSH
!undef SHCNF_FLUSH
!endif
!define SHCNF_FLUSH        0x1000

!ifdef SHCNF_IDLIST
!undef SHCNF_IDLIST
!endif
!define SHCNF_IDLIST       0x0000

!macro UPDATEFILEASSOC
  IntOp $1 ${SHCNE_ASSOCCHANGED} | 0
  IntOp $0 ${SHCNF_IDLIST} | ${SHCNF_FLUSH}
; Using the system.dll plugin to call the SHChangeNotify Win32 API function so we
; can update the shell.
  System::Call "shell32::SHChangeNotify(i,i,i,i) (${SHCNE_ASSOCCHANGED}, $0, 0, 0)"
!macroend

;--------------------------------
;Installer Sections

!define MIME "text/x-openyourwebdav"
!define EXT ".owy"

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ;ADD YOUR OWN FILES HERE...
  File "bin\release\OpenyourWebDAV.exe"
  File "bin\release\OpenyourWebDAV.pdb"

  SetOutPath "$INSTDIR"

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR

  WriteRegStr HKCU "Software\Classes\${EXT}" "" "OpenyourWebDAV"
  WriteRegStr HKCU "Software\Classes\${EXT}" "Content Type" "${MIME}"
  WriteRegStr HKCU "Software\Classes\${EXT}" "PerceivedType" "text"

  WriteRegStr HKCU "Software\Classes\OpenyourWebDAV" "" "Open your WebDAV!"
  WriteRegStr HKCU "Software\Classes\OpenyourWebDAV\shell\open\command" "" '"$INSTDIR\${APP}.exe" "%1"'

  WriteRegStr HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "Extension" "${EXT}"

  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\${EXT}"
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\RecentDocs\${EXT}"

  DetailPrint "関連付け更新中..."
  !insertmacro UPDATEFILEASSOC

SectionEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecDummy ${LANG_ENGLISH} "A test section."

  ;Assign language strings to sections
  !insertmacro XPUI_FUNCTION_DESCRIPTION_BEGIN
    !insertmacro XPUI_DESCRIPTION_TEXT ${SecDummy} $(DESC_SecDummy)
  !insertmacro XPUI_FUNCTION_DESCRIPTION_END
