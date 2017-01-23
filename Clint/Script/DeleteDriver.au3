;#RequireAdmin
#include-once
;$Os_Version = "win2003" 
;~ If $CmdLine[0] > 0  Then
;~    $Os_Version = $CmdLine[1] 
;~ EndIf
;$setup_flg = False
#include <GuiTab.au3>
#include <Excel.au3>
;#include <OsConfig\defineCom.au3>
#include <Common\CommonCase.au3>

#Cs
;install path
If $CmdLine[0] > 1  Then
   $InstallFromDiskDrivePath = $CmdLine[2] 
EndIf

;model 
If $CmdLine[0] > 2 Then
   $DriverName = $CmdLine[3] 
   $aryOem = StringSplit($DriverName," ")
   If $aryOem[0] > 0  Then
	  $DriverOEM = $aryOem[1]
   EndIf
EndIf

;log file
_CreateLogPath($LogPath, $InstallFromDiskDrivePath, $DriverName, $Os_Version)
$LogFileOK = $logFolder[0] & "\Delete-OK.log"
$LogFileNG = $logFolder[1] & "\Delete-NG.log"
#ce 
#cs
;for debug
$Os_Version="win7"
$language="cesky"
$DriverName = "infotec MP 5054 PCL 5e"
$DriverOEM = "infotec" 
$zipname="CoronaC1_EXP_5EW32_V1000_20140228"
$InstallFromDiskDrivePath="D:\1\oemrelease\RICOH\Drivers\PCL5e\XP_VISTA\cesky\disk1"

;excel
$Language_Config_FilePath=(@ScriptDir&"\langconfig.xls")
Local $oExcel = _ExcelBookOpen($Language_Config_FilePath, 0)
_ExcelSheetActivate($oExcel, "Sheet1")
Local $aArray2 = _ExcelReadArray($oExcel, 2, 1, 6, 0);读取第*行，第*格开始共*个，横向

$language=$aArray2[0]
$DriverName = $aArray2[1]
;$DriverOEM = $aArray2[2]
$aryOem = StringSplit($DriverName," ")
   If $aryOem[0] > 0  Then
	  $DriverOEM = $aryOem[1]
   EndIf
$InstallFromDiskDrivePath=$aArray2[3]
$zipname=$aArray2[4]
$Os_Version=$aArray2[5]
;_ExcelBookClose($oExcel )
;cmd
If  $CmdLine[0]>1 Then 
   $Os_Version=$CmdLine[1]
   $DriverName=$CmdLine[2]
   $aryOem = StringSplit($DriverName," ")
   If $aryOem[0] > 0  Then
	  $DriverOEM = $aryOem[1]
   EndIf
EndIf
If  $CmdLine[0]>2 Then 
   $InstallFromDiskDrivePath= $CmdLine[3]
EndIf
If $CmdLine[0]>3  Then
   $zipname=$CmdLine[4]
EndIf
If $CmdLine[0]>4 Then 
  $language=$CmdLine[5]
EndIf 
#include <OsConfig\defineCom.au3>
Select
    Case $language="English"
     Local $aArray = _ExcelReadArray($oExcel, 5, 3, 1, 0)
  Case $language="German" Or $language="deutsch"
     Local $aArray = _ExcelReadArray($oExcel, 6, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="French" Or $language="francais"
     Local $aArray = _ExcelReadArray($oExcel, 7, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Italian" Or $language="italiano"
     Local $aArray = _ExcelReadArray($oExcel, 8, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Spanish" Or $language="espanol"
     Local $aArray = _ExcelReadArray($oExcel, 9, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Dutch" Or $language="nedrlnds"
     Local $aArray = _ExcelReadArray($oExcel, 10, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Swedish" Or $language="svenska"
     Local $aArray = _ExcelReadArray($oExcel, 11, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Danish" Or $language="dansk"
     Local $aArray = _ExcelReadArray($oExcel, 12, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Norwegian" Or $language="norsk"
     Local $aArray = _ExcelReadArray($oExcel, 13, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Polish" Or $language="polski"
     Local $aArray = _ExcelReadArray($oExcel, 14, 3, 1, 0);读取第*行，第*格开始共*个，横向
   Case $language="Portuguese" Or $language="portugus"
     Local $aArray = _ExcelReadArray($oExcel, 15, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Czech" Or $language="cesky"
     Local $aArray = _ExcelReadArray($oExcel, 16, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Hungarian" Or $language="magyar"
     Local $aArray = _ExcelReadArray($oExcel, 17, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Finnish" Or $language="suomi"
     Local $aArray = _ExcelReadArray($oExcel, 18, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Russian"
     Local $aArray = _ExcelReadArray($oExcel, 19, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Catalan"
     Local $aArray = _ExcelReadArray($oExcel, 20, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Turkish"
     Local $aArray = _ExcelReadArray($oExcel, 21, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Brazilian Portuguese"
     Local $aArray = _ExcelReadArray($oExcel, 22, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="Greek"
     Local $aArray = _ExcelReadArray($oExcel, 23, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="TC"
     Local $aArray = _ExcelReadArray($oExcel, 24, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case $language="SC"
     Local $aArray = _ExcelReadArray($oExcel, 25, 3, 1, 0);读取第*行，第*格开始共*个，横向
    Case Else  $language="Korea"
     Local $aArray = _ExcelReadArray($oExcel, 26, 3, 1, 0);读取第*行，第*格开始共*个，横向
  EndSelect	 
  _ExcelBookClose($oExcel )
  $language=$aArray[0];"English" 
;_CreateLogPath($LogPath, $InstallFromDiskDrivePath, $DriverName , $Os_Version)
$LogPath = "c:\Log"
$logFolder[0]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$Os_Version&"\"&$language&"\OK"
$logFolder[1]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$Os_Version&"\"&$language&"\NG"
$logFolder[2]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$Os_Version&"\"&$language&"\"
#ce 
Func _delete()
$LogFileOK = $logFolder[0] & "\Delete-OK.log"
$LogFileNG = $logFolder[1] & "\Delete-NG.log"
$PRNFILE = $logFolder[2] & "InstallTestPage.prn"
$PRNFILE_TMP = $logFolder[2] & "Tmp.prn"
;DirCreate($logFolder[0])
;DirCreate($logFolder[1])
DirCreate($logFolder[2])


Sleep($SleepShort)
Run("rundll32 printui.dll,PrintUIEntry /gd /dl /n""\\" & @ComputerName & "\" & $DriverName &  """")
Sleep($SleepMedium)
_ReStartPrinterService()
Sleep($SleepMedium)
;~ If $Os_Version = "win2003"  Or $Os_Version = "win2003r2"  Then
;~    $processid = Run("Control printers")
;~    Sleep($SleepShort)
;~ EndIf
_DeletePort()
Sleep($SleepNormal)
Run("rundll32 printui.dll,PrintUIEntry /s /t1 /c""\\" &  @ComputerName & """")
_WinWait($Win_PrintServerProperties,$WaitTime)

;Enter the Driver Tab
$hWnd=ControlGetHandle ( $Win_PrintServerProperties, "", $Win_PrintServerProperties_TAB_AdvancedClass)
$iIndex=_GUICtrlTab_FindTab($hWnd, $Win_PrintServerProperties_Driver_TAB_Text)
_GUICtrlTab_ClickTab($hWnd, $iIndex )
Sleep($SleepShort)

;Select the Delete Driver
Send($DriverName)
Sleep($SleepShort)

_ControlClick($Win_PrintServerProperties,$Win_PrintServerProperties_Delete_Text,$Win_PrintServerProperties_Delete_AdvabcedClass)


 If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2"  Then
   _WinWait($Win_RemoveDriverAndPackage,$WaitTime)
   ;Select Remove driver and driver package
   ControlCommand ($Win_RemoveDriverAndPackage,$Win_RemoveDriverAndPackage_M_Delete_Text, $Win_RemoveDriverAndPackage_M_Delete_AdvancedClass, "Check", "" )
   Sleep($SleepShort)
   _ControlClick($Win_RemoveDriverAndPackage,$Win_RemoveDriverAndPackage_OK_Text,$Win_RemoveDriverAndPackage_OK_AdvancedClass)
EndIf

;Confirm Delete
_WinWait($Win_ConfirmDelete,$WaitTime)
_ControlClick($Win_ConfirmDelete,$Win_ConfirmDelete_OK_Text,$Win_ConfirmDelete_OK_AdvancedClass)

If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2"  Then
   _WinWait($Win_DriverPackageDelete,$WaitTime)
   Sleep($SleepMedium)
   _ControlClick($Win_DriverPackageDelete,$Win_DriverPackageDelete_Delete_Text,$Win_DriverPackageDelete_Delete_AdvancedClass)
   Sleep($SleepMedium)
   _ControlClick($Win_DriverPackageDelete,$Win_DriverPackageDelete_OK_Text,$Win_DriverPackageDelete_OK_AdvancedClass)
EndIf

;Close the  windows
WinActivate ($Win_PrintServerProperties)
Send("!{F4}")
;_ControlClick ($Win_PrintServerProperties,$Win_PrintServerProperties_Close_Text,$Win_PrintServerProperties_Close_AdvabcedClass)
;~ WinClose ($DAP)

_NFileWriteLog($LogFileOK,"The driver and package had been delete!")
 ;MsgBox(0,"","Delete Finish!")
;~ If $Os_Version = "win2003"  Or $Os_Version = "win2003r2"  Then
;~    ProcessClose ( $processid )
;~ EndIf

;~ _CopyLogToServer()
EndFunc