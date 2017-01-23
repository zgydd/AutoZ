using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MakeConfig.Bean
{
    public partial class CtrTestRecord : UserControl
    {
        public CtrTestRecord(string strPnPId, string strOem, string[] strsLag)
        {
            InitializeComponent();
            this.lblOem.Text = strOem;
            this.lblPnpId.Text = strPnPId;
            this.lstLang.Items.Clear();
            foreach (string str in strsLag)
            {
                this.lstLang.Items.Add(str);
            }
        }
        public CtrTestRecord(string strPnPId, string strOem, List<string> lstLag)
        {
            InitializeComponent();
            this.lblOem.Text = strOem;
            this.lblPnpId.Text = strPnPId;
            this.lstLang.Items.Clear();
            foreach (string str in lstLag)
            {
                this.lstLang.Items.Add(str);
            }
        }
        public bool getChk()
        {
            return this.chk.Checked;
        }
        public void getRecords(ref TestRcd tstResult)
        {
            tstResult.str_Oem = this.lblOem.Text.Trim();
            tstResult.str_PnP = this.lblPnpId.Text.Trim();
            string strLangs = string.Empty;
            foreach (string str in this.lstLang.Items)
            {
                strLangs += str.Trim() + ";";
            }
            tstResult.str_Languages = strLangs.Substring(0, strLangs.LastIndexOf(';'));
        }
        
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lstLang.SelectedIndex < 0) return;
            this.lstLang.Items.RemoveAt(this.lstLang.SelectedIndex);
        }
        private void btnRemove_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            this.toolTip.ShowAlways = true;
            this.toolTip.SetToolTip(btn, "Remove selected language from this record.");
        }
    }
}
