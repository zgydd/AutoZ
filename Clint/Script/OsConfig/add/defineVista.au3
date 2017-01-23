#include-once
Func _DefineVista()
   $AddPrinterWinName = "Add Printer"
   $AddPrinterLocalTxt = "Add a &local printer"
   $AddPrinterLocalCls = "[CLASS:Button; INSTANCE:4]"
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
   
   ;Create a new port:
   $AddPrinterNewPortTxt = "&Create a new port:"
   $AddPrinterNewPortCls = "[CLASS:Button; INSTANCE:7]"
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
   $AddPrinterHaveDiskCls = "[CLASS:Button; INSTANCE:10]"
   $InstallFromDiskWinName = "Install From Disk"
   $InstallFromDiskFilesEditClass = "[CLASS:Edit; INSTANCE:1]"
   $InstallFromDiskOKText = "OK"
   $InstallFromDiskOKClass = "[CLASS:Button; INSTANCE:1]"
   ;$InstallFromDiskDrivePath = "C:\test\ApollonC3_DOM_RPCS_W32_V11100_20140319\oemrelease\RICOH\Drivers\RPCS\XP_VISTA\disk1" ;*****

   ;Select model
  ; $DriverName = "RICOH imagio MP C5002 RPCS" ;*****
   ;$DriverOEM = "RICOH" ;*****
   $AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
   $AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
   $AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:1]"
   $AddPrinterPrinterSelModelNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;PrinterName
   $AddPrinterPrinterNameNextCls = "[CLASS:Button; INSTANCE:1]"
   
   ;check print test page
   $AddPrinterPrintTestPageCls = "[CLASS:Button; INSTANCE:15]"
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