using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AutoZKernel;

namespace MakeConfig
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ActiveControl is TextBox && keyData == Keys.Enter)
            {
                keyData = Keys.Tab;
            }
            return base.ProcessDialogKey(keyData);
        } 

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                this.loadConfig();
                this.chkReload(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Load ERROR!!!");
                this.Dispose();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.chkReload(false);
        }

        private void cboMachineGrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetMachine(this.cboMachineGrp.SelectedIndex);
        }

        private void btnToPath_Click(object sender, EventArgs e)
        {
            this.tbCtrlInfo.SelectedTab = this.tbCtrlInfo.TabPages["tpPath"];
        }

        private void btnToDrvInfo_Click(object sender, EventArgs e)
        {
            this.tbCtrlInfo.SelectedTab = this.tbCtrlInfo.TabPages["tpDriver"];
        }

        private void cboMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetInfo(false);
            if (this.cboMachine.SelectedIndex > 0)
            {
                this.txtSpec.Text = this.cboMachine.SelectedItem.ToString();
                this.txtBaseLine.Text = this.cboMachine.SelectedItem.ToString();
                this.txtNameFlg.Text = this.cboMachine.SelectedItem.ToString();
                if (this.cheEFI(this.cboMachine.SelectedItem.ToString()))
                {
                    this.chkEFI.Checked = true;
                }
            }
        }

        private void txtV_TextChanged(object sender, EventArgs e)
        {
            if (this.txtV1.Text.Equals(string.Empty) || this.txtV2.Text.Equals(string.Empty)
                || this.txtV3.Text.Equals(string.Empty) || this.txtV4.Text.Equals(string.Empty)) return;
            string strVer = "_Ver" + this.txtV1.Text.Trim() + "." + this.txtV2.Text.Trim()
             + "." + this.txtV3.Text.Trim() + "." + this.txtV4.Text.Trim();
            this.txtVersion.Text = this.txtV1.Text.Trim() + this.txtV2.Text.Trim()
                + this.txtV3.Text.Trim() + this.txtV4.Text.Trim();
            this.txtBaseLine.Text = this.chkBaseLine(strVer, 2);
        }

        private void dtFreeze_ValueChanged(object sender, EventArgs e)
        {
            //if (this.txtV1.Text.Equals(string.Empty) || this.txtV2.Text.Equals(string.Empty)
            //    || this.txtV3.Text.Equals(string.Empty) || this.txtV4.Text.Equals(string.Empty))
            //{
            //    MessageBox.Show("Please completed the driver version!");
            //    this.txtV1.Focus();
            //    return;
            //}
            DateTime dt = this.dtFreeze.Value;
            if (!this.txtBaseLine.Text.Trim().Equals(string.Empty))
                this.txtBaseLine.Text = this.chkBaseLine(this.appendDate(dt, true), 1);
        }

        private void chkNeedOld_CheckedChanged(object sender, EventArgs e)
        {
            this.txtOldPkgPath.Text = string.Empty;
            this.txtO32pkg.Text = string.Empty;
            this.txtO64pkg.Text = string.Empty;
            if (this.chkNeedOld.Checked) this.gbOld.Enabled = true;
            else this.gbOld.Enabled = false;
        }

        private void btnModBaseLine_Click(object sender, EventArgs e)
        {
            this.txtFullBaseLine.ReadOnly = false;
        }

        private void btnModPath_Click(object sender, EventArgs e)
        {
            this.txtFullUploadPath.ReadOnly = false;
        }

        private void btnModPkg_Click(object sender, EventArgs e)
        {
            this.txtPackages.ReadOnly = false;
        }

        private void btnChkBaseLine_Click(object sender, EventArgs e)
        {
            if (this.cboMachineGrp.SelectedIndex == 0 || this.cboMachine.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a machine!");
                this.cboMachineGrp.Focus();
                return;
            }
            if (this.txtSpec.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please input the baseline ID!");
                this.txtSpec.Focus();
                return;
            }
            if (this.txtBaseLine.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please input the baseline!");
                this.txtBaseLine.Focus();
                return;
            }
            if (this.txtV1.Text.Equals(string.Empty) || this.txtV2.Text.Equals(string.Empty)
                || this.txtV3.Text.Equals(string.Empty) || this.txtV4.Text.Equals(string.Empty))
            {
                MessageBox.Show("Please completed the driver version!");
                this.txtV1.Focus();
                return;
            }
            if (this.setupBaseLine())
            {
                this.btnModBaseLine.Enabled = true;
                this.btnToPath.Enabled = true;
            }
        }

        private void btnChkPath_Click(object sender, EventArgs e)
        {
            if (this.txtUploadPath.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please input the upload path!");
                this.txtUploadPath.Focus();
                return;
            }
            if (this.setupPath())
            {
                this.btnModPath.Enabled = true;
                this.btnToDrvInfo.Enabled = true;
            }
        }

        private void btnChkPkg_Click(object sender, EventArgs e)
        {
            if (this.txtNameFlg.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please input the package name!");
                this.txtNameFlg.Focus();
                return;
            }
            if (!this.chk32.Checked && !this.chk64.Checked)
            {
                MessageBox.Show("Please select package OS less one!");
                this.chk32.Focus();
                return;
            }
            if (this.setupDrvInf())
            {
                this.btnModPkg.Enabled = true;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (!this.btnToPath.Enabled || !this.btnToDrvInfo.Enabled
                || this.txtFullBaseLine.Text.Trim().Equals(string.Empty)
                || this.txtFullUploadPath.Text.Trim().Equals(string.Empty)
                || this.txtPackages.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please completed all information!!");
                return;
            }
            if (!this.insertPTCRecordFromDrvInfo())
            {
                MessageBox.Show("Occurs exception when insert the PTC record!!");
                return;
            }
            this.setSourceFromList();
            this.resetInfo(true);
        }

        private void chkEFI_CheckedChanged(object sender, EventArgs e)
        {
            this.b_EFIFlg = this.chkEFI.Checked;
            if (this.chkEFI.Checked)
            {
                string strValue = this.txtNameFlg.Text.Trim();
                if (strValue.IndexOf('_') > 0)
                {
                    this.txtNameFlg.Text = strValue.Substring(0, strValue.IndexOf('_'));
                }
            }
        }

        private void lbxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.resetTestPanel();
            if (this.lbxSource.SelectedIndex < 0)
            {
                this.lblPackageName.Text = string.Empty;
                this.pnlTestPlan.Enabled = false;
                return;
            }
            this.pnlTestPlan.Enabled = true;
            this.lblPackageName.Text = this.lbxSource.SelectedItem.ToString();
            this.setTestPlainByIdx(this.lbxSource.SelectedIndex);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lbxSource.SelectedIndex < 0) return;
            if (this.rmvPkgInf(this.lbxSource.SelectedIndex))
            {
                this.lbxSource.Items.RemoveAt(this.lbxSource.SelectedIndex);
            }
        }

        private void btnAddPlan_Click(object sender, EventArgs e)
        {
            if (this.txtPnp.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("Please input the PnPID!!");
                this.txtPnp.Focus();
                return;
            }
            if (!this.insertPlain())
            {
                MessageBox.Show("Please select less one language!!");
                this.chklstLang.Focus();
                return;
            }
        }

        private void btnSinUp_Click(object sender, EventArgs e)
        {
            if (this.lbxSource.SelectedIndex < 0) return;
            this.setInTestPlan(this.lbxSource.SelectedIndex);
            this.lbxSource.SelectedIndex = -1;
            for (int i = 0; i < this.chklstLang.Items.Count; i++)
            {
                this.chklstLang.SetItemChecked(i, false);
            }
        }

        private void btnSetted_Click(object sender, EventArgs e)
        {
            this.createCSVByData();
            this.chkReload(true);            
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btnResult = (Button)sender;
            this.toolTip.ShowAlways = true;
            this.toolTip.SetToolTip(btnResult, AutoZXML.getInnerTextByName(btnResult.Name.Replace("btn", string.Empty), this.nod_ToolTip));
        }
    }
}