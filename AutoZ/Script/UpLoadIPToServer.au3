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
If FileExists(@ScriptDir& "\testip.txt") Then
   FileDelete(@ScriptDir& "\testip.txt")
EndIf
$iscpy = False
;~ _CopyLogToServer("C:\Log", _GetZipName($InstallFromDiskDrivePath))
Sleep(15000)
$try=0
_CopyIPToServer("C:\Log")

Func _CloseConnectionS($strLocalName, $strRemoteName )
   _WinNet_CancelConnection2($strLocalName )
   _WinNet_CancelConnection2($strRemoteName )
EndFunc


;~ Func _CopyLogToServer($sLocalPath ,$folrder)
Func _CopyIPToServer($sLocalPath )
   
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
 	FileWriteLine(@ScriptDir& "\testip.txt",@IPAddress1)  
   FileCopy ( @ScriptDir& "\testip.txt", $sLocalName & "\", 1)
   Else
  
   $try += 1
	  If $try <= 2 Then
		 Sleep(2000)
	  ;ConsoleWrite("=========898======8888==2==============" & @crlf)
	  _CopyIPToServer("C:\Log")
	  EndIf
   _FileWriteLog(@ScriptDir & "\UpLoadLogToServer.log","Can not connection to server!")
   EndIf
   _CloseConnectionS($sLocalName,$sRemoteName )
   EndFunc