; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

!define APP   "AxTIF5"
!define TITLE "Ax TIFF Viewer five"

!define COM   "HIRAOKA HYPERS TOOLS, Inc."

!define VER    "1.1.0"
!define APPVER "1_1_0"

!define MIME "image/tiff"

!define PROGID "AXTIF5.AxTIF5Ctrl.1"

!define CLSID   "{05936E26-30E9-4210-84A6-A059B4512D14}"
!define TYPELIB "{3C358B7C-A227-42C7-A226-89C5CDD692C6}"

!define EXT  ".tif"
!define EXT2 ".tiff"

!include "LogicLib.nsh"
!include "FileFunc.nsh"

;--------------------------------

; The name of the installer
Name "${TITLE} -- ${VER}"

; The file to write
OutFile "Setup_${APP}_${APPVER}.exe"

; The default installation directory
InstallDir "$PROGRAMFILES\${APP}"

; Registry key to check for directory (so if you install again, it will
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\${COM}\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

AutoCloseWindow true

AllowSkipFiles off

;--------------------------------

; Pages

Page license
Page directory
Page components
Page instfiles

LicenseData "License.rtf"

UninstPage uninstConfirm
UninstPage instfiles

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

; The stuff to install
Section "${APP}" ;No components page, name is not important
  SectionIn ro

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  ; Put file there
  File "release\AxTIF5.ocx"
  
  RegDLL "$INSTDIR\AxTIF5.ocx"
  
  WriteRegStr HKCR "CLSID\${CLSID}\EnableFullPage\.tif" "" ""
  WriteRegStr HKCR "CLSID\${CLSID}\EnableFullPage\.tiff" "" ""

  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${TITLE}"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd ; end the section

Section "関連付け追加(コンピュータ全体の設定)"
  WriteRegStr HKCR "${EXT}" "Content Type" "${MIME}"
  WriteRegStr HKCR "${EXT2}" "Content Type" "${MIME}"

  WriteRegStr HKCR "Mime\Database\Content Type\${MIME}" "CLSID" "${CLSID}"
  WriteRegStr HKCR "Mime\Database\Content Type\${MIME}" "Extension" "${EXT}"

  DetailPrint "関連付け更新しています。しばらくお待ちください。"
  !insertmacro UPDATEFILEASSOC
SectionEnd

Section    "簡易表示 On" ddcompat1
  WriteRegDWORD HKLM "Software\${COM}\${APP}" "ddcompat" 1
SectionEnd

Section /o "簡易表示 Off" ddcompat0
  WriteRegDWORD HKLM "Software\${COM}\${APP}" "ddcompat" 0
SectionEnd

Section    "縮小表示 精密" slowzoom20
  WriteRegDWORD HKLM "Software\${COM}\${APP}" "slowzoom" 20
SectionEnd

Section /o "縮小表示 標準" slowzoom10
  WriteRegDWORD HKLM "Software\${COM}\${APP}" "slowzoom" 10
SectionEnd

Section /o "縮小表示 高速" slowzoom0
  WriteRegDWORD HKLM "Software\${COM}\${APP}" "slowzoom" 0
SectionEnd

Section    "全サイトで起動許可(IE8-9) On" allowie1
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Ext\Stats\${CLSID}\iexplore\AllowedDomains\*" "" ""
SectionEnd

Section /o "全サイトで起動許可(IE8-9) Off" allowie0
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Ext\Stats\${CLSID}\iexplore\AllowedDomains\*"
SectionEnd

Section "関連付け削除(アカウント単位の設定)"
  StrCpy $0 0
Loop:
  EnumRegKey $1 HKU "" $0
  StrCmp $1 "" Done

  DetailPrint "関連付け削除: $1"

  DeleteRegKey HKU "$1\Software\Classes\${PROGID}"
  DeleteRegKey HKU "$1\Software\Classes\CLSID\${CLSID}"
  DeleteRegKey HKU "$1\Software\Classes\TypeLib\${TYPELIB}"
  DeleteRegKey HKU "$1\Software\${COM}\${APP}"

  DeleteRegKey HKU "$1\Software\Classes\${EXT}"
  DeleteRegKey HKU "$1\Software\Classes\${EXT2}"

  DeleteRegKey HKU "$1\Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"

  IntOp $0 $0 + 1
  Goto Loop
Done:
SectionEnd

;--------------------------------

Function .onInit
  IntOp $R0 ${SF_RO} | ${SF_RO}
  IntOp $R1 ${SF_RO} | ${SF_SELECTED}

  ${GetOptions} $CMDLINE "--ddcompat=" $R2
  ${Switch} $R2
    ${Case} "0"
      SectionSetFlags ${ddcompat1} $R0
      SectionSetFlags ${ddcompat0} $R1
      ${Break}
    ${Case} "1"
      SectionSetFlags ${ddcompat1} $R1
      SectionSetFlags ${ddcompat0} $R0
      ${Break}
  ${EndSwitch}

  ${GetOptions} $CMDLINE "--slowzoom=" $R2
  ${Switch} $R2
    ${Case} "0"
      SectionSetFlags ${slowzoom20} $R0
      SectionSetFlags ${slowzoom10} $R0
      SectionSetFlags  ${slowzoom0} $R1
      ${Break}
    ${Case} "10"
      SectionSetFlags ${slowzoom20} $R0
      SectionSetFlags ${slowzoom10} $R1
      SectionSetFlags  ${slowzoom0} $R0
      ${Break}
    ${Case} "20"
      SectionSetFlags ${slowzoom20} $R1
      SectionSetFlags ${slowzoom10} $R0
      SectionSetFlags  ${slowzoom0} $R0
      ${Break}
  ${EndSwitch}

  ${GetOptions} $CMDLINE "--allowie=" $R2
  ${Switch} $R2
    ${Case} "0"
      SectionSetFlags ${allowie0} $R1
      SectionSetFlags ${allowie1} $R0
      ${Break}
    ${Case} "1"
      SectionSetFlags ${allowie0} $R0
      SectionSetFlags ${allowie1} $R1
      ${Break}
  ${EndSwitch}
FunctionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  UnRegDLL "$INSTDIR\AxTIF5.ocx"

  ReadRegStr $0 HKCR "Mime\Database\Content Type\${MIME}" "CLSID"
  ${If} $0 == "${CLSID}"
    DeleteRegValue HKCR "Mime\Database\Content Type\${MIME}" "CLSID"
  ${EndIf}

  DetailPrint "関連付け更新しています。しばらくお待ちください。"
  !insertmacro UPDATEFILEASSOC

  ; Remove files and uninstaller
  Delete "$INSTDIR\AxTIF5.ocx"

  Delete "$INSTDIR\uninstall.exe"

  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKLM "Software\${COM}\${APP}"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
