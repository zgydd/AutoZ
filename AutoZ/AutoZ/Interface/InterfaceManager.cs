//////////////////
///Barton Joe
//////////////////
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AutoZKernel;

namespace AutoZ.Interface
{
    public class InterfaceManager : Controller
    {
        public void run(string[] strsMains, string[] strsArgs)
        {
            if (strsMains == null || strsMains.Length <= 0) return;
            this.LoadBase(System.Windows.Forms.Application.StartupPath);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strsMains.Length; i++)
            {
                if (strsMains[i] == null || strsMains[i].Equals(string.Empty)) continue;
                string str = strsMains[i] + " ";
                if (i < strsArgs.Length && strsArgs[i] != null && !string.Empty.Equals(strsArgs[i]))
                {
                    string[] strTmp = strsArgs[i].Split(',');
                    foreach (string strSig in strTmp)
                    {
                        if (strSig.Contains("\\"))
                        {
                            str += strSig + " ";
                        }
                        else
                        {
                            str += AutoZXML.getInnerTextByName(strSig.Trim(), this.xmlNode) + " ";
                        }
                    }
                }
                sb.AppendLine(str);
            }
            string strSavePath = this.strInterfaceRoot + this.strRuntimeInterfaceName;
            AutoZDirectorysFiles.saveFile(sb, strSavePath);
            if (File.Exists(strSavePath))
            {
                AutoZRunner.runBat(this.strInterfaceRoot, strRuntimeInterfaceName, string.Empty);
                AutoZDirectorysFiles.delFile(strSavePath);
            }
        }
    }
}
