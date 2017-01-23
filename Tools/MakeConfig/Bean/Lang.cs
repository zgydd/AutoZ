//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace MakeConfig.Bean
{
    [Serializable] 
    public class Lang
    {
        public string LangFlg = string.Empty;
        public string LangName = string.Empty;
        public int Index = -1;
        public bool Checked = false;
    }
}
