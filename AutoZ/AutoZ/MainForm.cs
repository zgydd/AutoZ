//////////////////
///Barton Joe
//////////////////
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoZKernel;

namespace AutoZ
{
    public partial class fMain : Form
    {
        public fMain()
        {
            try{
                InitializeComponent();
                bool bolCtrlLoad = this.ctrl.Load(Application.StartupPath);
                this.btnSetSrc.Enabled = false;
                if (!bolCtrlLoad)
                {
                    MessageBox.Show("Config load error!");
                    this.Dispose();
                }
                else
                {
                    this.init();
                }
                AutoZController.PreventSleep(true);
                if (this.ctrl.isFromGetter())
                {
                    this.ctrl.getGetter();
                    this.startSDFRange();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init error! Please check configration!!" + ex.StackTrace);
            }
        }
        #region Events
        private void bSrc_Click(object sender, EventArgs e)
        {
            if (this.dSrc.ShowDialog() == DialogResult.OK)
            {
                if (this.lstSrc.Items.Contains(this.dSrc.SelectedPath))
                {
                    MessageBox.Show("This directory is exist!");
                }
                else
                {
                    int iRs = this.ctrSrcList(this.dSrc.SelectedPath, 0);
                    if (iRs == -1)
                    {
                        MessageBox.Show("This directory isn't a project root!");
                    }
                }
            }
        }
        private void bRemove_Click(object sender, EventArgs e)
        {
            this.ctrSrcList(this.lstSrc.SelectedIndex.ToString(), 2);
            if (this.lstSrc.Items.Count <= 0 || this.lstSrc.SelectedIndex < 0)
            {
                this.bRemove.Enabled = false;
            }
        }
        private void lstSrc_Click(object sender, EventArgs e)
        {
            if (this.lstSrc.SelectedIndex >= 0)
            {
                this.bRemove.Enabled = true;
            }
        }
        private void bGetMultiSrc_Click(object sender, EventArgs e)
        {
            this.ctrSrcList(null, 3);
            if (this.dSrc.ShowDialog() == DialogResult.OK)
            {
                String strRootPath = this.dSrc.SelectedPath;
                List<DirectoryInfo> dirSub = this.searchProjectPath(strRootPath, true);
                foreach (DirectoryInfo di in dirSub)
                {
                    this.ctrSrcList(di.FullName, 1);
                }
            }
        }
        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdDouble.Checked)
            {
                this.setDouble();
            }
            else
            {
                this.setSingle();
            }
        }
        private void chkLst_Click(object sender, EventArgs e)
        {
            this.resetRadio();
        }
        private void btnSetSrc_Click(object sender, EventArgs e)
        {
            if (this.lstSrc.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a project to set!!");
                this.lstSrc.Focus();
                return;
            }
            this.pnlSrc.Enabled = false;
            this.pnlPackageInfo.Enabled = true;
            this.setRecordCursor(this.lstSrc.SelectedIndex);
        }
        private void chkSpecTSKPkg_CheckedChanged(object sender, EventArgs e)
        {
            this.setSpecTSKPkg(this.chkSpecTSKPkg.Checked);
        }
        private void chkLstOEM_SelectedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lstOEM.Count; i++)
            {
                this.lstOEM[i].bChecked = false;
                if (this.chkLstOEM.GetItemChecked(i))
                {
                    this.lstOEM[i].bChecked = true;
                }
            }
        }
        private void chkDifBat_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.chkDifBat.Checked)
            {
                this.chkENUTSK.Enabled = true;
                this.chkLstOEM.Enabled = true;
                this.chkSpecTSKPkg.Enabled = true;
                this.chkLstOEM.Enabled = true;
            }
            else
            {
                this.chkENUTSK.Checked = false;
                this.chkENUTSK.Enabled = false;
                this.chkSpecTSKPkg.Enabled = false;
                this.setSpecTSKPkg(false);
                this.chkLstOEM.Enabled = false;
            }
        }
        private void btnSetOk_Click(object sender, EventArgs e)
        {
            this.pnlSrc.Enabled = true;
            if (this.lstSrc.SelectedIndex >= 0)
            {
                this.setRecordInfo();
            }
            this.setSingle();
            this.pnlPackageInfo.Enabled = false;
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (this.lstSrc.Items.Count <= 0)
            {
                MessageBox.Show("No project data!!");
                this.lstSrc.Focus();
                return;
            }
            this.pnlSrc.Enabled = false;
            this.pnlPackageInfo.Enabled = false;
            this.startPackage();
            this.pnlSrc.Enabled = true;
        }
        private void btnGetSDF_Click(object sender, EventArgs e)
        {
            this.startSDFRange();
        }
        private void rtxtLog_TextChanged(object sender, EventArgs e)
        {
            rtxtLog.SelectionStart = rtxtLog.Text.Length;
            rtxtLog.ScrollToCaret();
        }
        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            AutoZData.saveLogToRoot(this.rtxtLog.Text, this.ctrl.getLogRoot());
        }
        #endregion
    }
}