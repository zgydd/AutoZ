//////////////////
///Barton Joe
///V1002:   Modify startPackage
///         Save log to file in auto status
///For auto save log in auto model
//////////////////
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using AutoZ.Bean;
using AutoZ.Controls;
using AutoZKernel;

namespace AutoZ
{
    public partial class fMain
    {
        private Controller ctrl = new Controller();
        private List<Lang> lstLang = new List<Lang>();
        private List<OEM> lstOEM = new List<OEM>();

        private void init()
        {
            this.dSrc.SelectedPath = this.ctrl.getMultiRoot();
            this.bRemove.Enabled = false;
            this.setLangAndOEM();
            this.resetRadio();
            this.pnlPackageInfo.Enabled = false;
            this.setSingle();
        }
        private int ctrSrcList(string strPath, int ctrlFlg)
        {
            int iResult = -1;
            List<DirectoryInfo> dirSub = null;
            List<ProjectRecord> lstRecords = this.ctrl.getLstProjectRecords();
            if (ctrlFlg == 0) 
            { 
                dirSub = this.searchProjectPath(strPath, false); 
            }
            else 
            { 
                dirSub = this.searchProjectPath(strPath, false); 
            }
            switch (ctrlFlg)
            {
                case 0:
                case 1:
                    if (dirSub.Count <= 0) break;
                    this.lstSrc.Items.Add(strPath);
                    lstRecords.Add(new ProjectRecord(lstRecords.Count + 1, strPath));
                    iResult = lstRecords.Count;
                    break;
                case 2:
                    int idx = int.Parse(strPath.Trim());
                    this.lstSrc.Items.RemoveAt(idx);
                    lstRecords.RemoveAt(idx);
                    this.ctrl.reSortRecords();
                    iResult = lstRecords.Count;
                    break;
                case 3:
                    this.lstSrc.Items.Clear();
                    lstRecords.Clear();
                    iResult = 0;
                    break;
                default:
                    break;
            }
            return iResult;
        }
        private void setRecordCursor(int iSelectedIdx)
        {
            this.ctrl.setCursor(iSelectedIdx);
        }
        private void setRecordInfo()
        {
            ProjectRecord currentRecord = this.ctrl.getCurrentRecord();
            currentRecord.b_hasSet = true;
            foreach (Lang la in this.lstLang)
            {
                currentRecord.cloneLang(la);
            }
            if (this.rdDouble.Checked) currentRecord.i_packageNum = 2;
            if (this.chkDifBat.Checked) currentRecord.b_useDifferentBAT = true;
            if (this.chkENUTSK.Checked) currentRecord.b_haveENUDisk = true;
            if (this.chkSpecTSKPkg.Checked) currentRecord.b_haveSpecialOEM = true;
            if (currentRecord.i_packageNum > 1 && !currentRecord.b_useDifferentBAT)
            {
                foreach (OEM oem in this.lstOEM)
                {
                    currentRecord.cloneOEM(oem);
                }
            }
            if (currentRecord.b_haveSpecialOEM)
            {
                foreach (SpecialOEMCtrl specOemCtrl in this.spcPkgType.Panel2.Controls)
                {
                    if (specOemCtrl.used())
                    {
                        currentRecord.cloneLang_OEMs(specOemCtrl.getRecord());
                    }
                }
            }
        }
        private void setLangAndOEM()
        {
            this.lstLang = this.ctrl.getLang();
            this.lstOEM = this.ctrl.getOEM();
            foreach (Lang la in lstLang)
            {
                this.chkLstLang.Items.Add(la.LangName, la.bChecked);
            }
            foreach (OEM oem in lstOEM)
            {
                this.chkLstOEM.Items.Add(oem.OEMName, oem.bChecked);
            }
        }
        private List<DirectoryInfo> searchProjectPath(string strPath, bool bForSub)
        {
            if (strPath == null || strPath.Length <= 0) return null;
            DirectoryInfo dirRoot = new DirectoryInfo(strPath);
            List<DirectoryInfo> dirResult = new List<DirectoryInfo>();
            this.getProjectSub(dirRoot, bForSub, ref dirResult);
            return dirResult;
        }
        private void getProjectSub(DirectoryInfo dirInfo, bool bNeedSub, ref List<DirectoryInfo> lstResult)
        {
            if (!dirInfo.Exists) return;
            foreach (FileInfo f in dirInfo.GetFiles(this.ctrl.getMakeMark() + "*.BAT"))
            {
                if (!this.ctrl.getExcpMark().ToUpper().Contains(f.Name.ToUpper()))
                {
                    lstResult.Add(dirInfo);
                    return;
                }
            }
            if (!bNeedSub) return;
            if (dirInfo.GetDirectories().Length <= 0) return;
            foreach (DirectoryInfo dirSub in dirInfo.GetDirectories())
            {
                this.getProjectSub(dirSub, true, ref lstResult);
            }
        }
        private void resetRadio()
        {
            int iPakFlg = 0;
            bool bHasDifPak = false;
            for (int i = 0; i < this.lstLang.Count; i++)
            {
                this.lstLang[i].bChecked = false;
                if (this.chkLstLang.GetItemChecked(i))
                {
                    this.lstLang[i].bChecked = true;
                    if (iPakFlg == 0) iPakFlg = this.lstLang[i].iPakNo;
                    if (this.lstLang[i].iPakNo != iPakFlg) bHasDifPak = true;
                }
            }
            if (!bHasDifPak)
            {
                this.rdSingle.Checked = true;
                this.rdDouble.Enabled = false;
                this.chkENUTSK.Checked = false;
                this.chkENUTSK.Enabled = false;
                this.chkLstOEM.Enabled = false;
                this.chkSpecTSKPkg.Checked = false;
                this.chkSpecTSKPkg.Enabled = false;
            }
            else
            {
                this.rdDouble.Enabled = true;
            }
        }
        private void initSpecTSK(List<OEM> lstOem)
        {
            int iPkgNo = this.ctrl.getSpecPkgNo();
            int iSum = 0;
            for (int i = 0; i < this.lstLang.Count; i++)
            {
                if (iPkgNo == this.lstLang[i].iPakNo && this.chkLstLang.GetItemChecked(i))
                {
                    SpecialOEMCtrl spCtrl = new SpecialOEMCtrl(this.lstLang[i].LangName, this.lstLang[i].LangFlg, lstOem);
                    spCtrl.Location = new Point(2, 125 * iSum);
                    this.spcPkgType.Panel2.Controls.Add(spCtrl);
                    iSum++;
                }
            }
        }
        private void setSingle()
        {
            this.chkDifBat.Checked = false;
            this.chkDifBat.Enabled = false;
            this.chkENUTSK.Checked = false;
            this.chkENUTSK.Enabled = false;
            this.chkSpecTSKPkg.Checked = false;
            this.chkSpecTSKPkg.Enabled = false;
            this.setSpecTSKPkg(false);
            this.chkLstOEM.Enabled = false;
        }
        private void setDouble()
        {
            this.chkDifBat.Enabled = true;
            this.chkENUTSK.Enabled = true;
            this.chkSpecTSKPkg.Enabled = true;
            this.chkLstOEM.Enabled = true;
        }
        private void setSpecTSKPkg(bool flg)
        {
            List<OEM> lstTSKOem = new List<OEM>();
            if (flg)
            {
                this.spcPkgType.Panel2.Enabled = true;
                for (int i = 0; i < this.lstOEM.Count; i++)
                {
                    if (this.chkLstOEM.GetItemChecked(i))
                    {
                        lstTSKOem.Add(this.lstOEM[i]);
                    }
                }
                this.initSpecTSK(lstTSKOem);
                this.chkLstOEM.Enabled = false;
            }
            else
            {
                this.chkSpecTSKPkg.Checked = false;
                foreach (SpecialOEMCtrl ctrl in this.spcPkgType.Panel2.Controls)
                {
                    ctrl.Visible = false;
                }
                this.spcPkgType.Panel2.Enabled = false;
                this.chkLstOEM.Enabled = true;
            }
        }
        private void startPackage()
        {
            try
            {
                //this.ctrl.sendController();
                //this.ctrl.upAndDown();
                DataManager manager = new DataManager(this.ctrl);
                this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                 + "############\tMain Controller get control!!\tDataManager created!!!\t############\n";
                this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "############\tPackage start!!!\t############\n" + this.ctrl.getSplitLine() + "\n";
                this.ctrl.reSortRecords();
                List<ProjectRecord> data = this.ctrl.getLstProjectRecords();
                foreach (ProjectRecord rec in data)
                {
                    this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                     + "############\t Record range start!!!\t############\n";
                    this.ctrl.clearTmpFiles(rec.str_path, null);
                    manager.getRecord(rec);
                    manager.getRecordBAT();
                    this.rtxtLog.Text += manager.getLog();
                    manager.remakeBAT();
                    this.rtxtLog.Text += manager.getLog();
                    manager.resetPckInfo();
                    this.rtxtLog.Text += manager.getLog();
                    manager.createPackage();
                    this.rtxtLog.Text += manager.getLog();
                    this.ctrl.clearTmpFiles(rec.str_path, null);
                    this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                     + "############\t Record range finished!!!\t############\n";
                }
                this.rtxtLog.Text += this.ctrl.getSplitLine() + "\n" 
                    + DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "############\tPackage completed!!!\t############\n"
                    + "############\tNow send control to the Interface Manager to run addons!!!\t############\n" 
                    + this.ctrl.getSplitLine() + "\n";
                this.ctrl.sendController();
                this.rtxtLog.Text += this.ctrl.getSplitLine() + "\n" 
                    + DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "############\tAddons finished!!!\t############\n";

                this.rtxtLog.Text += this.ctrl.getSplitLine() + "\n"
                    + DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "############\tStart to upload packages and download them!!!\t############\n";
                if (this.ctrl.needUpD())
                {
                    this.ctrl.upAndDown();
                }
                this.rtxtLog.Text += this.ctrl.getSplitLine() + "\n"
                    + DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "############\tUpload packages and download them finished!!!\t############\n";
                this.ctrl.runAdditional();
                this.rtxtLog.Text += this.ctrl.getSplitLine() + "\n"
                    + DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "##\tAll Completed!!";
//V1002
                if (this.ctrl.isFromGetter())
                {
                    AutoZData.saveLogToRoot(this.rtxtLog.Text, this.ctrl.getLogRoot());
                }
//V1002
            }
            catch (Exception ex)
            {
                this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                    + "#\t!!Exception!!#" + ex.StackTrace + "\n";
            }
            finally
            {
                this.ctrl.turnOff(this.chkAutoTurnOff.Checked, this.rtxtLog.Text);
            }
        }
        private void getSDF()
        {
            this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                + "##\tGet SDF data start!!\n" + this.ctrl.getSplitLine() + "\n";
            string strPath = this.ctrl.getCommandPath() + this.ctrl.getCommandBat();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CD /d " + this.ctrl.getCommandPath());
            sb.AppendLine(this.ctrl.getSDFCmd());
            AutoZDirectorysFiles.delFile(strPath);
            using (StreamWriter outfile = new StreamWriter(strPath))
            {
                outfile.Write(sb.ToString());
            }
            this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                + "##\tFile " + strPath + " Created!!\n";
            this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                + "##\tA console will get control and please wait it finished!!\n";

            AutoZRunner.runBat(this.ctrl.getCommandPath(), this.ctrl.getCommandBat(), string.Empty);

            this.rtxtLog.Text += DateTime.Now.ToShortDateString() + "::" + DateTime.Now.ToLongTimeString()
                + "##\tSDF data geted!!\n" + this.ctrl.getSplitLine() + "\n";
            AutoZDirectorysFiles.delFile(strPath);
        }
        private void startSDFRange()
        {
            AutoZController.PreventSleep(false);
            this.pnlSrc.Enabled = false;
            this.pnlPackageInfo.Enabled = false;
            this.getSDF();
            this.ctrSrcList(null, 3);
            String strRootPath = this.ctrl.getSDFRoot();
            List<DirectoryInfo> dirSub = this.searchProjectPath(strRootPath, true);
            foreach (DirectoryInfo di in dirSub)
            {
                this.ctrSrcList(di.FullName, 1);
            }
            this.pnlSrc.Enabled = true;
            this.startPackage();
            AutoZController.ResotreSleep();
        }
    }
}
