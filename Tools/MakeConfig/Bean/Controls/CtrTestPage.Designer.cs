namespace MakeConfig.Bean
{
    partial class CtrTestPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrTestPage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoN = new System.Windows.Forms.RadioButton();
            this.rdoY = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOSMark = new System.Windows.Forms.Label();
            this.pnlCtrls = new System.Windows.Forms.Panel();
            this.btnRemoveOrderPlan = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoN);
            this.groupBox1.Controls.Add(this.rdoY);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(88, 41);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Need Cat";
            // 
            // rdoN
            // 
            this.rdoN.AutoSize = true;
            this.rdoN.Location = new System.Drawing.Point(46, 17);
            this.rdoN.Margin = new System.Windows.Forms.Padding(2);
            this.rdoN.Name = "rdoN";
            this.rdoN.Size = new System.Drawing.Size(39, 17);
            this.rdoN.TabIndex = 1;
            this.rdoN.TabStop = true;
            this.rdoN.Text = "No";
            this.rdoN.UseVisualStyleBackColor = true;
            // 
            // rdoY
            // 
            this.rdoY.AutoSize = true;
            this.rdoY.Location = new System.Drawing.Point(4, 17);
            this.rdoY.Margin = new System.Windows.Forms.Padding(2);
            this.rdoY.Name = "rdoY";
            this.rdoY.Size = new System.Drawing.Size(43, 17);
            this.rdoY.TabIndex = 0;
            this.rdoY.TabStop = true;
            this.rdoY.Text = "Yes";
            this.rdoY.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "OS Mark:";
            // 
            // lblOSMark
            // 
            this.lblOSMark.AutoSize = true;
            this.lblOSMark.Location = new System.Drawing.Point(161, 21);
            this.lblOSMark.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOSMark.Name = "lblOSMark";
            this.lblOSMark.Size = new System.Drawing.Size(35, 13);
            this.lblOSMark.TabIndex = 2;
            this.lblOSMark.Text = "XXXX";
            // 
            // pnlCtrls
            // 
            this.pnlCtrls.AutoScroll = true;
            this.pnlCtrls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCtrls.Location = new System.Drawing.Point(2, 48);
            this.pnlCtrls.Margin = new System.Windows.Forms.Padding(2);
            this.pnlCtrls.Name = "pnlCtrls";
            this.pnlCtrls.Size = new System.Drawing.Size(316, 135);
            this.pnlCtrls.TabIndex = 3;
            // 
            // btnRemoveOrderPlan
            // 
            this.btnRemoveOrderPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveOrderPlan.Image")));
            this.btnRemoveOrderPlan.Location = new System.Drawing.Point(274, 3);
            this.btnRemoveOrderPlan.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveOrderPlan.Name = "btnRemoveOrderPlan";
            this.btnRemoveOrderPlan.Size = new System.Drawing.Size(44, 44);
            this.btnRemoveOrderPlan.TabIndex = 4;
            this.btnRemoveOrderPlan.UseVisualStyleBackColor = true;
            this.btnRemoveOrderPlan.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemoveOrderPlan.MouseEnter += new System.EventHandler(this.btnRemove_MouseEnter);
            // 
            // CtrTestPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnRemoveOrderPlan);
            this.Controls.Add(this.pnlCtrls);
            this.Controls.Add(this.lblOSMark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrTestPage";
            this.Size = new System.Drawing.Size(328, 184);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoN;
        private System.Windows.Forms.RadioButton rdoY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOSMark;
        private System.Windows.Forms.Panel pnlCtrls;
        private System.Windows.Forms.Button btnRemoveOrderPlan;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
