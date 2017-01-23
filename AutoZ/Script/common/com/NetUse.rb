require 'win32ole'


class NetUse
    def reMove(driver,rname)
    
        Dir.chdir(Dir.getwd.gsub("/","\\"))
        system("del use.txt")
        system("net use >>use.txt")
        driver = driver.upcase
        rname = rname.upcase

        f = File.new("use.txt","r")
        begin
        
            f.each{|line|
                xline = line.chomp.strip

                fd = xline.index("\\\\")
                if fd.nil?
                    next
                end

                l_driver = xline[0..fd-1].strip
                rpath = xline[fd..xline.length-1].strip
                
                if rpath.include?("Microsoft")
                    rpath = rpath[0..rpath.index("Microsoft")-1].to_s.strip
                end
                /^([^\ ]+?)\ /.match(l_driver)
                l_driver = $'.to_s.strip
                # p l_driver            
                if l_driver.upcase == driver
                    cmd = "net use #{driver} /delete"
                    system(cmd)
                    $Log.wL(cmd)
                elsif rpath.upcase.include?(rname) || rname.include?(rpath.upcase)
                    # p rpath
                    cmd = "net use #{rpath} /delete"
                    system(cmd)
                    $Log.wL(cmd)
                end
            }
        
        rescue => err
            $Log.wL("#{err.message} \n #{err.backtrace} ")
        end
        f.close
        system("del use.txt")
        
        removeNetDriver(driver,rname)
    end
    
    def removeNetDriver(drivernm, serverName)
        net = WIN32OLE.new('WScript.Network')
        mapDrive = net.EnumNetworkDrives
        shareDrive = ""
        
        if mapDrive.count > 0 then
            0.step(mapDrive.count-1,2) do |i|   
                shareDrive = mapDrive.Item(i+1)
                # p mapDrive.Item(i)  + shareDrive
                if shareDrive.upcase.include?(serverName.upcase)
                    # p shareDrive
                    if !mapDrive.Item(i).nil? && mapDrive.Item(i) != ""
                        net.RemoveNetworkDrive(mapDrive.Item(i), false,true)
                    else
                        net.RemoveNetworkDrive(shareDrive , false,true)
                    end
                    break
                end
            end
        end       
    end
end
