#include-once
#include <CommonFun.au3>
#include <About_ComboBoxSelectItem.au3>

Func _OpenInstall()
   _DeletePort()
   Sleep (5000)
   Run("rundll32 printui.dll,PrintUIEntry /il ")
EndFunc


Func _GoToSetPort()
   _WinWait($AddPrinterWinName,$WaitTime)
   Sleep ($SleepShort)
   
   If $Os_Version = "xp"  Or $Os_Version = "win2003"  Or $Os_Version = "win2003r2" Then
	  
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterNextCls)
	  Sleep ($SleepMedium)

	  ;~    ControlCommand ( $AddPrinterWinName, "", $AddPrinterLocalPrinterAttachedCls, "Check" )
	  _ControlClick ($AddPrinterWinName,$AddPrinterLocalPrinterAttachedTxt,$AddPrinterLocalPrinterAttachedCls)
	  Sleep ($SleepMedium)
	  ControlCommand ( $AddPrinterWinName, "", $AddPrinterLocalPrinterDetectCls, "UnCheck" )
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterLocalPrinterNextCls)
	  
   ElseIf $Os_Version = "vista"  Or $Os_Version = "win2008"  Or $Os_Version = "win2008r2" Or $Os_Version = "win7"  Then
	  _ControlClick ($AddPrinterWinName,$AddPrinterLocalTxt,$AddPrinterLocalCls)
   Else
	  _ControlClick ($AddPrinterWinName,$AddPrinterLocalTxt,$AddPrinterLocalCls)
	  Sleep ($SleepMedium)
	  _ControlClick ($AddPrinterWinName,$AddPrinterLocalManualSettingsTxt,$AddPrinterLocalManualSettingsCls)
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterLocalManualSettingsNextCls)
   EndIf

   Sleep ($SleepMedium)
   
EndFunc


Func _CreatePort()
   
   _ControlClick ($AddPrinterWinName,$AddPrinterNewPortTxt,$AddPrinterNewPortCls)
   Sleep ($SleepShort)
   $hWndPortTypeCombo = ControlGetHandle($AddPrinterWinName,"",$AddPrinterPortTypeComboBoxCls)
   _ComboBoxSelectItem($hWndPortTypeCombo,$AddPrinterWinName,$AddPrinterPortTypeComboBoxCls,$AddPrinterPortTypeLocalPort)
   Sleep ($SleepNormal)
   
   If FileExists($PRNFILE_TMP) Then
	  FileDelete($PRNFILE_TMP)
   EndIf

   _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterPortNextCls)
   Sleep ($SleepShort)
   
   _WinWait($AddPrinterPortNameTit,$WaitTime)
   _ControlSetText ( $AddPrinterPortNameTit, $AddPrinterPortNameEditCls, $PRNFILE_TMP)
   Sleep ($SleepShort)
   _ControlClick ($AddPrinterPortNameTit,$AddPrinterPortNameOKTxt,$AddPrinterPortNameOKCls)
   Sleep ($SleepShort)
   WinActivate($AddPrinterWinName)
   Sleep ($SleepMore)
   
EndFunc

Func _SetDiskPath()
   
   _ControlClick ($AddPrinterWinName, $AddPrinterHaveDiskTxt,$AddPrinterHaveDiskCls)
   
   _WinWait($InstallFromDiskWinName,$WaitTime)
   Sleep ($SleepShort)
   _ControlSetText ( $InstallFromDiskWinName, $InstallFromDiskFilesEditClass, $InstallFromDiskDrivePath)
   Sleep ($SleepShort)
   _ControlClick ($InstallFromDiskWinName,$InstallFromDiskOKText,$InstallFromDiskOKClass)
   Sleep ($SleepMedium)

EndFunc


Func _SelectModel()

   WinActivate ($AddPrinterWinName)
   $a = ControlCommand ($AddPrinterWinName,"",$AddPrinterPrinterDOMSysListView32Class,"IsVisible", "")
   $b = ControlCommand ($AddPrinterWinName,"",$AddPrinterPrinterEXPSysListView32Class,"IsVisible", "")
   $c = ControlCommand ($AddPrinterWinName,"",$AddPrinterManufacturerSysListView32Class,"IsVisible", "")
   If $a = 1 Then
       ControlFocus ($AddPrinterWinName,"",$AddPrinterPrinterDOMSysListView32Class)
	  Sleep ($SleepShort)
	  $iIndexPrinterDOMSysListView = ControlListView($AddPrinterWinName,"",$AddPrinterPrinterDOMSysListView32Class,"FindItem",$DriverName)
	  
	  If $iIndexPrinterDOMSysListView = -1 Then
             
			   $LogFileNG = $logFolder[1] & "\Install&TestPage-NG.log"
			   _NFileWriteLog($LogFileNG, "The Driver name is disabled")
	   
       WinClose($AddPrinterWinName)
        Exit
      EndIf		
		 
	  ControlListView($AddPrinterWinName,"",$AddPrinterPrinterDOMSysListView32Class,"Select",$iIndexPrinterDOMSysListView)
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterPrinterSelModelNextCls)
	  
   ElseIf  $b = 1 and $c = 1 Then
	  
	  ControlFocus ($AddPrinterWinName,"",$AddPrinterManufacturerSysListView32Class)
	  Sleep ($SleepShort)
	  $iIndexManufacturerSysListView = ControlListView($AddPrinterWinName,"",$AddPrinterManufacturerSysListView32Class,"FindItem",$DriverOEM)
	  
	  If $iIndexManufacturerSysListView = -1 Then
               $LogFileNG = $logFolder[1] & "\Install&TestPage-NG.log"
			   _NFileWriteLog($LogFileNG, "The OEM name is disabled")
	    WinClose($AddPrinterWinName)
		Exit
	  EndIf		
	  
	  ControlListView($AddPrinterWinName,"",$AddPrinterManufacturerSysListView32Class,"Select",$iIndexManufacturerSysListView)
	  Sleep ($SleepShort)
	  ControlFocus ($AddPrinterWinName,"",$AddPrinterPrinterEXPSysListView32Class)
	  Sleep ($SleepShort)
	  $iIndexPrinterEXPSysListView = ControlListView($AddPrinterWinName, "",$AddPrinterPrinterEXPSysListView32Class,"FindItem",$DriverName)
	  
	  If $iIndexPrinterEXPSysListView = -1 Then
             
			   $LogFileNG = $logFolder[1] & "\Install&TestPage-NG.log"
			   _NFileWriteLog($LogFileNG, "The Driver name is disabled")
	   WinClose($AddPrinterWinName)
	  Exit
	  EndIf		
	  
	  ControlListView($AddPrinterWinName,"",$AddPrinterPrinterEXPSysListView32Class,"Select",$iIndexPrinterEXPSysListView)
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterPrinterSelModelNextCls)
   
   EndIf
   
   Sleep ($SleepMedium)
   ;print name
   WinActivate($AddPrinterWinName)
   _ControlClick($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterPrinterNameNextCls)
   Sleep ($SleepShort)
   
   If $Os_Version = "xp"  Or $Os_Version = "win2003"  Or $Os_Version = "win2003r2" Then

	  
	  If $Os_Version = "xp" Or $Os_Version = "win2003"  Or $Os_Version = "win2003r2" Then
		 _ControlClick ($AddPrinterWinName,"",$AddPrinterShareNameCls)
		 Sleep ($SleepShort)
		 _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterShareNextCls)
		 Sleep ($SleepNormal)
		 _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterCommentNextCls)
		 Sleep ($SleepNormal) 
	  EndIf

	  _ControlClick ($AddPrinterWinName,$AddPrinterPrintTestPageTxt,$AddPrinterPrintTestPageCls)
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterNextTxt,$AddPrinterPrintTestPageNextCls)
	  Sleep ($SleepShort)
	  _ControlClick ($AddPrinterWinName,$AddPrinterFinishText,$AddPrinterFinishClass)
	  Sleep ($SleepMore)
   
   EndIf

EndFunc

Func _WindowsSecurity()
   
   If $Os_Version <> "win2012" And $Os_Version <> "win2012r2"  Then
	  _WinWait2 ($WindowsSecurityWinName,$WaitTime,$WindowsSecurityInstallText,$WindowsSecurityInstallClass)
   EndIf
   WinActivate ($AddPrinterWinName)
    If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2"  Then
	   
	  For $SleepConut = 0 To 10000
		 WinActivate ($AddPrinterWinName)
		 $NextEnable = ControlCommand ($AddPrinterWinName,"",$AddPrinterSecurityNextCls,"IsEnabled", "")
		 _NFileWriteLog($LogFileOK, "$NextEnable=" & $NextEnable)
		 If $NextEnable = 1 Then
			Sleep ($SleepShort)
			ExitLoop
		 Else
			Sleep ($SleepShort)
		 EndIf
		 
	  Next
	  
	  If $NextEnable = 0 Then
		 _NFileWriteLog($LogFileNG, "Next Button is disabled!")
	  EndIf
	  WinActivate ($AddPrinterWinName)
	  
   EndIf
   Sleep($SleepMedium)
   
EndFunc

Func _TestPage()
   
   If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2"   Then
	  _ControlClick ($AddPrinterWinName, $AddPrinterNextTxt, $AddPrinterSecurityNextCls)
	  Sleep ($SleepMedium)
	  _ControlClick ($AddPrinterWinName,$AddPrinterPrintTestPageTxt,$AddPrinterPrintTestPageCls)
   EndIf
   
   Sleep ($SleepMedium)
   _WinWait($DriverName,$WaitTime)
   WinActivate($DriverName)
   _ControlClick ($DriverName,$AddPrinterTestPageOKTxt,$AddPrinterTestPageOKCls)
   Sleep($SleepNormal)
   FileCopy($PRNFILE_TMP, $PRNFILE, 1)
   FileDelete($PRNFILE_TMP)
   
EndFunc

Func _Close()
   
   If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2"   Then
	  Sleep ($SleepNormal)
	  _ControlClick ($AddPrinterWinName,$AddPrinterFinishText,$AddPrinterFinishClass)
   EndIf
   $LogFileOK = $logFolder[0] & "\Install&TestPage-OK.log"
   $LogFileNG = $logFolder[1] & "\Install&TestPage-NG.log"
   _NFileWriteLog($LogFileOK, "Driver is install !")
   
EndFunc