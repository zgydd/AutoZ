//////////////////
///Barton Joe
//////////////////
namespace AutoZ.Controls
{
    partial class SpecialOEMCtrl
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
            this.chkSpecLang = new System.Windows.Forms.CheckBox();
            this.chkLstOEM = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // chkSpecLang
            // 
            this.chkSpecLang.AutoSize = true;
            this.chkSpecLang.Location = new System.Drawing.Point(9, 3);
            this.chkSpecLang.Name = "chkSpecLang";
            this.chkSpecLang.Size = new System.Drawing.Size(68, 17);
            this.chkSpecLang.TabIndex = 15;
            this.chkSpecLang.Text = "For Lang";
            this.chkSpecLang.UseVisualStyleBackColor = true;
            this.chkSpecLang.CheckedChanged += new System.EventHandler(this.chkSpecLang_CheckedChanged);
            // 
            // chkLstOEM
            // 
            this.chkLstOEM.CheckOnClick = true;
            this.chkLstOEM.FormattingEnabled = true;
            this.chkLstOEM.Location = new System.Drawing.Point(9, 26);
            this.chkLstOEM.Name = "chkLstOEM";
            this.chkLstOEM.Size = new System.Drawing.Size(111, 94);
            this.chkLstOEM.TabIndex = 16;
            // 
            // SpecialOEMCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSpecLang);
            this.Controls.Add(this.chkLstOEM);
            this.Name = "SpecialOEMCtrl";
            this.Size = new System.Drawing.Size(140, 125);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSpecLang;
        private System.Windows.Forms.CheckedListBox chkLstOEM;
    }
}
