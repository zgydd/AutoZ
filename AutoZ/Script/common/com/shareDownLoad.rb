require 'win32ole'
require 'fileutils'

class ShareDownLoad 
    
    @diskName = nil
    @ShareServerName = nil
    @ShareUser = nil
    @SharePwd = nil
    @likename = nil
    
    def initialize(sServer,sUser,sPwd)
        @ShareServerName = sServer
        @ShareUser = sUser
        @SharePwd = sPwd
        
        @likename = @ShareServerName
        @ShareServerName.split("\\").each{|ip|
            if !ip.nil? && ip != ""
                @likename = ip
                break;
            end
        }
    end    

    #Create the Network drive map
    def createNetworkMapDrive(userName, password)        
        net = WIN32OLE.new('WScript.Network')
        mapDrive = net.EnumNetworkDrives
        shareDrive = ""
        
        p @likename
        flag = false
        if mapDrive.count > 0 then
            0.step(mapDrive.count-1,2) do |i|   
                shareDrive = mapDrive.Item(i+1)
                p mapDrive.Item(i)  + shareDrive
                # if shareDrive == @ShareServerName
                # p mapDrive.Item
                if shareDrive.upcase.include?(@likename.upcase)
                    p shareDrive
                    if !mapDrive.Item(i).nil? && mapDrive.Item(i) != ""
                        @diskName = mapDrive.Item(i)
                        flag = true
                    else
                        removeNetworkDriverByServerNM(shareDrive)
                    end
                    break
                end
            end 
            if flag == false
                @diskName = getMapDiskName + ':' 
                net.MapNetworkDrive(@diskName,@ShareServerName,true,userName,password)
            end
        else
            @diskName = getMapDiskName + ':' 
            net.MapNetworkDrive(@diskName,@ShareServerName,true,userName,password)
        end       
    end
    
    def removeNetworkDriver()
        #p @diskName
        if !@diskName.nil?
            net = WIN32OLE.new('WScript.Network')
            net.RemoveNetworkDrive(@diskName , false,true)
        end
    end
    
    def removeNetworkDriverByServerNM(serverName)
        #p @diskName
        if !serverName.nil?
            net = WIN32OLE.new('WScript.Network')
            net.RemoveNetworkDrive(serverName , false,true)
        end
    end
    
    def removeNetDriver(drivernm, serverName)
        net = WIN32OLE.new('WScript.Network')
        mapDrive = net.EnumNetworkDrives
        shareDrive = ""
        
        if mapDrive.count > 0 then
            0.step(mapDrive.count-1,2) do |i|   
                shareDrive = mapDrive.Item(i+1)
                p mapDrive.Item(i)  + shareDrive
                if shareDrive.upcase.include?(serverName.upcase)
                    p shareDrive
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
    
    def checkNetworkMapDrive()        
        net = WIN32OLE.new('WScript.Network')
        mapDrive = net.EnumNetworkDrives
        shareDrive = ""
        flag = false
        if mapDrive.count > 0 then   
            0.step(mapDrive.count-1,2) do |i|   
                shareDrive = mapDrive.Item(i+1)
                # if shareDrive.include?(@likename)
                if shareDrive.upcase.include?(@likename.upcase)
                    #p mapDrive.Item(i) + "  " + mapDrive.Item(i+1)
                    @diskName = mapDrive.Item(i)
                    flag = true
                    break
                end
            end              
        end
        return flag
    end
    
    #Get one disk letter which is not used in current machine.
    def getMapDiskName()
        diskName = ""
        fileSystemObject = WIN32OLE.new('Scripting.FileSystemObject')        
        diskHash = {}
        'A'.upto('Z') do |letter|
            diskHash[letter] = 'true'
        end
        
        #Mark used disk letter to  flase in current machine.
        fileSystemObject.Drives.each do |disk|
            'A'.upto('Z') do |letter|
                if disk.DriveLetter == letter
                    diskHash[letter] = 'false'
                    break
                end
            end   
        end
        #Get last unused letter for disk name.    
        diskHash.each do |key,value|
            if value == "true"
                diskName = key
            end
        end
        return diskName
    end
    
    def downLoadFiles(sPath,tPath)
        # p sPath
        # p tPath
        if !checkNetworkMapDrive
            createNetworkMapDrive(@ShareUser, @SharePwd)
        end
        
        if sPath.upcase.include?(@ShareServerName.upcase)
            idx = sPath.upcase.index(@ShareServerName.upcase)
            idx += @ShareServerName.length
            sPath = sPath[idx..sPath.length].strip
            sPath = File.join(@diskName,sPath)
        else
            sPath = File.join(@diskName,sPath)
        end
        
        p "********DownLoad(#{File.basename(sPath)}) Start**************"
        sPath = sPath.gsub('/','\\')
        l_file = File.join(tPath,File.basename(sPath))
         #p sPath
         #p tPath
        if File.directory?(sPath)
            #p sPath
            system("xcopy " + sPath + " " + tPath + " /D /y /s /r /q")
        elsif File.exist?(sPath)
            # p tPath
            system("xcopy " + sPath + " " + tPath + " /D /y /r /q")
        end
        p "********DownLoad(#{File.basename(sPath)}) End**************"
        return l_file
    end
    
    def upLoadFiles(lPath,uPath)
        
        if !checkNetworkMapDrive
            createNetworkMapDrive(@ShareUser, @SharePwd)
        end
        
        if uPath.upcase.include?(@ShareServerName.upcase)
            idx = uPath.upcase.index(@ShareServerName.upcase)
            idx += @ShareServerName.length
            uPath = uPath[idx..uPath.length].strip
            uPath = File.join(@diskName,uPath)
        else
            uPath = File.join(@diskName,uPath)
        end
        
        p "********UpLoad(#{File.basename(lPath)}) Start**************"
        uPath = uPath.gsub('/','\\')
		uPath = uPath.gsub('\\\\','\\')
        
        # l_file = File.join(tPath,File.basename(sPath))
        #p sPath
        if File.directory?(lPath)
            p lPath
			p " is a directory to "
			p uPath
			if Dir.exist?(uPath)
				system("xcopy " + uPath + " " + File.dirname(uPath) + "\\Back\\ /D /y /s /r /q >>..\\Log\\upload.log")
			end
			system("xcopy " + lPath + " " + uPath + " /D /y /s /r /q >>..\\Log\\upload.log")
        elsif File.exist?(lPath)
            p lPath
			p " is a file to "
			p uPath
			if File.exist?(uPath)
				system("xcopy " + File.dirname(uPath) + "\\" + File.basename(uPath) + " " + uPath + "Back\\ /D /y /r /q >>..\\Log\\upload.log")
			end
			system("xcopy " + lPath + " " + uPath + " /D /y /r /q >>..\\Log\\upload.log")
        end
        p "********UpLoad(#{File.basename(lPath)}) End**************"
        return true
    end
end
