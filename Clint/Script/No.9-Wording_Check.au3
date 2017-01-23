#cs
;default include file
#include <File.au3>
#include <GuiTab.au3>
#include <GuiButton.au3>

;new include file 
#include<Config.au3>
;#include <Config.au3>
;#include <Config_OS.au3>
#include<langconfig.au3>
#include <Check.au3>
#ce 
#cs
If $CmdLine[0] > 0  Then
   $InstallFromDiskDrivePath = $CmdLine[1]
   $NewDriverPath = $CmdLine[2] 
EndIf

;log file
_CreateLogPath($LogPath, $NewDriverPath, $DriverName)
$LogFileOK = $logFolder[0] & "\No.9-Wording_Check-OK.log"
$LogFileNG = $logFolder[1] & "\No.9-Wording_Check-NG.log"
#ce 
;Enter control printers  
;~ Send("#r")
;~ _WinWait($RunWinName,$WaitTime)
;~ _ControlSetText ($RunWinName,$RunNameEditClass,"Control Printers")
;~ $hWndRun_OK = ControlGetHandle($RunWinName,$RunOKText,$RunOKClass)
;~ _GUICtrlButton_Click($hWndRun_OK)
;~ _WinWait($DAP,$WaitTime)

;~ Sleep($SleepShort)
;~ Send($DriverName)
;~ Send("{APPSKEY}")
;~ Sleep($SleepShort)
;~ Send("P")
;~ Sleep($SleepShort)
Func _Wording_chek()
;log file
;~ $LogFileOK = "c:\Log\OK\No.9-Wording_Check-OK.log"
;~ $LogFileNG = "c:\Log\NG\No.9-Wording_Check-NG.log"
$LogFileOK = $logFolder[0] & "\No.9-Wording_Check-OK.log"
$LogFileNG = $logFolder[1] & "\No.9-Wording_Check-NG.log"
;Enter Printer Properties
Sleep($SleepMedium)
Run("rundll32 printui.dll,PrintUIEntry /p /n""\\" & @ComputerName & "\" & $DriverName & """")
_WinWait($DP,$WaitTime)

;Find  Accessories TAB
$hWnd=ControlGetHandle ( $DP, "", $Properties_TAB_AdvancedClass)
$iIndex=_GUICtrlTab_FindTab($hWnd, $Properties_Accessories_TAB_Text)
If $iIndex=-1   Then
   _NFileWriteLog($LogFileNG,"The Accessories tab'text is NG! $Accessories=" & $Properties_Accessories_TAB_Text)
Else
   _NFileWriteLog($LogFileOK,"The Accessories tab'text is OK! $Accessories=" & $Properties_Accessories_TAB_Text)

   ;Click the Accessories Tab
   _GUICtrlTab_ClickTab($hWnd, $iIndex )
   Sleep($SleepShort)

   For $x=0 to $Tab_Option_Datamax -1
	  $a=ControlGetText($DP,"",$Tab_Option_AdvancedClass[$x])
	  ;MsgBox(0,"",$a)
	 ; _NFileWriteLog($LogFileOK,$a);for debug
	  If $a= $Tab_Option_Text[$x]  Then
		 _NFileWriteLog($LogFileOK,$Tab_Option_Text[$x] &"(Option1) text is OK! $Option=" & $a)
	  Else
		 _NFileWriteLog($LogFileNG,$Tab_Option_Text[$x] &"(Option1) text is NG! $Option=" & $a)
	  EndIf
	  Sleep($SleepShort)
   Next

   ;$hWnd=ControlGetHandle ( $DP, "", $Properties_TAB_AdvancedClass)
   $iIndex=_GUICtrlTab_FindTab($hWnd, $Properties_General_TAB_Text)
   _GUICtrlTab_ClickTab($hWnd, $iIndex )
   Sleep($SleepShort)

EndIf

;Enter [Print Quality] tabes 
$hWnd_Properties = ControlGetHandle($DP,$Properties_General_BasicSettings_Text,$Properties_General_BasicSettings_AdvancedClass)
_GUICtrlButton_Click($hWnd_Properties)
Sleep($SleepShort)

_WinWait($DPP,$WaitTime)

$hWnd=ControlGetHandle($DPP,"",$DPP_TAB_AdvancedClass)
$b=_GUICtrlTab_FindTab($hWnd, $FUS_TAB_Text)
If $b=-1  Then
   _NFileWriteLog($LogFileNG,"The Print Quality tab'text is NG! $Print Quality =" & $FUS_TAB_Text)
Else
   _NFileWriteLog($LogFileOK,"The Print Quality tab'text is OK! $Print Quality=" & $FUS_TAB_Text)
EndIf

WinClose($DPP)
WinClose($DP)
;~ WinClose($DAP)
EndFunc
