#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/09/22   Gou Zhizhong    new
# 1.1      2014/10/28   ZW			    fit with MTI
#====================================================================
$:.unshift('.\\com')
require 'FileUtils'
# require 'extract_class'

class SDF
    
    @cfg = nil
    @appFolder = nil
    def initialize(exeCfg, appPath)
        @cfg = exeCfg
        @appFolder = "#{appPath}\\common"
    end

    def sdfDown(csv, param)
        sdfd = true
        begin
            if param.nil? || param.empty? || param != "sdf-d"
                return sdfd
            end
       
            #step 0 (sdflist update)
            dlen = 0
            FileUtils.cd(@appFolder)
            filename = File.join(@appFolder,"sdf.bat").gsub("/","\\")
            file = open(filename,"w")
            file.write('plink.exe -ssh -pw rits200233 root@172.25.73.164 "rm -rf /etc/tang/sdfdl/206/sdflist"')
            file.write("\n")
            file.close

            csv.All.each{|id,data|
                if id.nil? || id.empty?
                    next
                end
                
                s_df = data["sdf"].chomp.strip
                if s_df.nil? || s_df.empty?
                    next
                end
                
                dlen += 1
                # p s_df
                file = open(filename,"a")
                file.write('plink.exe -ssh -pw rits200233 root@172.25.73.164 "echo \'' + s_df + '\' >> /etc/tang/sdfdl/206/sdflist"')
                file.write("\n")
                file.close
            }
          
            if dlen > 0
                # system("sdf.bat")  
                system(filename)

                $Log.wL("SDF Downloading")
                $Log.wL("download start")
                FileUtils.cd(@appFolder)
                system("sdfdownload.bat")
                $Log.wL("download finished")
              
                #step 3. extract file
                s_Dir1 = "#{@cfg.r("App","localSdf")}\\10.60.108.81"
                if File.exist?(s_Dir1)
                    FileUtils.rm_r(s_Dir1)
                    $Log.wL("existed file deleted")
                end

                $Log.wL("extract start")
                command = "#{@appFolder}\\zip\\unzip.exe -o #{@cfg.r("App","sdfDown")}\\ritssdf.zip -x  -d #{@cfg.r("App","localSdf")}\\ >>..\\Log\\unzip.log"
                # p command
                system(command)
                $Log.wL("extract finished")
            else
                sdfd = false
            end
        rescue => err
            $Log.wL("#{err.message} \n #{err.backtrace} ")
            sdfd = false
        end
        
        return sdfd
    end

end