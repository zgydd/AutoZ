//////////////////
///Barton Joe
//////////////////
using System;
using System.Text;
using System.IO;

namespace AutoZKernel
{
    public class AutoZData
    {
        public static StringBuilder readFileToSB(string strPath, string strReadableFileType)
        {
            if (!File.Exists(strPath)) return null;
            string strRealPath = strPath;
            bool bUnReadable = false;
            FileInfo fi = new FileInfo(strPath);
            string strExt = fi.Extension;
            if (!strReadableFileType.ToUpper().Contains(strExt.ToUpper()))
            {
                strRealPath += ".txt";
                AutoZDirectorysFiles.delFile(strRealPath);
                AutoZDirectorysFiles.copyFile(strPath, strRealPath);
                bUnReadable = true;
            }
            StreamReader reader = new StreamReader(strRealPath);
            StringBuilder builder = new StringBuilder();
            while (reader.Peek() != -1)
            {
                builder.AppendLine(reader.ReadLine());
            }
            reader.Close();
            if (bUnReadable)
            {
                AutoZDirectorysFiles.delFile(strRealPath);
            }
            return builder;
        }
        public static void saveLogToRoot(string strLog, string strLogRoot)
        {
            if (!Directory.Exists(strLogRoot))
            {
                Directory.CreateDirectory(strLogRoot);
            }
            string strPath = strLogRoot + "\\saveLog.log";
            AutoZDirectorysFiles.saveFile(strLog, strPath);
        }
        public static void writeLog(string strLog, string strLogPath, string strProgramName)
        {
            string data = DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString() + "#####\n";
            if (strLogPath == null || strLogPath.Length == 0) strLogPath = strProgramName + "Log.log";
            if (!File.Exists(strLogPath))
            {
                File.Create(strLogPath).Close();
            }
            data += "{{{" + strLog + "}}}\n";
            using (StreamWriter outfile = File.AppendText(strLogPath))
            {
                outfile.Write(data);
            }
        }
        public static string appendLeft(string strSource, int iLength, bool bIsNum)
        {
            if (strSource.Length < iLength)
            {
                string strAddition = string.Empty;
                if (bIsNum) strAddition = "0";
                else strAddition = " ";
                for (int i = iLength; i > strSource.Length; i--)
                {
                    strSource = strAddition + strSource;
                }
            }
            return strSource;
        }
        public static DateTime getWeekend(DateTime dt)
        {
            int i = dt.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 5 - i;
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return dt.Add(ts);
        }
        public static bool isFullDate(string strValue)
        {
            string strResult = strValue.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Trim();
            if (strResult.Length != 8) return false;
            try
            {
                int.Parse(strResult);
                DateTime.Parse(strValue);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
