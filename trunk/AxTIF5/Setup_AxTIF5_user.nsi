; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

!define APP   "AxTIF5"
!define TITLE "Ax TIFF Viewer five"

!define VER    "1.0.3"
!define APPVER "1_0_3"

!define MIME "image/tiff"

!define PROGID "AXTIF5.AxTIF5Ctrl.1"

!define CLSID   "{05936E26-30E9-4210-84A6-A059B4512D14}"
!define TYPELIB "{3C358B7C-A227-42C7-A226-89C5CDD692C6}"

!define EXT  ".tif"
!define EXT2 ".tiff"

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

;--------------------------------

; Pages

Page license
Page directory
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

  WriteRegStr HKCU "Software\Classes\${PROGID}" "" "AxTIF5 Control"
  WriteRegstr HKCU "Software\Classes\${PROGID}\CLSID" "" "${CLSID}"

  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}" "" "AxTIF5 Control"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\Control" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\EnableFullPage\.tif" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\EnableFullPage\.tiff" "" ""
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "" "$INSTDIR\AxTIF5.ocx"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\InprocServer32" "ThreadingModel" "Apartment"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\MiscStatus" "" "0"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\ProgID" "" "${PROGID}"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\ToolboxBitmap32" "" "$INSTDIR\AxTIF5.ocx, 1"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\TypeLib" "" "${TYPELIB}"
  WriteRegStr HKCU "Software\Classes\CLSID\${CLSID}\Version" "" "${VER}"

  WriteRegStr HKCU "Software\Classes\TypeLib\${TYPELIB}\1.0" "" "AxTIF5 control module"
  WriteRegStr HKCU "Software\Classes\TypeLib\${TYPELIB}\1.0\0\win32" "" "$INSTDIR\AxTIF5.ocx"
  WriteRegStr HKCU "Software\Classes\TypeLib\${TYPELIB}\1.0\FLAGS" "" "2"
  WriteRegStr HKCU "Software\Classes\TypeLib\${TYPELIB}\1.0\HELPDIR" "" ""

  WriteRegStr HKCU "Software\HIRAOKA HYPERS TOOLS, Inc.\${APP}" "Install_Dir" "$INSTDIR"

  ; Write the uninstall keys for Windows
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${TITLE}"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd ; end the section

Section "関連付け(現在のアカウント)"
  WriteRegStr HKCU "Software\Classes\${EXT}" "Content Type" "${MIME}"

  WriteRegStr HKCU "Software\Classes\${EXT2}" "Content Type" "${MIME}"

  ReadRegStr $0 HKLM "Software\Classes\${EXT}" ""
  WriteRegStr   HKCU "Software\Classes\${EXT}" "" "$0"

  ReadRegStr $0 HKLM "Software\Classes\${EXT2}" ""
  WriteRegStr   HKCU "Software\Classes\${EXT2}" "" "$0"

  WriteRegStr HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "CLSID" "${CLSID}"
  WriteRegStr HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "Extension" "${EXT}"

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
  DeleteRegKey HKCU "Software\Classes\TypeLib\${TYPELIB}"

  ReadRegStr $0 HKCU "Software\Classes\Mime\Database\Content Type\${MIME}" "CLSID"
  StrCmp $0 "${CLSID}" 0 +1
  DeleteRegKey  HKCU "Software\Classes\Mime\Database\Content Type\${MIME}"

  DetailPrint "関連付け更新しています。しばらくお待ちください。"
  !insertmacro UPDATEFILEASSOC

  ; Remove files and uninstaller
  Delete "$INSTDIR\AxTIF5.ocx"

  Delete "$INSTDIR\uninstall.exe"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
