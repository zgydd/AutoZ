#include <File.au3>
#include <WinNet.au3>
;
$sLocalName = "R:"
$sRemoteName = "\\172.25.79.116\TestResult"
$sUserName = "huliangliang"
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
$iscpy = False
;~ _CopyLogToServer("C:\Log", _GetZipName($InstallFromDiskDrivePath))
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
	  EndIf
   EndIf

   If $ConnectServer Then
;~ 	  $sLocalPath =  "C:\Log"
 	FileWriteLine(@ScriptDir& "\testip.txt",@IPAddress1)  
   FileCopy ( @ScriptDir& "\testip.txt", $sLocalName & "\", 1)
   _CloseConnectionS($sLocalName,$sRemoteName )
   EndIf
   EndFunc