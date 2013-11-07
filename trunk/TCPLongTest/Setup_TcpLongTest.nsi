;ExperienceUI for NSIS
;Basic Example Script
;Written by Dan Fuhry

;OK, I cheated, Joost wrote it. :-)

;--------------------------------
;Include ExperienceUI

;--------------------------------
;General

  !define APP "TcpLongTest"
  !define COM "HIRAOKA HYPERS TOOLS, Inc."
  !define VER "0.1"
  !define APV "0_1"

  ;Name and file
  Name "${APP} ${VER}"
  OutFile "Setup_${APP}_${APV}.exe"

  ;Default installation folder
  InstallDir "$APPDATA\${APP}"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${COM}\${APP}" ""

  RequestExecutionLevel user

  !include "LogicLib.nsh"

;--------------------------------
;Interface Settings

  LoadLanguageFile "${NSISDIR}\Contrib\Language files\Japanese.nlf"

;--------------------------------
;Pages

  Page license
  Page directory
  Page components
  Page instfiles
  
  LicenseData "License.rtf"
  
;--------------------------------
;Languages
 
;--------------------------------
;Installer Sections

Section ""

  SetOutPath "$INSTDIR"
  
  SetOverwrite ifdiff
  
  AllowSkipFiles off

  ;ADD YOUR OWN FILES HERE...
  File /r /x "*.vshost.*" "bin\DEBUG\*.*"

  ;Store installation folder
  WriteRegStr HKCU "Software\${COM}\${APP}" "" $INSTDIR
  
  ;Create uninstaller
  ;WriteUninstaller "$INSTDIR\Uninstall.exe"
  
SectionEnd

Section "起動"
  Exec "$INSTDIR\TCPLongTest.exe"
SectionEnd

Section /o "ログフォルダを表示"
  Exec 'explorer.exe "$INSTDIR"'
SectionEnd

;--------------------------------
;Descriptions
