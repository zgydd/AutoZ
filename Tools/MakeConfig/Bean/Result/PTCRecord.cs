using System;
using System.Collections.Generic;
using System.Text;
using AutoZKernel.Bean;

namespace MakeConfig.Bean
{
    [Serializable] 
    public class PTCRecord : Cloneable
    {
        public string str_Spec = string.Empty;
        public string str_SDF = string.Empty;
        public string str_PkgNameFlg = string.Empty;
        public string str_UploadPath = string.Empty;
        public string str_OldPath = string.Empty;
        public string str_Old32 = string.Empty;
        public string str_Old64 = string.Empty;
        public string str_PkgName = string.Empty;
        public string str_PkgName64 = string.Empty;
    }
}
