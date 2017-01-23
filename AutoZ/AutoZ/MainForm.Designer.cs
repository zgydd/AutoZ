//////////////////
///Barton Joe
//////////////////
namespace AutoZ
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.dSrc = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlPackageInfo = new System.Windows.Forms.Panel();
            this.btnSetOk = new System.Windows.Forms.Button();
            this.gbPackageType = new System.Windows.Forms.GroupBox();
            this.spcPkgType = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDifBat = new System.Windows.Forms.CheckBox();
            this.chkSpecTSKPkg = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkLstOEM = new System.Windows.Forms.CheckedListBox();
            this.chkENUTSK = new System.Windows.Forms.CheckBox();
            this.rdDouble = new System.Windows.Forms.RadioButton();
            this.rdSingle = new System.Windows.Forms.RadioButton();
            this.chkLstLang = new System.Windows.Forms.CheckedListBox();
            this.pnlSrc = new System.Windows.Forms.Panel();
            this.chkAutoTurnOff = new System.Windows.Forms.CheckBox();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnGetSDF = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnSetSrc = new System.Windows.Forms.Button();
            this.bGetMultiSrc = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.lstSrc = new System.Windows.Forms.ListBox();
            this.bSrc = new System.Windows.Forms.Button();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.pnlPackageInfo.SuspendLayout();
            this.gbPackageType.SuspendLayout();
            this.spcPkgType.Panel1.SuspendLayout();
            this.spcPkgType.SuspendLayout();
            this.pnlSrc.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // dSrc
            // 
            this.dSrc.SelectedPath = "C:\\";
            this.dSrc.ShowNewFolderButton = false;
            // 
            // pnlPackageInfo
            // 
            this.pnlPackageInfo.Controls.Add(this.btnSetOk);
            this.pnlPackageInfo.Controls.Add(this.gbPackageType);
            this.pnlPackageInfo.Controls.Add(this.chkLstLang);
            this.pnlPackageInfo.Location = new System.Drawing.Point(2, 168);
            this.pnlPackageInfo.Name = "pnlPackageInfo";
            this.pnlPackageInfo.Size = new System.Drawing.Size(694, 294);
            this.pnlPackageInfo.TabIndex = 7;
            // 
            // btnSetOk
            // 
            this.btnSetOk.Location = new System.Drawing.Point(592, 15);
            this.btnSetOk.Name = "btnSetOk";
            this.btnSetOk.Size = new System.Drawing.Size(91, 23);
            this.btnSetOk.TabIndex = 9;
            this.btnSetOk.Text = "OK";
            this.btnSetOk.UseVisualStyleBackColor = true;
            this.btnSetOk.Click += new System.EventHandler(this.btnSetOk_Click);
            // 
            // gbPackageType
            // 
            this.gbPackageType.Controls.Add(this.spcPkgType);
            this.gbPackageType.Location = new System.Drawing.Point(179, 9);
            this.gbPackageType.Name = "gbPackageType";
            this.gbPackageType.Size = new System.Drawing.Size(379, 282);
            this.gbPackageType.TabIndex = 8;
            this.gbPackageType.TabStop = false;
            this.gbPackageType.Text = "Package Type";
            // 
            // spcPkgType
            // 
            this.spcPkgType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spcPkgType.Location = new System.Drawing.Point(6, 19);
            this.spcPkgType.Name = "spcPkgType";
            // 
            // spcPkgType.Panel1
            // 
            this.spcPkgType.Panel1.Controls.Add(this.label2);
            this.spcPkgType.Panel1.Controls.Add(this.chkDifBat);
            this.spcPkgType.Panel1.Controls.Add(this.chkSpecTSKPkg);
            this.spcPkgType.Panel1.Controls.Add(this.label1);
            this.spcPkgType.Panel1.Controls.Add(this.chkLstOEM);
            this.spcPkgType.Panel1.Controls.Add(this.chkENUTSK);
            this.spcPkgType.Panel1.Controls.Add(this.rdDouble);
            this.spcPkgType.Panel1.Controls.Add(this.rdSingle);
            // 
            // spcPkgType.Panel2
            // 
            this.spcPkgType.Panel2.AutoScroll = true;
            this.spcPkgType.Panel2.Enabled = false;
            this.spcPkgType.Size = new System.Drawing.Size(357, 257);
            this.spcPkgType.SplitterDistance = 188;
            this.spcPkgType.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "_______________________________";
            // 
            // chkDifBat
            // 
            this.chkDifBat.AutoSize = true;
            this.chkDifBat.Location = new System.Drawing.Point(6, 53);
            this.chkDifBat.Name = "chkDifBat";
            this.chkDifBat.Size = new System.Drawing.Size(117, 17);
            this.chkDifBat.TabIndex = 17;
            this.chkDifBat.Text = "Have different BAT";
            this.chkDifBat.UseVisualStyleBackColor = true;
            this.chkDifBat.CheckedChanged += new System.EventHandler(this.chkDifBat_CheckedChanged);
            // 
            // chkSpecTSKPkg
            // 
            this.chkSpecTSKPkg.AutoSize = true;
            this.chkSpecTSKPkg.Location = new System.Drawing.Point(6, 226);
            this.chkSpecTSKPkg.Name = "chkSpecTSKPkg";
            this.chkSpecTSKPkg.Size = new System.Drawing.Size(168, 17);
            this.chkSpecTSKPkg.TabIndex = 16;
            this.chkSpecTSKPkg.Text = "Specical set for TSK package";
            this.chkSpecTSKPkg.UseVisualStyleBackColor = true;
            this.chkSpecTSKPkg.CheckedChanged += new System.EventHandler(this.chkSpecTSKPkg_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "TSK Supported OEM";
            // 
            // chkLstOEM
            // 
            this.chkLstOEM.CheckOnClick = true;
            this.chkLstOEM.FormattingEnabled = true;
            this.chkLstOEM.Location = new System.Drawing.Point(6, 125);
            this.chkLstOEM.Name = "chkLstOEM";
            this.chkLstOEM.Size = new System.Drawing.Size(105, 94);
            this.chkLstOEM.TabIndex = 14;
            this.chkLstOEM.SelectedIndexChanged += new System.EventHandler(this.chkLstOEM_SelectedChanged);
            this.chkLstOEM.SelectedValueChanged += new System.EventHandler(this.chkLstOEM_SelectedChanged);
            this.chkLstOEM.Click += new System.EventHandler(this.chkLstOEM_SelectedChanged);
            // 
            // chkENUTSK
            // 
            this.chkENUTSK.AutoSize = true;
            this.chkENUTSK.Location = new System.Drawing.Point(6, 88);
            this.chkENUTSK.Name = "chkENUTSK";
            this.chkENUTSK.Size = new System.Drawing.Size(181, 17);
            this.chkENUTSK.TabIndex = 13;
            this.chkENUTSK.Text = "Have ENU disk in TSK Package";
            this.chkENUTSK.UseVisualStyleBackColor = true;
            // 
            // rdDouble
            // 
            this.rdDouble.AutoSize = true;
            this.rdDouble.Location = new System.Drawing.Point(6, 30);
            this.rdDouble.Name = "rdDouble";
            this.rdDouble.Size = new System.Drawing.Size(105, 17);
            this.rdDouble.TabIndex = 12;
            this.rdDouble.Text = "Double Package";
            this.rdDouble.UseVisualStyleBackColor = true;
            this.rdDouble.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // rdSingle
            // 
            this.rdSingle.AutoSize = true;
            this.rdSingle.Checked = true;
            this.rdSingle.Location = new System.Drawing.Point(6, 7);
            this.rdSingle.Name = "rdSingle";
            this.rdSingle.Size = new System.Drawing.Size(100, 17);
            this.rdSingle.TabIndex = 11;
            this.rdSingle.TabStop = true;
            this.rdSingle.Text = "Single Package";
            this.rdSingle.UseVisualStyleBackColor = true;
            this.rdSingle.CheckedChanged += new System.EventHandler(this.rd_CheckedChanged);
            // 
            // chkLstLang
            // 
            this.chkLstLang.CheckOnClick = true;
            this.chkLstLang.FormattingEnabled = true;
            this.chkLstLang.Location = new System.Drawing.Point(17, 15);
            this.chkLstLang.Name = "chkLstLang";
            this.chkLstLang.Size = new System.Drawing.Size(156, 274);
            this.chkLstLang.TabIndex = 7;
            this.chkLstLang.SelectedIndexChanged += new System.EventHandler(this.chkLst_Click);
            this.chkLstLang.SelectedValueChanged += new System.EventHandler(this.chkLst_Click);
            this.chkLstLang.Click += new System.EventHandler(this.chkLst_Click);
            // 
            // pnlSrc
            // 
            this.pnlSrc.Controls.Add(this.chkAutoTurnOff);
            this.pnlSrc.Controls.Add(this.btnSaveLog);
            this.pnlSrc.Controls.Add(this.btnGetSDF);
            this.pnlSrc.Controls.Add(this.btnFinish);
            this.pnlSrc.Controls.Add(this.btnSetSrc);
            this.pnlSrc.Controls.Add(this.bGetMultiSrc);
            this.pnlSrc.Controls.Add(this.bRemove);
            this.pnlSrc.Controls.Add(this.lstSrc);
            this.pnlSrc.Controls.Add(this.bSrc);
            this.pnlSrc.Location = new System.Drawing.Point(2, 12);
            this.pnlSrc.Name = "pnlSrc";
            this.pnlSrc.Size = new System.Drawing.Size(694, 156);
            this.pnlSrc.TabIndex = 8;
            // 
            // chkAutoTurnOff
            // 
            this.chkAutoTurnOff.AutoSize = true;
            this.chkAutoTurnOff.Location = new System.Drawing.Point(225, 131);
            this.chkAutoTurnOff.Name = "chkAutoTurnOff";
            this.chkAutoTurnOff.Size = new System.Drawing.Size(253, 17);
            this.chkAutoTurnOff.TabIndex = 13;
            this.chkAutoTurnOff.Text = "Turn off computer when finished(Auto save Log)";
            this.chkAutoTurnOff.UseVisualStyleBackColor = true;
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Location = new System.Drawing.Point(592, 127);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(90, 25);
            this.btnSaveLog.TabIndex = 12;
            this.btnSaveLog.Text = "Save Log";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnGetSDF
            // 
            this.btnGetSDF.Location = new System.Drawing.Point(10, 7);
            this.btnGetSDF.Name = "btnGetSDF";
            this.btnGetSDF.Size = new System.Drawing.Size(672, 27);
            this.btnGetSDF.TabIndex = 11;
            this.btnGetSDF.Text = "Get Data From SDF";
            this.btnGetSDF.UseVisualStyleBackColor = true;
            this.btnGetSDF.Click += new System.EventHandler(this.btnGetSDF_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(484, 128);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(102, 25);
            this.btnFinish.TabIndex = 10;
            this.btnFinish.Text = "Setting finished";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnSetSrc
            // 
            this.btnSetSrc.Location = new System.Drawing.Point(10, 127);
            this.btnSetSrc.Name = "btnSetSrc";
            this.btnSetSrc.Size = new System.Drawing.Size(93, 23);
            this.btnSetSrc.TabIndex = 9;
            this.btnSetSrc.Text = "Set";
            this.btnSetSrc.UseVisualStyleBackColor = true;
            this.btnSetSrc.Click += new System.EventHandler(this.btnSetSrc_Click);
            // 
            // bGetMultiSrc
            // 
            this.bGetMultiSrc.Location = new System.Drawing.Point(592, 39);
            this.bGetMultiSrc.Name = "bGetMultiSrc";
            this.bGetMultiSrc.Size = new System.Drawing.Size(90, 25);
            this.bGetMultiSrc.TabIndex = 8;
            this.bGetMultiSrc.Text = "Get Multiple";
            this.bGetMultiSrc.UseVisualStyleBackColor = true;
            this.bGetMultiSrc.Click += new System.EventHandler(this.bGetMultiSrc_Click);
            // 
            // bRemove
            // 
            this.bRemove.Location = new System.Drawing.Point(592, 97);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(90, 25);
            this.bRemove.TabIndex = 7;
            this.bRemove.Text = "Remove this";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // lstSrc
            // 
            this.lstSrc.FormattingEnabled = true;
            this.lstSrc.HorizontalScrollbar = true;
            this.lstSrc.Location = new System.Drawing.Point(10, 39);
            this.lstSrc.Name = "lstSrc";
            this.lstSrc.Size = new System.Drawing.Size(564, 82);
            this.lstSrc.TabIndex = 6;
            this.lstSrc.Click += new System.EventHandler(this.lstSrc_Click);
            // 
            // bSrc
            // 
            this.bSrc.Location = new System.Drawing.Point(592, 67);
            this.bSrc.Name = "bSrc";
            this.bSrc.Size = new System.Drawing.Size(90, 25);
            this.bSrc.TabIndex = 5;
            this.bSrc.Text = "Add Source";
            this.bSrc.UseVisualStyleBackColor = true;
            this.bSrc.Click += new System.EventHandler(this.bSrc_Click);
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.rtxtLog);
            this.gbLog.Location = new System.Drawing.Point(12, 468);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(675, 185);
            this.gbLog.TabIndex = 9;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // rtxtLog
            // 
            this.rtxtLog.Location = new System.Drawing.Point(7, 19);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.Size = new System.Drawing.Size(662, 160);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.WordWrap = false;
            this.rtxtLog.TextChanged += new System.EventHandler(this.rtxtLog_TextChanged);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(708, 665);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.pnlSrc);
            this.Controls.Add(this.pnlPackageInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fMain";
            this.Text = "AutoZ";
            this.pnlPackageInfo.ResumeLayout(false);
            this.gbPackageType.ResumeLayout(false);
            this.spcPkgType.Panel1.ResumeLayout(false);
            this.spcPkgType.Panel1.PerformLayout();
            this.spcPkgType.ResumeLayout(false);
            this.pnlSrc.ResumeLayout(false);
            this.pnlSrc.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog dSrc;
        private System.Windows.Forms.Panel pnlPackageInfo;
        private System.Windows.Forms.GroupBox gbPackageType;
        private System.Windows.Forms.CheckedListBox chkLstLang;
        private System.Windows.Forms.Panel pnlSrc;
        private System.Windows.Forms.Button btnSetSrc;
        private System.Windows.Forms.Button bGetMultiSrc;
        private System.Windows.Forms.Button bRemove;
        private System.Windows.Forms.ListBox lstSrc;
        private System.Windows.Forms.Button bSrc;
        private System.Windows.Forms.SplitContainer spcPkgType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkLstOEM;
        private System.Windows.Forms.CheckBox chkENUTSK;
        private System.Windows.Forms.RadioButton rdDouble;
        private System.Windows.Forms.RadioButton rdSingle;
        private System.Windows.Forms.CheckBox chkSpecTSKPkg;
        private System.Windows.Forms.CheckBox chkDifBat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Button btnSetOk;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnGetSDF;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.CheckBox chkAutoTurnOff;
    }
}

