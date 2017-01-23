//////////////////
///Barton Joe
///V1.01:    Add LangNickName
///For setup special package name
//////////////////
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoZ.Bean
{
    [Serializable]
    public class PackageInfo
    {
        public int iPkgNo = -1;
        public string strPackageName = null;
        public string strProjectRoot = null;
        public List<string> lstBatGrp = new List<string>();
//V1.01
        public string NickNameMark = string.Empty;
//V1.01
        public PackageInfo(int iPkgNo, string strPkgName, string strPjRoot)
        {
            this.iPkgNo = iPkgNo;
            this.strPackageName = strPkgName;
            this.strProjectRoot = strPjRoot;
        }
    }
}
