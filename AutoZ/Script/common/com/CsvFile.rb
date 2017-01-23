#====================================================================
# Version  Date         Author          Changed point
# 1.0      2014/09/03   Gou Zhizhong    new
#====================================================================

class CsvFile

    def initialize(csvFileName = nil)
        @CsvFileName = csvFileName
        @Cammon = ","
        @HeaderRow = 1
        @hhxData = Hash::new()
        if csvFileName != nil and csvFileName.empty? == false
            parseFile
        end
    end

    def loadFile(csvFile ,csvHearderRow, cam = nil)
        @CsvFileName = csvFile
        @HeaderRow = csvHearderRow
        if cam != nil
            @Cammon = cam
        end
        parseFile
    end

    def setCsvHeader(irow)
        @HeaderRow = irow
    end

    def All
        return @hhxData
    end
    
    def [](section)
        if @hhxData.nil? == false
            return @hhxData[section]
        end
    end

    def []=(section, hash)
        if @hhxData.nil? == true
            @hhxData = Hash::new( )
        end

        @hhxData[section] = hash
    end

    def printTest()
        @hhxData.each{ |xSection, hxPairs|
            p "******************* new row ********************","\r\n"
            hxPairs.each{ |xKey, xValue|
                print xKey,"=",xValue,"\r\n"
            }
        }
    end

    def clear
        @CsvFileName = nil
        @HeaderRow = nil
    end
        
protected
    def parseFile
        #xCurrentSection = 1
        xKey = nil
        xKeyAry = nil
        open( @CsvFileName, 'r' ) do |f|
            xLine = ''
            until f.eof?
            xLine = f.gets.chomp.strip;
            if xLine.empty? == false
                if f.lineno == @HeaderRow
                    xKeyAry = xLine.split(@Cammon)
                else
                    if f.lineno > @HeaderRow
                        array = xLine.split(@Cammon)
                        if array.length > 0 
                            xKey = array[0].chomp.strip
                            @hhxData[xKey] = Hash::new()
                            xDataIndx = 0
                            array.each{|dat|
                                @hhxData[xKey][xKeyAry[xDataIndx]] = dat.to_s.chomp.strip.gsub(";",",")
                                xDataIndx = xDataIndx + 1
                            }
                            #xCurrentSection = xCurrentSection + 1
                        end
                    end 
                end
            end
          end
        end
    end

end