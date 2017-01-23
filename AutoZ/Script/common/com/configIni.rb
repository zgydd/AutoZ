require 'iniFile'

class ConfigIni
    
    attr_accessor :iniHash
    
    def initialize(iniFileName)
        @iniFileName = iniFileName
        @iniHash = Hash.new
        readConfigIni
    end
    
    def saveFile()
        #write
        iniSection = IniFile::new("=",@iniFileName)
        iniSection.setDatas(@iniHash)
        iniSection.write()
    end
    
    def readConfigIni      
        iniSection = IniFile::new("=",@iniFileName)
        iniSection.sections do |section|           
            @iniHash[section] = Hash.new           
            iniSection[section].each do |key, value|          
                @iniHash[section].store(key,value).chomp.strip
            end
        end 
    end    
        
    def readValueString(key, value)
        returnValues = ""   
		if @iniHash.has_key?(key) == true
			if @iniHash[key].has_key?(value) == true
				returnValues = @iniHash[key][value]
			end
		end
        return returnValues       
    end 
    
    def setValueString(key, value, values)
		if @iniHash.has_key?(key) == true
			if @iniHash[key].has_key?(value) == true
				@iniHash[key][value] = values
			end
		end
    end
    
    def checkIsKeyExist(key, value)
        if @iniHash[key].has_key?(value) == true
            return true
        else
           return false
        end		
    end
	
    def r_keys(key)
        return @iniHash[key]
    end
    
    def r(key,value)
        return readValueString(key, value)
    end
end