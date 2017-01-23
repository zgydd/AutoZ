#include-once
Func _Define2003R2()
   
   $AddPrinterWinName = "Add Printer Wizard"
   $AddPrinterNextCls = "[CLASS:Button; INSTANCE:2]"
   $AddPrinterNextTxt = "&Next >"

   ;Local printer
   $AddPrinterLocalPrinterAttachedCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterLocalPrinterAttachedTxt = "&Local printer attached to this computer"

   $AddPrinterLocalPrinterDetectCls = "[CLASS:Button; INSTANCE:4]"
   $AddPrinterLocalPrinterDetectTxt = "&Automatically detect and install my Plug and Play printer"
;~    $AddPrinterLocalPrinterNextCls = "[CLASS:Button; INSTANCE:8]"
   $AddPrinterLocalPrinterNextCls = "[CLASS:Button; INSTANCE:6]"
   $AddPrinterLocalPrinterNextTxt = "&Next >"

   ;2003 2003 R2   xp    is not
   ;~    $AddPrinterLocalText = "Add a &local printer"
   ;~    $AddPrinterLocalClass = "[CLASS:Button; INSTANCE:5]"

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
   ;$InstallFromDiskDrivePath = "C:\test\MetisC1cde_EXP_PCLXL_W64_V1100_20130201\oemrelease\RICOH\Drivers\PCL6\x64\MUI\disk1" ;*****

   ;Select model
  ; $DriverName = "Gestetner MP C6003 PCL 6" ;*****
 ;  $DriverOEM = "Gestetner" ;*****
   $AddPrinterPrinterDOMSysListView32Class = "[CLASS:SysListView32; INSTANCE:2]"
   $AddPrinterPrinterEXPSysListView32Class = "[CLASS:SysListView32; INSTANCE:3]"
   $AddPrinterManufacturerSysListView32Class = "[CLASS:SysListView32; INSTANCE:1]"
   $AddPrinterPrinterSelModelNextCls = "[CLASS:Button; INSTANCE:11]"

   ;PrinterName
   $AddPrinterPrinterNameNextCls = "[CLASS:Button; INSTANCE:13]"

   $AddPrinterShareNextCls = "[CLASS:Button; INSTANCE:15]"

   $AddPrinterCommentNextCls = "[CLASS:Button; INSTANCE:16]"

   ;check print test page
   $AddPrinterPrintTestPageCls = "[CLASS:Button; INSTANCE:1]"
   $AddPrinterPrintTestPageTxt = "&Yes"
   $AddPrinterPrintTestPageNextCls = "[CLASS:Button; INSTANCE:18]"

   $AddPrinterFinishText = "Finish"
   $AddPrinterFinishClass = "[CLASS:Button; INSTANCE:20]"

   ;Hardware Installation
   $WindowsSecurityWinName = "Hardware Installation"
   $WindowsSecurityInstallText = "&Continue Anyway"
   $WindowsSecurityInstallClass = "[CLASS:Button; INSTANCE:1]"

   ;TestPageOK
   $AddPrinterTestPageOKTxt= "OK"
   $AddPrinterTestPageOKCls = "[CLASS:Button; INSTANCE:1]"

   EndFunc