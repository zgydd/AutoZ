AutoRun.bat [InstallPath]
#Install IIS, create FTP site on your PC and config permissions allow your download server can upload files
Install AutoIt v3.3.8.1(Developer's AutoIt version, must this version)
#Create a folder named "AutoZData" under the FTP site's root(Defined at server's sender configration "ClientDir")
#Connected server to set the test PC's information(ip;username;password etc)
Create share directory for server
Install this program
Copy directories under the "AddOns" and the "Script", "Main" floder to the program's root
Modify xml files under the "Config" as you PC
Install dotNet Framework V4.0.30319 or later for cat tool
Modify "SrvInfo.xml" under the "Interface\Config" with the servers ip and user information
Use anministrator to run AutoZClient if it not work and close UAC
Set it auto run at system boot if you need