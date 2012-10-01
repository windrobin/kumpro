; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

!define APP "CopyAsImage"
!define VER "0.1.0.0"
!define COM "HIRAOKA HYPERS TOOLS, Inc."

!searchreplace APV ${VER} "." "_"

; The name of the installer
Name "${APP} ${VER}"

; The file to write
OutFile "Setup_${APP}_${APV}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKCU "Software\${COM}\${APP}" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------

; Pages

Page directory
;Page components
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
Section ""

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File /x "*.vshost.*" "bin\DEBUG\*.*"
  
  ; Write the installation path into the registry
  WriteRegStr HKCU "Software\${COM}\${APP}" "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "DisplayName" "${APP}"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoModify" 1
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section ""
  WriteRegStr HKCU "Software\Classes\.jpe\OpenWithProgids" "${APP}" ""
  WriteRegStr HKCU "Software\Classes\.jpeg\OpenWithProgids" "${APP}" ""
  WriteRegStr HKCU "Software\Classes\.jpg\OpenWithProgids" "${APP}" ""

  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpe\OpenWithProgids" "${APP}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpeg\OpenWithProgids" "${APP}" ""
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpg\OpenWithProgids" "${APP}" ""

  ;WriteRegDWORD HKCU "Software\Classes\${APP}" "EditFlags" 65536
  
  WriteRegStr HKCU "Software\Classes\${APP}" "" "画像をコピー"
  WriteRegStr HKCU "Software\Classes\${APP}" "FriendlyTypeName" "画像をコピー"
  WriteRegStr HKCU "Software\Classes\${APP}\DefaultIcon" "" "$INSTDIR\Clipboard.ico"
  WriteRegStr HKCU "Software\Classes\${APP}\shell\open\command" "" '"$INSTDIR\${APP}.exe" "%1"'

  DetailPrint "関連付け更新中..."
  !insertmacro UPDATEFILEASSOC

SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"

  ; Remove files and uninstaller
  Delete "$INSTDIR\${APP}.exe"
  Delete "$INSTDIR\${APP}.pdb"

  ; Remove registry keys
  DeleteRegValue HKCU "Software\Classes\.jpe\OpenWithProgids" "${APP}"
  DeleteRegValue HKCU "Software\Classes\.jpeg\OpenWithProgids" "${APP}"
  DeleteRegValue HKCU "Software\Classes\.jpg\OpenWithProgids" "${APP}"

  DeleteRegKey HKCU "Software\Classes\${APP}"

  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APP}"
  DeleteRegKey HKCU "Software\${COM}\${APP}"

  DetailPrint "関連付け更新中..."
  !insertmacro UPDATEFILEASSOC

  ; Remove files and uninstaller
  Delete "$INSTDIR\uninstall.exe"

  ; Remove directories used
  RMDir "$INSTDIR"

SectionEnd
