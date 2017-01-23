//////////////////
///Barton Joe
///V1001:   Modify uploadToShare
///         Move uploadlog control to uploadLogToShare
///For upload log once
///         Remove compress
///To use AutoZKernel function
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using System.Net;
using System.Threading;

using AutoZKernel;

namespace Sender
{
    public class Program
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private static string strDriverRoot = string.Empty;
        private static string strBkDrvRoot = string.Empty;
        private static string strAddOnsRoot = string.Empty;
        private static string strSharePath = string.Empty;
        private static string strLog = string.Empty;
        private static string strCfgFileName = string.Empty;
        private static string strFTPDriversPath = string.Empty;
        private static string strSourceData = string.Empty;
        private static string strInfFileName = string.Empty;
        private static string strPkgType = string.Empty;
        private static string strSrvInfoName = string.Empty;
        private static string strClientDir = string.Empty;
        private static string strLangCfgFlg = string.Empty;
        private static int iClientWait = 0;
        private static int iTimeOut = 0;
        private static string strConfigPath = string.Empty;
        private static string strScriptPath = string.Empty;
        private static string strSrvInfo = string.Empty;
        private static string strLogScript = string.Empty;
        private static bool bHasCatInBasePath = false;

        private static void loadConfig(ref XmlNode nodLang, ref XmlNode nodClient)
        {
            xmlDoc.Load("Config/CfgSender.xml");
            XmlNode xmlNode = xmlDoc.DocumentElement;
            nodLang = AutoZXML.getXmlNodeByName("Lang", xmlNode);
            nodClient = AutoZXML.getXmlNodeByName("Client", xmlNode);
            strSharePath = AutoZXML.getInnerTextByName("SharePath", xmlNode);
            strLog = AutoZDirectorysFiles.setDirectoryFromXML("LogPath", xmlNode, Directory.GetCurrentDirectory());
            strCfgFileName = AutoZXML.getInnerTextByName("ConfigFile", xmlNode);
            strSourceData = AutoZXML.getInnerTextByName("SourceData", xmlNode);
            strInfFileName = AutoZXML.getInnerTextByName("InforFileName", xmlNode);
            strFTPDriversPath = AutoZXML.getInnerTextByName("FTPDriversPath", xmlNode);
            strPkgType = AutoZXML.getInnerTextByName("PkgType", xmlNode);
            strSrvInfoName = AutoZXML.getInnerTextByName("SrvInfoName", xmlNode);
            strClientDir = AutoZXML.getInnerTextByName("ClientDir", xmlNode);
            strLangCfgFlg = AutoZXML.getInnerTextByName("LangTestFlg", xmlNode);
            iClientWait = int.Parse(AutoZXML.getInnerTextByName("ClientWait", xmlNode));
            iTimeOut = int.Parse(AutoZXML.getInnerTextByName("ClientTimeOut", xmlNode));
            strConfigPath = AutoZXML.getInnerTextByName("ConfigPath", xmlNode);
            strScriptPath = AutoZXML.getInnerTextByName("ScriptPath", xmlNode);
            strSrvInfo = AutoZXML.getInnerTextByName("SrvInfo", xmlNode);
            strLogScript = AutoZXML.getInnerTextByName("UpLogScript", xmlNode);
            strBkDrvRoot = AutoZXML.getInnerTextByName("BackDrvRoot", xmlNode);
            bHasCatInBasePath = bool.Parse(AutoZXML.getInnerTextByName("DefaultHasCatFlg", xmlNode));
        }
        private static bool chkDirSub(DirectoryInfo dirSub)
        {
            if (!dirSub.Exists) return false;
            if (dirSub.GetFiles("*.(CAT|cat)").Length > 0) return true;
            if (dirSub.GetDirectories().Length <= 0) return false;
            foreach (DirectoryInfo dir in dirSub.GetDirectories())
            {
                return chkDirSub(dir);
            }
            return false;
        }
        private static bool chkPkg()
        {
            if (strDriverRoot.Equals(strBkDrvRoot)) return true;
            DirectoryInfo dir = new DirectoryInfo(strDriverRoot);
            if (dir.GetFiles("*." + strPkgType).Length > 0)
            {
                FileInfo fi = dir.GetFiles("*." + strPkgType)[0];
                AutoZRunner.unCompress(strAddOnsRoot, fi.FullName, strDriverRoot);
                bool bResult = chkDirSub(dir);
                foreach (DirectoryInfo dirSub in dir.GetDirectories())
                {
                    Directory.Delete(dirSub.FullName, true);
                }
                return bResult;
            }
            else return false;
        }
        private static FtpWebRequest GetRequest(string URL, string username, string password)
        {
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URL);
            result.Credentials = new System.Net.NetworkCredential(username, password);
            result.KeepAlive = false;
            return result;
        }
        private static bool ftpIsExistsFile(string dirName, string strFileName, string ftpHostIP, string username, string password)
        {
            bool flag = true;
            try
            {
                string uri = "ftp://" + ftpHostIP + "/" + dirName + "/" + strFileName;
                FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        private static void uploadFile(FileInfo fileinfo, string hostname, string username, string password)
        {
            //1. check target
            string target;
            if (strClientDir.Trim().Equals(string.Empty)) return;
            target = Guid.NewGuid().ToString();

            int itrCnt = 0;
            while (ftpIsExistsFile(strClientDir, strSourceData, hostname, username, password))
            {
                if (itrCnt > iTimeOut) return;
                itrCnt += iClientWait;
                Thread.Sleep(iClientWait);
            }

            string URL = "FTP://" + hostname + "/" + strClientDir + "/" + target;
            ///WebClient webcl = new WebClient();
            FtpWebRequest ftp = GetRequest(URL, username, password);

            //ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;

            ftp.ContentLength = fileinfo.Length;
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            using (FileStream fs = fileinfo.OpenRead())
            {
                try
                {
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }

                }
                catch (Exception) { }
                finally
                {
                    fs.Close();
                }

            }

            ftp = null;
            ftp = GetRequest(URL, username, password);
            ftp.Method = WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = fileinfo.Name;
            try
            {
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                ftp = GetRequest(URL, username, password);
                ftp.Method = WebRequestMethods.Ftp.DeleteFile; 
                ftp.GetResponse();
                throw ex;
            }
            finally
            {
                fileinfo.Delete();
            }
            ftp = null;
            #region
            /*****
             *FtpWebResponse
             * ****/
            //FtpWebResponse ftpWebResponse = (FtpWebResponse)ftp.GetResponse();
            #endregion
        }
        private static void uploadToShare(FileInfo fileinfo, string strOs)
        {
            string strNewFileName = strOs + "_" + fileinfo.Name;
            AutoZDirectorysFiles.mvFile(fileinfo.FullName, strSharePath + strNewFileName);
        }
//V1001
        private static void uploadLogToSrv()
        {
            if (!strSrvInfo.Contains(".")) strSrvInfo += ".xml";
            if (!strLogScript.Contains(".")) strLogScript += ".au3";
            if (File.Exists(strConfigPath + strSrvInfo))
            {
                XmlDocument xmlDocTmp = new XmlDocument();
                xmlDocTmp.Load(strConfigPath + strSrvInfo);
                XmlNode xmlNodeTmp = xmlDocTmp.DocumentElement;
                string strSrvPath = AutoZXML.getInnerTextByName("Server", xmlNodeTmp);
                string strUser = AutoZXML.getInnerTextByName("UserName", xmlNodeTmp);
                string strPwd = AutoZXML.getInnerTextByName("Password", xmlNodeTmp);
                string strCmd = strScriptPath + strLogScript;
                string[] strsArgs = { strSrvPath, strUser, strPwd };
                AutoZData.writeLog("Run: " + strCmd, strLog, "SrvGetter");
                AutoZRunner.runScriptAu3(strCmd, strsArgs);
                //AutoZRunner.runScriptAu3withLog(strCmd, strsArgs, strLog);
            }
        }
//V1001
        private static void makePackage(string strIdx, string[] strsRecords, XmlNode nodLang, XmlNode nodClient)
        {
            if (strIdx.IndexOf("-") <= 0) return;
            AutoZData.writeLog("Start to make test data!", strLog, "Sender");
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\"))
            {
                Directory.Delete(Directory.GetCurrentDirectory() + "\\Tmp\\", true);
            }
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Tmp\\");
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Tmp\\" + strFTPDriversPath);
            string[] strTmp = strIdx.Split('-');
            int iStart = int.Parse(strTmp[0]);
            int iEnd = int.Parse(strTmp[1]);
            string strClient = strsRecords[iStart].Substring(0, strsRecords[iStart].IndexOf(',')).Trim();
            string strClientIp = AutoZXML.getXmlElementByName(strClient, nodClient).InnerText.Trim();
            StringBuilder sbToClient = new StringBuilder();
            for (int i = iStart; i <= iEnd; i++)
            {
                string[] strsSingles = strsRecords[i].Split(',');
                string[] strsLangs = strsSingles[2].Split(';');
                string strMachine = strsSingles[1].Substring(0, strsSingles[1].IndexOf("_"));
                string strFullPNP = string.Empty;
                if (strsSingles[3] != null && !strsSingles[3].Trim().Equals(string.Empty))
                {
                    strFullPNP = strsSingles[3] + " " + strsSingles[4] + " " + strsSingles[5];
                }
                else
                {
                    strFullPNP = strsSingles[4] + " " + strsSingles[5];
                }
                string strDrvVer = strsSingles[6];
                string strPDKVer = strsSingles[7];
                int iNeedCat = int.Parse(strsSingles[8]);
                foreach (string strLang in strsLangs)
                {
                    string strLangFlg = AutoZXML.getXmlElementByName(strLang, nodLang).GetAttribute("langflg");
                    string strDiskFlg = AutoZXML.getXmlElementByName(strLang, nodLang).GetAttribute("diskflg");
                    sbToClient.AppendLine(strMachine + "," + strLangFlg + "," + strsSingles[3] + "," + strFullPNP 
                        + "," + strsSingles[1] + "," + strClient + "," + strDiskFlg + "," + strDrvVer + "," + strPDKVer);
                }
                string strDriverDir = strDriverRoot;
                if (iNeedCat > 0)
                {
                    if (!bHasCatInBasePath)
                    {
                        strDriverDir = strBkDrvRoot;
                        if (Directory.Exists(strDriverDir + "CAT\\"))
                        {
                            strDriverDir += "CAT\\";
                        }
                    }
                }
                if (File.Exists(strDriverDir + strsSingles[1] + "." + strPkgType))
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\" + strFTPDriversPath + strsSingles[1] + "." + strPkgType))
                        AutoZDirectorysFiles.copyFile(strDriverDir + strsSingles[1] + "." + strPkgType,
                            Directory.GetCurrentDirectory() + "\\Tmp\\" + strFTPDriversPath + strsSingles[1] + "." + strPkgType);
                }
                else
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\" + strFTPDriversPath + strsSingles[1] + "." + strPkgType))
                        AutoZDirectorysFiles.copyFile(strDriverRoot + strsSingles[1] + "." + strPkgType,
                            Directory.GetCurrentDirectory() + "\\Tmp\\" + strFTPDriversPath + strsSingles[1] + "." + strPkgType);
                }
            }
            string strSavPath = Directory.GetCurrentDirectory() + "\\Tmp\\" + strInfFileName;
            AutoZDirectorysFiles.saveFile(sbToClient, strSavPath);
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Config\\" + strSrvInfoName))
            {
                AutoZDirectorysFiles.copyFile(Directory.GetCurrentDirectory() + "\\Config\\" + strSrvInfoName, Directory.GetCurrentDirectory() + "\\Tmp\\" + strSrvInfoName);
            }
            DirectoryInfo dirCfg = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Config\\");
            foreach (FileInfo fi in dirCfg.GetFiles(strLangCfgFlg))
            {
                AutoZDirectorysFiles.copyFile(fi.FullName, Directory.GetCurrentDirectory() + "\\Tmp\\" + fi.Name);
            }
            AutoZData.writeLog("Test data getted!", strLog, "Sender");
//V1001
            AutoZRunner.compress("ZIP", strAddOnsRoot, Directory.GetCurrentDirectory() + "\\Tmp\\*", Directory.GetCurrentDirectory() + "\\Tmp\\" + strSourceData);
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\" + strSourceData))
            {
                FileInfo fiData = new FileInfo(Directory.GetCurrentDirectory() + "\\Tmp\\" + strSourceData);
                string strUserName = AutoZXML.getXmlElementByName(strClient, nodClient).GetAttribute("username");
                string strPassword = AutoZXML.getXmlElementByName(strClient, nodClient).GetAttribute("password");
                if (strSharePath.Trim().Equals(string.Empty) || !Directory.Exists(strSharePath))
                {
                    uploadFile(fiData, strClientIp, strUserName, strPassword);
                }
                else
                {
                    uploadToShare(fiData, strClient);
                }
            }
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\"))
            {
                Directory.Delete(Directory.GetCurrentDirectory() + "\\Tmp\\", true);
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Config\\" + strCfgFileName + ".txt"))
            {
                AutoZDirectorysFiles.delFile(Directory.GetCurrentDirectory() + "\\Config\\" + strCfgFileName + ".txt");
            }
            AutoZData.writeLog("Test data uploaded!", strLog, "Sender");
        }
        public static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length < 2) return;
                strDriverRoot = args[0].ToString().Trim();
                strAddOnsRoot = args[1].ToString().Trim();
                XmlNode nodLang = null;
                XmlNode nodClient = null;
                loadConfig(ref nodLang, ref nodClient);
                if (!Directory.Exists(strDriverRoot))
                {
                    if (!Directory.Exists(strBkDrvRoot)) return;
                    strDriverRoot = strBkDrvRoot;
                    bHasCatInBasePath = chkPkg();
                }
                if (iClientWait < 1000) iClientWait = 1000;
                if (iTimeOut < 10000) iTimeOut = 20000;
                strLog += "Sender.log";
                AutoZData.writeLog("Sender start!", strLog, "Sender");
                string strCfg = Directory.GetCurrentDirectory() + "\\Config\\" + strCfgFileName;
                if (!File.Exists(strCfg)) return;
                string[] strsConfigs = AutoZData.readFileToSB(strCfg, "txt").ToString().Replace("\r", string.Empty).Split('\n');
                AutoZData.writeLog("Configs getted!", strLog, "Sender");
                int iHead = 1;
                List<string> lstOSs = new List<string>();
                int iIdx = 0;
                for (iIdx = 2; iIdx < strsConfigs.Length; iIdx++)
                {
                    if (strsConfigs[iIdx].Trim().Equals(string.Empty)) break;
                    if (strsConfigs[iIdx].Substring(0, 1).Equals(",")) continue;
                    lstOSs.Add(iHead.ToString() + "-" + (iIdx - 1).ToString());
                    iHead = iIdx;
                }
                lstOSs.Add(iHead.ToString() + "-" + (iIdx - 1).ToString());
                foreach (string strOs in lstOSs)
                {
                    makePackage(strOs, strsConfigs, nodLang, nodClient);
                }
//V1001
                if (!strSharePath.Trim().Equals(string.Empty) && Directory.Exists(strSharePath))
                {
                    uploadLogToSrv();
                }
//V1001
            }
            catch (Exception ex)
            {
                AutoZData.writeLog(ex.StackTrace, strLog, "Sender");
            }
            finally
            {
                if (Directory.Exists(Directory.GetCurrentDirectory() + "\\Tmp\\"))
                {
                    Directory.Delete(Directory.GetCurrentDirectory() + "\\Tmp\\", true);
                }
                if (File.Exists(Directory.GetCurrentDirectory() + "\\Config\\" + strCfgFileName + ".txt"))
                {
                    AutoZDirectorysFiles.delFile(Directory.GetCurrentDirectory() + "\\Config\\" + strCfgFileName + ".txt");
                }
            }
        }
    }
    //UnUse
    //private static void compress()
    //{
    //    string strCmd = strAddOnsRoot + "WinRAR\\WinRAR.exe a -ep1 -r \"{0}\" \"{1}\"";
    //    strCmd = string.Format(strCmd, Directory.GetCurrentDirectory() + "\\Tmp\\" + strSourceData, Directory.GetCurrentDirectory() + "\\Tmp\\*");
    //    AutoZRunner.runCMD(strCmd, true);
    //    AutoZRunner.closeProcess("cmd.exe");
    //}
}
