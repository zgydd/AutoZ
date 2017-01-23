#include-once
#include <GuiListView.au3>
#include <GuiButton.au3>
#include <WinAPI.au3>
#include <Constants.au3>
#include <GuiListBox.au3>

Func _WinWait ($WinName,$WaitTime)
   $x = WinWait ($WinName,"",$WaitTime)
   If $x = 0 Then
	  _NFileWriteLog($LogFileNG,$WinName & " has not be displayed")
	  Exit
   Else
	  WinActivate ($WinName)
   EndIf
EndFunc

Func _ControlClick ($WinName,$ControlText,$ControlAdvancedClass)
;~    $x = ControlCommand ($WinName,$ControlText,$ControlAdvancedClass,"IsVisible","")
;~    $y = ControlCommand ($WinName,$ControlText,$ControlAdvancedClass,"IsEnabled","")
   $curStep = 1
   Do
	   Sleep($SleepShort)
       $x = ControlCommand ($WinName,"",$ControlAdvancedClass,"IsVisible","")
       $y = ControlCommand ($WinName,"",$ControlAdvancedClass,"IsEnabled","")
       If $curStep > $WaitTime Then
		  ExitLoop
	   EndIf
	   $curStep = $curStep + 1
	Until $x = 1  and $y = 1
	
   If $x = 1 and $y = 1 Then
	  WinActivate($WinName)
;~ 	  $reg = ControlClick ($WinName,$ControlText,$ControlAdvancedClass)
	  $hWndBtn = ControlGetHandle($WinName, "",$ControlAdvancedClass)
	  $style = _WinAPI_GetWindowLong($hWndBtn, $GWL_STYLE  )
	  If BitAND($style , $BS_AUTORADIOBUTTON) <> $BS_AUTORADIOBUTTON _
		 And BitAND($style , $BS_RADIOBUTTON) <> $BS_RADIOBUTTON Then
		 If ControlFocus($WinName,"",$ControlAdvancedClass) = 0  Then
			_GUICtrlButton_SetFocus($hWndBtn)
		 EndIf
	  EndIf
	  Sleep($SleepShort)
	  
	  $reg = ControlClick ($WinName,"",$ControlAdvancedClass)
	  If $reg = 0 Then
		 _NFileWriteLog($LogFileNG,$ControlText & @TAB & $ControlAdvancedClass & " can't be clicked" & @CRLF)		 
		 _GUICtrlButton_Click($hWndBtn)
	  EndIf
   Else
	  _NFileWriteLog($LogFileNG,$ControlText & @TAB & $ControlAdvancedClass & " can't be clicked")
	  Exit
   EndIf
EndFunc

Func _WinWait2 ($WinName,$WaitTime,$ControlText,$ControlAdvancedClass)
   $x = WinWait ($WinName,"",$WaitTime)
   If $x = 0 Then
	  _NFileWriteLog($LogFileNG,$WinName & " has not be displayed")
   Else
	  WinActivate ($WinName)
	  _ControlClick ($WinName,$ControlText,$ControlAdvancedClass)
   EndIf
EndFunc

Func _ControlSetText ($WinName,$ControlAdvancedClass,$SetText)
   $x = ControlCommand ($WinName,"",$ControlAdvancedClass,"IsVisible","")
   $y = ControlCommand ($WinName,"",$ControlAdvancedClass,"IsEnabled","")
   If $x = 1 and $y = 1 Then
;~ 	  ControlSetText ($WinName, "",$ControlAdvancedClass,$SetText)
	  ControlFocus ( $WinName, "", $ControlAdvancedClass )
	  ControlCommand ( $WinName, "", $ControlAdvancedClass, "EditPaste" , $SetText)
   Else
	  _NFileWriteLog($LogFileNG,$SetText & " can't be set")
	  Exit
   EndIf
EndFunc


Func _WinAPI_IsHungAppWindow($hWnd)
	Local $aResult = DllCall("user32.dll", "bool", "IsHungAppWindow", "hwnd", $hWnd)
	If @error Then Return SetError(@error, @extended, 0)
	Return $aResult[0]
EndFunc   ;==>_WinAPI_IsHungAppWindow

#CS
Func _DeletePort()
   Sleep($SleepShort)
   Run("rundll32 printui.dll,PrintUIEntry /s /t1 /c""\\" &  @ComputerName & """")
   _WinWait($Win_PrintServerProperties,$WaitTime)
   $hWnd = ControlGetHandle($Win_PrintServerProperties,"",$Win_PrintServerProperties_PortListClass)
   $cnt = _GUICtrlListView_GetItemCount($hWnd)
   $IsExist = False

   Local $item 
   For $idx = 0  To $cnt - 1
	  $item = _GUICtrlListView_GetItem($hWnd, $idx)
	  If StringLen($item[3]) > 0 Then
		 If StringInStr ( $item[3], $DriverName) > 0  Then
			 _GUICtrlListView_SetItemSelected($hWnd, $idx)
			 $IsExist = True
		  Else
			 _GUICtrlListView_SetItemSelected($hWnd, $idx, False)
		  EndIf
	   EndIf
   Next

   If $IsExist Then
	  _ControlClick($Win_PrintServerProperties, $Win_PrintServerProperties_PortDelText, $Win_PrintServerProperties_PortDelClass)
	  Sleep($SleepShort)
	  _WinWait($Win_PrintServerProperties_DelPort,$WaitTime)
	  _ControlClick($Win_PrintServerProperties_DelPort, $Win_PrintServerProperties_DelPortOKText, $Win_PrintServerProperties_DelPortOKClass)
	  Sleep($SleepMedium)
   EndIf

   WinActivate($Win_PrintServerProperties)
   If $IsExist  Then
	  _ControlClick ($Win_PrintServerProperties,$Win_PrintServerProperties_Close_Text,$Win_PrintServerProperties_DelCloseCls)
   Else
	  WinClose($Win_PrintServerProperties)
   EndIf
EndFunc
#ce

Func _DeletePort()
   Sleep($SleepShort)
   Run("rundll32 printui.dll,PrintUIEntry /s /t1 /c""\\" &  @ComputerName & """")
   _WinWait($Win_PrintServerProperties,$WaitTime)
   $hWnd = ControlGetHandle($Win_PrintServerProperties,"",$Win_PrintServerProperties_PortListClass)
   $cnt = _GUICtrlListView_GetItemCount($hWnd)
   $IsExist = False

   Local $item 
   For $idx = 0  To $cnt-1
	  $item = _GUICtrlListView_GetItem($hWnd, $idx)
	  ;MsgBox(0,"",$item[3])
	  If StringLen($item[3]) > 0 Then
		If StringInStr ( $item[3], $PRNFILE_TMP) > 0  Then
			 _GUICtrlListView_SetItemSelected($hWnd, $idx)
			 $IsExist = True
		  Else
			 _GUICtrlListView_SetItemSelected($hWnd, $idx, False)
		  EndIf
	   EndIf
   Next

   If $IsExist Then
	  ControlClick($Win_PrintServerProperties, $Win_PrintServerProperties_PortDelText, $Win_PrintServerProperties_PortDelClass)
	  Sleep($SleepShort)
	  _WinWait($Win_PrintServerProperties_DelPort,$WaitTime)
	  ControlClick($Win_PrintServerProperties_DelPort, $Win_PrintServerProperties_DelPortOKText, $Win_PrintServerProperties_DelPortOKClass)
	  Sleep($SleepShort)
	  WinActivate($Win_PrintServerProperties)
	  Send("!{F4}")
	  ;WinClose($Win_PrintServerProperties)
   Else
	  WinClose($Win_PrintServerProperties)
   EndIf

   ;WinActivate($Win_PrintServerProperties)
   ;If $IsExist  Then
	;  ControlClick ($Win_PrintServerProperties,$Win_PrintServerProperties_Close_Text,$Win_PrintServerProperties_Close_AdvabcedClass)
  ; Else
	;  WinClose($Win_PrintServerProperties)
  ; EndIf
  EndFunc

Func _ReStartPrinterService()
   RunWait("net stop ""Print Spooler""", "", @SW_HIDE)
   Sleep($SleepNormal)
   RunWait("net start ""Print Spooler""", "", @SW_HIDE)
EndFunc

Func _NFileWriteLog($File, $msg)
   $aryPath = StringSplit($File, "\")
   _ArrayDelete($aryPath, UBound($aryPath) - 1)
   _ArrayDelete($aryPath, 0)
   $nPath = _ArrayToString($aryPath, "\")
   DirCreate ( $nPath )
   _FileWriteLog($File,$msg)
EndFunc

Func _GetZipName($sDriverPath)
   Local $aryPath = StringSplit($sDriverPath, "\")
   
   If $aryPath[0] >= 3  Then
	  $zipNm = $aryPath[3]
	  $aryPath = StringSplit($zipNm, ".")
	  If $aryPath[0] > 0 Then
		 $zipNm = $aryPath[1]
	  EndIf
   Else
	  $zipNm = ""
   EndIf
   
   Return $zipNm
EndFunc


Local $logFolder[3] = ["c:\Log\OK", "c:\Log\NG" , "c:\Log\test.prn"]
Func _CreateLogPath($LogPath, $sDriverPath, $DriName, $Os_Version)
   $sZipNM = _GetZipName($sDriverPath)
   
   $logOk = $LogPath & "\" & $sZipNM & "\" & $DriverName & "\" & $Os_Version & "\OK"
   $logNg = $LogPath & "\" & $sZipNM & "\" & $DriverName & "\" & $Os_Version & "\NG"
   
   $ret = DirGetSize ( $logOk)
   If $ret = -1 And @error = 1 Then
	  DirCreate($logOk)
   EndIf
   
    $ret = DirGetSize ( $logNg)
   If $ret = -1 And @error = 1 Then
	  DirCreate($logNg)
   EndIf
   
   $logFolder[0] = $logOk
   $logFolder[1] = $logNg
   $logFolder[2] = $LogPath & "\" & $sZipNM & "\" & $DriverName & "\" & $Os_Version & "\"
EndFunc

Func _selListBox($title, $controlID, $text)
   
   $hWnd = ControlGetHandle($title, "", $controlID)
   $idx = _GUICtrlListBox_FindInText($hWnd, $text)
   
   If $idx = -1 Then
	  $cnt = _GUICtrlListBox_GetCount($hWnd)
	  For $idx = 0  To $cnt -1
		 $cText = _GUICtrlListBox_GetText($hWnd, $idx)
		 If StringCompare($cText, $text, 1) = 0  Then
			ExitLoop
		 EndIf
	  Next
	  
   EndIf

   If $idx > -1 Then
;~ 	  ControlCommand ( $title, "", $controlID, "SetCurrentSelection" , $idx ) 
		 _GUICtrlListBox_SetCurSel($hWnd, $idx)
		 _ControlClick ( $title, "", $controlID )
   EndIf

EndFunc