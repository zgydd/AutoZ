;#include <Config_JP.au3>
;#include <Config_EN.au3>
#cs
#include <GuiComboBox.au3>
#include <File.au3>
#include <Array.au3>
#include <GuiListView.au3>
#include <GuiButton.au3>
#include <WinAPI.au3>
#include <Constants.au3>
;#include <About_ComboBoxSelectItem.au3>
#ce 
;$language="english";Default 10.23
;$DiskDrivePath = "E:\test\oemrelease\RICOH\Drivers\PCL5e\XP_VISTA\english\disk1"
;$DriverName = "RICOH MP 6054 PCL 5e"
;$DriverOEM = "Ricoh" 
;~ $DiskDrivePath=$InstallFromDiskDrivePath

;$_sysLang = _MuiLanguage(@OSLang)
#cs 
$WaitTime=5000
$SleepMore=30000
;If $_sysLang = "English" Then
;#include-once
;OS
$OS = "Win7"
;Sleep
$SleepShort = 1000
$SleepMedium = 3000
$SleepLong = 60000

;Run
$RunWinName = "Run"
$RunOKText = "OK"
$RunOKClass = "[CLASS:Button; INSTANCE:2]"
$RunNameEditClass = "[CLASS:Edit; INSTANCE:1]"

;Device and Printer
$DAP = "Devices and Printers"

;Install
$AddPrinterWinName = "Add Printer"
$AddPrinterLocalText = "Add a &local printer"
$AddPrinterLocalClass = "[CLASS:Button; INSTANCE:5]"
$AddPrinterNewPortText = "&Create a new port:"
$AddPrinterNewPortClass = "[CLASS:Button; INSTANCE:8]"
$AddPrinterPortTypeComboBoxClass = "[CLASS:ComboBox; INSTANCE:2]"
$AddPrinterPortTypeTCPIP = "Standard TCP/IP Port"
$AddPrinterNextText = "&Next"
$AddPrinterNextClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterIPEditClass = "[CLASS:Edit; INSTANCE:1]"
$AddPrinterIPAdders = "10.10.10.32" ;*****
$AddPrinterAutoSelectText = "&Query the printer and automatically select the driver to use"
$AddPrinterAutoSelectClass = "[CLASS:Button; INSTANCE:9]"
$AddPrinterHaveDiskText = "&Have Disk..."
$AddPrinterHaveDiskClass = "[CLASS:Button; INSTANCE:11]";fixed10.20
$InstallFromDiskWinName = "Install From Disk"
$InstallFromDiskFilesEditClass = "[CLASS:Edit; INSTANCE:1]"
;$InstallFromDiskDrivePath = "E:\test\oemrelease\RICOH\Drivers\PCL5e\XP_VISTA\english\disk1" ;*****
$InstallFromDiskOKText = "OK"
$InstallFromDiskOKClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
$AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
$AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:1]"
;$DriverName = "RICOH Aficio MP 5002 PCL 5e" ;*****
;$DriverOEM = "RICOH" ;*****
$WindowsSecurityWinName = "Windows Security"
$WindowsSecurityInstallText = "&Install this driver software anyway"
$WindowsSecurityInstallClass = "[CLASS:Button; INSTANCE:2]"
$AddPrinterTestPageText = "&Print a test page"
$AddPrinterTestPageClass = "[CLASS:Button; INSTANCE:16]";fixed
$AddPrinterFinishText = "&Finish"
$AddPrinterFinishClass = "[CLASS:Button; INSTANCE:2]";fixed
$AddPrinterTestPageCloseClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterTestPageCloseText = "Close"
$AddPrinterIsNotListedText="The p&rinter that I want isn't listed"
$AddPrinterIsNotListedClass="[CLASS:Button; INSTANCE:5]"
$AddPrinterLocalPrinterText="Add a l&ocal printer or network printer with manual settings"
$AddPrinterLocalPrinterClass="[CLASS:Button; INSTANCE:11]"
$AddPrinterPrinterDOMSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:3]"
$AddPrinterPrinterEXPSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:4]"
$AddPrinterManufacturerSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:2]"

$AddPrinterPortTypeLocal  = "Local Port"
$AddPrinterPortLocal = "Port Name"
$AddPrinterPortLocalPort  = "[CLASS:Edit; INSTANCE:1]"
$AddPrinterPortLocalOK_Class   = "[CLASS:Button; INSTANCE:1]"
$AddPrinterPortLocalOK_Text = "OK"

;Driver Delete
$Win_DriverDelete="Remove Device"
$Win_DriverDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_DriverDelete_OK_Text="&Yes"
$OtherDN="Fax" 
$Win_PrintServerProperties="Print Server Properties"
$Win_PrintServerProperties_TAB_AdvancedClass="[CLASS:SysTabControl32; INSTANCE:1]"
$Win_PrintServerProperties_Driver_TAB_Text="Drivers"
$Win_PrintServerProperties_Delete_AdvabcedClass="[CLASS:Button; INSTANCE:2]"
$Win_PrintServerProperties_Delete_Text="&Remove..."
$Win_PrintServerProperties_Close_AdvabcedClass="[CLASS:Button; INSTANCE:5]"
$Win_PrintServerProperties_Close_Text="Close"
$Win_RemoveDriverAndPackage="Remove Driver And Package"
$Win_RemoveDriverAndPackage_M_Delete_AdvancedClass="[CLASS:Button; INSTANCE:2]"
$Win_RemoveDriverAndPackage_M_Delete_Text="Re&move driver and driver package."
$Win_RemoveDriverAndPackage_OK_AdvancedClass="[CLASS:Button; INSTANCE:3]"
$Win_RemoveDriverAndPackage_OK_Text="OK"
$Win_ConfirmDelete="Print Server Properties"
$Win_ConfirmDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_ConfirmDelete_OK_Text="&Yes"
$Win_DriverPackageDelete="Remove Driver Package"
$Win_DriverPackageDelete_Delete_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_DriverPackageDelete_Delete_Text="&Delete"
$Win_DriverPackageDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:2]"
$Win_DriverPackageDelete_OK_Text="OK"
$Win_PrintServerProperties_PortListClass="[CLASS:SysListView32; INSTANCE:1]"
$Win_PrintServerProperties_PortDelText="&Delete Port"
$Win_PrintServerProperties_PortDelClass="[CLASS:Button; INSTANCE:2]"
$Win_PrintServerProperties_DelPort="Delete Port"
$Win_PrintServerProperties_DelPortOKText="OK"
$Win_PrintServerProperties_DelPortOKClass="[CLASS:Button; INSTANCE:1]"
#ce
;Wording Check
;$DP=$DriverName&" Properties" 
$Properties_TAB_AdvancedClass="[CLASS:SysTabControl32; INSTANCE:1]"
$Properties_Accessories_TAB_Text ="Accessories"
$Properties_Accessories_Option_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Properties_Accessories_Option_Text="Options"
;$Tab_Option_Datamax=9 ;*****
;Local $Tab_Option_AdvancedClass[$Tab_Option_Datamax]=["[CLASS:Button; INSTANCE:2]","[CLASS:Button; INSTANCE:3]","[CLASS:Button; INSTANCE:4]","[CLASS:Button; INSTANCE:5]","[CLASS:Button; INSTANCE:6]","[CLASS:Button; INSTANCE:7]","[CLASS:Button; INSTANCE:8]","[CLASS:Button; INSTANCE:9]","[CLASS:Button; INSTANCE:10]"] ;*****
;local $Tab_Option_Text[$Tab_Option_Datamax]=["Lower Paper Trays","Tray 3 (LCT)","Large Capacity Tray","Upper Internal Tray","Internal Shift Tray","External Tray","Finisher SR3090","Finisher SR3120","Finisher SR3110"] ;*****
$Properties_General_TAB_Text ="General"
$Properties_General_BasicSettings_AdvancedClass ="[CLASS:Button; INSTANCE:4]"
$Properties_General_BasicSettings_Text ="Pr&eferences..."
$DPP_TAB_AdvancedClass = "[CLASS:SysTabControl32; INSTANCE:1]"
$FUS_TAB_Text = "Print Quality" 


;About
$CA_TAB_Text = "About..." 
$Win_About="About" 
$About_aboutinfo_AdvancedClass="[CLASS:ListBox; INSTANCE:1]"
;$About_aboutinfo_Text="aboutinfo"；fixed
$About_AdvancedClass="[CLASS:Button; INSTANCE:10]";有修改10.16
$About_Text= "About"
;$About_aboutinfo_DataMax=4 ;*****
;Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.80","Portion Copyright (C)2005-2014 RICOH COMPANY, LTD.","Configured driver flag:Not Configured."] ;*****

;Help
$DS_TAB_Text = "Detailed Settings" 
$DS_Basic_RestoreDefaults_AdvancedClass ="[CLASS:Button; INSTANCE:11]" ;有修改before5（10.16）
$DS_Basic_RestoreDefaults_Text ="Restore Defaults" ;daiyanzheng 10.21
$Win_Help="PCL Printer Driver Help" ;xiugaile
$language="English" 

;Find Infor
$DS_TAB_Menu_Class = "[CLASS:SysListView32; INSTANCE:1]"
$DS_TAB_Help_Class = "[CLASS:Internet Explorer_Server; INSTANCE:1]"
$DS_TAB_Help_Find_MatchWhole_Class = "[CLASS:Button; INSTANCE:1]"
$DS_TAB_Help_Find_FindEdit_Class = "[CLASS:Edit; INSTANCE:1]"
$DS_TAB_Help_Find_Err_Class = "[CLASS:Static; INSTANCE:4]"
;~    $DS_Basic_RestoreDefaults_HelpText = "Restore Defaults"
$DS_TAB_Help_Find_Title = "Find"
$Win_Help_Class = "[Class:HH Parent]"
$sText_Base="Accessories";自定义待修改
$DS_Basic_RestoreDefaults_HelpText="Print Quality" 


;new driver
$NewDriverPath ="C:\Disk1" ;*****
$DPP_OneClickPreset_TrayClass = "[CLASS:ComboBox; INSTANCE:3]" ;*****
$DPP_OneClickPreset_Tray_SelectText = "Tray 1" ;*****
$DPP_OneClickPresetList_Class = "[CLASS:SysListView32; INSTANCE:1]" ;*****
$DPP_OneClickPresetList_Default = "Basic Setting" ;*****
$DPP_OneClickPreset_OKClass = "[CLASS:Button; INSTANCE:15]" ;*****
$DPP_OneClickPreset_OKText = "OK" ;*****
$DP_PrintTestPageClass = "[CLASS:Button; INSTANCE:5]"
$DP_PrintTestPageText = "Print &Test Page"
$DP_Advanced =  "Advanced"
$NewDriver_AlwaysAvailableClass = "[CLASS:Button; INSTANCE:1]"
$NewDriver_AlwaysAvailableText = "A&lways available"
$NewDriverButtonClass = "[CLASS:Button; INSTANCE:3]"
$NewDriverButtonText = "Ne&w Driver..."
$Win_NewDriver_Title = "Add Printer Driver Wizard"
$Win_NewDriver_NextButtonClass = "[CLASS:Button; INSTANCE:2]"
$Win_NewDriver_NextButtonText = "&Next >"
$Win_NewDriver_HaveDiskClass = "[CLASS:Button; INSTANCE:3]"
$Win_NewDriver_HaveDiskText = "&Have Disk..."
$Win_NewDriver_SelName_NextButtonClass = "[CLASS:Button; INSTANCE:5]"
$Win_NewDriver_SelName_NextButtonText = "&Next >"
$Win_NewDriver_OKButtonClass = "[CLASS:Button; INSTANCE:7]"
$Win_NewDriver_OKButtonText = "Finish"
$DP_DetailSetting_OKClass = "[CLASS:Button; INSTANCE:15]"
$DP_DetailSetting_OKText = "OK"
$New_About_aboutinfo_DataMax=4 ;*****
Local $New_About_aboutinfo_List[$New_About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.20","Portion Copyright (C) 2005-2011 Ricoh Company, Ltd.","Configured driver flag: Not Configured"] ;*****

#cs
;ElseIf $_sysLang = "Japanese" Then
;OS
$OS = "Win7"
;Sleep
$SleepShort = 1000
$SleepMedium = 2000
$SleepLong = 60000

;Run
$RunWinName = "ファイル名を指定して実行"
$RunOKText = "OK"
$RunOKClass = "[CLASS:Button; INSTANCE:2]"
$RunNameEditClass = "[CLASS:Edit; INSTANCE:1]"

;Device and Printer
$DAP = "デバイスとプリンター"

;Install
$AddPrinterWinName = "プリンターの追加"
$AddPrinterLocalText = "ローカル プリンターを追加します(&L)"
$AddPrinterLocalClass = "[CLASS:Button; INSTANCE:5]"
$AddPrinterNewPortText = "新しいポートの作成(&C):"
$AddPrinterNewPortClass = "[CLASS:Button; INSTANCE:8]"
$AddPrinterPortTypeComboBoxClass = "[CLASS:ComboBox; INSTANCE:2]"
$AddPrinterPortTypeTCPIP = "Standard TCP/IP Port"
$AddPrinterNextText = "次へ(&N)"
$AddPrinterNextClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterIPEditClass = "[CLASS:Edit; INSTANCE:1]"
$AddPrinterIPAdders = "192.168.5.228" ;*****
$AddPrinterAutoSelectText = "プリンターを照会して、使用するプリンター ドライバーを自動的に選択する(&Q)"
$AddPrinterAutoSelectClass = "[CLASS:Button; INSTANCE:9]"
$AddPrinterHaveDiskText = "ディスク使用(&H)..."
$AddPrinterHaveDiskClass = "[CLASS:Button; INSTANCE:11]"
$InstallFromDiskWinName = "フロッピー ディスクからインストール"
$InstallFromDiskFilesEditClass = "[CLASS:Edit; INSTANCE:1]"
$InstallFromDiskDrivePath = "C:\Disk1" ;*****
$InstallFromDiskOKText = "OK"
$InstallFromDiskOKClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
$AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
$AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:1]"
$DriverName = "RICOH imagio MP 5002 RPCS" ;*****
$DriverOEM = "Ricoh" ;*****
$WindowsSecurityWinName = "Windows セキュリティ"
$WindowsSecurityInstallText = "このドライバー ソフトウェアをインストールします(&I)"
$WindowsSecurityInstallClass = "[CLASS:Button; INSTANCE:2]"
$AddPrinterTestPageText = "テスト ページの印刷(&P)"
$AddPrinterTestPageClass = "[CLASS:Button; INSTANCE:16]"
$AddPrinterFinishText = "完了(&F)"
$AddPrinterFinishClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterTestPageCloseClass = "[CLASS:Button; INSTANCE:1]"
$AddPrinterTestPageCloseText = "閉じる"
$AddPrinterIsNotListedText="探しているプリンターはこの一覧にはありません(&R)"
$AddPrinterIsNotListedClass="[CLASS:Button; INSTANCE:5]"
$AddPrinterLocalPrinterText="ローカル プリンターまたはネットワーク プリンターを手動設定で追加する(&O)"
$AddPrinterLocalPrinterClass="[CLASS:Button; INSTANCE:11]"
$AddPrinterPrinterDOMSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:3]"
$AddPrinterPrinterEXPSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:4]"
$AddPrinterManufacturerSysListView32Class_Win8 = "[CLASS:SysListView32; INSTANCE:2]"
$AddPrinterPortTypeLocal  = "Local Port"
$AddPrinterPortLocal = "ポート名"
$AddPrinterPortLocalPort  = "[CLASS:Edit; INSTANCE:1]"
$AddPrinterPortLocalOK_Class   = "[CLASS:Button; INSTANCE:1]"
$AddPrinterPortLocalOK_Text = "OK"

;Driver Delete
$Win_DriverDelete="デバイスの削除"
$Win_DriverDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_DriverDelete_OK_Text="はい(&Y)"
$OtherDN="Fax"
$Win_PrintServerProperties="プリント サーバーのプロパティ"
$Win_PrintServerProperties_TAB_AdvancedClass="[CLASS:SysTabControl32; INSTANCE:1]"
$Win_PrintServerProperties_Driver_TAB_Text="ドライバー"
$Win_PrintServerProperties_Delete_AdvabcedClass="[CLASS:Button; INSTANCE:2]"
$Win_PrintServerProperties_Delete_Text="削除(&R)..."
$Win_PrintServerProperties_Close_AdvabcedClass="[CLASS:Button; INSTANCE:5]"
$Win_PrintServerProperties_Close_Text="閉じる"
$Win_RemoveDriverAndPackage="ドライバーとパッケージの削除"
$Win_RemoveDriverAndPackage_M_Delete_AdvancedClass="[CLASS:Button; INSTANCE:2]"
$Win_RemoveDriverAndPackage_M_Delete_Text="ドライバーとパッケージを削除する(&M)"
$Win_RemoveDriverAndPackage_OK_AdvancedClass="[CLASS:Button; INSTANCE:3]"
$Win_RemoveDriverAndPackage_OK_Text="OK"
$Win_ConfirmDelete="プリント サーバー プロパティ"
$Win_ConfirmDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_ConfirmDelete_OK_Text="はい(&Y)"
$Win_DriverPackageDelete="ドライバー パッケージの削除"
$Win_DriverPackageDelete_Delete_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Win_DriverPackageDelete_Delete_Text="削除(&D)"
$Win_DriverPackageDelete_OK_AdvancedClass="[CLASS:Button; INSTANCE:2]"
$Win_DriverPackageDelete_OK_Text="OK"
$Win_PrintServerProperties_Port_TAB_Text = "ポート"
$Win_PrintServerProperties_PortListClass = "[CLASS:SysListView32; INSTANCE:1]"
$Win_PrintServerProperties_PortDelClass = "[CLASS:Button; INSTANCE:2]"
$Win_PrintServerProperties_PortDelText = "ポートの削除(&D)"
$Win_PrintServerProperties_DelPort = "ポートの削除"
$Win_PrintServerProperties_DelPortOKClass = "[CLASS:Button; INSTANCE:1]"
$Win_PrintServerProperties_DelPortOKText = "OK"

;Wording Check
$DP=$DriverName & "のプロパティ"
$Properties_TAB_AdvancedClass="[CLASS:SysTabControl32; INSTANCE:1]"
$Properties_Accessories_TAB_Text ="オプション構成"
$Properties_Accessories_Option_AdvancedClass="[CLASS:Button; INSTANCE:1]"
$Properties_Accessories_Option_Text="オプション選択"
$Tab_Option_Datamax=9 ;*****
;Local $Tab_Option_AdvancedClass[$Tab_Option_Datamax]=["[CLASS:Button; INSTANCE:2]","[CLASS:Button; INSTANCE:3]","[CLASS:Button; INSTANCE:4]","[CLASS:Button; INSTANCE:5]","[CLASS:Button; INSTANCE:6]","[CLASS:Button; INSTANCE:7]","[CLASS:Button; INSTANCE:8]","[CLASS:Button; INSTANCE:9]","[CLASS:Button; INSTANCE:10]"] ;*****
;local $Tab_Option_Text[$Tab_Option_Datamax]=["Lower Paper Trays","Tray 3 (LCT)","Large Capacity Tray","Upper Internal Tray","Internal Shift Tray","External Tray","Finisher SR3090","Finisher SR3120","Finisher SR3110"] ;*****
;Local $Tab_Option_AdvancedClass[$Tab_Option_Datamax]=["[CLASS:Button; INSTANCE:1]","[CLASS:Button; INSTANCE:2]","[CLASS:Button; INSTANCE:4]","[CLASS:Button; INSTANCE:6]","[CLASS:Button; INSTANCE:8]","[CLASS:Button; INSTANCE:10]","[CLASS:Button; INSTANCE:12]","[CLASS:Button; INSTANCE:14]","[CLASS:Button; INSTANCE:16]","[CLASS:Button; INSTANCE:18]"] ;*****
;local $Tab_Option_Text[$Tab_Option_Datamax]=["僆僾僔儑儞慖戰","2抜僶儞僋(&L)","僩儗僀3(戝検媼巻僩儗僀)(&T)","戝検媼巻僩儗僀(&L)","杮懱忋僩儗僀(&I)","僔僼僩僩儗僀(&I)","嵍僩儗僀(&E)","僼傿僯僢僔儍乕SR3090(&F)","僼傿僯僢僔儍乕SR3120(&F)","僼傿僯僢僔儍乕SR3110(&F)"] ;*****
Local $Tab_Option_AdvancedClass[$Tab_Option_Datamax]=["[CLASS:Button; INSTANCE:2]","[CLASS:Button; INSTANCE:3]","[CLASS:Button; INSTANCE:4]","[CLASS:Button; INSTANCE:5]","[CLASS:Button; INSTANCE:6]","[CLASS:Button; INSTANCE:7]","[CLASS:Button; INSTANCE:8]","[CLASS:Button; INSTANCE:9]","[CLASS:Button; INSTANCE:10]"] ;*****
local $Tab_Option_Text[$Tab_Option_Datamax]=["2段バンク","トレイ3(大量給紙トレイ)","大量給紙トレイ","Upper Internal Tray","Internal Shift Tray","External Tray","Finisher SR3090","Finisher SR3120","Finisher SR3110"]
$Properties_General_TAB_Text ="全般"
$Properties_General_BasicSettings_AdvancedClass ="[CLASS:Button; INSTANCE:4]"
$Properties_General_BasicSettings_Text ="基本設定(&E)..."
$DPP=$DriverName & " 印刷設定"
$DPP_TAB_AdvancedClass = "[CLASS:SysTabControl32; INSTANCE:1]"
$FUS_TAB_Text = "かんたん設定" ;*****

;About
$CA_TAB_Text = "バージョン...";待修改
$Win_About="バージョン情報"
$About_aboutinfo_AdvancedClass="[CLASS:ListBox; INSTANCE:1]"
;$About_aboutinfo_Text="aboutinfo"
$About_AdvancedClass="[CLASS:Button; INSTANCE:10]"
$About_Text= "バージョン情報"
$About_aboutinfo_DataMax=4 
Local $About_aboutinfo_List[$About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.20","Portion Copyright (C) 2005-2011 Ricoh Company, Ltd.","Configured driver flag: Not Configured"] ;*****

;Help
$DS_TAB_Text = "項目別設定"
$DS_Basic_RestoreDefaults_AdvancedClass ="[CLASS:Button; INSTANCE:11]"
$DS_Basic_RestoreDefaults_Text ="標準に戻す(&R)"
$Win_Help="プリンタードライバーのヘルプ";待修改
$language="japanese"

;Find Infor
$DS_TAB_Menu_Class = "[CLASS:SysListView32; INSTANCE:1]"
$DS_TAB_Help_Class = "[CLASS:Internet Explorer_Server; INSTANCE:1]"
$DS_TAB_Help_Find_MatchWhole_Class = "[CLASS:Button; INSTANCE:1]"
$DS_TAB_Help_Find_FindEdit_Class = "[CLASS:Edit; INSTANCE:1]"
$DS_TAB_Help_Find_Err_Class = "[CLASS:Static; INSTANCE:4]"
;~    $DS_Basic_RestoreDefaults_HelpText = "標準に戻す"
$DS_TAB_Help_Find_Title = "検索"
$Win_Help_Class = "[Class:HH Parent]"
$sText_Base="Accessories";自定义待修改
$DS_Basic_RestoreDefaults_HelpText="Print Quality"

;new driver
$NewDriverPath ="C:\Disk1" ;*****
$DPP_OneClickPreset_TrayClass = "[CLASS:ComboBox; INSTANCE:3]" ;*****
$DPP_OneClickPreset_Tray_SelectText = "トレイ1"
$DPP_OneClickPresetList_Class = "[CLASS:SysListView32; INSTANCE:1]"
$DPP_OneClickPresetList_Default = "標準設定"
$DPP_OneClickPreset_OKClass = "[CLASS:Button; INSTANCE:15]" ;*****
$DPP_OneClickPreset_OKText = "OK"
$DP_PrintTestPageClass = "[CLASS:Button; INSTANCE:5]"
$DP_PrintTestPageText = "テスト ページの印刷(&T)"
$DP_Advanced =  "詳細設定"
$NewDriver_AlwaysAvailableClass = "[CLASS:Button; INSTANCE:1]"
$NewDriver_AlwaysAvailableText = "常に利用可能(&L)"
$NewDriverButtonClass = "[CLASS:Button; INSTANCE:3]"
$NewDriverButtonText = "新しいドライバー(&W)..."
$Win_NewDriver_Title = "プリンター ドライバーの追加ウィザード"
$Win_NewDriver_NextButtonClass = "[CLASS:Button; INSTANCE:2]"
$Win_NewDriver_NextButtonText = "次へ(&N) >"
$Win_NewDriver_HaveDiskClass = "[CLASS:Button; INSTANCE:3]"
$Win_NewDriver_HaveDiskText = "ディスク使用(&H)..."
$Win_NewDriver_SelName_NextButtonClass = "[CLASS:Button; INSTANCE:5]"
$Win_NewDriver_SelName_NextButtonText = "次へ(&N) >"
$Win_NewDriver_OKButtonClass = "[CLASS:Button; INSTANCE:7]"
$Win_NewDriver_OKButtonText = "完了"
$DP_DetailSetting_OKClass = "[CLASS:Button; INSTANCE:15]"
$DP_DetailSetting_OKText = "OK"
$New_About_aboutinfo_DataMax=4
Local $New_About_aboutinfo_List[$New_About_aboutinfo_DataMax]=["Driver Version: 	 1.0.0.0","PDK Version: 	 12.20","Portion Copyright (C) 2005-2011 Ricoh Company, Ltd.","Configured driver flag: Not Configured"] 
;EndIf
#ce 
