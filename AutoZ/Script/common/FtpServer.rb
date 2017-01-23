#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/09/22   Gou Zhizhong    new
# 1.1      2014/11/07   ZW    			fix MTI
# 1.2      2014/12/24   ZW			    Add name flag to split different baseline with different package group(on test)
#====================================================================
$:.unshift(".\\com")

require 'shareDownLoad'

class FtpServer < ShareDownLoad
    
    @share = nil
    @paths = nil
    
    def getDownPaths
        return @paths
    end
    
    def checkZip()
    end
	
	def traverse_dir(file_path, upath, nameflg, dircatname, catflg, updircatname, name32, name64)
		if File.directory? file_path
			Dir.foreach(file_path) do |file|
				if file !="." and file !=".."
					traverse_dir(file_path + "\\" + file, upath, nameflg, dircatname, catflg, updircatname, name32, name64)
				end
			end
		else	
			if catflg == "0"
				if file_path.include? nameflg and !file_path.include? dircatname
					UpFiles(file_path,upath, name32, name64)
				end
			else
				if catflg == "1"
					if file_path.include? nameflg and file_path.include? dircatname
						UpFiles(file_path,upath, name32, name64)
					end
				else
					if file_path.include? nameflg and !file_path.include? dircatname
						UpFiles(file_path,upath, name32, name64)
					end
					if file_path.include? nameflg and file_path.include? dircatname
						UpFiles(file_path,upath + "\\" + updircatname + "\\", name32, name64)
					end
				end
			end
		end
	end
	
    def DownPackages(data, extCfg, catflg, needOld, updircatname, osType)
        if @paths.nil?
            @paths = Hash.new
        else
            @paths.clear
        end
    
        @paths["x32"] = Hash.new
        @paths["x64"] = Hash.new
		if needOld != "0"
			path = data["OldPath"]
			if path[path.length-1]=="\\"
				path = path[0..path.length-2]
			end
			
			if !path.nil? && !path.empty?
				@paths["x32"]["old"] = downLoadFiles("#{path}\\#{data["OldPackage32"]}", extCfg.r("App","oldPath"))
				@paths["x64"]["old"] = downLoadFiles("#{path}\\#{data["OldPackage64"]}", extCfg.r("App","oldPath"))
			end
		end
        path = data["UploadPath"]
        if path[path.length-1]=="\\"
            path = path[0..path.length-2]
        end
        #down new 
        if !path.nil? && !path.empty?
			if catflg != "2"
				if osType == "0"
					@paths["x32"]["new"] = downLoadFiles("#{path}\\#{data["PackageName"]}", extCfg.r("App","newPath"))
				else
					if osType == "1"
						@paths["x64"]["new"] = downLoadFiles("#{path}\\#{data["PackageName64"]}", extCfg.r("App","newPath"))
					else
						@paths["x32"]["new"] = downLoadFiles("#{path}\\#{data["PackageName"]}", extCfg.r("App","newPath"))
						@paths["x64"]["new"] = downLoadFiles("#{path}\\#{data["PackageName64"]}", extCfg.r("App","newPath"))
					end
				end
			else
				if osType == "0"
					@paths["x32"]["new"] = downLoadFiles("#{path}\\#{updircatname}\\#{data["PackageName"]}", extCfg.r("App","newPath"))
				else
					if osType == "1"
						@paths["x64"]["new"] = downLoadFiles("#{path}\\#{updircatname}\\#{data["PackageName64"]}", extCfg.r("App","newPath"))
					else
						@paths["x32"]["new"] = downLoadFiles("#{path}\\#{updircatname}\\#{data["PackageName"]}", extCfg.r("App","newPath"))
						@paths["x64"]["new"] = downLoadFiles("#{path}\\#{updircatname}\\#{data["PackageName64"]}", extCfg.r("App","newPath"))
					end
				end
			end
        end
    end
    
    def UpFiles(local, path, name32, name64)
		if local.include? name32 or local.include? name64
			return upLoadFiles(local, path)
		end
    end
    
end