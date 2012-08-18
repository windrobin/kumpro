; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "Assoc_TIFF_Viewer"

; The name of the installer
Name "Setup ${APP} 0.2"

; The file to write
OutFile "Setup_${APP}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel user

;--------------------------------

; Pages

Page directory
Page components
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Assoc_TIFF_Viewer.exe"
  
SectionEnd ; end the section

Section /o "�X�^�[�g�A�b�v�֓o�^�FAlternatiff�D��"
  CreateShortCut "$SMSTARTUP\TIFF�֘A�t��.lnk" \
    "$INSTDIR\Assoc_TIFF_Viewer.exe" \
    " /S /alternatiff "
SectionEnd

Section /o "�X�^�[�g�A�b�v�֓o�^�FAxTIF5�D��"
  CreateShortCut "$SMSTARTUP\TIFF�֘A�t��.lnk" \
    "$INSTDIR\Assoc_TIFF_Viewer.exe" \
    " /S /axtif5 "
SectionEnd

Section /o "�X�^�[�g�A�b�v�֓o�^�F����(Alternatiff��AxTIF5)"
  CreateShortCut "$SMSTARTUP\TIFF�֘A�t��.lnk" \
    "$INSTDIR\Assoc_TIFF_Viewer.exe" \
    " /S "
SectionEnd
