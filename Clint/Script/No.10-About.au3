#cs
;default include file 
#include <GuiButton.au3>
#include <GuiTab.au3>
#include <File.au3>
#include <GuiListView.au3>
#include <Array.au3>
#include <GuiComboBox.au3>
#include <GuiListBox.au3>
#include <ScreenCapture.au3>

;new include file  
;#include <Config.au3>
#include<Config.au3>
#include<langconfig.au3>
#include <Check.au3>
#ce 
;#include <Config_OS.au3>
#cs
If $CmdLine[0] > 0  Then
   $InstallFromDiskDrivePath = $CmdLine[1]
   $NewDriverPath = $CmdLine[2] 
EndIf

;log file
_CreateLogPath($LogPath, $NewDriverPath, $DriverName)
$LogFileOK= $logFolder[0] & "\No.10-About_OK.log"
$LogFileNG= $logFolder[1] & "\No.10-About_NG.log"
#ce 

;~ $LogFileOK= "C:\Log\OK\No.10-About_OK.log"
;~ $LogFileNG= "C:\Log\NG\No.10-About_NG.log"

Func _about()
;enter control printers  
;~ send("#r")
;~ Sleep($SleepMedium)
;~ _WinWait($RunWinName,$WaitTime)
;~ _ControlSetText ($RunWinName,$RunNameEditClass,"Control Printers")
;~ $hWndRun_OK = ControlGetHandle($RunWinName,$RunOKText,$RunOKClass)
;~ _GUICtrlButton_Click($hWndRun_OK)
;~ _WinWait($DAP,$WaitTime)
;~ Sleep($SleepShort)
;~ send($DriverName)
;~ Send("{Appskey}")
;~ sleep($SleepShort)
;~ send("{G}")
$LogFileOK = $logFolder[0] & "\No.10-About_OK.log"
$LogFileNG = $logFolder[1] & "\No.10-About_NG.log"
Sleep($SleepMedium)
Run("rundll32 printui.dll,PrintUIEntry /p /n""\\" & @ComputerName & "\" & $DriverName & """")
_WinWait($DP,$WaitTime)

;enter  Properties_Preferences
$hWnd1=ControlGetHandle($DP,$Properties_General_BasicSettings_Text,$Properties_General_BasicSettings_AdvancedClass)
_GUICtrlButton_Click($hWnd1)
_WinWait($DPP,$WaitTime)

;;choose the TAB  text
$hWnd=ControlGetHandle ($DPP,"",$DPP_TAB_AdvancedClass)
$iIndex=_GUICtrlTab_FindTab($hWnd,$CA_TAB_Text)
_GUICtrlTab_ClickTab($hWnd, $iIndex )
sleep($SleepShort)

;ControlClick the About Botton
_ControlClick($DPP,$About_Text,$About_AdvancedClass);fixed10.23
_WinWait($Win_About,$WaitTime)
$hWnd1= ControlGetHandle($Win_About,"",$About_aboutinfo_AdvancedClass)
$ab=_GUICtrlListBox_GetText($hWnd1, 8)
;MsgBox(0,"hao",$ab)
;_NFileWriteLog($LogFileOK,"About aboutinfo:"&$ab)
If _ArrayBinarySearch($About_aboutinfo_List, $DriverName) <0  Then
   _ArrayAdd($About_aboutinfo_List, $DriverName)
EndIf
#cs
  For $x=2 To 14
  $ab=_GUICtrlListBox_GetText($hWnd1,$x)
   _NFileWriteLog($LogFileNG,$ab&"  test")
Next
#ce
   
;~ For $y=0 to $About_aboutinfo_DataMax -1
For $y=0 to UBound($About_aboutinfo_List) -1
   $a=_GUICtrlListBox_FindInText($hWnd1,$About_aboutinfo_List[$y]) 
   If $a= -1 Then
	  _NFileWriteLog($LogFileNG,"About aboutinfo " & $About_aboutinfo_List[$y] & " is not Found!  $About=" & $About_aboutinfo_List[$y])
   Else
	  _NFileWriteLog($LogFileOK,"About aboutinfo " & $About_aboutinfo_List[$y] & " is Found.  $About=" & $About_aboutinfo_List[$y])
   EndIf
Next
$b=_GUICtrlListBox_FindInText($hWnd1,$New_About_aboutinfo_List1) 
$c=_GUICtrlListBox_FindInText($hWnd1,$New_About_aboutinfo_List2)
If $b= -1 And $c= -1 Then
   _NFileWriteLog($LogFileNG,"About aboutinfo " & $New_About_aboutinfo_List1 & " is not Found!  $About=" & $New_About_aboutinfo_List1)
Else
   _NFileWriteLog($LogFileOK,"About aboutinfo " & $New_About_aboutinfo_List1 & " is Found.  $About=" & $New_About_aboutinfo_List1)
EndIf
$d=_GUICtrlListBox_FindInText($hWnd1,$New_About_aboutinfo_List3) 
$e=_GUICtrlListBox_FindInText($hWnd1,$New_About_aboutinfo_List4)
If $d= -1 And $e= -1 Then
   _NFileWriteLog($LogFileNG,"About aboutinfo " & $New_About_aboutinfo_List3 & " is not Found!  $About=" & $New_About_aboutinfo_List3)
Else
   _NFileWriteLog($LogFileOK,"About aboutinfo " & $New_About_aboutinfo_List3 & " is Found.  $About=" & $New_About_aboutinfo_List3)
EndIf

;ScreenCapture
$hWnd_About = WinGetHandle($Win_About)
_ScreenCapture_SetBMPFormat(2)
_ScreenCapture_CaptureWnd( $logFolder[2]  & $DriverName & ".bmp", $hWnd_About) 

;Close Windows
WinClose($Win_About)
WinClose($DPP)
;~ WinClose($DAP)
WinClose($DP)
EndFunc
