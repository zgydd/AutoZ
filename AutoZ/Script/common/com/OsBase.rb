$:.unshift(".\\")
require 'NetUse'

class OsBase
    @hdisk = nil
    @ipfile = nil
    @appPath = nil
    @net_use = nil
    
    def initialize()
        @net_use = NetUse.new
    end
    
protected
    def unZip(nm32, nm64, spath ,tpath)
       
        _nm32 = nm32.gsub(/\..{3}/,"")
        _nm64 = nm64.gsub(/\..{3}/,"")
        /^([^\.]+?)\./.match(_nm32)
        ext_ = $'.to_s.strip.upcase
        
        system("rd /s /q #{tpath}\\#{_nm32}")
        system("rd /s /q #{tpath}\\#{_nm64}")
        
        if !Dir.exist?(tpath)
            Dir.mkdir(tpath)
        end
        
        if Dir.exist?("#{tpath}\\#{_nm32}")
            system("rd /q /s #{tpath}\\#{_nm32}")
        end
        
        if Dir.exist?("#{tpath}\\#{_nm64}")
            system("rd /q /s #{tpath}\\#{_nm64}")
        end
        
        Dir.mkdir("#{tpath}\\#{_nm32}")
        Dir.mkdir("#{tpath}\\#{_nm64}")
        
        if ext_=="EXE"
            command = "winrar x #{spath}\\#{nm32} #{tpath}\\#{_nm32}\\ >>1.log"
            system(command)
            command = "winrar x #{spath}\\#{nm64} #{tpath}\\#{_nm64}\\ >>1.log"
            system(command)
        else
            command = "#{@appPath}\\common\\zip\\unzip.exe -o #{spath}\\#{nm32} -x  -d #{tpath}\\#{_nm32}\\ >>1.log"
            system(command)
            command = "#{@appPath}\\common\\zip\\unzip.exe -o #{spath}\\#{nm64} -x  -d #{tpath}\\#{_nm64}\\ >>1.log"
            system(command)
        end
        
    end
    
    def batgenerate(psex, va32, va64)

        cathostdir = psex
        filename = "Make_Cat_File.bat"
        strf = ""
        ts = "call %BAT_PATH%\\Make_Cat.bat %WinRAR_Path% "
      
        filenames = "Make_Cat_File1.bat"
        FileUtils.cd(cathostdir)
        FileUtils.cp filename, filenames
      
        # FileUtils.cd(cathostdir)
        file = open(filenames,"r") do |file|
            while text =file.gets do
                if /rem make cat file to package/i =~ text
                    strf = strf + text

                    if va32 !=""
                        strf = strf + ts + findname(va32)
                        strf = strf + "\n"
                    end

                    if va64 !=""
                        strf = strf + ts + findname(va64)
                        strf = strf + "\n"
                    end

                    strf = strf + "\n"
                    strf = strf + "rem log file end"
                    strf = strf + "\n"
                    strf = strf + "@echo ===============log end================ >> %BAT_PATH%\\Result.log"
                    break
                else
                    strf = strf + text
                end
            end
        end

        FileUtils.cd(cathostdir)
        file = open(filenames,"w")
        file.write(strf)
        p "cat bat file created"
        file.close
    end

    def findname(inputvalue)
      fv = inputvalue.gsub(/\..{3}/,"").to_s
      return fv
    end
    
    def waitVm()
        fid = false
        begin
            for i in 1..500
                if File.exist?(@ipfile)
                    fid = true
                    break
                end
                sleep(1)
            end
            
            @re_ip = nil
            if fid
                f = File.new(@ipfile, "r")
                f.each{|line|
                    xline = line.chomp.strip
                    if xline.nil? || xline.empty?
                        next
                    end
                    
                    @re_ip = xline
                }
                f.close
            end
            
            $Log.wLP("Test Computer ip:#{@re_ip}")
            if @re_ip.nil?
                fid =false   
            end
        rescue => err
            $Log.wL("#{err.message} \n #{err.backtrace} ")
            return false
        end
        
        return fid
    end
 
    def mapDriver()
        # @hdisk = "U:"
        
        begin
            # net32delete = "net use #{@hdisk} /DELETE"
            # system(net32delete)
            @net_use.reMove(@hdisk, @re_ip)
            
            sleep(10)            
            net32 = "net use #{@hdisk} \\\\" + @re_ip + '\\test "pcl0driver" /user:"administrator"'

            p net32
            system(net32)
            err = $?.to_s
            _err = err[err.index("exit")..err.length]
            _err = _err.gsub("exit","").strip
            # p _err
            if _err.to_i != 0
                $Log.wL(err)
                return false
            end
            
            if !Dir.exist?("#{@hdisk}") && _err.to_i != 0
                return false
            end
            
            return true
        rescue => err
            $Log.wL("#{err.message} \n #{err.backtrace} ")
            return false
        end
    end
     
    def delFile(file)
        if File.exist?(file)
            system("del /q #{file} >>1.log")
        end
    end

    def getDisk1Path(path)
        if !Dir.exist?(path)
            return ""
        end
        
        d= Dir.new(path)
        foldername = "disk1"
        file_path = path
        d.each{|folder|
            if folder[0]=="."
                # p "next"
                next
            end

            temp = "#{path}\\#{folder}"
            # p temp
            if !File.directory?(temp)
                next
            end
            
            if folder == foldername
                file_path = temp
                break
            else
                return getDisk1Path(temp)
            end
        }

        return file_path
    end
end