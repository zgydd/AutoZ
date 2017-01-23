#include <GuiComboBox.au3>
#include <File.au3>
#include <Array.au3>
Func _ComboBoxSelectItem($hWnd,$DPP1,$Class,$Text)
   $aList = _GUICtrlComboBox_GetListArray($hWnd)
   $iIndex = _ArraySearch($aList,$Text,0,0,0,2)
   If $iIndex = -1 Then
	  _FileWriteLog($LogFileNG,$Text & " is not be found")
   Else
	  ControlCommand($DPP1,"",$Class,"SetCurrentSelection",$iIndex -1)
   EndIf
EndFunc
