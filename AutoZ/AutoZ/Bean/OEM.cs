//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoZ.Bean
{
    [Serializable] 
    public class OEM
    {
        public string OEMFlg = null;
        public string OEMName = null;
        public bool bChecked = false;
        public bool bSpFlg = false;
        public OEM(string strOemFlg, string strOemName, bool bChked, bool bSpFlg)
        {
            this.OEMFlg = strOemFlg;
            this.OEMName = strOemName;
            this.bChecked = bChked;
            this.bSpFlg = bSpFlg;
        }
    }
}
