#include-once
Func _DefineWin81()
   
   ;
   $AddPrinterWinName = "Add Printer"
   $AddPrinterLocalTxt = "The p&rinter that I want isn't listed"
   $AddPrinterLocalCls = "[CLASS:Button; INSTANCE:5]"
;~    
;~    $AddPrinterNextCls = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterNextTxt = "&Next"
;~    
;~    ;Local printer
;~    $AddPrinterLocalPrinterAttachedCls = "[CLASS:Button; INSTANCE:1]"
;~    $AddPrinterLocalPrinterAttachedTxt = "&Local printer attached to this computer"
;~    
;~    $AddPrinterLocalPrinterDetectCls = "[CLASS:Button; INSTANCE:4]"
;~    $AddPrinterLocalPrinterDetectTxt = "&Automatically detect and install my Plug and Play printer"
;~    $AddPrinterLocalPrinterNextCls = "[CLASS:Button; INSTANCE:6]"
;~    $AddPrinterLocalPrinterNextTxt = "&Next >"

   $AddPrinterLocalManualSettingsCls = "[CLASS:Button; INSTANCE:11]"
   $AddPrinterLocalManualSettingsTxt = "Add a l&ocal printer or network printer with manual settings"
   $AddPrinterLocalManualSettingsNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;Create a new port:
   $AddPrinterNewPortTxt = "&Create a new port:"
   $AddPrinterNewPortCls = "[CLASS:Button; INSTANCE:14]"
   $AddPrinterPortTypeComboBoxCls = "[CLASS:ComboBox; INSTANCE:2]"
   $AddPrinterPortTypeLocalPort = "Local Port"
   $AddPrinterPortNextCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterPortNextTxt = "&Next"
   
   ;set port dialog
   $AddPrinterPortNameTit = "Port Name"
   $AddPrinterPortNameEditCls = "[CLASS:Edit; INSTANCE:1]"
   $AddPrinterPortNameOKCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterPortNameOKTxt = "OK"
   
   ;Have Disk
   $AddPrinterHaveDiskTxt = "&Have Disk..."
   $AddPrinterHaveDiskCls = "[CLASS:Button; INSTANCE:17]"
   $InstallFromDiskWinName = "Install From Disk"
   $InstallFromDiskFilesEditClass = "[CLASS:Edit; INSTANCE:1]"
   $InstallFromDiskOKText = "OK"
   $InstallFromDiskOKClass = "[CLASS:Button; INSTANCE:1]"
   ;$InstallFromDiskDrivePath = "C:\test\MetisC1cde_EXP_PCLXL_W32_V1100_20130201\oemrelease\RICOH\Drivers\PCL6\XP_VISTA\MUI\disk1" ;*****

   ;Select model
   ;$DriverName = "Gestetner MP C5503 PCL 6" ;*****
   ;$DriverOEM = "Gestetner" ;*****
   $AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
   $AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:4]"
   $AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
   $AddPrinterPrinterSelModelNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;PrinterName
   $AddPrinterPrinterNameNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;check print test page
   $AddPrinterPrintTestPageCls = "[CLASS:Button; INSTANCE:22]"
   $AddPrinterPrintTestPageTxt = "&Print a test page"
;~    $AddPrinterPrintTestPageNextCls = "[CLASS:Button; INSTANCE:16]"
   
   $AddPrinterFinishText = "Finish"
   $AddPrinterFinishClass = "[CLASS:Button; INSTANCE:2]"
   
   ;Hardware Installation
   $WindowsSecurityWinName = "Windows Security"
   $WindowsSecurityInstallText = "&Install this driver software anyway"
   $WindowsSecurityInstallClass = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterSecurityNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;TestPageOK
   $AddPrinterTestPageOKTxt= "Close"
   $AddPrinterTestPageOKCls = "[CLASS:Button; INSTANCE:1]"
   
EndFunc