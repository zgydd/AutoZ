//////////////////
///Barton Joe
//////////////////
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading;

namespace AutoZKernel
{
    public class AutoZDirectorysFiles
    {
        public static void copyFile(string strFrom, string strTo)
        {
            if (!File.Exists(strFrom)) return;
            File.Copy(strFrom, strTo, true);
        }
        public static void mvFile(string strFrom, string strTo)
        {
            if (!File.Exists(strFrom)) return;
            if (File.Exists(strTo)) delFile(strTo);
            File.Move(strFrom, strTo);
        }
        public static void saveFile(StringBuilder data, string strSavePath)
        {
            delFile(strSavePath);
            StreamWriter outfile = null;
            using (outfile = new StreamWriter(strSavePath))
            {
                outfile.Write(data.ToString());
            }
            outfile.Close();
            outfile.Dispose();
        }
        public static void saveFile(string strData, string strSavePath)
        {
            delFile(strSavePath);
            StreamWriter outfile = null;
            using (outfile = new StreamWriter(strSavePath))
            {
                outfile.Write(strData);
            }
            outfile.Close();
            outfile.Dispose();
        }
        public static void delFile(string strPath)
        {
            if (strPath.Trim().Equals(string.Empty)) return;
            if (File.Exists(strPath))
            {
                try
                {
                    FileInfo fi = new FileInfo(strPath);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(strPath);
                }
                catch (Exception)
                {
                    try
                    {
                        Thread.Sleep(500);
                        File.Delete(strPath);
                    }
                    catch (Exception)
                    {
                        FileStream fs = null;
                        fs = File.Create(strPath);
                        fs.Close();
                        fs.Dispose();
                        File.Delete(strPath);
                    }
                }
            }
        }
        public static string chkDirectory(string strData, string strAppRoot)
        {
            if (!strData.Contains(":"))
            {
                strData = strAppRoot + "\\" + strData;
            }
            if (!Directory.Exists(strData))
            {
                Directory.CreateDirectory(strData);
            }
            return strData;
        }
        public static string setDirectoryFromXML(string strData, XmlNode xmlNode, string strAppRoot)
        {
            return chkDirectory(AutoZXML.getInnerTextByName(strData, xmlNode), strAppRoot);
        }
        public static string getOneBaseSub(DirectoryInfo dirInfo, string strExt)
        {
            string strResult = string.Empty;
            foreach (FileInfo f in dirInfo.GetFiles("*." + strExt))
            {
                strResult = f.FullName;
                break;
            }
            if (strResult.Length > 0) return strResult;
            if (dirInfo.GetDirectories().Length <= 0) return string.Empty;
            foreach (DirectoryInfo dirSub in dirInfo.GetDirectories())
            {
                strResult = getOneBaseSub(dirSub, strExt);
                break;
            }
            return strResult;
        }
    }
}
