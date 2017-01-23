using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MakeConfig.Bean
{
    public partial class CtrTestPage : UserControl
    {
        public CtrTestPage(string strOsMark, int iNeedCat)
        {
            InitializeComponent();
            switch (iNeedCat)
            {
                case 0:
                    this.rdoN.Checked = true;
                    break;
                case 1:
                default:
                    this.rdoY.Checked = true;
                    break;
            }
            this.lblOSMark.Text = strOsMark;
        }
        public void addRecord(string strPnPID, string strOem, List<string> lstLag)
        {
            CtrTestRecord tRcd = new CtrTestRecord(strPnPID, strOem, lstLag);
            this.addRecordControl(tRcd);
        }
        public void setupRecords(ref TestPlan tstResult)
        {
            if (tstResult == null || tstResult.i_Idx < 0) return;
            tstResult.str_Os = this.lblOSMark.Text.Trim();
            tstResult.b_NeedCat = !this.rdoN.Checked;
            tstResult.lst_TestRcd.Clear();
            foreach (Control ctrl in this.pnlCtrls.Controls)
            {
                try
                {
                    CtrTestRecord rcd = (CtrTestRecord)ctrl;
                    TestRcd tstRcd = new TestRcd();
                    rcd.getRecords(ref tstRcd);
                    tstResult.lst_TestRcd.Add(tstRcd);
                }
                catch (Exception) { continue; }
            }
        }
        public bool hasRecord()
        {
            if (this.pnlCtrls.Controls.Count > 0) return true;
            return false;
        }
        public string getOs()
        {
            return this.lblOSMark.Text.Trim();
        }
        public void setTestPlan(bool bNeedCat, List<TestRcd> lstRcd)
        {
            if (bNeedCat) this.rdoY.Checked = true;
            else this.rdoN.Checked = true;
            this.pnlCtrls.Controls.Clear();
            foreach (TestRcd rcd in lstRcd)
            {
                string[] strsLang = rcd.str_Languages.Split(';');
                CtrTestRecord tstRcd = new CtrTestRecord(rcd.str_PnP, rcd.str_Oem, strsLang);
                this.addRecordControl(tstRcd);
            }
        }
        private void addRecordControl(CtrTestRecord ctrRcd)
        {
            ctrRcd.Location = new Point(5, this.pnlCtrls.Controls.Count * 100 + 3);
            this.pnlCtrls.Controls.Add(ctrRcd);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.pnlCtrls.Controls)
            {
                try
                {
                    CtrTestRecord rcd = (CtrTestRecord)ctrl;
                    if (rcd.getChk()) this.pnlCtrls.Controls.Remove(ctrl);
                }
                catch (Exception) { }
            }
            for (int i = 0; i < this.pnlCtrls.Controls.Count; i++)
            {
                this.pnlCtrls.Controls[i].Location = new Point(5, i * 100 + 3);
            }
        }
        private void btnRemove_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            this.toolTip.ShowAlways = true;
            this.toolTip.SetToolTip(btn, "Remove selected records from test plan.");
        }
    }
}
