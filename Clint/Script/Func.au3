#include-once
#include <File.au3>
#include <GuiListView.au3>
#include <Array.au3>
#include <GuiButton.au3>
#include <GuiComboBox.au3>
#include <WinAPI.au3>
#include <Constants.au3>
$WaitTime=500

;About_ComboBoxSelectItem
Func _ComboBoxSelectItem($hWnd,$DPP1,$Class,$Text)
   $aList = _GUICtrlComboBox_GetListArray($hWnd)
   $iIndex = _ArraySearch($aList,$Text,0,0,0,2)
   If $iIndex = -1 Then
	  _FileWriteLog($LogFileNG,$Text & " is not be found")
   Else
	  ControlCommand($DPP1,"",$Class,"SetCurrentSelection",$iIndex -1)
   EndIf
EndFunc


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
   ;$x = ControlCommand ($WinName,$ControlText,$ControlAdvancedClass,"IsVisible","")
   ;$y = ControlCommand ($WinName,$ControlText,$ControlAdvancedClass,"IsEnabled","")
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
	  $hWndBtn = ControlGetHandle($WinName, "",$ControlAdvancedClass)
	  $style = _WinAPI_GetWindowLong($hWndBtn, $GWL_STYLE  )
	  If BitAND($style , $BS_AUTORADIOBUTTON) <> $BS_AUTORADIOBUTTON _
		 And BitAND($style , $BS_RADIOBUTTON) <> $BS_RADIOBUTTON Then
		 If ControlFocus($WinName,"",$ControlAdvancedClass) = 0  Then
			_GUICtrlButton_SetFocus($hWndBtn)
		 EndIf
	  EndIf
	  Sleep($SleepShort)
	  
	  ;If ControlClick ($WinName,$ControlText,$ControlAdvancedClass) = 0 Then
	  If ControlClick ($WinName,"",$ControlAdvancedClass) = 0 Then
		 _GUICtrlButton_Click($hWndBtn)
	  EndIf
   Else
	  _NFileWriteLog($LogFileNG,$ControlText & " can't be clicked")
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
	  ControlSetText ($WinName, "",$ControlAdvancedClass,$SetText)
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

Local $logFolder[3] = ["c:\Log\OK", "c:\Log\NG" , "c:\Log\"]
$LogPath="c:\Log"
Func _CreateLogPath($LgPath, $sDriverPath, $DriName)
   $sZipNM = _GetZipName($sDriverPath)
   $sMuiLanguage = _MuiLanguage(@OSLang)
   $logOk = $LgPath & "\" & $sZipNM & "\" & $sMuiLanguage & "\" & $DriverName & "\OK"
   $logNg = $LgPath & "\" & $sZipNM & "\" & $sMuiLanguage & "\" & $DriverName & "\NG"
   
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
   $logFolder[2] = $LgPath & "\" & $sZipNM & "\" & $sMuiLanguage & "\"  & $DriverName & "\"

EndFunc

#cs
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
	  ControlClick($Win_PrintServerProperties, $Win_PrintServerProperties_PortDelText, $Win_PrintServerProperties_PortDelClass)
	  Sleep($SleepShort)
	  _WinWait($Win_PrintServerProperties_DelPort,$WaitTime)
	  ControlClick($Win_PrintServerProperties_DelPort, $Win_PrintServerProperties_DelPortOKText, $Win_PrintServerProperties_DelPortOKClass)
	  Sleep($SleepMedium)
   EndIf

   WinActivate($Win_PrintServerProperties)
   If $IsExist  Then
	  ControlClick ($Win_PrintServerProperties,$Win_PrintServerProperties_Close_Text,$Win_PrintServerProperties_Close_AdvabcedClass)
   Else
	  WinClose($Win_PrintServerProperties)
   EndIf
EndFunc
#ce 

Func _ReStartPrinterService()
   RunWait("net stop ""Print Spooler""", "", @SW_HIDE)
   RunWait("net start ""Print Spooler""", "", @SW_HIDE)
EndFunc
#cs
Func _NFileWriteLog($File, $msg)
   
   $aryPath = StringSplit($File, "\")
   _ArrayDelete($aryPath, UBound($aryPath) - 1)
   _ArrayDelete($aryPath, 0)
   $nPath = _ArrayToString($aryPath, "\")
   DirCreate ( $nPath )
   
   Local $wlog
   If FileExists($File) = 1 Then
	  $wlog = FileOpen($File, 1  + 128  + 8)
   Else
	  $wlog = FileOpen($File, 2  + 128 + 8)
   EndIf
   
   If $wlog <> -1 Then
	  $slog = @YEAR & "/"  & @MON & "/"  & @MDAY & " "  & @HOUR & ":"  & @MIN & ":"  & @SEC & ":"  & @MSEC & ">" & $msg
	  FileWriteLine ( $wlog, $slog)
	  FileClose($wlog)
   EndIf
   ;_NFileWriteLog($File,$msg)
EndFunc
#ce 

Func _NFileWriteLog($File, $msg)
   $aryPath = StringSplit($File, "\")
   _ArrayDelete($aryPath, UBound($aryPath) - 1)
   _ArrayDelete($aryPath, 0)
   $nPath = _ArrayToString($aryPath, "\")
   DirCreate ( $nPath )
   _FileWriteLog($File,$msg)
EndFunc

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

Func _MuiLanguage($language)
   Select
	   Case StringInStr("0413 0813", $language)
		   Return "Dutch"

	   Case StringInStr("0409 0809 0c09 1009 1409 1809 1c09 2009 2409 2809 2c09 3009 3409", $language)
		   Return "English"

	   Case StringInStr("040c 080c 0c0c 100c 140c 180c", $language)
		   Return "French"

	   Case StringInStr("0407 0807 0c07 1007 1407", $language)
		   Return "German"

	   Case StringInStr("0410 0810", $language)
		   Return "Italian"

	   Case StringInStr("0414 0814", $language)
		   Return "Norwegian"

	   Case StringInStr("0415", $language)
		   Return "Polish"

	   Case StringInStr("0416 0816", $language)
		   Return "Portuguese"

	   Case StringInStr("040a 080a 0c0a 100a 140a 180a 1c0a 200a 240a 280a 2c0a 300a 340a 380a 3c0a 400a 440a 480a 4c0a 500a", $language)
		   Return "Spanish"

	   Case StringInStr("041d 081d", $language)
		   Return "Swedish"
		   
	  Case StringInStr("0411", $language)
		   Return "Japanese"
	   Case Else
		   Return "Other (can't determine with @OSLang directly)"

    EndSelect
 EndFunc
 
Func _GetDisk1Information($disk1Path,ByRef $Array)
   
   Local $Infor[4] 
   Local $search = FileFindFirstFile($disk1Path & "\*")
   $pszFile = ""
   $fileCnt = 0
   $Infor[3] =""
   
   ; Check if the search was successful
   If $search = -1 Then
	  Exit
   EndIf

   While 1
	  Local $file = FileFindNextFile($search)
	  If @error Then ExitLoop
	  If StringInStr(StringUpper ($file),".PSZ") > 0 Then
		 $Infor[1]  = $disk1Path & "\" & $file
;~ 		 _ArrayAdd($Infor, $disk1Path & "\" & $file)
	  ElseIf StringInStr(StringUpper ($file),".INF") > 0 Then
		 $Infor[2]  = $disk1Path & "\" & $file
;~ 		 _ArrayAdd($Infor, $disk1Path & "\" & $file)
	  ElseIf StringInStr(StringUpper ($file),".CAT") > 0 Then
		 $Infor[3]  = $disk1Path & "\" & $file
;~ 		 _ArrayAdd($Infor, $disk1Path & "\" & $file)
	  EndIf
	  $fileCnt = $fileCnt + 1
	  If $Array <> "" Then
		 $Array[0] = $Array[0] + 1
	  EndIf
	  _ArrayAdd($Array, $file)
	  
   WEnd
   
   FileClose($search)

   $Infor[0] = $fileCnt
   Return $Infor
EndFunc
