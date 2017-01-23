using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using AutoZKernel;
using MakeConfig.Bean;

namespace MakeConfig
{
    public partial class Main
    {
        private XmlDocument xmlDoc = new XmlDocument();
        private XmlNode xmlBaseNode = null;
        private XmlNode nod_MachineGrp = null;
        private XmlNode nod_Machines = null;
        private XmlNode nod_PDL = null;
        private XmlNode nod_ToolTip = null;
        private List<Lang> lst_Lang = new List<Lang>();
        private string str_SDFRoot = string.Empty;
        private string str_ShareRoot = string.Empty;
        private int i_MaxCoreVNum = -1;
        private bool b_EFIFlg = false;
        private List<PTCRecord> lst_PTC = new List<PTCRecord>();
        private List<TestPkgInfo> lst_PkgInfo = new List<TestPkgInfo>();
        private List<TestPlan> lst_TestPlan = new List<TestPlan>();
        private void resetInfo(bool bNeedRstMach)
        {
            if (bNeedRstMach)
            {
                this.cboMachineGrp.SelectedIndex = 0;
                this.cboMachine.SelectedIndex = 0;
            }
            this.txtV1.Text = string.Empty;
            this.txtV2.Text = string.Empty;
            this.txtV3.Text = string.Empty;
            this.txtV4.Text = string.Empty;
            this.dtFreeze.Value = DateTime.Now;
            this.dtRelease.Value = AutoZData.getWeekend(DateTime.Now);
            this.txtSpec.Text = string.Empty;
            this.txtBaseLine.Text = string.Empty;
            this.chkEFI.Checked = false;
            this.b_EFIFlg = false;
            this.chkSpFlg.Checked = false;
            this.btnModBaseLine.Enabled = false;
            this.btnToPath.Enabled = false;
            this.txtFullBaseLine.Text = string.Empty;
            this.txtFullBaseLine.ReadOnly = true;
            this.txtUploadPath.Text = string.Empty;
            this.chkNeedOld.Checked = false;
            this.btnModPath.Enabled = false;
            this.btnToDrvInfo.Enabled = false;
            this.txtFullUploadPath.Text = string.Empty;
            this.txtFullUploadPath.ReadOnly = true;
            this.txtNameFlg.Text = string.Empty;
            this.cboDelivery.SelectedIndex = 0;
            this.cboPDL.SelectedIndex = 0;
            this.chk32.Checked = true;
            this.chk64.Checked = true;
            this.txtVersion.Text = string.Empty;
            this.cboPkgType.SelectedIndex = 0;
            this.cboCoreV1.SelectedIndex = 0;
            this.cboCoreV2.SelectedIndex = 0;
            this.btnModPkg.Enabled = false;
            this.txtPackages.Text = string.Empty;
            this.txtPackages.ReadOnly = true;
        }
        private void freezeAll()
        {
            this.tbCtrlInfo.Enabled = false;
            this.lbxSource.Enabled = false;
            this.btnRemove.Enabled = false;
            this.btnSetted.Enabled = false;
            this.pnlTestPlan.Enabled = false;
        }
        private void releaseAll()
        {
            this.tbCtrlInfo.Enabled = true;
            this.lbxSource.Enabled = true;
            this.btnRemove.Enabled = true;
            this.btnSetted.Enabled = true;
            this.pnlTestPlan.Enabled = true;
        }
        private void initControl()
        {
            this.lbxSource.Items.Clear();
            this.resetNormalCbo();
            this.setLangLst();
            this.initTestPlan();
            this.dtFreeze.Value = DateTime.Now;
            this.dtRelease.Value = AutoZData.getWeekend(DateTime.Now);
            this.cboMachineGrp.Items.Clear();
            this.cboMachineGrp.Items.Add("Please select a item...");
            this.cboMachineGrp.SelectedIndex = 0;
            if (this.nod_MachineGrp.HasChildNodes) this.setCboItems(this.cboMachineGrp.Items, this.nod_MachineGrp);
            this.resetMachine(this.cboMachineGrp.SelectedIndex);
            this.tbCtrlInfo.SelectedTab = this.tbCtrlInfo.TabPages["tpBaseLine"];
            this.tbCtrlInfo.TabPages["tpBaseLine"].Focus();
            this.pnlTestPlan.Enabled = false;
        }
        private void resetMachine(int iSelectedIdx)
        {
            this.cboMachine.Items.Clear();
            this.cboMachine.Items.Add("Please select a item...");
            this.cboMachine.SelectedIndex = 0;
            if (this.nod_Machines == null || !this.nod_Machines.HasChildNodes) return;
            if (iSelectedIdx < 1) return;
            XmlElement eleSelectedGrp = (XmlElement)this.nod_MachineGrp.ChildNodes[iSelectedIdx - 1];
            string strGroup = eleSelectedGrp.GetAttribute("value").Trim().ToUpper();
            foreach (XmlElement ele in this.nod_Machines.ChildNodes)
            {
                if (strGroup.Equals(ele.GetAttribute("group").Trim().ToUpper()))
                {
                    this.cboMachine.Items.Add(AutoZXML.getInnerTextByName(ele.Name, this.nod_Machines));
                }
            }
        }
        private void setCboItems(ComboBox.ObjectCollection clItems, XmlNode nodLst)
        {
            foreach (XmlElement ele in nodLst.ChildNodes)
            {
                clItems.Add(AutoZXML.getInnerTextByName(ele.Name, nodLst));
            }
        }
        private void resetNormalCbo()
        {
            this.cboDelivery.Items.Clear();
            XmlNode nodDelivery = AutoZXML.getXmlNodeByName("Delivery", this.xmlBaseNode);
            if (nodDelivery.HasChildNodes) this.setCboItems(this.cboDelivery.Items, nodDelivery);
            this.cboDelivery.SelectedIndex = 0;
            this.cboPDL.Items.Clear();
            XmlNode nodPDL = AutoZXML.getXmlNodeByName("PDL", this.xmlBaseNode);
            if (nodPDL.HasChildNodes) this.setCboItems(this.cboPDL.Items, nodPDL);
            this.cboPDL.SelectedIndex = 0;
            this.cboPkgType.Items.Clear();
            XmlNode nodPkgType = AutoZXML.getXmlNodeByName("PkgType", this.xmlBaseNode);
            if (nodPkgType.HasChildNodes) this.setCboItems(this.cboPkgType.Items, nodPkgType);
            this.cboPkgType.SelectedIndex = 0;
            this.cboOem.Items.Clear();
            XmlNode nodOem = AutoZXML.getXmlNodeByName("OEM", this.xmlBaseNode);
            if (nodOem.HasChildNodes) this.setCboItems(this.cboOem.Items, nodOem);
            this.cboOem.SelectedIndex = 0;
            if (this.i_MaxCoreVNum > 0)
            {
                this.cboCoreV1.Items.Clear();
                this.cboCoreV2.Items.Clear();
                for (int i = 1; i <= i_MaxCoreVNum; i++)
                {
                    this.cboCoreV1.Items.Add(AutoZData.appendLeft(i.ToString(), 2, true));
                    this.cboCoreV2.Items.Add(AutoZData.appendLeft(i.ToString(), 2, true));
                }
                this.cboCoreV1.SelectedIndex = 0;
                this.cboCoreV2.SelectedIndex = 0;
            }
        }
        private void resetTestPanel()
        {
            if (this.cboOem.Items.Count > 0)
            {
                this.cboOem.SelectedIndex = 0;
            }
            this.txtPnp.Text = string.Empty;
            this.initTestPlan();
        }
        private void setLangLst()
        {
            XmlNode nodLang = AutoZXML.getXmlNodeByName("Lang", this.xmlBaseNode);
            this.lst_Lang.Clear();
            int idx = 0;
            if (nodLang.HasChildNodes)
            {
                this.chklstLang.Items.Clear();
                foreach (XmlElement ele in nodLang.ChildNodes)
                {
                    Lang la = new Lang();
                    la.LangFlg = ele.Name;
                    la.LangName = AutoZXML.getInnerTextByName(ele.Name, nodLang);
                    this.chklstLang.Items.Add(la.LangName);
                    la.Index = idx;
                    this.lst_Lang.Add(la);
                    idx++;
                }
            }
        }
        private void initTestPlan()
        {
            XmlNode nodTestOS = AutoZXML.getXmlNodeByName("Systems", this.xmlBaseNode);
            if (nodTestOS.HasChildNodes)
            {
                this.tbCtrlTestPlain.TabPages.Clear();
                foreach (XmlElement ele in nodTestOS.ChildNodes)
                {
                    TabPage tp = new TabPage(AutoZXML.getInnerTextByName(ele.Name, nodTestOS));
                    CtrTestPage page = new CtrTestPage(ele.Name, int.Parse(ele.GetAttribute("needCat").Trim()));
                    tp.Controls.Add(page);
                    this.tbCtrlTestPlain.TabPages.Add(tp);
                }
            }
        }
        private void init()
        {
            this.releaseAll();
            this.lst_PTC.Clear();
            this.lst_PkgInfo.Clear();
            this.lst_TestPlan.Clear();
            this.initControl();
        }
        private void loadConfig()
        {
            this.xmlDoc.Load("Config/Config.xml");
            this.xmlBaseNode = this.xmlDoc.DocumentElement;
            if (this.xmlBaseNode == null || !this.xmlBaseNode.HasChildNodes) return;
            this.str_SDFRoot = AutoZXML.getInnerTextByName("SDFRoot", this.xmlBaseNode);
            this.str_ShareRoot = AutoZXML.getInnerTextByName("ShareRoot", this.xmlBaseNode);
            this.i_MaxCoreVNum = int.Parse(AutoZXML.getInnerTextByName("MaxCoreVerNum", this.xmlBaseNode));
            this.nod_MachineGrp = AutoZXML.getXmlNodeByName("MachineGroup", this.xmlBaseNode);
            this.nod_Machines = AutoZXML.getXmlNodeByName("Machines", this.xmlBaseNode);
            this.nod_PDL = AutoZXML.getXmlNodeByName("PDL", this.xmlBaseNode);
            this.nod_ToolTip = AutoZXML.getXmlNodeByName("ToolTips", this.xmlBaseNode);
        }
        private void chkReload(bool bIsInit)
        {
            if (!bIsInit)
            {
                this.xmlDoc = null;
                this.xmlDoc = new XmlDocument();
                this.xmlBaseNode = null;
                this.loadConfig();
            }
            if (this.xmlBaseNode == null)
            {
                this.freezeAll();
                this.btnReload.Enabled = true;
                this.btnReload.Focus();
            }
            else
            {
                this.init();
            }
        }
        private bool cheEFI(string strMachine)
        {
            if (strMachine.Contains("EFI")) return true;
            if (this.nod_PDL.HasChildNodes)
            {
                foreach (XmlElement ele in this.nod_PDL.ChildNodes)
                {
                    if (strMachine.Contains(ele.GetAttribute("group"))) return true;
                }
            }
            return false;
        }
        private string appendDate(DateTime dt, bool bNeedHead)
        {
            string strResult = string.Empty;
            if (bNeedHead) strResult = "_";
            strResult += AutoZData.appendLeft(dt.Year.ToString(), 4, true)
                + "." + AutoZData.appendLeft(dt.Month.ToString(), 2, true)
                + "." + AutoZData.appendLeft(dt.Day.ToString(), 2, true);
            return strResult;
        }
        private bool chkPartBaseLine(ref string strBaseLine, string[] strsSrc, int iMarkFlg, ref int[] idx)
        {
            if (strsSrc.Length < iMarkFlg) return false;
            string strSrc = strsSrc[strsSrc.Length - iMarkFlg];
            if (strsSrc.Length == iMarkFlg)
            {
                strBaseLine = strSrc;
                return false;
            }
            switch (iMarkFlg)
            {
                case 1:
                    if (!strSrc.Contains(".")) return false;
                    if (AutoZData.isFullDate(strSrc))
                        idx[1] = strBaseLine.LastIndexOf("_");
                    else
                        return false;
                    break;
                case 2:
                    if (!strSrc.Contains("Ver") && AutoZData.isFullDate(strsSrc[strsSrc.Length - 1])) strBaseLine = strBaseLine.Substring(0, strBaseLine.LastIndexOf("_"));
                    if (strBaseLine.Contains("Ver") && (!strSrc.Contains("Ver") || strSrc.IndexOf("Ver") > 0)) return false;
                    try
                    {
                        int i = int.Parse(strSrc.Substring(3).Replace(".", string.Empty));
                        for (i = 0; i < strsSrc.Length - iMarkFlg; i++)
                        {
                            idx[0] += strsSrc[i].Length + 1;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }
        private string chkBaseLine(string strAdditionalValue, int iMarkFlg)
        {
            string strBaseLineValue = this.txtBaseLine.Text;
            int[] iIdx = { -1, -1 };
            string[] strsTmp = strBaseLineValue.Split('_');
            bool bModed = false;
            //char[] chr = strBaseLineValue.ToCharArray();
            //for (int i = 0; i < chr.Length; i++)
            //{
            //    if (iCnt > 1) break;
            //    if ('_'.Equals(chr[i]))
            //    {
            //        iIdx[iCnt] = i;
            //        iCnt++;
            //    }
            //}
            if (this.chkPartBaseLine(ref strBaseLineValue, strsTmp, iMarkFlg, ref iIdx))
            {
                switch (iMarkFlg)
                {
                    case 1:
                        if (iIdx[1] > 0)
                        {
                            strBaseLineValue = strBaseLineValue.Substring(0, iIdx[1]) + strAdditionalValue;
                            bModed = true;
                        }
                        break;
                    case 2:
                        if (iIdx[0] > 0)
                        {
                            string strHead = strBaseLineValue.Substring(0, iIdx[0]);
                            string strTail = string.Empty;
                            iIdx[1] = strBaseLineValue.LastIndexOf("_");
                            if (iIdx[1] > iIdx[0])
                            {
                                strTail = strBaseLineValue.Substring(iIdx[1]);
                            }
                            strBaseLineValue = strHead + strAdditionalValue + strTail;
                            bModed = true;
                        }
                        break;
                    default: break;
                }
            }
            if (!bModed) strBaseLineValue += strAdditionalValue;
            return strBaseLineValue;
        }
        private bool setupBaseLine()
        {
            string[] strBaseLineGrp = this.txtBaseLine.Text.Trim().Split('_');
            bool bHasDate = true;
            if (strBaseLineGrp.Length > 0)
            {
                string strTail = strBaseLineGrp[strBaseLineGrp.Length - 1];
                bHasDate = AutoZData.isFullDate(strTail);
            }
            if (!bHasDate)
            {
                DateTime dt = this.dtFreeze.Value;
                this.txtBaseLine.Text = this.chkBaseLine(this.appendDate(dt, true), 1);
            }
            this.txtFullBaseLine.Text = string.Empty;
            if (!this.nod_MachineGrp.HasChildNodes || !this.nod_Machines.HasChildNodes || this.str_SDFRoot.Trim().Equals(string.Empty))
                return false;
            this.txtFullBaseLine.Text = this.str_SDFRoot.Trim();
            foreach (XmlElement ele in this.nod_MachineGrp.ChildNodes)
            {
                if (this.cboMachineGrp.SelectedItem.ToString().Trim().Equals(AutoZXML.getInnerTextByName(ele.Name, this.nod_MachineGrp)))
                {
                    this.txtFullBaseLine.Text += ele.Name.Trim() + "/";
                    break;
                }
            }
            foreach (XmlElement ele in this.nod_Machines.ChildNodes)
            {
                if (this.cboMachine.SelectedItem.ToString().Trim().Equals(AutoZXML.getInnerTextByName(ele.Name, this.nod_Machines)))
                {
                    this.txtFullBaseLine.Text += ele.GetAttribute("head").Trim() + ele.Name.Trim() + "/";
                    break;
                }
            }
            this.txtFullBaseLine.Text += this.txtBaseLine.Text.Trim();
            //if (this.b_EFIFlg && this.chkEFI.Checked)
            //    this.txtFullBaseLine.Text += "\n" + this.getAnotherPDL(this.txtFullBaseLine.Text, "group");
            return true;
        }
        private bool setupPath()
        {
            this.txtFullUploadPath.Text = string.Empty;
            if (this.str_ShareRoot.Trim().Equals(string.Empty) || this.txtUploadPath.Text.Trim().Equals(string.Empty))
                return false;
            this.txtFullUploadPath.Text = this.str_ShareRoot.Trim() + this.txtUploadPath.Text.Trim();
            return true;
        }
        private bool setupDrvInf()
        {
            if (this.txtNameFlg.Text.Trim().Equals(string.Empty) || !this.nod_PDL.HasChildNodes
                || (!this.chk32.Checked && !this.chk64.Checked)) return false;
            this.txtPackages.Text = string.Empty;
            string strBaseDrvName = this.txtNameFlg.Text.Trim() 
                + "_" + this.cboDelivery.SelectedItem.ToString().Trim();
            foreach (XmlElement ele in this.nod_PDL.ChildNodes)
            {
                if (AutoZXML.getInnerTextByName(ele.Name, this.nod_PDL).Trim().Equals(this.cboPDL.SelectedItem.ToString().Trim()))
                {
                    strBaseDrvName += "_" + ele.GetAttribute("flg").Trim();
                    break;
                }
            }
            DateTime dt = this.dtRelease.Value;
            strBaseDrvName += "{0}_V" + this.txtVersion.Text.Trim() + "{1}"
                + this.appendDate(dt, true).Replace(".", string.Empty) + ".";
            XmlNode nodType = AutoZXML.getXmlNodeByName("PkgType", this.xmlBaseNode);
            if (nodType.HasChildNodes)
            {
                foreach (XmlElement ele in nodType.ChildNodes)
                {
                    if (this.cboPkgType.SelectedItem.ToString().Trim().Equals(AutoZXML.getInnerTextByName(ele.Name, nodType)))
                    {
                        strBaseDrvName += ele.Name.Trim();
                        break;
                    }
                }
            }
            string str32 = string.Empty;
            string str64 = string.Empty;
            string strSpMark = AutoZXML.getInnerTextByName("SpecPkgMark", this.xmlBaseNode);
            XmlNode nodOs = AutoZXML.getXmlNodeByName("OS", this.xmlBaseNode);
            if (nodOs.HasChildNodes)
            {
                str32 = AutoZXML.getInnerTextByName("flg32", nodOs);
                str64 = AutoZXML.getInnerTextByName("flg64", nodOs);
            }
            if (this.chk32.Checked)
            {
                this.txtPackages.Text += string.Format(strBaseDrvName, str32, string.Empty) + "\n";
                if (this.chkSpFlg.Checked)
                {
                    this.txtPackages.Text += string.Format(strBaseDrvName, str32, strSpMark) + "\n";
                }
            }
            if (this.chk64.Checked)
            {
                this.txtPackages.Text += string.Format(strBaseDrvName, str64, string.Empty) + "\n";
                if (this.chkSpFlg.Checked)
                {
                    this.txtPackages.Text += string.Format(strBaseDrvName, str64, strSpMark) + "\n";
                }
            }
            //if (this.b_EFIFlg && this.chkEFI.Checked)
            //    this.txtPackages.Text += this.getAnotherPDL(this.txtPackages.Text, "flg");
            return true;
        }
        private string getAnotherPDL(string strLine, string strAttbuFlg)
        {
            string strResult = string.Empty;
            string strThis = string.Empty;
            string strThisGroup = string.Empty;
            string strAnothers = string.Empty;
            foreach (XmlElement ele in this.nod_PDL)
            {
                if (strLine.Contains(ele.GetAttribute(strAttbuFlg)))
                {
                    strThis = ele.GetAttribute(strAttbuFlg);
                    strThisGroup = ele.GetAttribute("group");
                    continue;
                }
                if (ele.GetAttribute("group").Equals(strThisGroup)) continue;
                strAnothers += ele.GetAttribute(strAttbuFlg) + "-";
            }
            if (strAnothers.LastIndexOf("-") == strAnothers.Length - 1) 
                strAnothers = strAnothers.Substring(0, strAnothers.Length - 1);
            string[] strAths = strAnothers.Split('-');
            if (!strThis.Trim().Equals(string.Empty))
            {
                foreach (string str in strAths)
                {
                    strResult += strLine.Replace(strThis, str) + "\n";
                }
            }
            return strResult;
        }
        private string appendSpMarkToPkgName(string strOldName)
        {
            if (strOldName.LastIndexOf("_") > 0)
            {
                string strHead = strOldName.Substring(0, strOldName.LastIndexOf("_"));
                string strTail = strOldName.Substring(strOldName.LastIndexOf("_"));
                return strHead + AutoZXML.getInnerTextByName("SpecPkgMark", this.xmlBaseNode) + strTail;
            }
            else return strOldName;
        }
        private bool insertPTCRecordFromDrvInfo()
        {
            //lst_PTC.Clear();
            string[] strsBaseLine = this.txtFullBaseLine.Text.Trim().Split('\n');
            string strUpPath = this.txtFullUploadPath.Text.Trim();
            string[] strsPkgs = this.txtPackages.Text.Trim().Split('\n');
            foreach (string strSigBaseLine in strsBaseLine)
            {
                PTCRecord record = new PTCRecord();
                record.str_Spec = this.txtSpec.Text.Trim();
                record.str_SDF = strSigBaseLine;
                record.str_PkgNameFlg = this.txtNameFlg.Text.Trim();
                record.str_UploadPath = this.txtFullUploadPath.Text.Trim() + "\\";
                if (this.chkNeedOld.Checked)
                {
                    record.str_OldPath = this.str_ShareRoot + this.txtOldPkgPath.Text.Trim() + "\\";
                    record.str_Old32 = this.txtO32pkg.Text.Trim();
                    record.str_Old64 = this.txtO64pkg.Text.Trim();
                }
                string strPDL = string.Empty;
                string strPDLMark = string.Empty;
                string str32Mark = string.Empty;
                string str64Mark = string.Empty;
                if (this.nod_PDL.HasChildNodes)
                {
                    foreach (XmlElement ele in this.nod_PDL.ChildNodes)
                    {
                        if (this.cboPDL.SelectedItem.ToString().Trim().Equals(AutoZXML.getInnerTextByName(ele.Name, this.nod_PDL)))
                        {
                            strPDL = ele.GetAttribute("group").Trim();
                            strPDLMark = ele.GetAttribute("flg").Trim();
                            str32Mark += strPDLMark;
                            str64Mark += strPDLMark;
                            break;
                        }
                    }
                    XmlNode nodOS = AutoZXML.getXmlNodeByName("OS", this.xmlBaseNode);
                    if (nodOS.HasChildNodes)
                    {
                        str32Mark += AutoZXML.getInnerTextByName("flg32", nodOS);
                        str64Mark += AutoZXML.getInnerTextByName("flg64", nodOS);
                    }
                }
                if (this.chk32.Checked)
                {
                    foreach (string str in strsPkgs)
                    {
                        if (str.Contains(str32Mark))
                        {
                            record.str_PkgName = str;
                            break;
                        }
                    }
                }
                if (this.chk64.Checked)
                {
                    foreach (string str in strsPkgs)
                    {
                        if (str.Contains(str64Mark))
                        {
                            record.str_PkgName64 = str;
                            break;
                        }
                    }
                }
                if (this.b_EFIFlg && this.chkEFI.Checked)
                {
                    string strNotShownedEFI = AutoZXML.getInnerTextByName("NotShownedEFI", this.xmlBaseNode);
                    //if (strSigBaseLine.Contains(strNotShownedEFI))
                    //{
                    //    string str32XL = str32Mark.Replace(strPDLMark, AutoZXML.getXmlElementByName(strNotShownedEFI, this.nod_PDL).GetAttribute("flg"));
                    //    string str64XL = str64Mark.Replace(strPDLMark, AutoZXML.getXmlElementByName(strNotShownedEFI, this.nod_PDL).GetAttribute("flg"));
                    //    record.str_Spec = record.str_Spec.Replace(strPDL, strNotShownedEFI);
                    //    record.str_Old32 = record.str_Old32.Replace(str32Mark, str32XL);
                    //    record.str_Old64 = record.str_Old64.Replace(str64Mark, str64XL);
                    //    record.str_PkgName = record.str_PkgName.Replace(str32Mark, str32XL);
                    //    record.str_PkgName64 = record.str_PkgName64.Replace(str64Mark, str64XL);
                    //}
                }
                lst_PTC.Add(record);
                if (this.chkSpFlg.Checked)
                {
                    PTCRecord recordSp = new PTCRecord();
                    recordSp.str_Spec = record.str_Spec + AutoZXML.getInnerTextByName("SpecPkgMark", this.xmlBaseNode);
                    recordSp.str_PkgNameFlg = record.str_PkgNameFlg;
                    recordSp.str_UploadPath = record.str_UploadPath.Substring(0, record.str_UploadPath.Length - 1)
                        + AutoZXML.getInnerTextByName("SpecPkgMark", this.xmlBaseNode) + "\\";
                    if (this.chkNeedOld.Checked)
                    {
                        recordSp.str_OldPath = record.str_OldPath.Substring(0, record.str_OldPath.Length - 1)
                        + AutoZXML.getInnerTextByName("SpecPkgMark", this.xmlBaseNode) + "\\";
                        recordSp.str_Old32 = this.appendSpMarkToPkgName(record.str_Old32);
                        recordSp.str_Old64 = this.appendSpMarkToPkgName(record.str_Old64);
                    }
                    recordSp.str_PkgName = this.appendSpMarkToPkgName(record.str_PkgName);
                    recordSp.str_PkgName64 = this.appendSpMarkToPkgName(record.str_PkgName64);
                    lst_PTC.Add(recordSp);
                }
            }
            return true;
        }
        private void setSourceFromList()
        {
            string[] strsPkgs = this.txtPackages.Text.Trim().Split('\n');
            if (strsPkgs.Length <= 0) return;
            int i = this.lbxSource.Items.Count;
            foreach (string str in strsPkgs)
            {
                TestPkgInfo pkgInf = new TestPkgInfo();
                pkgInf.i_Index = i;
                pkgInf.str_PkgName = str;
                pkgInf.b_IsEFI = this.chkEFI.Checked;
                pkgInf.str_PDL = this.cboPDL.SelectedItem.ToString().Trim();
                pkgInf.str_Version = this.txtV1.Text.Trim() + "." + this.txtV2.Text.Trim()
                    + "." + this.txtV3.Text.Trim() + "." + this.txtV4.Text.Trim();
                pkgInf.str_CorVer = this.cboCoreV1.SelectedItem.ToString().Trim() + "."
                    + this.cboCoreV2.SelectedItem.ToString().Trim();
                this.lbxSource.Items.Add(str);
                this.lst_PkgInfo.Add(pkgInf);
                i++;
            }
        }
        private bool rmvPkgInf(int iSelectedIdx)
        {
            if (iSelectedIdx < 0 || iSelectedIdx >= this.lst_PkgInfo.Count) return false;
            for (int i = iSelectedIdx + 1; i < this.lst_PkgInfo.Count; i++)
            {
                this.lst_PkgInfo[i].i_Index--;
            }
            this.removeTestPlainByIndex(iSelectedIdx);
            foreach (TestPlan tp in this.lst_TestPlan)
            {
                if (tp.i_Idx > iSelectedIdx) tp.i_Idx--;
            }
            this.lst_PkgInfo.RemoveAt(iSelectedIdx);            
            return true;
        }
        private bool insertPlain()
        {
            CtrTestPage tpCurrentPlan = (CtrTestPage)this.tbCtrlTestPlain.SelectedTab.Controls[0];
            List<string> lstLag = new List<string>();
            for (int i = 0; i < this.chklstLang.Items.Count; i++)
            {
                if (this.chklstLang.GetItemChecked(i))
                {
                    lstLag.Add(this.chklstLang.Items[i].ToString().Trim());
                    this.chklstLang.SetItemChecked(i, false);
                }
            }
            if (lstLag.Count <= 0) return false;
            tpCurrentPlan.addRecord(this.txtPnp.Text.Trim(), this.cboOem.SelectedItem.ToString().Trim(), lstLag);
            return true;
        }
        private void setInTestPlan(int iSelectIdx)
        {
            this.removeTestPlainByIndex(iSelectIdx);
            foreach (TabPage tp in this.tbCtrlTestPlain.TabPages)
            {
                if (tp.Controls.Count <= 0) continue;
                CtrTestPage ctrTstPg = (CtrTestPage)tp.Controls[0];
                if (!ctrTstPg.hasRecord()) continue;
                TestPlan tstPlan = new TestPlan(iSelectIdx);
                ctrTstPg.setupRecords(ref tstPlan);
                this.lst_TestPlan.Add(tstPlan);
            }
        }
        private void removeTestPlainByIndex(int idx)
        {
            int iOldCnt = this.lst_TestPlan.Count;
            for (int i = 0; i < iOldCnt; i++)
            {
                if (this.lst_TestPlan.Count <= 0) break;
                try
                {
                    if (this.lst_TestPlan[i].i_Idx == idx)
                    {
                        this.lst_TestPlan.RemoveAt(i);
                        i--;
                    }
                }
                catch (Exception) { continue; }
            }
        }
        private void setTestPlainByIdx(int idx)
        {
            foreach (TestPlan plan in this.lst_TestPlan)
            {
                if (plan.i_Idx != idx) continue;
                foreach (TabPage tp in this.tbCtrlTestPlain.TabPages)
                {
                    try
                    {
                        if (tp.Controls.Count <= 0) continue;
                        CtrTestPage tstPage = (CtrTestPage)tp.Controls[0];
                        if (!plan.str_Os.Equals(tstPage.getOs())) continue;
                        tstPage.setTestPlan(plan.b_NeedCat, plan.lst_TestRcd);
                    }
                    catch (Exception) { continue; }
                }
            }
        }
        private List<TestRecord> getAllTestRecordList()
        {
            List<TestRecord> lstResult = new List<TestRecord>();
            XmlNode nodSys = AutoZXML.getXmlNodeByName("Systems", this.xmlBaseNode);
            if (!nodSys.HasChildNodes) return null;
            foreach (XmlElement ele in nodSys.ChildNodes)
            {
                bool bIsFirst = true;
                foreach (TestPlan tstPlan in this.lst_TestPlan)
                {
                    if (!tstPlan.str_Os.Equals(ele.Name.Trim())) continue;
                    foreach (TestRcd record in tstPlan.lst_TestRcd)
                    {
                        TestRecord recordSingle = new TestRecord();
                        recordSingle.str_OEM = record.str_Oem;
                        recordSingle.str_PnP = record.str_PnP;
                        string[] strs = record.str_Languages.Split(';');
                        string strLangFlgs = string.Empty;
                        foreach (string str in strs)
                        {
                            foreach (Lang la in this.lst_Lang)
                            {
                                if (la.LangName.Trim().Equals(str.Trim()))
                                {
                                    strLangFlgs += la.LangFlg.Trim() + ";";
                                    break;
                                }
                            }
                        }
                        if (!strLangFlgs.Trim().Equals(string.Empty))
                        {
                            strLangFlgs = strLangFlgs.Substring(0, strLangFlgs.Length - 1);
                        }
                        recordSingle.str_Language = strLangFlgs;
                        if (bIsFirst)
                        {
                            recordSingle.str_OS = tstPlan.str_Os;
                        }
                        if (tstPlan.b_NeedCat)
                        {
                            recordSingle.str_NeedCat = "1";
                        }
                        else
                        {
                            recordSingle.str_NeedCat = "0";
                        }
                        foreach (TestPkgInfo ifo in this.lst_PkgInfo)
                        {
                            if (ifo.i_Index != tstPlan.i_Idx) continue;
                            else
                            {
                                recordSingle.str_PkgName = ifo.str_PkgName;
                                recordSingle.str_PDL = ifo.str_PDL;
                                recordSingle.str_Version = ifo.str_Version;
                                recordSingle.str_CoreVersion = ifo.str_CorVer;
                                if (ifo.b_IsEFI) recordSingle.str_OEM = string.Empty;
                                break;
                            }
                        }
                        lstResult.Add(recordSingle);
                        bIsFirst = false;
                    }
                }
            }
            return lstResult;
        }
        private void createCSVByData()
        {
            string strSaveRoot = AutoZDirectorysFiles.setDirectoryFromXML("ResultRoot", this.xmlBaseNode, Application.StartupPath);
            
            string strPTCList = AutoZXML.getInnerTextByName("PTCListFile", this.xmlBaseNode);
            if (!strPTCList.Contains(".")) strPTCList += ".csv";
            string strPTCHead = AutoZXML.getInnerTextByName("PTClistHead", this.xmlBaseNode);
            StringBuilder sbPTC = new StringBuilder();
            sbPTC.AppendLine(strPTCHead);
            foreach (PTCRecord ptcRecord in this.lst_PTC)
            {
                sbPTC.AppendLine(ptcRecord.str_Spec + ","
                                + ptcRecord.str_SDF + ","
                                + ptcRecord.str_PkgNameFlg + ","
                                + ptcRecord.str_UploadPath + ","
                                + ptcRecord.str_OldPath + ","
                                + ptcRecord.str_Old32 + ","
                                + ptcRecord.str_Old64 + ","
                                + ptcRecord.str_PkgName + ","
                                + ptcRecord.str_PkgName64);
            }
            AutoZDirectorysFiles.saveFile(sbPTC, strSaveRoot + strPTCList);

            List<TestRecord> lstAllTestRecord = this.getAllTestRecordList();
            string strAllTestList = AutoZXML.getInnerTextByName("AllTestConfigFile", this.xmlBaseNode);
            if (!strAllTestList.Contains(".")) strAllTestList += ".csv";
            string strAllTestHead = AutoZXML.getInnerTextByName("AllTestHead", this.xmlBaseNode);
            StringBuilder sbAllTestConfig = new StringBuilder();
            sbAllTestConfig.AppendLine(strAllTestHead);
            if (lstAllTestRecord != null)
            {
                foreach (TestRecord rcd in lstAllTestRecord)
                {
                    sbAllTestConfig.AppendLine(rcd.str_OS + ","
                                                + rcd.str_PkgName.Substring(0, rcd.str_PkgName.Length - 4) + ","
                                                + rcd.str_Language + ","
                                                + rcd.str_OEM + ","
                                                + rcd.str_PnP + ","
                                                + rcd.str_PDL + ","
                                                + rcd.str_Version + ","
                                                + rcd.str_CoreVersion + ","
                                                + rcd.str_NeedCat);
                }
            }
            AutoZDirectorysFiles.saveFile(sbAllTestConfig, strSaveRoot + strAllTestList);
        }
    }
}
