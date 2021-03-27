namespace Wii
{
    partial class WiiMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WiiMain));
            this.panelSqlChecking = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btn3DSTSVOutput = new System.Windows.Forms.Button();
            this.btnOrchTSVOutPut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVersionInfo = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.panelSqlChecking.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSqlChecking
            // 
            this.panelSqlChecking.Controls.Add(this.label2);
            this.panelSqlChecking.Location = new System.Drawing.Point(198, 184);
            this.panelSqlChecking.Name = "panelSqlChecking";
            this.panelSqlChecking.Size = new System.Drawing.Size(381, 212);
            this.panelSqlChecking.TabIndex = 7;
            this.panelSqlChecking.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(124, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "データーベース接続中";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.Controls.Add(this.btn3DSTSVOutput);
            this.panelMain.Controls.Add(this.btnOrchTSVOutPut);
            this.panelMain.Location = new System.Drawing.Point(144, 143);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(476, 97);
            this.panelMain.TabIndex = 6;
            // 
            // btn3DSTSVOutput
            // 
            this.btn3DSTSVOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn3DSTSVOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn3DSTSVOutput.ImageIndex = 0;
            this.btn3DSTSVOutput.Location = new System.Drawing.Point(25, 3);
            this.btn3DSTSVOutput.Name = "btn3DSTSVOutput";
            this.btn3DSTSVOutput.Size = new System.Drawing.Size(200, 50);
            this.btn3DSTSVOutput.TabIndex = 2;
            this.btn3DSTSVOutput.Tag = "108";
            this.btn3DSTSVOutput.Text = "3DS TSV出力(I)";
            this.btn3DSTSVOutput.UseVisualStyleBackColor = true;
            this.btn3DSTSVOutput.Click += new System.EventHandler(this.btn3DSTSVOutput_Click);
            // 
            // btnOrchTSVOutPut
            // 
            this.btnOrchTSVOutPut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnOrchTSVOutPut.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOrchTSVOutPut.ImageIndex = 1;
            this.btnOrchTSVOutPut.Location = new System.Drawing.Point(252, 3);
            this.btnOrchTSVOutPut.Name = "btnOrchTSVOutPut";
            this.btnOrchTSVOutPut.Size = new System.Drawing.Size(200, 50);
            this.btnOrchTSVOutPut.TabIndex = 1;
            this.btnOrchTSVOutPut.Tag = "106";
            this.btnOrchTSVOutPut.Text = "Orch TSV出力(G)";
            this.btnOrchTSVOutPut.UseVisualStyleBackColor = true;
            this.btnOrchTSVOutPut.Click += new System.EventHandler(this.btnOrchTSVOutPut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ver 1.0.0";
            // 
            // btnVersionInfo
            // 
            this.btnVersionInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnVersionInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVersionInfo.Location = new System.Drawing.Point(634, 7);
            this.btnVersionInfo.Name = "btnVersionInfo";
            this.btnVersionInfo.Size = new System.Drawing.Size(151, 36);
            this.btnVersionInfo.TabIndex = 0;
            this.btnVersionInfo.Text = "バージョン情報";
            this.btnVersionInfo.UseVisualStyleBackColor = true;
            this.btnVersionInfo.Visible = false;
            this.btnVersionInfo.Click += new System.EventHandler(this.btnVersionInfo_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.BackColor = System.Drawing.SystemColors.Control;
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnFinish.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnFinish.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFinish.Location = new System.Drawing.Point(634, 434);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(151, 38);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "終了";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // WiiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(797, 484);
            this.Controls.Add(this.panelSqlChecking);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVersionInfo);
            this.Controls.Add(this.btnFinish);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WiiMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wii メニュー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WiiMain_FormClosing);
            this.Load += new System.EventHandler(this.WiiMain_Load);
            this.panelSqlChecking.ResumeLayout(false);
            this.panelSqlChecking.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrchTSVOutPut;
        private System.Windows.Forms.Button btn3DSTSVOutput;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnVersionInfo;
        private System.Windows.Forms.Label label1;        
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelSqlChecking;
        private System.Windows.Forms.Label label2;
    }
}