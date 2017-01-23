using System;
using System.Collections.Generic;
using System.Text;
using AutoZKernel.Bean;

namespace MakeConfig.Bean
{
    [Serializable]
    public class TestRecord : Cloneable
    {
        public string str_OS = string.Empty;
        public string str_PkgName = string.Empty;
        public string str_Language = string.Empty;
        public string str_OEM = string.Empty;
        public string str_PnP = string.Empty;
        public string str_PDL = string.Empty;
        public string str_Version = string.Empty;
        public string str_CoreVersion = string.Empty;
        public string str_NeedCat = string.Empty;
    }
}
