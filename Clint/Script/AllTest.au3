;RunWait(@ScriptDir&"\No.9-Wording_Check.au3")
;RunWait("D:\program\AutoIt3\SciTE\..\autoit3.exe   E:\yizhi5 - new\TestCaseAll\No.9-Wording_Check.au3")
;default include files
#RequireAdmin
#include <File.au3>
#include <GuiButton.au3>
#include <GuiEdit.au3>
#include <GuiTab.au3>
#include <GuiListView.au3>
#include <GuiTab.au3>
#include <Array.au3>
#include <GuiComboBox.au3>
#include <GuiListBox.au3>
#include <ScreenCapture.au3>
#include <Excel.au3>
#include <WinAPI.au3>
#include <Constants.au3>
;new include files
#include<config.au3>
;#include<langconfig.au3>
;#include<Func.au3>
#include<InstallDriver.au3>
#include<No.9-Wording_Check.au3>
#include<No.10-About.au3>
#include<No.11-Help.au3>
#include<DeleteDriver.au3>

$language="SC"
$DriverName = "Gestetner DSm2540 PCL 5e"
$DriverOEM = "Gestetner" 
$zipname="CoronaC1_EXP_5EW32_V1000_20140228"
$DiskDrivePath="D:\1\"
$OS="Win7"
$Driversion="1.0.0.0"
$DDKversion="2.0.0.0"
;cmd
;$aryOem = StringSplit($DriverName," ")
   ;If $aryOem[0] > 0  Then
	;  $DriverOEM = $aryOem[1]
   ;EndIf

If  $CmdLine[0]>1 Then 
   $language=$CmdLine[1]
   $DriverOEM=$CmdLine[2]
EndIf
If  $CmdLine[0]>2 Then 
   $DriverName= $CmdLine[3]
EndIf
If  $CmdLine[0]>3 Then 
   $DiskDrivePath= $CmdLine[4]
EndIf
If $CmdLine[0]>4  Then
   $zipname=$CmdLine[5]
EndIf
If $CmdLine[0]>5  Then
   $OS=$CmdLine[6]
EndIf
If $CmdLine[0]>6  Then
   $Driversion=$CmdLine[7]
EndIf
If $CmdLine[0]>7  Then
   $DDKversion=$CmdLine[8]
EndIf

$arycon = StringSplit($zipname,"_")
If $arycon[0]>0 Then
   $aryconfig=$arycon[1]
EndIf
ConsoleWrite("=========898======8888==1=====4="&$arycon[1]& @crlf)
 $Language_Config_FilePath=(@ScriptDir&"\langconfig\langconfig_"&$aryconfig&".xls")

ConsoleWrite($Language_Config_FilePath& @crlf)

Local $oExcel = _ExcelBookOpen($Language_Config_FilePath, 0)
_ExcelSheetActivate($oExcel, "Sheet1")

#cs
Local $aArray2 = _ExcelReadArray($oExcel, 2, 1, 8, 0);读取第*行，第*格开始共*个，横向  
;excel
$language=$aArray2[0]
$DriverName = $aArray2[1]
;$DriverOEM = $aArray2[2]
$aryOem = StringSplit($DriverName," ")
   If $aryOem[0] > 0  Then
	  $DriverOEM = $aryOem[1]
   EndIf
$DiskDrivePath=$aArray2[3]
$zipname=$aArray2[4]
$OS=$aArray2[5]
$Driversion=$aArray2[6]
$DDKversion=$aArray2[7]
#ce 

$Os_Version=$OS
$InstallFromDiskDrivePath=$DiskDrivePath
$LogPath="C:\log"
#include <OsConfig\defineAdd.au3>
#include <OsConfig\defineCom.au3>

;$LogPath
;log file
;set language

;$language="russian";for debug
;Local $oExcel = _ExcelBookOpen(@ScriptDir &  "\langconfig.xls", 0)
;$Language_Config_FilePath="E:\autoexceldemo\langconfig.xls"
;$Language_Config_FilePath=(@ScriptDir&"\langconfig.xls")
;Local $oExcel = _ExcelBookOpen($Language_Config_FilePath, 0)
;_ExcelSheetActivate($oExcel, "Sheet1")
Select
    Case $language="English"
     Local $aArray = _ExcelReadArray($oExcel, 5, 3, 15, 0)
  Case $language="German" Or $language="deutsch"
     Local $aArray = _ExcelReadArray($oExcel, 6, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="French" Or $language="francais"
     Local $aArray = _ExcelReadArray($oExcel, 7, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Italian" Or $language="italiano"
     Local $aArray = _ExcelReadArray($oExcel, 8, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Spanish" Or $language="espanol"
     Local $aArray = _ExcelReadArray($oExcel, 9, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Dutch" Or $language="nedrlnds"
     Local $aArray = _ExcelReadArray($oExcel, 10, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Swedish" Or $language="svenska"
     Local $aArray = _ExcelReadArray($oExcel, 11, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Danish" Or $language="dansk"
     Local $aArray = _ExcelReadArray($oExcel, 12, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Norwegian" Or $language="norsk"
     Local $aArray = _ExcelReadArray($oExcel, 13, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Polish" Or $language="polski"
     Local $aArray = _ExcelReadArray($oExcel, 14, 3, 15, 0);读取第*行，第*格开始共*个，横向
   Case $language="Portuguese" Or $language="portugus"
     Local $aArray = _ExcelReadArray($oExcel, 15, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Czech" Or $language="cesky"
     Local $aArray = _ExcelReadArray($oExcel, 16, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Hungarian" Or $language="magyar"
     Local $aArray = _ExcelReadArray($oExcel, 17, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Finnish" Or $language="suomi"
     Local $aArray = _ExcelReadArray($oExcel, 18, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Russian"
     Local $aArray = _ExcelReadArray($oExcel, 19, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Catalan"
     Local $aArray = _ExcelReadArray($oExcel, 20, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Turkish"
     Local $aArray = _ExcelReadArray($oExcel, 21, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Brazilian Portuguese"
     Local $aArray = _ExcelReadArray($oExcel, 22, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="Greek"
     Local $aArray = _ExcelReadArray($oExcel, 23, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="TC"
     Local $aArray = _ExcelReadArray($oExcel, 24, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case $language="SC"
     Local $aArray = _ExcelReadArray($oExcel, 25, 3, 15, 0);读取第*行，第*格开始共*个，横向
    Case  $language="Korea" Or "korean"
     Local $aArray = _ExcelReadArray($oExcel, 26, 3, 15, 0);读取第*行，第*格开始共*个，横向
  EndSelect	 
;MsgBox(0,"0",$aArray[0])
;_ArrayDisplay($aArray, "Horizontal")

Local $aArray1 = _ExcelReadArray($oExcel, 2, 9, 3, 0)
$year=$aArray1[0]
$About_AdvancedClass=$aArray1[1];click about
$DS_Basic_RestoreDefaults_AdvancedClass=$aArray1[2]


_ExcelBookClose($oExcel )
;Wording Check
$Properties_Accessories_TAB_Text = $aArray[2];"Accessories"
$Tab_Option_Datamax=1 ;*****
Local $Tab_Option_AdvancedClass[$Tab_Option_Datamax]=["[CLASS:Button; INSTANCE:2]"] ;*****
Local $Tab_Option_Text[$Tab_Option_Datamax]=[$aArray[3]];["Lower Paper Trays"];*****
$Properties_General_BasicSettings_Text ="Pr&eferences..."
$FUS_TAB_Text = $aArray[6];"Print Quality"
$DP=$DriverName&" Properties" 
$DPP=$DriverName&" Printing Preferences" 

;About
$CA_TAB_Text = $aArray[10];"About..." 
$Win_About=$aArray[11];"About" 
$About_AdvancedClass=$aArray[12];click about
$DS_Basic_RestoreDefaults_AdvancedClass=$aArray[13]

$About_aboutinfo_DataMax=2 
;Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.80","Portion Copyright (C) 2005-2014 RICOH COMPANY, LTD.","Configured driver flag: Not Configured."]
Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 "&$Driversion,"PDK Version: 	 "&$DDKversion]

;Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.80","Portion Copyright (C)2005-2014 RICOH COMPANY, LTD.","Configured driver flag:Not Configured."]
;Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 "&$Driversion,"PDK Version: 	 "&$DDKversion,"Configured driver flag:Not Configured."]
Local $New_About_aboutinfo_List1="Portion Copyright (C) "&$year&" RICOH COMPANY, LTD."
Local $New_About_aboutinfo_List2="Portion Copyright (C)"&$year&" RICOH COMPANY, LTD."
Local $New_About_aboutinfo_List3="Configured driver flag:Not Configured."
Local $New_About_aboutinfo_List4="Configured driver flag: Not Configured."


;help
$DS_Basic_RestoreDefaults_Text =$aArray[5];"Restore Defaults" ;daiyanzheng 10.21
$Win_Help=$aArray[7];"PCL Printer Driver Help"
$language=$aArray[0];"English" 

;findinfo
$sText_Base=$aArray[8];"Accessories";自定义待修改
$DS_Basic_RestoreDefaults_HelpText=$aArray[9];"Print Quality" 


;log file
$logFolder[0]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$OS&"\"&$language&"\OK"
$logFolder[1]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$OS&"\"&$language&"\NG"
$logFolder[2]=$LogPath&"\"&$zipname&"\"&$DriverName&"\"&$OS&"\"&$language&"\"
$PRNFILE = $logFolder[2] & "InstallTestPage.prn"
$PRNFILE_TMP = $logFolder[2] & "Tmp.prn"
DirCreate($logFolder[2])
;DirCreate($logFolder[0])
;DirCreate($logFolder[1])
$LogFileOK = $logFolder[0] & "\Install&TestPage-OK.log"
$LogFileNG = $logFolder[1] & "\Install&TestPage-NG.log"
Local $NextEnable

_Install_TestPage()
If $OS="win7" Then
 sleep($SleepShort)
_Wording_chek()
 sleep($SleepShort)
_about()
 sleep($SleepShort)
_Help()
EndIf
 sleep($SleepShort)
$LogFileOK = $logFolder[0] & "\Delete-OK.log"
$LogFileNG = $logFolder[1] & "\Delete-NG.log"  
_delete()

