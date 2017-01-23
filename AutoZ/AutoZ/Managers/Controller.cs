//////////////////
///Barton Joe
///V1.01:	Modify runAdditional 
///		  	Add strAllTestCfgFlg
/// For Cat package check
///V1.02:   Add some defination
/// For save nmake log for upload
///V1.03:   Add dicNickNameSort etc
/// For setup special package name by nicknames automatically
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

using AutoZ.Bean;
using AutoZ.Interface;
using AutoZKernel;

namespace AutoZ
{
    public class Controller
    {
        #region Private definition
        private XmlDocument xmlDoc = new XmlDocument();
        private List<Lang> lstLang = new List<Lang>();
        private List<OEM> lstOEM = new List<OEM>();
        private bool bFromGetter = false;
        private int strSpecPkgNo = -1;
        private string[] strsGetterRange = null;
        private string[] strsGetterArgsRange = null;
        private string strCommandPath = string.Empty;
        private string strCommandBat = string.Empty;
        private string strSDFCmd = string.Empty;
        private string strSDFRoot = string.Empty;
        private string strMakeMark = string.Empty;
        private string strExcpMark = string.Empty;
        private string strMultiDirRoot = string.Empty;
        private string strSplitLine = string.Empty;
        private string strAutoZMark = string.Empty;
        private int iMaxPackage = -1;
        private string strLangMark = string.Empty;
        private string strOEMMark = string.Empty;
        private string strOS = string.Empty;
        private string strOSMark = string.Empty;
        private string strSpPkgNameMark = string.Empty;
        private string strSpPkgNameSplitMark = string.Empty;
        private string strInstallRoot = string.Empty;
        private string strCleanArg = string.Empty;
        private string strDriverRoot = string.Empty;
        private string strCleanBat = string.Empty;
        private string strLittleCleanName = string.Empty;
        private string strLittleCleanExcpMark = string.Empty;
        private string strCSVFile = string.Empty;
        private string strCSVStartFlg = string.Empty;
        private string strCSVEndFlg = string.Empty;
        private string strTmpCsvNameFlg = string.Empty;
        private string strReadableFileType = string.Empty;
        private int iCSVCnt = -1;
        private int iNeedUpD = -1;
        private bool bUpdCatDrv = true;

        private string strAddOnsRoot = string.Empty;
        private string strX86Flg = string.Empty;

        protected XmlNode xmlNode;
        protected string strInterfaceRoot = string.Empty;
        protected string strRuntimeInterfaceName = string.Empty;
        protected string strLogRoot = string.Empty;
        private string[] strsInterfaceRange = null;
        private string[] strsInterfaceArgsRange = null;

        private string strUpDownBat = string.Empty;
        private string strUpDownCmd = string.Empty;

        private string[] strsAdditionalRange = null;
        private string[] strsAdditionalArgsRange = null;

        private List<ProjectRecord> lstProjectRecords = new List<ProjectRecord>();
        private int iCursor = -1;

        private string strAllTestCfgFlg = string.Empty;
//V1.02
        private string strScanLogRoot = string.Empty;
        private string strUpLogName = string.Empty;
//V1.02
//V1.03
        private Dictionary<int, string> dicNickNameSort = new Dictionary<int, string>();
//V1.03
        #endregion
        #region Properties
        public Controller()
        {
            this.xmlDoc.Load("Config/Config.xml");
            this.xmlNode = this.xmlDoc.DocumentElement;
        }
        public List<Lang> getLang()
        {
            return this.lstLang;
        }
        public List<OEM> getOEM()
        {
            return this.lstOEM;
        }
        public bool isFromGetter()
        {
            return this.bFromGetter;
        }
        public int getSpecPkgNo()
        {
            return this.strSpecPkgNo;
        }
        public string getMakeMark()
        {
            return this.strMakeMark;
        }
        public string getExcpMark()
        {
            return this.strExcpMark;
        }
        public string getMultiRoot()
        {
            return this.strMultiDirRoot;
        }
        public string getSplitLine()
        {
            return this.strSplitLine;
        }
        public string getAutoZMark()
        {
            return this.strAutoZMark;
        }
        public int getMaxPackage()
        {
            return this.iMaxPackage;
        }
        public string getCommandPath()
        {
            return this.strCommandPath;
        }
        public string getCommandBat()
        {
            return this.strCommandBat;
        }
        public string getSDFCmd()
        {
            return this.strSDFCmd;
        }
        public string getSDFRoot()
        {
            return this.strSDFRoot;
        }
        public string getLangMark()
        {
            return this.strLangMark;
        }
        public string getOEMMark()
        {
            return this.strOEMMark;
        }
        public string getOS()
        {
            return this.strOS;
        }
        public string getOSMark()
        {
            return this.strOSMark;
        }
        public string getSpPkgNameMark()
        {
            return this.strSpPkgNameMark;
        }
        public string getSpPkgNameSplitMark()
        {
            return this.strSpPkgNameSplitMark;
        }
        public string getInstallRoot()
        {
            return this.strInstallRoot;
        }
        public string getCleanArg()
        {
            return this.strCleanArg;
        }
        public string getDriverRoot()
        {
            return this.strDriverRoot;
        }
        public string getReadableFileType()
        {
            return this.strReadableFileType;
        }
        public string getLogRoot()
        {
            return this.strLogRoot;
        }
        public void setCursor(int iCur)
        {
            this.iCursor = iCur;
        }
        public ProjectRecord getCurrentRecord()
        {
            return this.lstProjectRecords[iCursor];
        }
        public List<ProjectRecord> getLstProjectRecords()
        {
            return this.lstProjectRecords;
        }
        public bool needUpD()
        {
            if (this.iNeedUpD > 0) return true;
            return false;
        }
//V1.02
        public string getScanLogRoot()
        {
            return this.strScanLogRoot;
        }
        public string getUpLogName()
        {
            return this.strUpLogName;
        }
//V1.02
//V1.03
        public Dictionary<int, string> getNickNameSort()
        {
            return this.dicNickNameSort;
        }
//V1.03
        #endregion
        #region Private Prtoected Function
        protected void LoadBase(string strAppRoot)
        {
            this.bFromGetter = bool.Parse(AutoZXML.getInnerTextByName("FromGetter", this.xmlNode));
            this.strsGetterRange = AutoZXML.getInnerTextByName("GetterRange", this.xmlNode).Split(';');
            this.strsGetterArgsRange = AutoZXML.getInnerTextByName("GetterArgsRange", this.xmlNode).Split(';');
            this.strSpecPkgNo = int.Parse(AutoZXML.getInnerTextByName("SpecialPkgNo", this.xmlNode));
            this.strMakeMark = AutoZXML.getInnerTextByName("MakeMark", this.xmlNode);
            this.strExcpMark = AutoZXML.getInnerTextByName("ExcpMark", this.xmlNode);
            this.strMultiDirRoot = AutoZDirectorysFiles.setDirectoryFromXML("MultiRootDir", this.xmlNode, strAppRoot);
            this.strSplitLine = AutoZXML.getInnerTextByName("LogSplitLine", this.xmlNode);
            this.strAutoZMark = AutoZXML.getInnerTextByName("AutoZsMark", this.xmlNode);
            this.iMaxPackage = int.Parse(AutoZXML.getInnerTextByName("MaxPkgNo", this.xmlNode));
            this.strCommandPath = AutoZDirectorysFiles.setDirectoryFromXML("CommandPath", this.xmlNode, strAppRoot);
            this.strCommandBat = AutoZXML.getInnerTextByName("RunSDFBat", this.xmlNode);
            this.strSDFCmd = AutoZXML.getInnerTextByName("SDFCommand", this.xmlNode);
            this.strSDFRoot = AutoZDirectorysFiles.setDirectoryFromXML("SDFRoot", this.xmlNode, strAppRoot);
            this.strLangMark = AutoZXML.getInnerTextByName("LangMark", this.xmlNode);
            this.strOEMMark = AutoZXML.getInnerTextByName("OEMMark", this.xmlNode);
            this.strOS = AutoZXML.getInnerTextByName("OS", this.xmlNode);
            this.strOSMark = AutoZXML.getInnerTextByName("OSMark", this.xmlNode);
            this.strSpPkgNameMark = AutoZXML.getInnerTextByName("SpecialPkgNameMark", this.xmlNode);
            this.strSpPkgNameSplitMark = AutoZXML.getInnerTextByName("SpecialPkgNameSplitMark", this.xmlNode);
            this.strInstallRoot = AutoZXML.getInnerTextByName("InstallRoot", this.xmlNode);
            this.strCleanArg = AutoZXML.getInnerTextByName("CleanArg", this.xmlNode);
            this.strDriverRoot = AutoZDirectorysFiles.setDirectoryFromXML("DriverRoot", this.xmlNode, strAppRoot);
            this.strCleanBat = AutoZXML.getInnerTextByName("CleanBat", this.xmlNode);
            this.strLittleCleanName = AutoZXML.getInnerTextByName("LittleCleanName", this.xmlNode);
            this.strLittleCleanExcpMark = AutoZXML.getInnerTextByName("LittleCleanExcpMark", this.xmlNode);
            this.strCSVFile = AutoZXML.getInnerTextByName("PnPCSVFile", this.xmlNode);
            this.strCSVStartFlg = AutoZXML.getInnerTextByName("PnPCSVStartingFlg", this.xmlNode);
            this.strCSVEndFlg = AutoZXML.getInnerTextByName("PnPCSVEndingFlg", this.xmlNode);
            this.strTmpCsvNameFlg = AutoZXML.getInnerTextByName("TmpCsvFlg", this.xmlNode);
            this.strReadableFileType = AutoZXML.getInnerTextByName("ReadableFileType", this.xmlNode);
            this.iCSVCnt = int.Parse(AutoZXML.getInnerTextByName("CSVFileCount", this.xmlNode));
            this.strAddOnsRoot = AutoZDirectorysFiles.setDirectoryFromXML("AddOnsRoot", this.xmlNode, strAppRoot);
            this.strInterfaceRoot = AutoZDirectorysFiles.setDirectoryFromXML("InterfaceRoot", this.xmlNode, strAppRoot);
            this.strLogRoot = AutoZDirectorysFiles.setDirectoryFromXML("LogRoot", this.xmlNode, strAppRoot);
            this.strsInterfaceRange = AutoZXML.getInnerTextByName("InterfacesRange", this.xmlNode).Split(';');
            this.strsInterfaceArgsRange = AutoZXML.getInnerTextByName("InterfacesArgsRange", this.xmlNode).Split(';');
            this.strRuntimeInterfaceName = AutoZXML.getInnerTextByName("RuntimeInterfaceName", this.xmlNode);
            this.strUpDownBat = AutoZXML.getInnerTextByName("UpDownBat", this.xmlNode);
            this.strUpDownCmd = AutoZXML.getInnerTextByName("UpDownCmd", this.xmlNode);
            this.strX86Flg = AutoZXML.getInnerTextByName("X86Flg", this.xmlNode);
            this.strsAdditionalRange = AutoZXML.getInnerTextByName("AdditionalRange", this.xmlNode).Split(';');
            this.strsAdditionalArgsRange = AutoZXML.getInnerTextByName("AdditionalArgsRange", this.xmlNode).Split(';');
            this.iNeedUpD = int.Parse(AutoZXML.getInnerTextByName("neddUpAndDown", this.xmlNode));
            this.strAllTestCfgFlg = AutoZXML.getInnerTextByName("AllTestConfigCatFlg", this.xmlNode);
//V1.02
            this.strScanLogRoot = AutoZDirectorysFiles.setDirectoryFromXML("ScanLogRoot", this.xmlNode, strAppRoot);
            this.strUpLogName = AutoZXML.getInnerTextByName("UpLogName", this.xmlNode);
//V1.02
//V1.03
            XmlNode nickNameSort = AutoZXML.getXmlNodeByName("SpPkgNameSort", this.xmlNode);
            if (nickNameSort.HasChildNodes)
            {
                foreach (XmlElement ele in nickNameSort.ChildNodes)
                {
                    this.dicNickNameSort.Add(int.Parse(ele.InnerText), ele.Name);
                }
            }
//V1.03
        }
        private void resetOEMCSV(string strRoot, string strSplitFlg, int iRank)
        {
            if (!File.Exists(strRoot + strSplitFlg)) return;
            string strFormatFlg = string.Empty;
            if (iRank > 0)
            {
                strFormatFlg = iRank.ToString();
            }
            string strCSVPath = strRoot + string.Format(this.strCSVFile, strFormatFlg);
            string strTmpCSVPath = strRoot + string.Format(this.strCSVFile, strFormatFlg) + this.strTmpCsvNameFlg;
            StringBuilder sb = new StringBuilder();

            string strRealSplitFlg = Regex.Replace(strSplitFlg, @"\d", string.Empty);
            strRealSplitFlg = strRealSplitFlg.Replace(string.Format(this.strAutoZMark, string.Empty), string.Empty);
            if (File.Exists(strCSVPath))
            {
                sb = AutoZData.readFileToSB(strCSVPath, this.strReadableFileType);
            }
            string[] strSB = sb.ToString().Split('\n');
            StringBuilder sbNew = new StringBuilder();
            int iHeadIdx = 0;
            int iStartIdx = 0;
            int iEndIdx = 0;
            for (int i = 0; i < strSB.Length; i++)
            {
                if (strSB[i].ToUpper().Contains(this.strCSVStartFlg.ToUpper()))
                {
                    iHeadIdx = i;
                    break;
                }
                sbNew.AppendLine(strSB[i].Replace("\r", string.Empty));
            }
            for (int i = iHeadIdx; i < strSB.Length; i++)
            {
                if (strSB[i].ToUpper().Contains(strRealSplitFlg.ToUpper()))
                {
                    iStartIdx = i;
                    break;
                }
            }
            for (int i = iStartIdx; i < strSB.Length; i++)
            {
                if (strSB[i].ToUpper().Contains(this.strCSVEndFlg.ToUpper()))
                {
                    iEndIdx = i;
                    break;
                }
            }
            if (iEndIdx <= 0)
            {
                iEndIdx = strSB.Length - 1;
            }
            if (iHeadIdx <= 0 || iStartIdx <= 0 || iEndIdx <= 0
                || iHeadIdx > iStartIdx || iHeadIdx > iEndIdx || iStartIdx >= iEndIdx) return;
            for (int i = iStartIdx + 1; i < iEndIdx; i++)
            {
                sbNew.AppendLine(strSB[i].Replace("#", string.Empty).Replace("\r", string.Empty));
            }
            AutoZDirectorysFiles.copyFile(strCSVPath, strTmpCSVPath);
            AutoZDirectorysFiles.saveFile(sbNew, strCSVPath);
        }
        private void rollBackCSV(string strRoot, int iRank)
        {
            string strFormatFlg = string.Empty;
            if (iRank > 0)
            {
                strFormatFlg = iRank.ToString();
            }
            string strCSVPath = strRoot + string.Format(this.strCSVFile, strFormatFlg);
            string strTmpCSVPath = strRoot + string.Format(this.strCSVFile, strFormatFlg) + this.strTmpCsvNameFlg;
            if (!File.Exists(strTmpCSVPath)) return;
            AutoZDirectorysFiles.delFile(strCSVPath);
            AutoZDirectorysFiles.copyFile(strTmpCSVPath, strCSVPath);
            AutoZDirectorysFiles.delFile(strTmpCSVPath);
        }
        #endregion
        #region Public Function
        public bool Load(string strAppRoot)
        {
            if (this.xmlNode.HasChildNodes)
            {
                this.LoadBase(strAppRoot);
                XmlNode supLang = AutoZXML.getXmlNodeByName("SupportLang",this.xmlNode);
                foreach (XmlElement ele in supLang.ChildNodes)
                {
                    Lang lg = new Lang();
                    lg.LangFlg = ele.Name;
                    lg.LangName = ele.InnerText;
                    lg.bChecked = Boolean.Parse(ele.Attributes["checked"].Value);
                    lg.iPakNo = int.Parse(ele.Attributes["pakNo"].Value);
//V1.03
                    lg.LangNickName = ele.Attributes["nickName"].Value;
//V1.03
                    this.lstLang.Add(lg);
                }
                XmlNode supOEM = AutoZXML.getXmlNodeByName("OEM", this.xmlNode);
                foreach (XmlElement ele in supOEM.ChildNodes)
                {
                    this.lstOEM.Add(new OEM(ele.Name, ele.InnerText, Boolean.Parse(ele.Attributes["checked"].Value), Boolean.Parse(ele.Attributes["specialFlg"].Value)));
                }
            }
            return true;
        }
        public void reSortRecords()
        {
            for (int i = 0; i < this.lstProjectRecords.Count; i++)
            {
                this.lstProjectRecords[i].i_index = i;
            }
        }
        public void clearTmpFiles(string strDir, string strFiliter)
        {
            string strFli = strFiliter;
            if (strFli == null || strFli.Length <= 0)
            {
                strFli = string.Format(this.strAutoZMark, string.Empty);
            }
            if (Directory.Exists(strDir))
            {
                DirectoryInfo dirRoot = new DirectoryInfo(strDir);
                foreach (FileInfo fi in dirRoot.GetFiles("*" + strFli + "*"))
                {
                    AutoZDirectorysFiles.delFile(fi.FullName);
                }
            }
        }
        public string getBasePkgName(string strPkgRoot)
        {
            if (!Directory.Exists(strPkgRoot)) return string.Empty;
            DirectoryInfo dirRoot = new DirectoryInfo(strPkgRoot);
            return AutoZDirectorysFiles.getOneBaseSub(dirRoot, "exe");
        }
        public void runLittleClean(string strPath)
        {
            string strBase = strPath + this.strCleanBat;
            string strNew = strPath + this.strLittleCleanName;
            StringBuilder sbLittleClean = new StringBuilder();
            if (File.Exists(strBase))
            {
                StringBuilder builder = AutoZData.readFileToSB(strBase, this.strReadableFileType);
                string[] strSB = builder.ToString().Split('\n');
                foreach (string str in strSB)
                {
                    if (!str.ToUpper().Contains(this.strLittleCleanExcpMark.ToUpper()))
                    {
                        sbLittleClean.AppendLine(str.Replace("\r", string.Empty));
                    }
                }
                AutoZDirectorysFiles.saveFile(sbLittleClean, strNew);
                if (File.Exists(strNew))
                {
                    AutoZRunner.runBat(strPath, this.strLittleCleanName, string.Empty);
                    AutoZDirectorysFiles.delFile(strNew);
                }
            }
        }
        public void resetOEMCSV(string strRoot, string strSplitFlg)
        {
            for (int i = 0; i < this.iCSVCnt; i++)
            {
                this.resetOEMCSV(strRoot, strSplitFlg, i);
            }
        }
        public void rollBackCSV(string strRoot)
        {
            for (int i = 0; i < this.iCSVCnt; i++)
            {
                this.rollBackCSV(strRoot, i);
            }
        }
        public void turnOff(bool bFlg, string strLog)
        {
            if (!bFlg) return;
            if (!Directory.Exists(this.strAddOnsRoot)) return;
            AutoZData.saveLogToRoot(strLog, this.strLogRoot);
            AutoZRunner.runBat(this.strAddOnsRoot, "turnOff.bat", string.Empty);
        }
        public void getGetter()
        {
            if (this.strsGetterRange == null || this.strsGetterRange.Length <= 0
                || this.strsGetterRange[0].Trim().Equals(string.Empty)) return;
            InterfaceManager Im = new InterfaceManager();
            Im.run(this.strsGetterRange, this.strsGetterArgsRange);
        }
        public void sendController()
        {
            if (this.strsInterfaceRange == null || this.strsInterfaceRange.Length <= 0 
                || this.strsInterfaceRange[0].Trim().Equals(string.Empty)) return;
            InterfaceManager Im = new InterfaceManager();
            Im.run(this.strsInterfaceRange, this.strsInterfaceArgsRange);
        }
        public void upAndDown()
        {
            string strFilePath = this.strCommandPath + this.strUpDownBat;
            string strDrvRoot = this.strDriverRoot;
            if (strDrvRoot.LastIndexOf("\\") == strDrvRoot.Length - 1)
            {
                strDrvRoot = strDrvRoot.Substring(0, strDrvRoot.Length - 1);
            }
            int iIsX86 = 0;
            if (!this.strOS.Trim().ToUpper().Equals(this.strX86Flg.Trim().ToUpper()))
            {
                iIsX86 = 1;
            }
            string strCmdLine = string.Format(this.strUpDownCmd, this.strCommandPath, strDrvRoot, iIsX86.ToString());
            string[] strsTmp = strCmdLine.Split(' ');

            if (strsTmp.Length < 6 || "0".Equals(strsTmp[5].Trim()))
            {
                this.bUpdCatDrv = false;
            }
            AutoZDirectorysFiles.saveFile(strCmdLine, strFilePath);
            AutoZRunner.runBat(this.strCommandPath, this.strUpDownBat, string.Empty);
            AutoZDirectorysFiles.delFile(strFilePath);
        }
        public void runAdditional()
        {
            if (this.strsAdditionalRange == null || this.strsAdditionalRange.Length <= 0 
                || this.strsAdditionalRange[0].Trim().Equals(string.Empty)) return;
            InterfaceManager Im = new InterfaceManager();
            bool bNeedCatTest = false;
//V1.01
            if (this.strAllTestCfgFlg != string.Empty)
            {
                string[] strsTmp = this.strAllTestCfgFlg.Split(',');
//V1.01
                if (strsTmp.Length == 2)
                {
                    int iIdx = int.Parse(strsTmp[1]);
                    StringBuilder sb = AutoZData.readFileToSB(this.strInterfaceRoot + strsTmp[0], this.strReadableFileType);
                    if (sb != null)
                    {
                        string[] strsTmp2 = sb.ToString().Split('\n');
                        foreach (string str in strsTmp2)
                        {
                            string[] strsTmp3 = str.Split(',');
                            if (strsTmp3.Length > iIdx && iIdx > 1)
                            {
                                if ("1".Equals(strsTmp3[iIdx].Trim()))
                                {
                                    bNeedCatTest = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (!this.bUpdCatDrv && bNeedCatTest)
            {
                int i = 0;
                for (i = 0; i < this.strsAdditionalRange.Length; i++)
                {
                    if ("Sender.exe".Equals(this.strsAdditionalRange[i].Trim())) break;
                }
                if (i < this.strsAdditionalRange.Length && i < this.strsAdditionalArgsRange.Length)
                {
                    string strTmpChg = this.strsAdditionalArgsRange[i];
                    string strTail = string.Empty;
                    if (strTmpChg.IndexOf(",") > 0)
                    {
                        strTail = strTmpChg.Substring(strTmpChg.IndexOf(","));
                    }
                    this.strsAdditionalArgsRange[i] = this.strDriverRoot + strTail;
                }
            }
            Im.run(this.strsAdditionalRange, this.strsAdditionalArgsRange);
        }
        #endregion
    }
}
