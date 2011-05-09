; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

!define APP   "AxXlsVw"
!define TITLE "Ax Excel Workbook Viewer"

!define VER    "1.1.0"
!define APPVER "1_1_0"

!define MIME "application/vnd.ms-excel"

!define PROGID "AxXlsVw.VwXls"

!define EXT ".xls"

!define CLSID   "{73ef63ff-c109-40a8-bc58-0bd79e719a38}"
!define TYPELIB "{3C358B7C-A227-42C7-A226-89C5CDD692C6}"

;--------------------------------

; The name of the installer
Name "${TITLE} -- ${VER}"

; The file to write
OutFile "Setup_${APP}_${APPVER}_user.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}\"

; Registry key to check for directory (so if you install again, it will
; overwrite the old one automatically)
InstallDirRegKey HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel user

AutoCloseWindow true

AllowSkipFiles off

!define DOTNET_VERSION "2.0"

!include "DotNET.nsh"
!include LogicLib.nsh

;--------------------------------

; Pages

PageEx license
  LicenseData "License.rtf"
PageExEnd

PageEx license
  LicenseData "Lic_Ionic_Zip.rtf"
PageExEnd

PageEx license
  LicenseData "Lic_NPOI.rtf"
PageExEnd

Page directory
Page instfiles

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

  !insertmacro CheckDotNET ${DOTNET_VERSION}

  ; Put file there
  File "bin\release\AxXlsVw.dll"
  File "bin\release\Ionic.Zip.dll"
  File "bin\release\NPOI.dll"

  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}" "" "AxXlsVw.VwXls"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\Control" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\EnableFullPage\${EXT}" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\Implemented Categories\{62C8FE65-4EBB-45e7-B440-6E39B2CDBF29}" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "" "mscoree.dll"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "Assembly" "AxXlsVw, Version=1.1.0.0, Culture=neutral, PublicKeyToken=ab20999cc208234b"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "Class" "AxXlsVw.VwXls"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "CodeBase" "file://$INSTDIR\AxXlsVw.dll"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "RuntimeVersion" "v2.0.50727"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "ThreadingModel" "Both"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\ProgID" "" "${PROGID}"

  WriteRegStr HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" "Install_Dir" "$INSTDIR"

  ; Write the uninstall keys for Windows
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${TITLE}"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd ; end the section

Section "関連付け(現在のアカウント)"
  WriteRegStr HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "CLSID" "${CLSID}"

  DetailPrint "関連付け更新しています。しばらくお待ちください。"
  !insertmacro UPDATEFILEASSOC
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  ; Remove registry keys
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}"

  DeleteRegKey HKCU "Software\Classes\${PROGID}"
  DeleteRegKey HKCU "Software\Classes\CLSID\${CLSID}"

  ReadRegStr $0 HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "CLSID"
  StrCmp $0 "${CLSID}" 0 +1
  DeleteRegValue  HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "CLSID"

  DetailPrint "関連付け更新しています。しばらくお待ちください。"
  !insertmacro UPDATEFILEASSOC

  ; Remove files and uninstaller
  Delete "$INSTDIR\AxXlsVw.dll"
  Delete "$INSTDIR\Ionic.Zip.dll"
  Delete "$INSTDIR\NPOI.dll"

  Delete "$INSTDIR\uninstall.exe"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
