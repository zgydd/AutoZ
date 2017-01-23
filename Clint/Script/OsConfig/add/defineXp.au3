#include-once
Func _DefineXp()
   
   ;
   $AddPrinterWinName = "Add Printer Wizard"
   $AddPrinterNextCls = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterNextTxt = "&Next >"
   
   ;Local printer
   $AddPrinterLocalPrinterAttachedCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterLocalPrinterAttachedTxt = "&Local printer attached to this computer"
   
   $AddPrinterLocalPrinterDetectCls = "[CLASS:Button; INSTANCE:4]"
   $AddPrinterLocalPrinterDetectTxt = "&Automatically detect and install my Plug and Play printer"
   $AddPrinterLocalPrinterNextCls = "[CLASS:Button; INSTANCE:6]"
   $AddPrinterLocalPrinterNextTxt = "&Next >"
   
   ;Create a new port:
   $AddPrinterNewPortTxt = "&Create a new port:"
   $AddPrinterNewPortCls = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterPortTypeComboBoxCls = "[CLASS:ComboBox; INSTANCE:2]"
   $AddPrinterPortTypeLocalPort = "Local Port"
   $AddPrinterPortNextCls = "[CLASS:Button; INSTANCE:8]"
   $AddPrinterPortNextTxt = "&Next >"
   
   ;set port dialog
   $AddPrinterPortNameTit = "Port Name"
   $AddPrinterPortNameEditCls = "[CLASS:Edit; INSTANCE:1]"
   $AddPrinterPortNameOKCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterPortNameOKTxt = "OK"
   
   ;Have Disk
   $AddPrinterHaveDiskTxt = "&Have Disk..."
   $AddPrinterHaveDiskCls = "[CLASS:Button; INSTANCE:3]"
   $InstallFromDiskWinName = "Install From Disk"
   $InstallFromDiskFilesEditClass = "[CLASS:Edit; INSTANCE:1]"
   $InstallFromDiskOKText = "OK"
   $InstallFromDiskOKClass = "[CLASS:Button; INSTANCE:1]"
  ; $InstallFromDiskDrivePath = "C:\test\MetisC1cde_EXP_PCLXL_W64_V1100_20130201\oemrelease\RICOH\Drivers\PCL6\x64\MUI\disk1" ;*****
   
   ;Select model
  ; $DriverName = "infotec MP C5503 PCL 6" ;*****
  ; $DriverOEM = "infotec" ;*****
   $AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
   $AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
   $AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:1]"
   $AddPrinterPrinterSelModelNextCls = "[CLASS:Button; INSTANCE:11]"
   
   ;PrinterName
   $AddPrinterPrinterNameNextCls = "[CLASS:Button; INSTANCE:13]"
   
   ;click Share name
   $AddPrinterShareNameTxt = "&Share name:"
   $AddPrinterShareNameCls = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterShareNextCls = "[CLASS:Button; INSTANCE:15]"
   ;comment
   $AddPrinterCommentNextCls = "[CLASS:Button; INSTANCE:16]"
   
   ;check print test page
   $AddPrinterPrintTestPageCls = "[CLASS:Button; INSTANCE:1]"
   
   $AddPrinterPrintTestPageTxt = "&Yes"
   $AddPrinterPrintTestPageNextCls = "[CLASS:Button; INSTANCE:18]"
   
   $AddPrinterFinishText = "Finish"
   $AddPrinterFinishClass = "[CLASS:Button; INSTANCE:20]"
   
   If StringInStr(" X86 ",@OSArch) > 0  Then
	  
	  $AddPrinterPrintTestPageCls = "[CLASS:Button; INSTANCE:2]" ;32bit
	  $AddPrinterPrintTestPageNextCls = "[CLASS:Button; INSTANCE:19]"
	  $AddPrinterFinishClass = "[CLASS:Button; INSTANCE:21]"
	  
   EndIf
   
   ;Hardware Installation
   $WindowsSecurityWinName = "Hardware Installation"
   $WindowsSecurityInstallText = "&Continue Anyway"
   $WindowsSecurityInstallClass = "[CLASS:Button; INSTANCE:1]"
   
   ;TestPageOK
   $AddPrinterTestPageOKTxt= "OK"
   $AddPrinterTestPageOKCls = "[CLASS:Button; INSTANCE:1]"
   
EndFunc