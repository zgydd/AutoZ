//////////////////
///Barton Joe
///V1.0.0.1 Modify Main
///         Create new try group to do main program,
///         move rollback system time to final
/// Try to let system time roll back when have exception
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Xml;
using AutoZKernel;

namespace MakeCat
{
    public class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }
        [DllImport("Kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime sysTime);
        [DllImport("Kernel32.dll")]
        public static extern void GetLocalTime(ref SystemTime sysTime);

        private static XmlDocument cfgDoc = new XmlDocument();
        private static void loadConfig(ref string strPkgType, ref string strINFName, ref string strCatFileFlg, ref string strDateFlg, ref string strBitFlg, ref string strLogPath)
        {
            cfgDoc.Load("Config/CfgMakeCat.xml");
            XmlNode xmlNode = cfgDoc.DocumentElement;
            strPkgType = AutoZXML.getInnerTextByName("PackageType", xmlNode);
            strINFName = AutoZXML.getInnerTextByName("INFName", xmlNode);
            strCatFileFlg = AutoZXML.getInnerTextByName("CATFileFlg", xmlNode);
            strDateFlg = AutoZXML.getInnerTextByName("CATDateFlg", xmlNode);
            strBitFlg = AutoZXML.getInnerTextByName("Bit32Flg", xmlNode);
            strLogPath = AutoZXML.getInnerTextByName("LogPath", xmlNode);
        }
        private static int copyPkg(string strFrom, string strTo, string strType)
        {
            DirectoryInfo di = new DirectoryInfo(strFrom);
            int i = 0;
            if (!Directory.Exists(strTo)) Directory.CreateDirectory(strTo);
            foreach (FileInfo fi in di.GetFiles("*" + strType))
            {
                AutoZDirectorysFiles.copyFile(fi.FullName, strTo + "\\" + fi.Name);
                i++;
            }
            return i;
        }
        private static void getDiskRange(DirectoryInfo dirInfo, string strINFName, ref List<string> lstRange)
        {
            foreach (FileInfo f in dirInfo.GetFiles(strINFName))
            {
                lstRange.Add(f.DirectoryName);
                break;
            }
            if (dirInfo.GetDirectories().Length <= 0) return;
            foreach (DirectoryInfo dirSub in dirInfo.GetDirectories())
            {
                getDiskRange(dirSub, strINFName, ref lstRange);
            }
        }
        private static string getCATFileName(string strPath, string strINFName, string strCATFileFlg, string strReleaseDateFlg, ref string strDateFlg)
        {
            string strResult = string.Empty;
            string strFlg = strPath + "\\" + strINFName;
            if (!File.Exists(strFlg)) return string.Empty;
            StreamReader reader = new StreamReader(strFlg);
            while (reader.Peek() != -1)
            {
                if (strResult != null && strResult != string.Empty
                    && strDateFlg != null && strDateFlg != string.Empty) break;
                string strTmp = reader.ReadLine();
                if (strTmp.Contains(strCATFileFlg))
                {
                    strResult = strTmp.Substring(strCATFileFlg.Length);
                }
                if (strTmp.Contains(strReleaseDateFlg))
                {
                    strDateFlg = strTmp.Substring(strReleaseDateFlg.Length, 10);
                }
            }
            reader.Close();

            return strPath + "\\" + strResult;
        }
        private static void signCatFile(string strCatPath, string strSignTool, bool b64Flg)
        {
            XmlNode xmlNode = cfgDoc.DocumentElement;
            string strTool = string.Empty;
            if (b64Flg)
            {
                strTool = AutoZXML.getInnerTextByName("CatBase64", xmlNode);
            }
            else
            {
                strTool = AutoZXML.getInnerTextByName("CatBase32", xmlNode);
            }
            string strCmd = strSignTool + strTool + " \"{0}\"";
            strCmd = string.Format(strCmd, strCatPath);
            AutoZRunner.runCMD(strCmd, true);
            AutoZRunner.closeProcess("cmd.exe");
        }
        private static void createCatFunc(string strDrvRoot, string strAddOnsRoot, string strINFName, string strCatFileFlg, string strDateFlg, string strBitFlg, string strPkgType, string strLogPath)
        {
            DirectoryInfo di = new DirectoryInfo(strDrvRoot);
            AutoZData.writeLog("Clean sub directories under the Cat directory!", strLogPath, "MakeCat");
            foreach (DirectoryInfo subDi in di.GetDirectories())
            {
                Directory.Delete(subDi.FullName, true);
            }
            AutoZData.writeLog("Cat range start", strLogPath, "MakeCat");
            foreach (FileInfo fi in di.GetFiles())
            {
                string strFullName = fi.FullName;
                string strName = fi.Name;
                bool b64bit = false;
                AutoZData.writeLog("Start to sign package::(" + strName + ")!", strLogPath, "MakeCat");
                if (!strName.Contains(strBitFlg)) b64bit = true;
                DateTime dtCreate = fi.CreationTime;
                DateTime dtLW = fi.LastWriteTime;
                DateTime dtLA = fi.LastAccessTime;
                AutoZData.writeLog("Package file's date information saved!", strLogPath, "MakeCat");
                //UnCompress file
                AutoZRunner.unCompress(strAddOnsRoot, strFullName, strDrvRoot);
                AutoZData.writeLog("Package uncompressed!", strLogPath, "MakeCat");
                List<string> lstDiskRange = new List<string>();
                AutoZData.writeLog("Start to get disk range!", strLogPath, "MakeCat");
                foreach (DirectoryInfo diRelease in di.GetDirectories())
                {
                    getDiskRange(diRelease, strINFName, ref lstDiskRange);
                }
                AutoZData.writeLog("Disk range saved!", strLogPath, "MakeCat");
                //Get CatFlieName
                foreach (string strDiskPath in lstDiskRange)
                {
                    AutoZData.writeLog("Disk:(" + strDiskPath + ") start!", strLogPath, "MakeCat");
                    //Create Catfile
                    string strReleaseDate = string.Empty;
                    string strCATFile = getCATFileName(strDiskPath, strINFName, strCatFileFlg, strDateFlg, ref strReleaseDate);
                    if (!File.Exists(strCATFile))
                    {
                        File.Create(strCATFile).Close();
                    }
                    AutoZData.writeLog("Cat file:(" + strCATFile + ") created!", strLogPath, "MakeCat");
                    SystemTime stCatDate = new SystemTime();
                    string[] strRDate;
                    AutoZData.writeLog("Get cat file time informations!", strLogPath, "MakeCat");
                    if (strReleaseDate != null && strReleaseDate != string.Empty)
                    {
                        strRDate = strReleaseDate.Split('/');
                        if (strRDate.Length > 2)
                        {
                            stCatDate.wMonth = (ushort)int.Parse(strRDate[0]);
                            stCatDate.wDay = (ushort)int.Parse(strRDate[1]);
                            stCatDate.wYear = (ushort)int.Parse(strRDate[2]);
                            stCatDate.wHour = 12;
                            stCatDate.wMinute = 0;
                            stCatDate.wSecond = 0;
                            AutoZData.writeLog("Release date saved!(" + strReleaseDate + ")!", strLogPath, "MakeCat");
                        }
                        else
                        {
                            AutoZData.writeLog("Release date's format uncurrent!(" + strReleaseDate + ")!", strLogPath, "MakeCat");
                        }
                    }
                    else
                    {
                        GetLocalTime(ref stCatDate);
                        AutoZData.writeLog("Can't get release date, the cat file's date will setted as one month later by now!", strLogPath, "MakeCat");
                    }
                    //Run signtool
                    AutoZData.writeLog("Start to run signtool!", strLogPath, "MakeCat");
                    signCatFile(strCATFile, strAddOnsRoot + "driversigning\\", b64bit);
                    AutoZData.writeLog("Disk been signed!", strLogPath, "MakeCat");
                    FileInfo fiCatFile = new FileInfo(strCATFile);
                    if (fiCatFile.Length > 0)
                    {
                        fiCatFile.CreationTime = formatToDT(stCatDate);
                        fiCatFile.LastWriteTime = formatToDT(stCatDate);
                        fiCatFile.LastAccessTime = formatToDT(stCatDate);
                        AutoZData.writeLog("Cat file's date imformation resetted as release date -- 12:00!", strLogPath, "MakeCat");
                        AutoZData.writeLog("Disk:(" + strDiskPath + ") end!", strLogPath, "MakeCat");
                    }
                    else
                    {
                        AutoZData.writeLog("Cat file's size is zero!", strLogPath, "MakeCat");
                    }
                }
                //Delete oldFile
                AutoZDirectorysFiles.delFile(strFullName);
                AutoZData.writeLog("Old package in cat directory be deleted!", strLogPath, "MakeCat");
                //Compress file return FileInfo
                AutoZRunner.compress(strPkgType, strAddOnsRoot, strDrvRoot, strFullName);
                AutoZData.writeLog("Package compressed!", strLogPath, "MakeCat");
                FileInfo fiPkg = new FileInfo(strFullName);
                fiPkg.CreationTime = dtCreate;
                fiPkg.LastWriteTime = dtLW;
                fiPkg.LastAccessTime = dtLA;
                AutoZData.writeLog("Package's date information is restored!", strLogPath, "MakeCat");
                //Delete unziped file
                foreach (DirectoryInfo subDi in di.GetDirectories())
                {
                    Directory.Delete(subDi.FullName, true);
                }
                AutoZData.writeLog("Uncompressed directory deleted!", strLogPath, "MakeCat");
                AutoZData.writeLog("Package::(" + strName + ") signed!", strLogPath, "MakeCat");
            }
            AutoZData.writeLog("Cat range finished!", strLogPath, "MakeCat");
        }
        private static SystemTime formatToST(DateTime dt)
        {
            SystemTime st = new SystemTime();
            st.wYear = (ushort)dt.Year;
            st.wMonth = (ushort)dt.Month;
            st.wDay = (ushort)dt.Day;
            st.wDayOfWeek = (ushort)dt.DayOfWeek;
            st.wHour = (ushort)dt.Hour;
            st.wMinute = (ushort)dt.Minute;
            st.wSecond = (ushort)dt.Second;
            st.wMiliseconds = (ushort)dt.Millisecond;
            return st;
        }
        private static DateTime formatToDT(SystemTime st)
        {
            return new DateTime(st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, st.wMiliseconds);
        }

        public static void Main(string[] args)
        {
            if (args.Length < 2) return;
            #region Debug
            /*
            StringBuilder sb = new StringBuilder();
            foreach (string str in args)
            {
                sb.AppendLine(str);
            }
            */
            //if (IntPtr.Size == 8)
            //{
            //    this.strOSType = "X64";
            //}
            //else
            //{
            //    this.strOSType = "X86";
            //}
            //sb.AppendLine(this.strOSType);
            #endregion
            string strDrvRoot = args[0].ToString().Trim();
            string strAddOnsRoot = args[1].ToString().Trim();
            string strCATRoot = strDrvRoot + "CAT\\";
            string strPkgType = string.Empty; //"." + args[2].ToString().Trim();
            string strINFName = string.Empty; //args[3].ToString().Trim();
            string strCatFileFlg = string.Empty; //args[4].ToString().Trim();
            string strDateFlg = string.Empty; //args[5].ToString().Trim();
            string strBitFlg = string.Empty; //args[6].ToString().Trim();
            string strLogPath = string.Empty;
//V1.0.0.1
            SystemTime stKeepNow = new SystemTime();
            bool bTimeIsRollBack = false;
//V1.0.0.1
            try
            {
                loadConfig(ref strPkgType, ref strINFName, ref strCatFileFlg, ref strDateFlg, ref strBitFlg, ref strLogPath);
                AutoZData.writeLog("Init config completed and main program started!", strLogPath, "MakeCat");
                if (!Directory.Exists(strDrvRoot))
                {
                    AutoZData.writeLog("No driver packages directory!", strLogPath, "MakeCat");
                    return;
                }
                if (!Directory.Exists(strAddOnsRoot + "driversigning\\"))
                {
                    AutoZData.writeLog("Can't find sign tool's directory(" + strAddOnsRoot + "driversigning)!", strLogPath, "MakeCat");
                    return;
                }
                if (!Directory.Exists(strAddOnsRoot + "WinRAR\\"))
                {
                    AutoZData.writeLog("Can't find WinRAR directory(" + strAddOnsRoot + "WinRAR)!", strLogPath, "MakeCat");
                    return;
                }
                if (Directory.Exists(strCATRoot))
                {
                    Directory.Delete(strCATRoot, true);
                }
                Directory.CreateDirectory(strCATRoot);
                AutoZData.writeLog("Cat directory(" + strCATRoot + ") created!", strLogPath, "MakeCat");
                AutoZData.writeLog("Copy driver packages to cat directory!", strLogPath, "MakeCat");
                int i = copyPkg(strDrvRoot, strCATRoot, strPkgType);
                if (i <= 0)
                {
                    AutoZData.writeLog("No package copy to cat directory!", strLogPath, "MakeCat");
                    return;
                }
                AutoZData.writeLog("[" + i.ToString() + "] packages copy to cat directory!", strLogPath, "MakeCat");

                SystemTime stNow = new SystemTime();
                SystemTime stRuntimeNow = new SystemTime();
                Stopwatch swMain = new Stopwatch();
                GetLocalTime(ref stNow);
                AutoZData.writeLog("Local time saved!", strLogPath, "MakeCat");
                DateTime dtResetNow = formatToDT(stNow);
                SystemTime stResetNow = new SystemTime();

                DateTime dtRuntimeNow = formatToDT(stNow).AddMonths(1);
                stRuntimeNow = formatToST(dtRuntimeNow);
                AutoZData.writeLog("Try to set local time to one month later!", strLogPath, "MakeCat");
                SetLocalTime(ref stRuntimeNow);
                AutoZData.writeLog("Local time setted!", strLogPath, "MakeCat");
                AutoZData.writeLog("Stop watch will start!!", strLogPath, "MakeCat");
                swMain.Start();
                AutoZData.writeLog("Sign process start!!", strLogPath, "MakeCat");
                createCatFunc(strCATRoot, strAddOnsRoot, strINFName, strCatFileFlg, strDateFlg, strBitFlg, strPkgType, strLogPath);
                AutoZData.writeLog("Sign process finished!!", strLogPath, "MakeCat");
                swMain.Stop();
                AutoZData.writeLog("Stop watch ended!!", strLogPath, "MakeCat");
                dtResetNow.AddMilliseconds(swMain.Elapsed.TotalMilliseconds);
                stResetNow = formatToST(dtResetNow);
                AutoZData.writeLog("Try to roll back local time!", strLogPath, "MakeCat");
                SetLocalTime(ref stResetNow);
                AutoZData.writeLog("Local time restored!", strLogPath, "MakeCat");
//V1.0.0.1
                bTimeIsRollBack = true;
//V1.0.0.1
                #region Debug
                /*
                sb.AppendLine("TotalMilliseconds:  " + swMain.Elapsed.TotalMilliseconds.ToString());
                using (StreamWriter outfile = new StreamWriter("makeCatRuntimeArg.log"))
                {
                    outfile.Write(sb.ToString());
                }
                */
                #endregion
            }
            catch (Exception e)
            {
                AutoZData.writeLog(e.StackTrace, strLogPath, "MakeCat");
            }
//V1.0.0.1
            finally
            {
                if (!bTimeIsRollBack)
                {
                    AutoZData.writeLog("System rollback fail, Try to rollback to keeped time", strLogPath, "MakeCat");
                    SetLocalTime(ref stKeepNow);
                }
            }
//V1.0.0.1
        }
    }
}
