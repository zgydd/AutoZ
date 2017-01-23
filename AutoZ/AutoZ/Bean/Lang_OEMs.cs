//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoZ.Bean
{
    [Serializable] 
    public class Lang_OEMs
    {
        public string strOEMFlg = null;
        public string strOEMName = null;
        public List<OEM> lstSupportOEMs = new List<OEM>();
    }
}
