//////////////////
///Barton Joe
///V1.01:    Add LangNickName
/// For setup special package name
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization; 

namespace AutoZ.Bean
{
    [Serializable] 
    public class Lang
    {
        public string LangFlg = null;
        public string LangName = null;
        public bool bChecked = false;
        public int iPakNo = 1;
//V1.01
        public string LangNickName = null;
//V1.01
    }
}
