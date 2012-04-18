; example1.nsi
;
; This script is perhaps one of the simplest NSIs you can make. All of the
; optional settings are left to their default settings. The installer simply 
; prompts the user asking them where to install, and drops a copy of example1.nsi
; there. 

;--------------------------------

!define APP "Assoc_TIFF_Viewer"

!define CLSID_AltTiff "{106E49CF-797A-11D2-81A2-00E02C015623}"
!define CLSID_AxTIF5  "{05936E26-30E9-4210-84A6-A059B4512D14}"

; The name of the installer
Name "${APP} 0.1"

; The file to write
OutFile "${APP}.exe"

; The default installation directory
InstallDir "$APPDATA\${APP}"

; Request application privileges for Windows Vista
RequestExecutionLevel user

!include "LogicLib.nsh"
!include "FileFunc.nsh"

;--------------------------------

; Pages

Page directory
Page components
Page instfiles

;--------------------------------

; The stuff to install
Section /o "Alternatiff" AltTiff ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  ClearErrors
  WriteRegStr HKCR "MIME\DataBase\Content Type\image/tiff"   "CLSID" "${CLSID_AltTiff}"
  IfErrors +3
    DetailPrint "image/tiff èëÇ´çûÇ›ê¨å˜"
    Goto +2
    DetailPrint "image/tiff èëÇ´çûÇ›é∏îs"

  ClearErrors
  WriteRegStr HKCR "MIME\DataBase\Content Type\image/x-tiff" "CLSID" "${CLSID_AltTiff}"
  IfErrors +3
    DetailPrint "image/x-tiff èëÇ´çûÇ›ê¨å˜"
    Goto +2
    DetailPrint "image/x-tiff èëÇ´çûÇ›é∏îs"

SectionEnd ; end the section

Section /o "AxTIF5" AxTIF5 ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  ; Put file there
  ClearErrors
  WriteRegStr HKCR "MIME\DataBase\Content Type\image/tiff"   "CLSID" "${CLSID_AxTIF5}"
  IfErrors +3
    DetailPrint "image/tiff èëÇ´çûÇ›ê¨å˜"
    Goto +2
    DetailPrint "image/tiff èëÇ´çûÇ›é∏îs"

  ClearErrors
  WriteRegStr HKCR "MIME\DataBase\Content Type\image/x-tiff" "CLSID" "${CLSID_AxTIF5}"
  IfErrors +3
    DetailPrint "image/x-tiff èëÇ´çûÇ›ê¨å˜"
    Goto +2
    DetailPrint "image/x-tiff èëÇ´çûÇ›é∏îs"

SectionEnd ; end the section

Function .onInit
  ReadRegStr $0 HKCR "CLSID\${CLSID_AltTiff}\InprocServer32" ""
  ReadRegStr $1 HKCR "CLSID\${CLSID_AxTIF5}\InprocServer32" ""

  ${GetParameters} $R0

  ${GetOptions} $R0 "/?" $2
  IfErrors NotHelp
    MessageBox MB_OK|MB_ICONINFORMATION "égópï˚ñ@:$\n$\n$EXEFILE [/axtif5] [/alternatiff]"
    Abort
NotHelp:

  ${GetOptions} $R0 "/axtif5" $2
  IfErrors +3
    StrCpy $2 "1"
    Goto +2
    StrCpy $2 ""

  ${GetOptions} $R0 "/alternatiff" $3
  IfErrors +3
    StrCpy $3 "1"
    Goto +2
    StrCpy $3 ""

  ${If} $0 != ""
  ${AndIf} $1 != ""
    ${If} $2 != ""
      ;MessageBox MB_OK "B Ax"
      SectionSetFlags ${AxTIF5} ${SF_SELECTED}
    ${ElseIf} $3 != ""
      ;MessageBox MB_OK "B Alt"
      SectionSetFlags ${AltTiff} ${SF_SELECTED}
    ${Else}
      ;MessageBox MB_OK "B"
      SectionSetFlags ${AltTiff} ${SF_SELECTED}
    ${EndIf}
  ${ElseIf} $0 != ""
    ;MessageBox MB_OK "Alt"
    SectionSetFlags ${AltTiff} ${SF_SELECTED}
  ${ElseIf} $1 != ""
    ;MessageBox MB_OK "Ax"
    SectionSetFlags ${AxTIF5} ${SF_SELECTED}
  ${Else}
    ;MessageBox MB_OK "No"
  ${EndIf}

FunctionEnd
