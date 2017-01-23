#include <Func.au3>
;#include <Config.au3>
#include <GuiListView.au3>
#include <Array.au3>
#include <String.au3>
#include <SendMessage.au3>
#include <WinAPI.au3>
#include <GuiTab.au3>


 If $cmdline[0] = 0  Then
    Exit -2
 EndIf

 If FileExists($cmdline[1]) = 0 Then
    Exit -3
 EndIf
 $wbit=32
 If $cmdline[2]="en" Then
	$OfficeVersionTitle ="Version Information"
    Local $OfficeData[4] =["Virus Scan Engine ("&$wbit&"-bit)", "Smart Scan Agent Pattern", "Virus Cleanup Engine ("&$wbit&"-bit)", "Virus Cleanup Template"]
    $Scantitle ="Scanning..."
    $TrendMicroText = "Trend Micro OfficeScan"
 ElseIf  $cmdline[2]="sc" Then
	$OfficeVersionTitle = "版本信息"
	Local $OfficeData[4] =["病毒扫描引擎 ("&$wbit&" 位)","云安全客户端病毒码","病毒清除引擎 ("&$wbit&" 位)","病毒清除模板"]
    $Scantitle = "正在扫描..."
	$TrendMicroText = "趋势科技防毒墙网络版"
 Else 
	$OfficeVersionTitle ="Version Information"
    Local $OfficeData[4] =["Virus Scan Engine ("&$wbit&"-bit)", "Smart Scan Agent Pattern", "Virus Cleanup Engine ("&$wbit&"-bit)", "Virus Cleanup Template"]
    $Scantitle ="Scanning..."
    $TrendMicroText = "Trend Micro OfficeScan"
EndIf	
	

;
;~ $cmdline[1]="C:\Users\huliangliang\Desktop\New folder";fordebug
;~ $ok="C:\Users\huliangliang\Desktop\PTClist.csv";fordebug
$a=@OSArch
If $a="x86" Then
$OfficeScan = "C:\Program Files\Trend Micro\OfficeScan Client\PccNTMon.exe"
$OfficeScann = "C:\Program Files\Trend Micro\OfficeScan Client\PccNT.exe"
else
$OfficeScan="C:\Program Files (x86)\Trend Micro\OfficeScan Client\PccNTMon.exe"   
$OfficeScann="C:\Program Files (x86)\Trend Micro\OfficeScan Client\PccNT.exe"   
$wbit=64
EndIf
Local $OfficeVersion[4]
$OfficeVersion_ListCls = "[CLASS:SysListView32; INSTANCE:1]"
$LogFile="C:\log\OfficeScanVersion.log";fordebug

RunWait($OfficeScan & " -v")
_WinWait ($OfficeVersionTitle,$WaitTime)
$hWnd = ControlGetHandle($OfficeVersionTitle, "", $OfficeVersion_ListCls)
$ItemCnt = _GUICtrlListView_GetItemCount($hWnd)

For $idx = 0 To $ItemCnt -1
   $ItemInfor = _GUICtrlListView_GetItem($hWnd, $idx)
;~    ConsoleWrite($ItemInfor[3] & @crlf)
   $sIndex = _ArraySearch($OfficeData, $ItemInfor[3] )
   If $sIndex > -1  Then
	  $ItemVersion = _GUICtrlListView_GetItem($hWnd, $idx, 1)
	  $OfficeVersion[$sIndex] = $ItemVersion[3]
;~ 	  ConsoleWrite($ItemInfor[3] & @crlf)
;~ 	  ConsoleWrite($ItemVersion[3] & @crlf)
   EndIf
Next

WinClose($OfficeVersionTitle)
;~ _ArrayDisplay($OfficeVersion)
$AntivirusEngPtn = "AntivirusEng/Ptn=" & $OfficeVersion[0] & "/" & $OfficeVersion[1]
$DCSEngPtn = "DCSEng/Ptn=" & $OfficeVersion[2] & "/" & $OfficeVersion[3]
;~ ConsoleWrite($AntivirusEngPtn & @crlf)
;~ ConsoleWrite($DCSEngPtn & @crlf)

;~ If $CmdLine[0] > 2 Then
;~    $VerDriverPath = $CmdLine[3] 
;~ ElseIf $CmdLine[0] > 1 Then
;~    $VerDriverPath = $CmdLine[2] 
;~ EndIf

;log file
;~ _CreateLogPath($LogPath, $VerDriverPath, $DriverName)
;~ $VersionFile = $logFolder[2] & "OfficeScanVersion.log"
;~ _ArrayDisplay($logFolder)
;~ $VersionFile ="D:\Project\TestResult\" & "OfficeScanVersion.log
_FileWriteLog($LogFile, $AntivirusEngPtn)
_FileWriteLog($LogFile, $DCSEngPtn)


#cs
Local $file = FileOpen($VersionFile, 130)

; Check if file opened for writing OK
If $file = -1 Then
;~     MsgBox(0, "Error", "Unable to open file.")
    Exit
EndIf

FileWriteLine($file, $AntivirusEngPtn)
FileWriteLine($file, $DCSEngPtn)

FileClose($file)
#ce 
;scan 
Run($OfficeScann&" "&$cmdline[1])

$ScanMainCls = "[Class:PCCNTClassName]"

$ScanProgressCls = "[CLASS:msctls_progress32; INSTANCE:1]"


$hWnd = _WinAPI_FindWindow("#32770",$Scantitle)
While $hWnd = 0
;~    ConsoleWrite("$hWnd=" & $hWnd  & @crlf)
   $hWnd = _WinAPI_FindWindow("#32770",$Scantitle)
WEnd
 ;ConsoleWrite("$hWnd=" & $hWnd  & @crlf)

$hWnd = ControlGetHandle($Scantitle, "", $ScanProgressCls)
$PBM_GETPOS  = dec("0408")
$lResult = _SendMessageA($hWnd, $PBM_GETPOS, 0, 0)

;ConsoleWrite("ksdfnknvb000001"& @crlf)
While $lResult <> 100
   
   $lResult = _SendMessageA($hWnd, $PBM_GETPOS, 0, 0)
;~    ConsoleWrite("Progress Val=" & $lResult  & @crlf)

WEnd


$TrendMicroOkCls = "[CLASS:Button; INSTANCE:1]"
$TrendMicroOkText = ""

;ConsoleWrite("ksdfnknvb000001"& @crlf)
$hWnd = 0
For $idx = 1 To 10000
   $hWnd = _WinAPI_FindWindow("#32770",$TrendMicroText)
   If $hWnd <> 0  Then
	  ExitLoop
   EndIf
   Sleep(1000)
Next
;ConsoleWrite("000001"& @crlf)
$ScanResultTab = "[CLASS:SysTabControl32; INSTANCE:1]"
$ScanResultTabNm = "Manual Scan Results"
$ScanVirusCls = "[CLASS:Static; INSTANCE:7]"

If $hWnd <> 0  Then
   WinActivate($TrendMicroText)
   ControlFocus($TrendMicroText,"", $TrendMicroOkCls)
   ControlClick($TrendMicroText,"", $TrendMicroOkCls)
   
   $tab = ControlGetHandle($ScanMainCls, "", $ScanResultTab)
   $iIndex = _GUICtrlTab_FindTab($tab, $ScanResultTabNm)
   _GUICtrlTab_ClickTab($tab, $iIndex )
   Sleep(1000)
   $virusCnt = ControlGetText($ScanMainCls, "", $ScanVirusCls)
   ConsoleWrite("$virusCnt Val=" & $virusCnt  & @crlf)
   WinClose($ScanMainCls)
   Exit($virusCnt)
Else
   Exit(-1)
EndIf