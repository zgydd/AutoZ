
class LogFile
    @f = nil
    def initialize(logFile)
        @f = File.new(logFile,"w+")
    end

    def w(log)
        @f.write("#{Time.now.to_s} : #{log}")
        @f.flush
    end
    
    def wL(log)
        @f.write("#{Time.now.to_s} : #{log}\n")
        @f.flush
    end
    
    def wLP(log)
        p log
        @f.write("#{Time.now.to_s} : #{log}\n")
        @f.flush
    end

    def r()
        return @f.gets
    end

    def close()
        @f.close
    end

end