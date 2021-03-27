namespace Wii
{
    partial class ImportRanking
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImportFile = new System.Windows.Forms.Button();
            this.chkYearRankingInputFile = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpenFileInputYear = new System.Windows.Forms.Button();
            this.txtFileInputPathYear = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.btnOpenFileInput = new System.Windows.Forms.Button();
            this.lblnputFilePath = new System.Windows.Forms.Label();
            this.txtAnnualFileInputPath = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(515, 253);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImportFile
            // 
            this.btnImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportFile.Location = new System.Drawing.Point(434, 253);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(75, 23);
            this.btnImportFile.TabIndex = 8;
            this.btnImportFile.Text = "入力(I)";
            this.btnImportFile.UseVisualStyleBackColor = true;
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // chkYearRankingInputFile
            // 
            this.chkYearRankingInputFile.AutoSize = true;
            this.chkYearRankingInputFile.Location = new System.Drawing.Point(12, 132);
            this.chkYearRankingInputFile.Name = "chkYearRankingInputFile";
            this.chkYearRankingInputFile.Size = new System.Drawing.Size(149, 17);
            this.chkYearRankingInputFile.TabIndex = 4;
            this.chkYearRankingInputFile.Text = " 年間ランキングも取り込む";
            this.chkYearRankingInputFile.UseVisualStyleBackColor = true;
            this.chkYearRankingInputFile.CheckedChanged += new System.EventHandler(this.chkAnnualRankingInputFile_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnOpenFileInputYear);
            this.groupBox2.Controls.Add(this.txtFileInputPathYear);
            this.groupBox2.Location = new System.Drawing.Point(12, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(578, 78);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "年間用";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "入力ファイル";
            // 
            // btnOpenFileInputYear
            // 
            this.btnOpenFileInputYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFileInputYear.Enabled = false;
            this.btnOpenFileInputYear.Location = new System.Drawing.Point(544, 25);
            this.btnOpenFileInputYear.Name = "btnOpenFileInputYear";
            this.btnOpenFileInputYear.Size = new System.Drawing.Size(26, 30);
            this.btnOpenFileInputYear.TabIndex = 7;
            this.btnOpenFileInputYear.Text = "...";
            this.btnOpenFileInputYear.UseVisualStyleBackColor = false;
            this.btnOpenFileInputYear.Click += new System.EventHandler(this.btnOpenFileInputYear_Click);
            // 
            // txtFileInputPathYear
            // 
            this.txtFileInputPathYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileInputPathYear.Enabled = false;
            this.txtFileInputPathYear.Location = new System.Drawing.Point(101, 31);
            this.txtFileInputPathYear.Name = "txtFileInputPathYear";
            this.txtFileInputPathYear.ReadOnly = true;
            this.txtFileInputPathYear.Size = new System.Drawing.Size(437, 20);
            this.txtFileInputPathYear.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtFrom);
            this.groupBox1.Controls.Add(this.btnOpenFileInput);
            this.groupBox1.Controls.Add(this.lblnputFilePath);
            this.groupBox1.Controls.Add(this.txtAnnualFileInputPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(578, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "総合用";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "~";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "yyyy/MM/dd";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(249, 78);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(104, 20);
            this.dtTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "期間";
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "yyyy/MM/dd";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(101, 78);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(112, 20);
            this.dtFrom.TabIndex = 2;
            // 
            // btnOpenFileInput
            // 
            this.btnOpenFileInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFileInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOpenFileInput.Location = new System.Drawing.Point(544, 30);
            this.btnOpenFileInput.Name = "btnOpenFileInput";
            this.btnOpenFileInput.Size = new System.Drawing.Size(28, 30);
            this.btnOpenFileInput.TabIndex = 1;
            this.btnOpenFileInput.Text = "...";
            this.btnOpenFileInput.UseVisualStyleBackColor = false;
            this.btnOpenFileInput.Click += new System.EventHandler(this.btnOpenFileInput_Click);
            // 
            // lblnputFilePath
            // 
            this.lblnputFilePath.AutoSize = true;
            this.lblnputFilePath.Location = new System.Drawing.Point(21, 40);
            this.lblnputFilePath.Name = "lblnputFilePath";
            this.lblnputFilePath.Size = new System.Drawing.Size(65, 13);
            this.lblnputFilePath.TabIndex = 11;
            this.lblnputFilePath.Text = "入力ファイル";
            // 
            // txtAnnualFileInputPath
            // 
            this.txtAnnualFileInputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAnnualFileInputPath.Location = new System.Drawing.Point(101, 36);
            this.txtAnnualFileInputPath.Name = "txtAnnualFileInputPath";
            this.txtAnnualFileInputPath.ReadOnly = true;
            this.txtAnnualFileInputPath.Size = new System.Drawing.Size(437, 20);
            this.txtAnnualFileInputPath.TabIndex = 0;
            // 
            // ImportRanking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(602, 292);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnImportFile);
            this.Controls.Add(this.chkYearRankingInputFile);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ImportRanking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ランキングリスト取込";
            this.Load += new System.EventHandler(this.ImportRanking_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblnputFilePath;
        private System.Windows.Forms.TextBox txtAnnualFileInputPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenFileInputYear;
        private System.Windows.Forms.TextBox txtFileInputPathYear;
        private System.Windows.Forms.CheckBox chkYearRankingInputFile;
        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.Button btnOpenFileInput;
        private System.Windows.Forms.Button btnClose;
    }
}