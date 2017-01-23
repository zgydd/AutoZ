//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using AutoZ.Bean;

namespace AutoZ.Controls
{
    public partial class SpecialOEMCtrl : UserControl
    {
        private string strLangFlg = null;
        private List<OEM> lstOEM = new List<OEM>();
        public SpecialOEMCtrl()
        {
            InitializeComponent();
            this.chkLstOEM.Enabled = false;
        }

        public SpecialOEMCtrl(string strLang, string strLangFlg, List<OEM> lst)
        {
            InitializeComponent();
            this.chkLstOEM.Enabled = false;
            this.chkSpecLang.Text = strLang;
            this.strLangFlg = strLangFlg;
            foreach (OEM oem in lst)
            {
                this.chkLstOEM.Items.Add(oem.OEMName, oem.bChecked);
                this.lstOEM.Add(new OEM(strLangFlg, strLang, true, false));
            }
        }

        private void chkSpecLang_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSpecLang.Checked)
            {
                this.chkLstOEM.Enabled = true;
            }
            else
            {
                this.chkLstOEM.Enabled = false;
            }
        }

        public bool used()
        {
            return this.chkSpecLang.Checked;
        }
        public Lang_OEMs getRecord()
        {
            Lang_OEMs result = new Lang_OEMs();
            result.strOEMFlg = this.strLangFlg;
            result.strOEMName = this.chkSpecLang.Text.Trim();
            result.lstSupportOEMs = this.lstOEM;
            return result;
        }
    }
}
