#include <File.au3>
#include <WinNet.au3>
;
$sLocalName = "R:"
$sRemoteName = "\\172.25.73.158\TestResult"
$sUserName = "rits"
$sPassword = "rst200233"
$iType = 1
$hWnd = 0

$ConnectServer = True

;install path
;If $CmdLine[0] > 1  Then
;   $InstallFromDiskDrivePath = $CmdLine[2] 
;EndIf
IF $CmdLine[0] = 3  Then
   $sRemoteName=$CmdLine[1]
   $sUserName = $CmdLine[2]
   $sPassword = $CmdLine[3]
EndIf
$iscpy = False
;~ _CopyLogToServer("C:\Log", _GetZipName($InstallFromDiskDrivePath))
_CopyLogToServer("C:\Log")

Func _CloseConnectionS($strLocalName, $strRemoteName )
   _WinNet_CancelConnection2($strLocalName )
   _WinNet_CancelConnection2($strRemoteName )
EndFunc


;~ Func _CopyLogToServer($sLocalPath ,$folrder)
Func _CopyLogToServer($sLocalPath )
   
   If Not _WinNet_AddConnection3($hWnd, $sLocalName, $sRemoteName , $sUserName , $sPassword ) Then
	  _CloseConnectionS($sLocalName,$sRemoteName )
	  If Not _WinNet_AddConnection3($hWnd, $sLocalName, $sRemoteName , $sUserName , $sPassword ) Then
		 $ConnectServer = False
	  EndIf
   EndIf

   If $ConnectServer Then
;~ 	  $sLocalPath =  "C:\Log"
;~ 	  
	  Local $search = FileFindFirstFile($sLocalPath & "\*")

	  ; Check if the search was successful
	  If $search <> -1 Then
		 
		 While 1
			Local $file = FileFindNextFile($search)
			If @error Then ExitLoop
			   
			If @extended  = 1  Then
			   DirCopy ( $sLocalPath & "\" & $file, $sLocalName & "\"  & $file, 1)
			   $iscpy = True
			EndIf
		 WEnd
	  Else
		 _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Can not find Local log files!")
	  EndIf

	  FileClose($search)
	  
	  If $iscpy = False Then
		 _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Log not copy to server!")
	  EndIf
;~ 	  DirCopy ( $sLocalPath & "\" & $folrder, $sLocalName & "\"  & $folrder, 1)
   Else
	  _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Can not connection to server!")
   EndIf
   
   _CloseConnectionS($sLocalName,$sRemoteName )
   
EndFunc