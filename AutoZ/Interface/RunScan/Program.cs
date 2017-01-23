//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using AutoZKernel;

namespace RunScan
{
    public class Program
    {
        private static XmlDocument cfgDoc = new XmlDocument();
        private static string strDrvRoot = string.Empty;
        private static string strAddOnsRoot = string.Empty;
        private static string strToolsRoot = string.Empty;
        private static string strToolName = string.Empty;
        private static string strLogPath = string.Empty;
        private static string strPkgType = string.Empty;
        private static string strTmpFName = string.Empty;
        private static string strTrendType = string.Empty;
        private static void loadConfig()
        {
            cfgDoc.Load("Config/CfgRunScan.xml");
            XmlNode xmlNode = cfgDoc.DocumentElement;
            strToolName = AutoZXML.getInnerTextByName("Au3sName", xmlNode);
            strLogPath = AutoZXML.getInnerTextByName("LogPath", xmlNode);
            strPkgType = AutoZXML.getInnerTextByName("PkgType", xmlNode);
            strTmpFName = AutoZXML.getInnerTextByName("TempBatFileName", xmlNode);
            strTrendType = AutoZXML.getInnerTextByName("TrendType", xmlNode);
        }
        private static void searchPkg(string strCmdHead, DirectoryInfo dirInfo, ref int iPkgCnt, ref StringBuilder sbResult)
        {
            if (!dirInfo.Exists) return;
            string strTp = strPkgType;
            if (strTp.IndexOf('.') < 0)
            {
                strTp = "*." + strPkgType;
            }
            foreach (FileInfo f in dirInfo.GetFiles(strTp))
            {
                string strCmdLine = "\"" + strCmdHead + "\" \"{0}\" \"{1}\"";
                strCmdLine = string.Format(strCmdLine, f.FullName, strTrendType);
                sbResult.AppendLine(strCmdLine);
                iPkgCnt++;
            }
            if (dirInfo.GetDirectories().Length <= 0) return;
            foreach (DirectoryInfo dirSub in dirInfo.GetDirectories())
            {
                searchPkg(strCmdHead, dirSub, ref iPkgCnt, ref sbResult);
            }
        }
        public static void Main(string[] args)
        {
            if (args.Length < 2) return;
            strDrvRoot = args[0].ToString().Trim();
            strAddOnsRoot = args[1].ToString().Trim();
            strToolsRoot = strAddOnsRoot + "GetOfficeScanVersion-scan\\";
            //string strToolName = string.Empty;
            //string strLogPath = string.Empty;
            //string strPkgType = string.Empty;
            //string strTmpFName = string.Empty;
            try
            {
                loadConfig();
                AutoZData.writeLog("Init config completed and main program started!", strLogPath, "RunScan");
                if (strToolName.Trim().Equals(string.Empty))
                {
                    AutoZData.writeLog("Can't get Au3sName, use default[GetOfficeScanVersion-scan.au3]!", strLogPath, "RunScan");
                    strToolName = "GetOfficeScanVersion-scan.au3";
                }
                AutoZData.writeLog("Start to read packages!", strLogPath, "RunScan");
                StringBuilder sbCmd = new StringBuilder();

                DirectoryInfo dirRoot = new DirectoryInfo(strDrvRoot);
                int iPkgCnt = 0;
                searchPkg(strToolsRoot + strToolName, dirRoot, ref iPkgCnt, ref sbCmd);
                if (sbCmd.ToString().Trim().Equals(string.Empty) || iPkgCnt == 0)
                {
                    AutoZData.writeLog("No package information in [" + strDrvRoot + "[!", strLogPath, "RunScan");
                }
                else
                {
                    AutoZData.writeLog("[" + iPkgCnt.ToString() + "] package geted!", strLogPath, "RunScan");
                }
                if (strTmpFName.Trim().Equals(string.Empty))
                {
                    AutoZData.writeLog("Can't get TempBatFileName, use default[tmp.bat]!", strLogPath, "RunScan");
                    strTmpFName = "tmp.bat";
                }
                AutoZData.writeLog("Start to save temp batch file [" + strTmpFName + "]!", strLogPath, "RunScan");
                AutoZDirectorysFiles.saveFile(sbCmd, strAddOnsRoot + strTmpFName);
                AutoZData.writeLog("Start to run scan, scanner's log will saved as script's definition (may)[C:\\log\\OfficeScanVersion.log]!", strLogPath, "RunScan");
                AutoZRunner.runBatWriteLog(strAddOnsRoot, strTmpFName, string.Empty, strLogPath, "RunScan");
                AutoZData.writeLog("Delete temp batch file [" + strTmpFName + "]!", strLogPath, "RunScan");
                AutoZDirectorysFiles.delFile(strAddOnsRoot + strTmpFName);
            }
            catch (Exception ex)
            {
                AutoZData.writeLog(ex.StackTrace, strLogPath, "RunScan");
            }
        }
    }
}
