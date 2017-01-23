#cs
;default include file 
#include <GuiButton.au3>
#include <GuiTab.au3>
#include <GuiListView.au3>
#include <File.au3>

;new include file  
;#include <Config.au3>
#include<Config.au3>
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
$LogFileOK = $logFolder[0] & "\No.11-Help-OK.log"
$LogFileNG = $logFolder[1] & "\No.11-Help-NG.log"
#ce 
;log file
;~ $LogFileOK = "c:\Log\OK\No.11-Help-OK.log"
;~ $LogFileNG = "c:\Log\NG\No.11-Help-NG.log"
;enter control printers 
;Send("#r")
;_WinWait($RunWinName,$WaitTime)
;~ _ControlSetText ($RunWinName,$RunNameEditClass,"Control Printers")
;~ Sleep ($SleepShort)
;~ _ControlClick ($RunWinName,$RunOKText,$RunOKClass)
;~ _WinWait($DAP,$WaitTime)
;~ Sleep($SleepShort)
;~ Send($DriverName)
;~ Send("{Appskey}")
;~ Send("{P}")
Func _Help()

$LogFileOK = $logFolder[0] & "\No.11-Help-OK.log"
$LogFileNG = $logFolder[1] & "\No.11-Help-NG.log"

Sleep($SleepMedium)
Run("rundll32 printui.dll,PrintUIEntry /p /n""\\" & @ComputerName & "\" & $DriverName & """")
_WinWait($DP,$WaitTime)

;enter  Properties_Preferences
$hWnd1=ControlGetHandle($DP,$Properties_General_BasicSettings_Text,$Properties_General_BasicSettings_AdvancedClass)
_GUICtrlButton_Click($hWnd1)
_WinWait($DPP,$WaitTime)
;Sleep(5000)
;ConsoleWrite("=========898======8888==1==============" & @crlf)

;enter DS Tab
$hWnd2=ControlGetHandle($DPP,"",$DPP_TAB_AdvancedClass)
;Sleep(5000)
;ConsoleWrite("=========898======8888===5=============" & @crlf)
;$iIndex2=_GUICtrlTab_FindTab($hWnd2, $DS_TAB_Text) 
;_GUICtrlTab_ClickTab($hWnd2, $iIndex2)
Sleep($SleepShort)
;ConsoleWrite("=========898======8888===8=============" & @crlf)

;$hWnd_Menu = ControlGetHandle ( $DPP, "", $DS_TAB_Menu_Class );deleted at 10.21
;$hItem = _GUICtrlListView_GetSelectedIndices($hWnd_Menu , True)
;$sText = _GUICtrlListView_GetItem($hWnd_Menu, $hItem[1])
;$sText_Base = $sText[3]
;Sleep($SleepShort)

;click Restore Defaults botton
_ControlClick ( $DPP, $DS_Basic_RestoreDefaults_Text, $DS_Basic_RestoreDefaults_AdvancedClass)
Sleep($SleepShort)
;ConsoleWrite("=========898======8888===5=============" & @crlf)

;enter help
Send("{F1}")
;ConsoleWrite("=========898======8888===4=============" & @crlf)
Sleep($SleepMedium)

;_WinWait($Win_Help,  $WaitTime)
_WinWait($Win_Help, $WaitTime);fixed

$x = WinExists ($Win_Help)
If $x = 1 Then 
   
   WinActivate($Win_Help)
   Sleep($SleepShort)
   ControlFocus($Win_Help, "", $DS_TAB_Help_Class)
   ControlClick($Win_Help, "", $DS_TAB_Help_Class)
   Sleep($SleepShort)
   Send("^f")
   _WinWait($DS_TAB_Help_Find_Title,  $WaitTime)
   If WinExists ($Win_Help) = 1  Then
	  Sleep($SleepShort)
	  ;ControlClick($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_MatchWhole_Class)
	  ControlCommand ( $DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_MatchWhole_Class, "Check")
	  Sleep($SleepShort)
	  ControlFocus($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_FindEdit_Class)
	  ControlClick($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_FindEdit_Class)
	  Sleep($SleepShort)
	  Send($sText_Base)
	  Sleep(2000)
	  If ControlCommand($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_Err_Class, "IsVisible") = 1 Then
		_NFileWriteLog($LogFileNG,"The Help is not "&$language)
	 Else
		 
		 ControlSetText($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_FindEdit_Class, "")
		 Send($DS_Basic_RestoreDefaults_HelpText)
		 Sleep(2000)
		     If ControlCommand($DS_TAB_Help_Find_Title, "", $DS_TAB_Help_Find_Err_Class, "IsVisible") = 1 Then
			_NFileWriteLog($LogFileNG,"The Help is not "&$language)
			   Else
			_NFileWriteLog($LogFileOK,"The Help is "&$language)
		      EndIf
      EndIf
	  WinClose($DS_TAB_Help_Find_Title)
   Else
	  _NFileWriteLog($LogFileNG,"The Find of Help is't popup !")
   EndIf
   
   Winclose ($Win_Help)
Else
   _NFileWriteLog($LogFileNG,"The Title of  Help is not "&  WinGetTitle ( $Win_Help_Class))
   WinClose($Win_Help_Class)
EndIf

;close windows
WinClose($DPP)
WinClose($DP)
;~ WinClose($DAP)
EndFunc
