#AutoRun.bat [InstallPath]
Install IIS, create FTP site on your PC and config permissions allow your download server can upload files
Ensure download server's config(like "/etc/tang/sdfdl/206/mshell") fix with your PC(SDF.rb/sdfdownload.bat)
Create a folder named "downloads" under the FTP site's root
Install ruby on your PC which version is 1.9.3p545 or later(Developer's ruby version)
Install DDK and make sure you can build driver by run batch file only and currently
Install AutoIt V3.3.8.1 or later
Close UAC
Install the program
Copy directories under the "AddOns" and the "Script" floder to the program's root
Install dotNet Framework V4.0.30319 or later for cat tool
Run "registCert_Admin.bat" under the "AddOns\driversigning" as administrator
Modify xml files under the "Config" and "Interface\Config" as you PC
Modify "SrvInfo.xml" under the "Interface\Config" with the servers ip and user information
Create share directory for server
#Modify "PTClist.csv" under the "Script" as you need
#Design "AllTestConfig.csv" under the "Interface\Config" as you need for test
#Check the clients information in the "CfgSender.xml" and ensure every client had installed AutoZClient and worked
Delete all of "langconfig_[machine name].xls" under the "Interface\Config"
Copy "langconfig.xls" under the "Interface\Config" as "langconfig_[machine name].xls" and modify them as you need for test
Set it auto run at system boot if you need