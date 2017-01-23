#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/11/07   ZW			    fit with MTI
# 1.1      2014/12/24   ZW			    Add name flag to split different baseline with different package group(on test)
#====================================================================
$:.unshift(".\\common")
$:.unshift(".\\common\\com")

require 'configIni'
require 'CsvFile'
require 'FtpServer'
require 'LogFile'

at_exit {
    # p "6565656565656565656565656565656565656565656565344============================="
}

params =  ARGF.argv #LocalPath
localpath = ""
osType = "0"
catflg = "0"
dircatname = ""
updircatname = ""
needOld = "0"
if !params.nil? 
    if !params[0].nil?
        localpath = params[0].to_s
    end
    if !params[1].nil?
        dircatname = params[1].to_s
    end
    if !params[2].nil?
        updircatname = params[2].to_s
    end
    if !params[3].nil?
        catflg = params[3].to_s
    end
    if !params[4].nil?
        osType = params[4].to_s
    end
    if !params[5].nil?
        needOld = params[5].to_s
    end
end

appPath = Dir.getwd.gsub("/","\\")
#system("del /q /s *.log")
#system("del /q /s *.txt")
$Log = LogFile.new("#{appPath}\\Log\\UpandDownLog.log")
$Log.wLP("App Start")

cfg = ConfigIni.new("#{appPath}\\Config.ini")
#system("del /q #{cfg.r("App","logFolder")}\\*.log")
csvFile = cfg.r("App","csvName")
if !File.exist?("#{appPath}\\#{csvFile}")
    $Log.wLP("File is not exist !(#{csvFile})")
    exit
end

csv = CsvFile.new()
csv.loadFile("#{appPath}\\#{csvFile}",1)
if csv.All.length<=0
    $Log.wLP("File data is not error !(#{csvFile})")
    exit
end

ftp = FtpServer.new(cfg.r("server","sname"),cfg.r("server","uname"),cfg.r("server","pwd"))

csv.All.each{|row_id,data|
    $Log.wLP("")
    $Log.wLP("-----------------Start #{row_id} -----------------")
    
	$Log.wLP("Upload to Server")
	upath = data["UploadPath"]
	if upath[upath.length-1]!="\\"
		upath = upath + "\\"
	end
	nameflg = data["pkgNameFlg"]
	name32 = data["PackageName"]
	name64 = data["PackageName64"]
	
    ftp.traverse_dir(localpath, upath, nameflg, dircatname, catflg, updircatname, name32, name64)
    $Log.wLP("Upload Packages finished")
	
    $Log.wLP("Start Download Packages")
    ftp.DownPackages(data, cfg, catflg, needOld, updircatname, osType)
    $Log.wLP("Download Packages finished")
    $Log.wLP("-----------------Finished #{row_id} -----------------")
}

