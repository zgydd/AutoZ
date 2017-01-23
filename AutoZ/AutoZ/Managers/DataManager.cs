//////////////////
///Barton Joe
///V1.01    Modify analizeBat()
/// Don't split special batch file if it's name contains "_"
///V1.02    Modify createPackage()
/// Save nmake log to scan log's directory for upload
///V1.03    Add strMakedSpNameMark and reSortNickName() etc
/// For setup special package name by nicknames automatically
//////////////////
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using AutoZ.Bean;
using AutoZKernel;
using AutoZKernel.Bean;

namespace AutoZ
{
    public class DataManager : Cloneable
    {
        #region Private definition
        private Controller dataController = null;
        private ProjectRecord dataRecord = null;
        private List<string> lstOldBatPath = new List<string>();
        private string strLog = string.Empty;
        private List<PackageInfo> pkgInfo = new List<PackageInfo>();
        private int iCursor = -1;
//V1.03
        private string strMakedSpNameMark = string.Empty;
//V1.03
        #endregion
        #region Properties
        public DataManager(Controller ctrl)
        {
            this.dataController = ctrl;
        }
        #endregion
        #region Private Prtoected Function
        private void initCursor()
        {
            this.lstOldBatPath.Clear();
            this.pkgInfo.Clear();
        }
//V1.03
        private void reSortNickName()
        {
            Dictionary<int, string> dirNickNames = this.dataController.getNickNameSort();
            string strSortedNickName = string.Empty;
            for (int i = 1; i <= dirNickNames.Count; i++)
            {
                foreach (int key in dirNickNames.Keys)
                {
                    if (i == key && this.strMakedSpNameMark.Contains(dirNickNames[key]))
                    {
                        strSortedNickName += dirNickNames[key];
                        break;
                    }
                }
            }
            this.strMakedSpNameMark = strSortedNickName;
        }
//V1.03
        private string getNewPath(string path, int iStart, int iEnd, string strFormat, ref int iPkgNo)
        {
            string strMarkFlg = string.Empty;
            string strAutoZMark = this.dataController.getAutoZMark();
            if (iStart < 0 || iEnd < 0 || iEnd < iStart
                              || (iEnd - iStart) == this.dataController.getMakeMark().Length)
            {
                strMarkFlg = string.Format(strAutoZMark, strFormat);
                iPkgNo = 1;
            }
            else
            {
                int iNewStart = iStart + this.dataController.getMakeMark().Length;
                strMarkFlg = path.Substring(iNewStart, iEnd - iNewStart)
                    + string.Format(strAutoZMark, strFormat);
                iPkgNo = 2;
            }
            string strResult = path.Substring(0, iStart)
                + this.dataController.getMakeMark() + strMarkFlg + ".BAT";
            return strResult;
        }
        private void saveBat(StringBuilder data, string strSavePath)
        {
            try
            {
                AutoZDirectorysFiles.saveFile(data, strSavePath);
                this.strLog += "\tNew Bat::::::" + strSavePath + "\t\tSAVED!!\n";
            }
            catch (Exception ex)
            {
                this.strLog += "\tBAT saver has exceptions!!" + ex.StackTrace + "\n";
            }
        }
        private void analizeBat(StringBuilder sb, string strPath)
        {
            this.strLog += "\tStart to analize the batch:::::[" + strPath + "]!!\n";
            string strLang = null;
            string strOEM = null;
            string strLangMark = this.dataController.getLangMark();
            string strOEMMark = this.dataController.getOEMMark();
            string strOS = this.dataController.getOS();
            string strOSMark = this.dataController.getOSMark();
            string[] strSB = sb.ToString().Split('\n');
            int iLangIdx = -1;
            int iOEMIdx = -1;
            int iOSIdx = -1;
//V1.01
            bool isSpecialBat = true;

            if (strPath.LastIndexOf("\\") > 0 && strPath.LastIndexOf("\\") < strPath.Length)
            {
                isSpecialBat = strPath.Substring(strPath.LastIndexOf("\\"))
                    .Replace(String.Format(this.dataController.getAutoZMark(), string.Empty), string.Empty).Contains("_");
            }
//V1.01
            for (int i = 0; i < strSB.Length; i++)
            {
                if (strSB[i].ToUpper().Contains(strLangMark.ToUpper()))
                {
                    strLang = strSB[i].Substring(strLangMark.Length);
                    iLangIdx = i;
                }
                if (strSB[i].ToUpper().Contains(strOEMMark.ToUpper()))
                {
                    strOEM = strSB[i].Substring(strOEMMark.Length);
                    iOEMIdx = i;
                }
                if (strSB[i].ToUpper().Contains(strOSMark.ToUpper()))
                {
                    iOSIdx = i;
                }
            }
            string[] strLangGroup = (strLang.Replace("\r", string.Empty)).Split(' ');
            string[] strOEMGroup = (strOEM.Replace("\r", string.Empty)).Split(' ');

            string[] strNewLang = new string[this.dataController.getMaxPackage()];
            string[] strNewOEM = new string[this.dataController.getMaxPackage()];
            List<Lang> lstLang = this.dataController.getLang();
            List<OEM> lstOEM = this.dataController.getOEM();
            int iSpPkg = this.dataController.getSpecPkgNo();
//V1.03
            Dictionary<int, string> dirNickNames = this.dataController.getNickNameSort();
//V1.03
            foreach (Lang la in lstLang)
            {
                foreach (string strChkLang in strLangGroup)
                {
                    if (la.LangFlg.Equals(strChkLang.Trim()))
                    {
                        if (strNewLang[la.iPakNo - 1] == null || strNewLang[la.iPakNo - 1].Length <= 0)
                        {
                            strNewLang[la.iPakNo - 1] = strChkLang + " ";
                        }
                        else if (!strNewLang[la.iPakNo - 1].ToUpper().Contains(strChkLang.Trim().ToUpper()))
                        {
                            strNewLang[la.iPakNo - 1] += strChkLang + " ";
                        }
//V1.03
                        if (la.iPakNo == iSpPkg && !this.strMakedSpNameMark.Contains(la.LangNickName))
                        {
                            this.strMakedSpNameMark += la.LangNickName;
                        }
//V1.03
                    }
                }
                foreach (OEM oem in lstOEM)
                {
                    foreach (string strChkOEM in strOEMGroup)
                    {
                        if (oem.OEMFlg.Equals(strChkOEM))
                        {
                            if (la.iPakNo != iSpPkg)
                            {
                                if (strNewOEM[la.iPakNo - 1] == null || strNewOEM[la.iPakNo - 1].Length <= 0)
                                {
                                    strNewOEM[la.iPakNo - 1] = strChkOEM + " ";
                                }
                                else if (!strNewOEM[la.iPakNo - 1].ToUpper().Contains(strChkOEM.ToUpper()))
                                {
                                    strNewOEM[la.iPakNo - 1] += strChkOEM + " ";
                                }
                            }
                            else if (oem.bSpFlg)
                            {
                                if (strNewOEM[iSpPkg - 1] == null || strNewOEM[iSpPkg - 1].Length <= 0)
                                {
                                    strNewOEM[iSpPkg - 1] = strChkOEM + " ";
                                }
                                else if (!strNewOEM[iSpPkg - 1].ToUpper().Contains(strChkOEM.ToUpper()))
                                {
                                    strNewOEM[iSpPkg - 1] += strChkOEM + " ";
                                }
                            }
                        }
                    }
                }
            }
//V1.03
            if (this.strMakedSpNameMark != null && !this.strMakedSpNameMark.Trim().Equals(string.Empty))
            {
                this.reSortNickName();
            }
//V1.03
//V1.01
            if (isSpecialBat) strNewLang[0] = null;
//V1.01
            if (strNewLang[0] == null || strLang == null || strNewOEM[0] == null || strOEM == null
                || strNewLang[0].Trim().Equals(strLang.Trim()) && strNewOEM[0].Trim().Equals(strOEM.Trim()))
            {
                this.strLog += "\tThe batch:::::[" + strPath + "] don't need to be changed!!\n";
                StringBuilder sbNewBat = new StringBuilder();
                for (int j = 0; j < strSB.Length; j++)
                {
                    if (j == iOSIdx)
                    {
                        sbNewBat.AppendLine((strOSMark + strOS).Replace("\r", string.Empty));
                    }
                    else
                    {
                        sbNewBat.AppendLine(strSB[j].Replace("\r", string.Empty));
                    }
                }
//V1.03
                if (this.strMakedSpNameMark != null && !this.strMakedSpNameMark.Equals(string.Empty))
                {
                    foreach (PackageInfo pk in this.pkgInfo)
                    {
                        if (pk.iPkgNo == iSpPkg)
                        {
                            pk.NickNameMark = this.strMakedSpNameMark;
                            break;
                        }
                    }
                }
//V1.03
                this.saveBat(sbNewBat, strPath);
            }
            else
            {
                this.strLog += "\tThe batch:::::[" + strPath + "] need to be splited!!\n";
                for (int i = 0; i < this.dataController.getMaxPackage(); i++)
                {
                    StringBuilder sbNewBat = new StringBuilder();
                    if (strNewLang[i] == null || strNewOEM[i] == null
                        || strNewLang[i].Length <= 0 || strNewOEM[i].Length <= 0)
                        continue;
                    string strNewBatPath = null;
                    for (int j = 0; j < strSB.Length; j++)
                    {
                        if(j == iLangIdx)
                        {
                            sbNewBat.AppendLine((this.dataController.getLangMark() + strNewLang[i]));
                        }
                        else if (j == iOEMIdx)
                        {
                            sbNewBat.AppendLine(this.dataController.getOEMMark() + strNewOEM[i]);
                        }
                        else if (j == iOSIdx)
                        {
                            sbNewBat.AppendLine((strOSMark + strOS).Replace("\r", string.Empty));
                        }
                        else
                        {
                            sbNewBat.AppendLine(strSB[j].Replace("\r", string.Empty));
                        }
                    }
                    strNewBatPath = strPath.Substring(0, strPath.Length - 4) + i.ToString() + ".BAT";
                    if (i + 1 == iSpPkg)
                    {
                        int iExSpIdx = -1;
                        bool bExSpInfoFlg = false;
                        for (iExSpIdx = 0; iExSpIdx < this.pkgInfo.Count; iExSpIdx++)
                        {
                            if (this.pkgInfo[iExSpIdx].iPkgNo == iSpPkg)
                            {
                                bExSpInfoFlg = true;
                                break;
                            }
                        }
                        if (!bExSpInfoFlg)
                        {
                            PackageInfo pk = new PackageInfo(iSpPkg, string.Empty, strPath.Substring(0, strPath.LastIndexOf('\\')));
                            pk.lstBatGrp.Add(strNewBatPath);
                            pk.NickNameMark = this.strMakedSpNameMark;
                            this.pkgInfo.Add(pk);
                        }
                        else
                        {
                            this.pkgInfo[iExSpIdx].lstBatGrp.Add(strNewBatPath);
                        }
                    }
                    else
                    {
                        if (i >= this.pkgInfo.Count)
                        {
                            PackageInfo pk = new PackageInfo(i, string.Empty, strPath.Substring(0, strPath.LastIndexOf('\\')));
                            pk.lstBatGrp.Add(strNewBatPath);
                            this.pkgInfo.Add(pk);
                        }
                        else
                        {
                            this.pkgInfo[i].lstBatGrp.Add(strNewBatPath);
                        }
                    }
                    this.strLog += "\tSplited batch:::::[" + i.ToString() + "---" + strNewBatPath + "] be setted!!\n";
                    this.saveBat(sbNewBat, strNewBatPath);
                }
            }
        }
        #endregion
        #region Public Function
        public void getRecord(ProjectRecord record)
        {
            this.dataRecord = record;
            this.iCursor = record.i_index;
            this.initCursor();
        }
        public void getRecordBAT()
        {
            if (this.dataRecord.b_hasSet && !this.dataRecord.b_useDifferentBAT)
            {
                string strBatPath = this.dataRecord.str_path.Trim() + "\\" + this.dataController.getMakeMark() + ".BAT";
                this.lstOldBatPath.Add(strBatPath);
                this.strLog += "\t" + strBatPath + "\t\tGET!!";
            }
            else
            {
                DirectoryInfo dirInfo = new DirectoryInfo(this.dataRecord.str_path.Trim());
                if (!dirInfo.Exists)
                {
                    this.strLog += "\tUnreachable path::" + this.dataRecord.str_path.Trim() + "\n";
                    return;
                }
                foreach (FileInfo f in dirInfo.GetFiles(this.dataController.getMakeMark() + "*.BAT"))
                {
                    string strBatPath = this.dataRecord.str_path.Trim() + "\\" + f.Name;
                    this.lstOldBatPath.Add(strBatPath);
                    this.strLog += "\t" + strBatPath + "\t\tGET!!\n";
                }
            }
        }
        public void remakeBAT()
        {
            StreamReader reader = null;
            int iPkgNo = 1;
            foreach (string path in this.lstOldBatPath)
            {
                StringBuilder builder = new StringBuilder();
                try
                {
                    builder = AutoZData.readFileToSB(path, this.dataController.getReadableFileType());

                    if (!this.dataRecord.b_hasSet)
                    {
                        int idxStart = path.IndexOf(this.dataController.getMakeMark());
                        int idxEnd = path.IndexOf(".BAT");
                        string strNew = this.getNewPath(path, idxStart, idxEnd, string.Empty, ref iPkgNo);
                        if (iPkgNo == 1)
                        {
                            PackageInfo info = new PackageInfo(iPkgNo, string.Empty, path.Substring(0, path.LastIndexOf('\\')));
                            info.lstBatGrp.Add(strNew);
                            this.pkgInfo.Add(info);
                        }
                        else
                        {
                            bool bHasInfo = false;
                            int i = 0;
                            for (i = 0; i < this.pkgInfo.Count; i++)
                            {
                                if (iPkgNo == this.pkgInfo[i].iPkgNo)
                                {
                                    bHasInfo = true;
                                    break;
                                }
                            }
                            if (bHasInfo)
                            {
                                this.pkgInfo[i].lstBatGrp.Add(strNew);
                            }
                            else
                            {
                                PackageInfo info = new PackageInfo(iPkgNo, string.Empty, path.Substring(0, path.LastIndexOf('\\')));
                                info.lstBatGrp.Add(strNew);
                                this.pkgInfo.Add(info);
                            }
                        }
                        this.analizeBat(builder, strNew);
                    }
                    else
                    {
#region Panding
                        //_________________________________Panding_______________________________________________________
                        //One bat and need to split and change it
                        string[] tmpPkgLang = new string[this.dataController.getMaxPackage()];
                        for (int i = 0; i < tmpPkgLang.Length; i++)
                        {
                            foreach (Lang la in this.dataRecord.lst_supportedLang)
                            {
                                if (!la.bChecked) continue;
                                if (la.iPakNo != i + 1) continue;
                                if (la.iPakNo != this.dataController.getSpecPkgNo())
                                {
                                    tmpPkgLang[i] += la.LangFlg + " ";
                                }
                                else
                                {
                                    //include OEM   ZW
                                    if (!this.dataRecord.b_haveSpecialOEM)
                                    {
                                        tmpPkgLang[i] += la.LangFlg + " ";
                                        //OEM
                                    }
                                    else
                                    {
                                    }
                                }
                            }
                        }
                        
                        //List<StringBuilder> lstData = new List<StringBuilder>();
                        
                        //if (this.dataRecord.b_hasSet)
                        //{

                        //    for (int i = 0; i < lstNewBatPath.Count; i++)
                        //    {
                        //        this.configNewBat(lstData[i]);
                        //    }
                        //}
                        //_________________________________Panding_______________________________________________________
#endregion 
                    }
                }
                catch (Exception ex)
                {
                    reader.Close();
                    this.strLog += "\tString Reader has exceptions!!\n" + ex.StackTrace + "\n";
                }
            }
            
        }
        public void resetPckInfo()
        {
            this.strLog += "\tStart to reset Package's information!!\n";
            List<PackageInfo> lstTmp = new List<PackageInfo>();
            foreach (PackageInfo pk in this.pkgInfo)
            {
                lstTmp.Add(Clone<PackageInfo>(pk));
            }
            this.pkgInfo.Clear();
//V1.03
            this.strMakedSpNameMark = string.Empty;
//V1.03
            foreach (PackageInfo pk in lstTmp)
            {
                if (this.pkgInfo.Count <= 0)
                {
                    this.pkgInfo.Add(Clone<PackageInfo>(pk));
                    continue;
                }
                else
                {
                    int iExtIdx = 0;
                    bool bExtFlg = false;
                    for (iExtIdx = 0; iExtIdx < this.pkgInfo.Count; iExtIdx++)
                    {
                        if (this.pkgInfo[iExtIdx].iPkgNo == pk.iPkgNo)
                        {
                            bExtFlg = true;
                            break;
                        }
                    }
                    if (bExtFlg)
                    {
                        foreach (string strT in pk.lstBatGrp)
                        {
                            this.pkgInfo[iExtIdx].lstBatGrp.Add(strT);
                        }
                    }
                    else
                    {
                        this.pkgInfo.Add(Clone<PackageInfo>(pk));
                        continue;
                    }
                }
            }
            this.strLog += "\tPackage's information resetted!!\n";
        }
        public void createPackage()
        {
            if (this.pkgInfo.Count <= 0)
            {
                this.strLog += "##No package information!!\n";
                return;
            }
            this.strLog += "\tStart to create packages!!\n";
            string strPkgName = string.Empty;
            string strSpPkgName = this.dataController.getSpPkgNameMark();
            int iSpcSplitMarkIdx = -1;
            char chSplit = this.dataController.getSpPkgNameSplitMark().ToCharArray()[0];
            string strInstallRootMark = this.dataController.getInstallRoot();
            string strCleanArg = this.dataController.getCleanArg();
            string strPkgFullOnlyName = string.Empty;
            foreach (PackageInfo pk in this.pkgInfo)
            {
                this.strLog += this.dataController.getSplitLine() + "\n\t\t######Package disk range start!!######\n";
                AutoZRunner.runBat(pk.strProjectRoot, this.dataController.getMakeMark() + ".BAT", strCleanArg);
                string strExeRootDir = string.Empty;
                if (strPkgName.Length > 0)
                {
                    if (pk.iPkgNo == this.dataController.getSpecPkgNo())
                    {
                        iSpcSplitMarkIdx = strPkgName.LastIndexOf(chSplit);
                        string strArg0 = strPkgName.Substring(strPkgName.LastIndexOf('\\') + 1, iSpcSplitMarkIdx - strPkgName.LastIndexOf('\\') - 1);
                        string strArg1 = strPkgName.Substring(iSpcSplitMarkIdx + 1);
//V1.03
                        if (pk.NickNameMark != null && !pk.NickNameMark.Equals(string.Empty))
                        {
                            pk.strPackageName = strArg0 + this.dataController.getSpPkgNameSplitMark()
                                + pk.NickNameMark + this.dataController.getSpPkgNameSplitMark() + strArg1;
                        }
                        else
                        {
//V1.03
                            pk.strPackageName = string.Format(strSpPkgName, strArg0, strArg1);
                        }
                    }
                }
                foreach (string strPath in pk.lstBatGrp)
                {
                    string strDir = strPath.Substring(0, strPath.LastIndexOf('\\') + 1); //pk.strProjectRoot + \
                    string strBatName = strPath.Substring(strPath.LastIndexOf('\\') + 1);
                    this.strLog += "\n\t\t######Disk " + strBatName + " start!!######\n";
                    this.dataController.runLittleClean(strDir);
                    this.dataController.resetOEMCSV(strDir, strBatName);
                    AutoZRunner.runBat(strDir, strBatName, string.Empty);
//V1.02
                    if (File.Exists(pk.strProjectRoot + "\\" + this.dataController.getUpLogName()))
                    {
                        AutoZDirectorysFiles.copyFile(pk.strProjectRoot + "\\" + this.dataController.getUpLogName(),
                            this.dataController.getScanLogRoot() + pk.strProjectRoot.Substring(pk.strProjectRoot.LastIndexOf('\\'))
                            + strBatName.Replace(string.Format(this.dataController.getAutoZMark(), string.Empty), string.Empty)
                            + "_" + this.dataController.getUpLogName());
                    }
//V1.02
                    this.dataController.rollBackCSV(strDir);
                    this.strLog += "\t\t######Disk " + strBatName + " finished!!######\n";
                }
                if (strPkgName.Length <= 0)
                {
                    strPkgName = this.dataController.getBasePkgName(pk.strProjectRoot + strInstallRootMark);
                    pk.strPackageName = strPkgName.Substring(strPkgName.LastIndexOf('\\') + 1);
                    strPkgFullOnlyName = strPkgName;
                }
                if (strPkgFullOnlyName.Length > 0)
                {
                    AutoZDirectorysFiles.copyFile(strPkgFullOnlyName, this.dataController.getDriverRoot() + pk.strPackageName);
                }
                this.strLog += "\n\t\t######Package disk range finished!!######\n" + this.dataController.getSplitLine() + "\n";
            }
            this.strLog += "\tPackage created and copy to " + this.dataController.getDriverRoot() + " Compeleted!!\n";
        }
        public string getLog()
        {
            if (this.strLog == null || this.strLog.Length <= 0) return string.Empty;
            string strResult = DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString() 
                + "##\n" + this.strLog + "\n";
            this.strLog = string.Empty;
            return strResult;
        }
        #endregion
    }
}
