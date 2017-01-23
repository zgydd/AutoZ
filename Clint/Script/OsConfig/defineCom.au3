#include-once

;Sleep Infor
;$LogPath = "c:\Log"
$WaitTime = 120
$SleepShort = 1000
$SleepNormal= 6000
$SleepMedium = 15000
$SleepMore = 30000
$SleepLong = 60000

;Driver Delete
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

$Win_PrintServerProperties_Port_TAB_Text = "Ports"
$Win_PrintServerProperties_PortListClass = "[CLASS:SysListView32; INSTANCE:1]"
$Win_PrintServerProperties_PortDelClass = "[CLASS:Button; INSTANCE:2]"
$Win_PrintServerProperties_PortDelText = "&Delete Port"
$Win_PrintServerProperties_DelPort = "Delete Port"
$Win_PrintServerProperties_DelPortOKClass = "[CLASS:Button; INSTANCE:1]"
$Win_PrintServerProperties_DelCloseCls = "[CLASS:Button; INSTANCE:5]"

If $Os_Version <> "xp"  And $Os_Version <> "win2003"  And $Os_Version <>"win2003r2" Then
   $Win_PrintServerProperties_DelPortOKText = "OK"
   If $Os_Version <> "win8" And $Os_Version <> "win8.1"  And $Os_Version  <> "win2008r2" And _ 
	  $Os_Version <> "win2012" And $Os_Version <> "win2012r2" Then
	  $Win_PrintServerProperties_Close_AdvabcedClass="[CLASS:Button; INSTANCE:4]"
   EndIf
   
   If $Os_Version = "win8"  Or $Os_Version = "win8.1"  Or $Os_Version  = "win2008r2" Or _ 
	  $Os_Version= "win2012" Or $Os_Version = "win2012r2" Then
	  $Win_PrintServerProperties_DelCloseCls = "[CLASS:Button; INSTANCE:6]"
   EndIf
Else
   $Win_PrintServerProperties_DelPortOKText = "&Yes"
   $Win_PrintServerProperties_Delete_Text="&Remove"
EndIf