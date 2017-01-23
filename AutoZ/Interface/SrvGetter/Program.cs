//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using AutoZKernel;
using System.IO;
using System.Threading;

namespace SrvGetter
{
    public class Program
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private static string strSharePath = string.Empty;
        private static string strStartFlg = string.Empty;
        private static string strLog = string.Empty;
        private static int iTimeTick = 0;
        private static string strLangCfgFlg = string.Empty;
        private static string[] strsNeedFiles = null;
        private static string strToConfig = string.Empty;
        private static string strToScript = string.Empty;
        private static string strConfigPath = string.Empty;
        private static string strScriptPath = string.Empty;
        private static string strSrvInfo = string.Empty;
        private static string strSrvIpScript = string.Empty;
        private static string strLogScript = string.Empty;

        private static void loadConfig()
        {
            xmlDoc.Load("Config/CfgGetter.xml");
            XmlNode xmlNode = xmlDoc.DocumentElement;
            strSharePath = AutoZXML.getInnerTextByName("SharePath", xmlNode);
            strStartFlg = AutoZXML.getInnerTextByName("StartFlg", xmlNode);
            strLog = AutoZDirectorysFiles.setDirectoryFromXML("LogPath", xmlNode, Directory.GetCurrentDirectory());
            iTimeTick = int.Parse(AutoZXML.getInnerTextByName("TimeTick", xmlNode));
            strLangCfgFlg = AutoZXML.getInnerTextByName("LangTestFlg", xmlNode);
            strsNeedFiles = AutoZXML.getInnerTextByName("NeedFiles", xmlNode).Split(';');
            strToConfig = AutoZXML.getInnerTextByName("ToConfig", xmlNode);
            strToScript = AutoZXML.getInnerTextByName("ToScript", xmlNode);
            strConfigPath = AutoZXML.getInnerTextByName("ConfigPath", xmlNode);
            strScriptPath = AutoZXML.getInnerTextByName("ScriptPath", xmlNode);
            strSrvInfo = AutoZXML.getInnerTextByName("SrvInfo", xmlNode);
            strSrvIpScript = AutoZXML.getInnerTextByName("SrvIpScript", xmlNode);
            strLogScript = AutoZXML.getInnerTextByName("UpLogScript", xmlNode);
        }
        private static bool chkFiles()
        {
            bool bResult = true;
            foreach (string str in strsNeedFiles)
            {
                if (!File.Exists(strSharePath + str))
                {
                    bResult = false;
                    break;
                }
            }
            if (!bResult)
            {
                DirectoryInfo dir = new DirectoryInfo(strSharePath);
                foreach (FileInfo fi in dir.GetFiles())
                {
                    AutoZDirectorysFiles.delFile(fi.FullName);
                }
                foreach (DirectoryInfo dirSub in dir.GetDirectories())
                {
                    Directory.Delete(dirSub.FullName, true);
                }
            }
            return bResult;
        }
        private static void mvFiles(string strFiles, string strToPath)
        {
            DirectoryInfo dir = new DirectoryInfo(strSharePath);
            if (Directory.Exists(strToPath))
            {
                if (strFiles.Contains(";"))
                {
                    string[] strsFiles = strFiles.Split(';');
                    foreach (string strFile in strsFiles)
                    {
                        if (strFile.Contains("*"))
                        {
                            foreach (FileInfo fi in dir.GetFiles(strFile))
                            {
                                AutoZDirectorysFiles.mvFile(fi.FullName, strToPath + fi.Name);
                            }
                        }
                        else
                        {
                            AutoZDirectorysFiles.mvFile(strSharePath + strFile, strToPath + strFile);
                        }
                    }
                }
                else
                {
                    if (strFiles.Contains("*"))
                    {
                        foreach (FileInfo fi in dir.GetFiles(strFiles))
                        {
                            AutoZDirectorysFiles.mvFile(fi.FullName, strToPath + fi.Name);
                        }
                    }
                    else
                    {
                        AutoZDirectorysFiles.mvFile(strSharePath + strFiles, strToPath + strFiles);
                    }
                }
            }
        }
        private static void startUpload(bool bIsIP)
        {
            if (!strSrvInfo.Contains(".")) strSrvInfo += ".xml";
            if (!strSrvIpScript.Contains(".")) strSrvIpScript += ".au3";
            if (!strLogScript.Contains(".")) strLogScript += ".au3";
            if (File.Exists(strConfigPath + strSrvInfo))
            {
                XmlDocument xmlDocTmp = new XmlDocument();
                xmlDocTmp.Load(strConfigPath + strSrvInfo);
                XmlNode xmlNodeTmp = xmlDocTmp.DocumentElement;
                string strSrvPath = AutoZXML.getInnerTextByName("Server", xmlNodeTmp);
                string strUser = AutoZXML.getInnerTextByName("UserName", xmlNodeTmp);
                string strPwd = AutoZXML.getInnerTextByName("Password", xmlNodeTmp);
                string strCmd = string.Empty;
                if (bIsIP && File.Exists(strScriptPath + strSrvIpScript))
                {
                    strCmd = strScriptPath + strSrvIpScript;
                }
                else if (!bIsIP && File.Exists(strScriptPath + strLogScript))
                {
                    strCmd = strScriptPath + strLogScript;
                }
                else return;
                string[] strsArgs = { strSrvPath, strUser, strPwd };
                AutoZData.writeLog("Run: " + strCmd, strLog, "SrvGetter");
                AutoZRunner.runScriptAu3(strCmd, strsArgs);
                //AutoZRunner.runScriptAu3withLog(strCmd, strsArgs, strLog);
            }
        }
        private static bool mainProc()
        {
            if (!chkFiles())
            {
                AutoZData.writeLog("Some important file losted!", strLog, "SrvGetter");
                return false;
            }
            AutoZData.writeLog("Move files!", strLog, "SrvGetter");
            mvFiles(strToConfig, strConfigPath);
            mvFiles(strToScript, strScriptPath);
            AutoZData.writeLog("Move files completed!", strLog, "SrvGetter");
            return true;
        }

        public static void Main(string[] args)
        {
            try
            {
                loadConfig();
                strLog += "SrvGetter.log";
                if (iTimeTick < 1000)
                {
                    iTimeTick = 60000;
                }
                AutoZController.PreventSleep(false);
                AutoZData.writeLog("SrvGetter range start!!", strLog, "SrvGetter");
                startUpload(true);
                AutoZData.writeLog(strSrvIpScript + " is running!", strLog, "SrvGetter");
                while (true)
                {
                    if (!Directory.Exists(strSharePath)) continue;
                    if (!File.Exists(strSharePath + strStartFlg)) continue;
                    if (mainProc()) break;
                    Thread.Sleep(iTimeTick);
                }
            }
            catch (Exception ex)
            {
                AutoZData.writeLog(ex.StackTrace, strLog, "SrvGetter");
            }
            finally
            {
                try
                {
                    AutoZDirectorysFiles.delFile(strSharePath + strStartFlg);
                    AutoZData.writeLog("SrvGetter Finished!!", strLog, "SrvGetter");
                }
                catch { }
            }
        }
    }
}
