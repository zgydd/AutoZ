//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;

using AutoZKernel;

namespace AutoZClient
{
    public class Program
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private static string strFTPPath = string.Empty;
        private static string strFTPDriversPath = string.Empty;
        private static string strSourceData = string.Empty;
        private static string strStartFlg = string.Empty;
        private static string strRunPath = string.Empty;
        private static string strDriverPath = string.Empty;
        private static string strScriptRoot = string.Empty;
        private static string strLog = string.Empty;
        private static int iTimeTick = 0;
        private static string strPkgCfg = string.Empty;
        private static string strAllTestName = string.Empty;
        private static string strAddOnsRoot = string.Empty;
        private static string strPkgType = string.Empty;
        private static string strTmpBatName = string.Empty;
        private static string strDiskFlg = string.Empty;
        private static string strAdditionalPath = string.Empty;
        private static string strAdditionalPrg = string.Empty;
        private static string strAddBatch = string.Empty;
        private static string strLangCfgDir = string.Empty;
        private static string strLangCfgFlg = string.Empty;
        private static string strSrvInfo = string.Empty;
        private static string strSrvIpScript = string.Empty;
        private static string strLogScript = string.Empty;

        private static void loadConfig()
        {
            xmlDoc.Load("Config/Config.xml");
            XmlNode xmlNode = xmlDoc.DocumentElement;
            strFTPPath = AutoZXML.getInnerTextByName("FTPPath", xmlNode);
            strFTPDriversPath = AutoZXML.getInnerTextByName("FTPDriversPath", xmlNode);
            strRunPath = AutoZDirectorysFiles.chkDirectory(AutoZXML.getInnerTextByName("RunPath", xmlNode), Directory.GetCurrentDirectory());
            strDriverPath = AutoZDirectorysFiles.setDirectoryFromXML("DriverPath", xmlNode, Directory.GetCurrentDirectory());
            strScriptRoot = AutoZDirectorysFiles.setDirectoryFromXML("ScriptRoot", xmlNode, Directory.GetCurrentDirectory());
            strAddOnsRoot = AutoZDirectorysFiles.setDirectoryFromXML("AddOnsRoot", xmlNode, Directory.GetCurrentDirectory());
            strLog = AutoZDirectorysFiles.setDirectoryFromXML("LogPath", xmlNode, Directory.GetCurrentDirectory());
            iTimeTick = int.Parse(AutoZXML.getInnerTextByName("TimeTick", xmlNode));
            strPkgCfg = AutoZXML.getInnerTextByName("InforFileName", xmlNode);
            strAllTestName = AutoZXML.getInnerTextByName("AllTestName", xmlNode);
            strPkgType = AutoZXML.getInnerTextByName("PkgType", xmlNode);
            strTmpBatName = AutoZXML.getInnerTextByName("TempBatchName", xmlNode);
            strDiskFlg = AutoZXML.getInnerTextByName("DiskFlg", xmlNode);
            strAdditionalPath = AutoZDirectorysFiles.setDirectoryFromXML("AdditionalPath", xmlNode, Directory.GetCurrentDirectory());
            strAdditionalPrg = AutoZXML.getInnerTextByName("AdditionalPrograms", xmlNode);
            strAddBatch = AutoZXML.getInnerTextByName("AdditionalBatch", xmlNode);
            strSourceData = AutoZXML.getInnerTextByName("SourceData", xmlNode);
            strStartFlg = AutoZXML.getInnerTextByName("StartFlg", xmlNode);
            strLangCfgDir = AutoZXML.getInnerTextByName("LangConfigDir", xmlNode);
            strLangCfgFlg = AutoZXML.getInnerTextByName("LangTestFlg", xmlNode);
            strSrvInfo = AutoZXML.getInnerTextByName("SrvInfo", xmlNode);
            strSrvIpScript = AutoZXML.getInnerTextByName("SrvIpScript", xmlNode);
            strLogScript = AutoZXML.getInnerTextByName("UpLogScript", xmlNode);
        }
        private static bool getDataFromFTP()
        {
            bool bResult = false;
            if (Directory.Exists(strFTPPath))
            {
                if (File.Exists(strFTPPath + strSourceData) && File.Exists(strFTPPath + strStartFlg))
                {
                    AutoZDirectorysFiles.mvFile(strFTPPath + strSourceData, strRunPath + strSourceData);
                    AutoZRunner.unCompress(strAddOnsRoot, strRunPath + strSourceData, strRunPath);
                    AutoZDirectorysFiles.delFile(strRunPath + strSourceData);
                    bResult = true;
                }
            }
            return bResult;
        }
        private static void getDiskRange(DirectoryInfo dirInfo, ref List<string> lstRange)
        {
            foreach (FileInfo f in dirInfo.GetFiles(strDiskFlg))
            {
                lstRange.Add(f.DirectoryName);
                break;
            }
            if (dirInfo.GetDirectories().Length <= 0) return;
            foreach (DirectoryInfo dirSub in dirInfo.GetDirectories())
            {
                getDiskRange(dirSub, ref lstRange);
            }
        }
        private static void controlRecord(string strRecord, ref StringBuilder sbMain)
        {
            string[] strsOneRecord = strRecord.Replace("\r", string.Empty).Split(',');
            if (strsOneRecord.Length < 9) return;
            if (strsOneRecord[0].Trim().Equals(string.Empty)) return;
            List<string> lstDisks = new List<string>();
            DirectoryInfo dirRoot = new DirectoryInfo(strDriverPath);
            getDiskRange(dirRoot, ref lstDisks);
            string strCmd = strScriptRoot + strAllTestName + " \"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\"";
            foreach (string strDsk in lstDisks)
            {
                if (strDsk.Contains(strsOneRecord[6].Trim()))
                {
                    string[] strsArgs = new string[9];
                    strsArgs[0] = strsOneRecord[1];
                    strsArgs[1] = strsOneRecord[2];
                    strsArgs[2] = strsOneRecord[3];
                    strsArgs[3] = strDsk;
                    strsArgs[4] = strsOneRecord[4];
                    strsArgs[5] = strsOneRecord[5];
                    strsArgs[6] = strsOneRecord[7];
                    strsArgs[7] = strsOneRecord[8];
                    if (strAllTestName.Contains(".au3"))
                    {
                        sbMain.AppendLine(string.Format(strCmd, strsArgs));
                    }
                    else
                    {
                        AutoZData.writeLog("Start test [" + strsArgs[0] + "]::[" + strsArgs[1] + "] Command:[" + string.Format(strCmd, strsArgs) + "]!!", strLog, "AutoZClient");
                        AutoZRunner.runCMD(string.Format(strCmd, strsArgs), true);
                        AutoZRunner.closeProcess("cmd.exe");
                    }
                }
            }
        }
        private static void runAdditional()
        {
            if (strAddBatch.IndexOf('.') <= 0)
            {
                strAddBatch += ".bat";
            }
            string[] strPrgs = strAdditionalPrg.Split(';');
            if (strPrgs.Length <= 0 || strPrgs[0] == null || strPrgs[0].Trim().Equals(string.Empty))
            {
                AutoZData.writeLog("No additional programs!!", strLog, "AutoZClient");
                return;
            }
            StringBuilder sbBat = new StringBuilder();
            string strSavePath = strAdditionalPath + strAddBatch;
            foreach (string str in strPrgs)
            {
                sbBat.AppendLine(str);
                AutoZData.writeLog("Additional program:[" + str + "]!!", strLog, "AutoZClient");
            }
            AutoZDirectorysFiles.saveFile(sbBat, strSavePath);
            AutoZRunner.runBat(strAdditionalPath, strAddBatch, string.Empty);
            AutoZDirectorysFiles.delFile(strSavePath);
        }
        private static void startUpload(bool bIsIP)
        {
            if (!strSrvInfo.Contains(".")) strSrvInfo += ".xml";
            if (!strSrvIpScript.Contains(".")) strSrvIpScript += ".au3";
            if (!strLogScript.Contains(".")) strLogScript += ".au3";
            if (File.Exists(strRunPath + strSrvInfo))
            {
                XmlDocument xmlDocTmp = new XmlDocument();
                xmlDocTmp.Load(strRunPath + strSrvInfo);
                XmlNode xmlNodeTmp = xmlDocTmp.DocumentElement;
                string strSrvPath = AutoZXML.getInnerTextByName("Server", xmlNodeTmp);
                string strUser = AutoZXML.getInnerTextByName("UserName", xmlNodeTmp);
                string strPwd = AutoZXML.getInnerTextByName("Password", xmlNodeTmp);
                string strCmd = string.Empty;
                if (bIsIP && File.Exists(strRunPath + strSrvIpScript))
                {
                    strCmd = strRunPath + strSrvIpScript;
                }
                else if (!bIsIP && File.Exists(strRunPath + strLogScript))
                {
                    strCmd = strRunPath + strLogScript;
                }
                else return;
                string[] strsArgs = { strSrvPath, strUser, strPwd };
                AutoZData.writeLog("Run: " + strCmd, strLog, "AutoZClient");
                AutoZRunner.runScriptAu3(strCmd, strsArgs);
                //AutoZRunner.runScriptAu3withLog(strCmd, strsArgs, strLog);
            }
        }
        private static void mainProc()
        {
            //writeLog("Process start!!", strLog);
            if (strPkgCfg.IndexOf('.') <= 0)
            {
                strPkgCfg += ".csv";
            }
            if (strAllTestName.IndexOf('.') <= 0)
            {
                strAllTestName += ".exe";
            }
            if (strTmpBatName.IndexOf('.') <= 0)
            {
                strTmpBatName += ".bat";
            }
            if (strSourceData.IndexOf('.') <= 0)
            {
                strSourceData += ".zip";
            }
            //writeLog("Check data from FTP Root!!", strLog);
            if (!getDataFromFTP())
            {
                //writeLog("No data!!", strLog);
                return;
            }
            string strCfgFull = strRunPath + strPkgCfg;
            if (!File.Exists(strCfgFull))
            {
                //writeLog("No config data!!", strLog);
                return;
            }
            if (!Directory.Exists(strRunPath + strFTPDriversPath))
            {
                //writeLog("No driver package!!", strLog);
                return;
            }
            AutoZData.writeLog("Process start!!", strLog, "AutoZClient");
            DirectoryInfo dirDriverFrom = new DirectoryInfo(strRunPath + strFTPDriversPath);
            foreach (FileInfo fi in dirDriverFrom.GetFiles("*." + strPkgType))
            {
                AutoZDirectorysFiles.copyFile(strRunPath + strFTPDriversPath + fi.Name, strDriverPath + fi.Name);
            }
            Directory.Delete(strRunPath + strFTPDriversPath, true);
            DirectoryInfo dirRunPath = new DirectoryInfo(strRunPath);
            if (!Directory.Exists(strScriptRoot + strLangCfgDir))
            {
                Directory.CreateDirectory(strScriptRoot + strLangCfgDir);
            }
            foreach (FileInfo fi in dirRunPath.GetFiles(strLangCfgFlg))
            {
                AutoZDirectorysFiles.mvFile(fi.FullName, strScriptRoot + strLangCfgDir + fi.Name);
            }

            AutoZData.writeLog("Start to get config!!", strLog, "AutoZClient");
            StringBuilder sbData = AutoZData.readFileToSB(strCfgFull, "txt");
            if (sbData.ToString().Trim().Equals(string.Empty))
            {
                AutoZData.writeLog("Fail to get config!!", strLog, "AutoZClient");
                return;
            }
            string[] strsRecords = sbData.ToString().Split('\n');
            AutoZData.writeLog("Config data geted!!", strLog, "AutoZClient");

            DirectoryInfo dirDriver = new DirectoryInfo(strDriverPath);
            foreach (DirectoryInfo dirSub in dirDriver.GetDirectories())
            {
                Directory.Delete(dirSub.FullName, true);
            }
            foreach (FileInfo fi in dirDriver.GetFiles("*." + strPkgType))
            {
                StringBuilder sbMainBatch = new StringBuilder();
                AutoZData.writeLog("UnCompress [" + fi.Name + "]!!", strLog, "AutoZClient");
                AutoZRunner.unCompress(strAddOnsRoot, fi.FullName, strDriverPath);
                foreach (string strRecord in strsRecords)
                {
                    if (strRecord.Contains(fi.Name.Substring(0, fi.Name.LastIndexOf('.'))))
                    {
                        controlRecord(strRecord, ref sbMainBatch);
                    }
                }
                if (strAllTestName.Contains(".au3"))
                {
                    AutoZDirectorysFiles.saveFile(sbMainBatch, strRunPath + strTmpBatName);
                    AutoZData.writeLog("Save and run config batch for [" + fi.Name + "]!!", strLog, "AutoZClient");
                    AutoZRunner.runBat(strRunPath, strTmpBatName, string.Empty);
                    AutoZDirectorysFiles.delFile(strRunPath + strTmpBatName);
                }
                AutoZData.writeLog("Do some clean!!", strLog, "AutoZClient");
                foreach (DirectoryInfo dirSub in dirDriver.GetDirectories())
                {
                    Directory.Delete(dirSub.FullName, true);
                }
            }
            Directory.Delete(strDriverPath, true);
            Directory.CreateDirectory(strDriverPath);
            AutoZDirectorysFiles.delFile(strCfgFull);
            AutoZData.writeLog("Run additional if has!!", strLog, "AutoZClient");
            runAdditional();
            AutoZData.writeLog("Upload logs!!", strLog, "AutoZClient");
            startUpload(false);
            AutoZData.writeLog("Process finished!!", strLog, "AutoZClient");
            try
            {
                Directory.Delete(strDriverPath, true);
                AutoZDirectorysFiles.delFile(strRunPath + strPkgCfg);
                AutoZDirectorysFiles.delFile(strFTPPath + strStartFlg);
                AutoZData.writeLog("AutoZ Client Finished!!", strLog, "AutoZClient");
            }
            catch { }
        }
        public static void Main(string[] args)
        {
            try
            {
                loadConfig();
                strLog += "AutoZClient.log";
                if (iTimeTick < 1000)
                {
                    iTimeTick = 60000;
                }
                AutoZController.PreventSleep(false);
                AutoZData.writeLog("AutoZ Client range start!!", strLog, "AutoZClient");
                startUpload(true);
                while (true)
                {
                    mainProc();
                    Thread.Sleep(iTimeTick);
                }
            }
            catch (Exception ex)
            {
                AutoZData.writeLog(ex.StackTrace, strLog, "AutoZClient");
            }
            finally
            {
                try
                {
                    Directory.Delete(strDriverPath, true);
                    AutoZDirectorysFiles.delFile(strRunPath + strPkgCfg);
                    AutoZDirectorysFiles.delFile(strFTPPath + strStartFlg);
                    AutoZData.writeLog("AutoZ Client Finished!!", strLog, "AutoZClient");
                }
                catch { }
            }
        }
    }
}
