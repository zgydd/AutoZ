namespace MakeConfig.Bean
{
    partial class CtrTestRecord
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrTestRecord));
            this.chk = new System.Windows.Forms.CheckBox();
            this.lblPnpId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstLang = new System.Windows.Forms.ListBox();
            this.btnRemoveLang = new System.Windows.Forms.Button();
            this.lblOem = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Location = new System.Drawing.Point(2, 2);
            this.chk.Margin = new System.Windows.Forms.Padding(2);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(15, 14);
            this.chk.TabIndex = 0;
            this.chk.UseVisualStyleBackColor = true;
            // 
            // lblPnpId
            // 
            this.lblPnpId.AutoSize = true;
            this.lblPnpId.Location = new System.Drawing.Point(92, 3);
            this.lblPnpId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPnpId.Name = "lblPnpId";
            this.lblPnpId.Size = new System.Drawing.Size(84, 13);
            this.lblPnpId.TabIndex = 1;
            this.lblPnpId.Text = "XXXXXXXXXXX";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Language:";
            // 
            // lstLang
            // 
            this.lstLang.FormattingEnabled = true;
            this.lstLang.Location = new System.Drawing.Point(8, 41);
            this.lstLang.Margin = new System.Windows.Forms.Padding(2);
            this.lstLang.Name = "lstLang";
            this.lstLang.Size = new System.Drawing.Size(165, 30);
            this.lstLang.TabIndex = 3;
            // 
            // btnRemoveLang
            // 
            this.btnRemoveLang.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveLang.Image")));
            this.btnRemoveLang.Location = new System.Drawing.Point(177, 24);
            this.btnRemoveLang.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveLang.Name = "btnRemoveLang";
            this.btnRemoveLang.Size = new System.Drawing.Size(44, 48);
            this.btnRemoveLang.TabIndex = 6;
            this.btnRemoveLang.UseVisualStyleBackColor = true;
            this.btnRemoveLang.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemoveLang.MouseEnter += new System.EventHandler(this.btnRemove_MouseEnter);
            // 
            // lblOem
            // 
            this.lblOem.AutoSize = true;
            this.lblOem.Location = new System.Drawing.Point(18, 3);
            this.lblOem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOem.Name = "lblOem";
            this.lblOem.Size = new System.Drawing.Size(70, 13);
            this.lblOem.TabIndex = 7;
            this.lblOem.Text = "XXXXXXXXX";
            // 
            // CtrTestRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblOem);
            this.Controls.Add(this.btnRemoveLang);
            this.Controls.Add(this.lstLang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPnpId);
            this.Controls.Add(this.chk);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrTestRecord";
            this.Size = new System.Drawing.Size(223, 81);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.Label lblPnpId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstLang;
        private System.Windows.Forms.Button btnRemoveLang;
        private System.Windows.Forms.Label lblOem;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
