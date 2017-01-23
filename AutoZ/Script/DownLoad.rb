#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/09/22   Gou Zhizhong    new
# 1.1      2014/10/28   ZW			    fit with MTI
#====================================================================
$:.unshift(".\\common")
$:.unshift(".\\common\\com")

require 'configIni'
require 'CsvFile'
require 'SDF'
require 'LogFile'

at_exit {
    # p "6565656565656565656565656565656565656565656565344============================="
}

params =  ARGF.argv #sdf-d sdf-n all new
param = "all"
isother = nil

if !params.nil? 
    if !params[0].nil?
        param = params[0].to_s
    end
    
    if !params[1].nil?
        isother = params[1].to_s
    end
end

appPath = Dir.getwd.gsub("/","\\")
system("del /q /s *.log")
system("del /q /s *.txt")
$Log = LogFile.new("#{appPath}\\Log\\logMain.log")
$Log.wLP("App Start")

cfg = ConfigIni.new("#{appPath}\\Config.ini")
system("del /q #{cfg.r("App","logFolder")}\\*.log")
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

#sdf down
if param.include?("sdf")
    sdf = SDF.new(cfg, appPath)
    if !sdf.sdfDown(csv, param)
        $Log.wLP("sdf down err! or [sdf] field is empty(#{csvFile})")
         exit
    end
end
