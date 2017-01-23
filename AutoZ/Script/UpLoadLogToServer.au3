#include <File.au3>
#include <WinNet.au3>
;
$sLocalName = "R:"
$sRemoteName = "\\172.25.79.116\TestResult"
$sUserName = "domrst\huliangliang"
$sPassword = "pcl0driver"
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

;~ _CopyLogToServer("C:\Log", _GetZipName($InstallFromDiskDrivePath))
$iscpy = False
$try=0
_CopyLogToServer("C:\Log")

Func _CloseConnectionS($strLocalName, $strRemoteName )
   _WinNet_CancelConnection2($strLocalName )
   _WinNet_CancelConnection2($strRemoteName )
EndFunc


;~ Func _CopyLogToServer($sLocalPath ,$folrder)
Func _CopyLogToServer($sLocalPath )
   ;ConsoleWrite("=========898======8888==1==============" & @crlf)
   If Not _WinNet_AddConnection3($hWnd, $sLocalName, $sRemoteName , $sUserName , $sPassword ) Then
	  _CloseConnectionS($sLocalName,$sRemoteName )
	  If Not _WinNet_AddConnection3($hWnd, $sLocalName, $sRemoteName , $sUserName , $sPassword ) Then
		 $ConnectServer = False
	  Else
		 $ConnectServer = True
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
	   FileCopy ( "C:\log\OfficeScanVersion32.log", $sLocalName & "\", 1)
	   FileWriteLine(@ScriptDir& "\over.txt","test over!")  
       FileCopy ( @ScriptDir& "\over.txt", $sLocalName & "\", 1)
	  If $iscpy = False Then
		 _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Log have not copy to server!If you are in server now,Please ignore this message.")
	  EndIf
;~ 	  DirCopy ( $sLocalPath & "\" & $folrder, $sLocalName & "\"  & $folrder, 1)
   Else
	  $try += 1
	  If $try <= 8 Then
		 Sleep(9000)
	  ;ConsoleWrite("=========898======8888==2==============" & @crlf)
	  _CopyLogToServer($sLocalPath )
	  EndIf
  _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Can not connection to server!")
   EndIf
   
   _CloseConnectionS($sLocalName,$sRemoteName )
   
EndFunc