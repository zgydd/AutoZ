#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/09/19   Gou Zhizhong    new
#====================================================================
require 'readLogPath'
require 'shareDownLoad'
require 'configIni'

class LOGDownload 
    
    def initialize(cfgini,rlogPath)
        @cfg = cfgini
        @rlg = rlogPath
    end
     
    def downloadLogFiles(savePath)
        a = @cfg.readValueString("ser","ip")
        b = @cfg.readValueString("ser","us")
        c = @cfg.readValueString("ser","pw")

        sdl = ShareDownLoad.new(a,b,c)
        
        @rlg[].each{|tmp|
            sdl.downLoadFiles(tmp,savePath + tmp + "\\")
            tmp = tmp.gsub("32","64")
            sdl.downLoadFiles(tmp,savePath + tmp + "\\")
        }
        
        sdl.downLoadFiles("OfficeScanVersion.log",savePath)
        sdl.removeNetworkDriver
    end
    
end
