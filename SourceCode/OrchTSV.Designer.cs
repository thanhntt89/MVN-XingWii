namespace Wii
{
    partial class OrchTSV
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
            this.btnTSVOutput = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCheckDataOutput = new System.Windows.Forms.Label();
            this.lblConfirmDate = new System.Windows.Forms.Label();
            this.lblConfirmNumberOfOutputFile = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRanking = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportRecommendSong = new System.Windows.Forms.Button();
            this.lblImportRankingList = new System.Windows.Forms.Label();
            this.lblImportRecomment = new System.Windows.Forms.Label();
            this.lblRecommendSong = new System.Windows.Forms.Label();
            this.btnImportRanking = new System.Windows.Forms.Button();
            this.btnConfirmLog = new System.Windows.Forms.Button();
            this.grpTSVOutput = new System.Windows.Forms.GroupBox();
            this.chkSingerRanking = new System.Windows.Forms.CheckBox();
            this.chkUpdateDay = new System.Windows.Forms.CheckBox();
            this.chkHitPradeInfo = new System.Windows.Forms.CheckBox();
            this.chkServiceInfo = new System.Windows.Forms.CheckBox();
            this.chkGeneralInfo = new System.Windows.Forms.CheckBox();
            this.chkSingerNameInEnglish = new System.Windows.Forms.CheckBox();
            this.chkSingingStart = new System.Windows.Forms.CheckBox();
            this.chkTieUp = new System.Windows.Forms.CheckBox();
            this.chkGenreList = new System.Windows.Forms.CheckBox();
            this.chkSingerIdChangeHistories = new System.Windows.Forms.CheckBox();
            this.chkAddSong = new System.Windows.Forms.CheckBox();
            this.chkTieUpRanking = new System.Windows.Forms.CheckBox();
            this.chkMedleyCompositionSong = new System.Windows.Forms.CheckBox();
            this.chkMovieContents = new System.Windows.Forms.CheckBox();
            this.chkDisRecordSong = new System.Windows.Forms.CheckBox();
            this.chkContentRanking = new System.Windows.Forms.CheckBox();
            this.chkAgeDistinction = new System.Windows.Forms.CheckBox();
            this.chkHitParadeList = new System.Windows.Forms.CheckBox();
            this.chkContents = new System.Windows.Forms.CheckBox();
            this.chkSinger = new System.Windows.Forms.CheckBox();
            this.chkSongNameInEnglish = new System.Windows.Forms.CheckBox();
            this.chkRecommendSong = new System.Windows.Forms.CheckBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lblServiceDateNotify = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.chkSerialNumberOutput = new System.Windows.Forms.CheckBox();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.numSerial = new System.Windows.Forms.NumericUpDown();
            this.dtServicesDate = new System.Windows.Forms.DateTimePicker();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpTSVOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSerial)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTSVOutput
            // 
            this.btnTSVOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTSVOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTSVOutput.Location = new System.Drawing.Point(585, 189);
            this.btnTSVOutput.Name = "btnTSVOutput";
            this.btnTSVOutput.Size = new System.Drawing.Size(170, 35);
            this.btnTSVOutput.TabIndex = 8;
            this.btnTSVOutput.Text = "TSV出力 (O)";
            this.btnTSVOutput.UseVisualStyleBackColor = true;
            this.btnTSVOutput.Click += new System.EventHandler(this.btnTSVOutput_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(7, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 606);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCheckDataOutput);
            this.tabPage1.Controls.Add(this.lblConfirmDate);
            this.tabPage1.Controls.Add(this.lblConfirmNumberOfOutputFile);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnConfirmLog);
            this.tabPage1.Controls.Add(this.btnTSVOutput);
            this.tabPage1.Controls.Add(this.grpTSVOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(770, 580);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "全件出力";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCheckDataOutput
            // 
            this.lblCheckDataOutput.AutoSize = true;
            this.lblCheckDataOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckDataOutput.ForeColor = System.Drawing.Color.Blue;
            this.lblCheckDataOutput.Location = new System.Drawing.Point(11, 198);
            this.lblCheckDataOutput.Name = "lblCheckDataOutput";
            this.lblCheckDataOutput.Size = new System.Drawing.Size(464, 16);
            this.lblCheckDataOutput.TabIndex = 27;
            this.lblCheckDataOutput.Text = "2. 出力対象データーにチェックし、右記の「TSV出力」ボタンを押下してください。";
            // 
            // lblConfirmDate
            // 
            this.lblConfirmDate.AutoSize = true;
            this.lblConfirmDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmDate.ForeColor = System.Drawing.Color.Blue;
            this.lblConfirmDate.Location = new System.Drawing.Point(12, 170);
            this.lblConfirmDate.Name = "lblConfirmDate";
            this.lblConfirmDate.Size = new System.Drawing.Size(188, 16);
            this.lblConfirmDate.TabIndex = 28;
            this.lblConfirmDate.Text = "1. サービス発表日を確認します";
            // 
            // lblConfirmNumberOfOutputFile
            // 
            this.lblConfirmNumberOfOutputFile.AutoSize = true;
            this.lblConfirmNumberOfOutputFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmNumberOfOutputFile.ForeColor = System.Drawing.Color.Blue;
            this.lblConfirmNumberOfOutputFile.Location = new System.Drawing.Point(12, 227);
            this.lblConfirmNumberOfOutputFile.Name = "lblConfirmNumberOfOutputFile";
            this.lblConfirmNumberOfOutputFile.Size = new System.Drawing.Size(271, 16);
            this.lblConfirmNumberOfOutputFile.TabIndex = 29;
            this.lblConfirmNumberOfOutputFile.Text = "3. 出力したTSVファイルの件数を確認します。";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRanking);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnImportRecommendSong);
            this.groupBox2.Controls.Add(this.lblImportRankingList);
            this.groupBox2.Controls.Add(this.lblImportRecomment);
            this.groupBox2.Controls.Add(this.lblRecommendSong);
            this.groupBox2.Controls.Add(this.btnImportRanking);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(755, 140);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // lblRanking
            // 
            this.lblRanking.AutoSize = true;
            this.lblRanking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRanking.ForeColor = System.Drawing.Color.Red;
            this.lblRanking.Location = new System.Drawing.Point(29, 63);
            this.lblRanking.Name = "lblRanking";
            this.lblRanking.Size = new System.Drawing.Size(448, 15);
            this.lblRanking.TabIndex = 15;
            this.lblRanking.Text = "Path:(C:\\Users\\ZZPSVC23\\Desktop\\WiiU作業用\\recommend_song_20200304.tsv)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(7, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "最終取込ファイル名＝";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(17, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(314, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "年間ランキングも取り込む場合はチェックしてください。";
            // 
            // btnImportRecommendSong
            // 
            this.btnImportRecommendSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportRecommendSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportRecommendSong.Location = new System.Drawing.Point(579, 19);
            this.btnImportRecommendSong.Name = "btnImportRecommendSong";
            this.btnImportRecommendSong.Size = new System.Drawing.Size(170, 35);
            this.btnImportRecommendSong.TabIndex = 5;
            this.btnImportRecommendSong.Text = "おすすめ曲取込 (D)";
            this.btnImportRecommendSong.UseVisualStyleBackColor = true;
            this.btnImportRecommendSong.Click += new System.EventHandler(this.btnImportRecommendSong_Click);
            // 
            // lblImportRankingList
            // 
            this.lblImportRankingList.AutoSize = true;
            this.lblImportRankingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportRankingList.ForeColor = System.Drawing.Color.Blue;
            this.lblImportRankingList.Location = new System.Drawing.Point(7, 99);
            this.lblImportRankingList.Name = "lblImportRankingList";
            this.lblImportRankingList.Size = new System.Drawing.Size(205, 16);
            this.lblImportRankingList.TabIndex = 6;
            this.lblImportRankingList.Text = "2. ランキングリストを取り込みます。";
            // 
            // lblImportRecomment
            // 
            this.lblImportRecomment.AutoSize = true;
            this.lblImportRecomment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportRecomment.ForeColor = System.Drawing.Color.Blue;
            this.lblImportRecomment.Location = new System.Drawing.Point(7, 19);
            this.lblImportRecomment.Name = "lblImportRecomment";
            this.lblImportRecomment.Size = new System.Drawing.Size(214, 16);
            this.lblImportRecomment.TabIndex = 7;
            this.lblImportRecomment.Text = "1. おすすめ曲リストを取り込みます。";
            // 
            // lblRecommendSong
            // 
            this.lblRecommendSong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecommendSong.AutoSize = true;
            this.lblRecommendSong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecommendSong.ForeColor = System.Drawing.Color.Red;
            this.lblRecommendSong.Location = new System.Drawing.Point(255, 19);
            this.lblRecommendSong.Name = "lblRecommendSong";
            this.lblRecommendSong.Size = new System.Drawing.Size(162, 16);
            this.lblRecommendSong.TabIndex = 12;
            this.lblRecommendSong.Text = "済み (2020/03/04 12:15:50)";
            // 
            // btnImportRanking
            // 
            this.btnImportRanking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportRanking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportRanking.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnImportRanking.Location = new System.Drawing.Point(579, 99);
            this.btnImportRanking.Name = "btnImportRanking";
            this.btnImportRanking.Size = new System.Drawing.Size(170, 35);
            this.btnImportRanking.TabIndex = 6;
            this.btnImportRanking.Text = "ランキング取込 (R)";
            this.btnImportRanking.UseVisualStyleBackColor = true;
            this.btnImportRanking.Click += new System.EventHandler(this.btnImportRanking_Click);
            // 
            // btnConfirmLog
            // 
            this.btnConfirmLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnConfirmLog.Location = new System.Drawing.Point(585, 535);
            this.btnConfirmLog.Name = "btnConfirmLog";
            this.btnConfirmLog.Size = new System.Drawing.Size(170, 39);
            this.btnConfirmLog.TabIndex = 32;
            this.btnConfirmLog.Text = "ログ確認 >>>";
            this.btnConfirmLog.UseVisualStyleBackColor = true;
            this.btnConfirmLog.Click += new System.EventHandler(this.btnConfirmLog_Click);
            // 
            // grpTSVOutput
            // 
            this.grpTSVOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTSVOutput.Controls.Add(this.chkSingerRanking);
            this.grpTSVOutput.Controls.Add(this.chkUpdateDay);
            this.grpTSVOutput.Controls.Add(this.chkHitPradeInfo);
            this.grpTSVOutput.Controls.Add(this.chkServiceInfo);
            this.grpTSVOutput.Controls.Add(this.chkGeneralInfo);
            this.grpTSVOutput.Controls.Add(this.chkSingerNameInEnglish);
            this.grpTSVOutput.Controls.Add(this.chkSingingStart);
            this.grpTSVOutput.Controls.Add(this.chkTieUp);
            this.grpTSVOutput.Controls.Add(this.chkGenreList);
            this.grpTSVOutput.Controls.Add(this.chkSingerIdChangeHistories);
            this.grpTSVOutput.Controls.Add(this.chkAddSong);
            this.grpTSVOutput.Controls.Add(this.chkTieUpRanking);
            this.grpTSVOutput.Controls.Add(this.chkMedleyCompositionSong);
            this.grpTSVOutput.Controls.Add(this.chkMovieContents);
            this.grpTSVOutput.Controls.Add(this.chkDisRecordSong);
            this.grpTSVOutput.Controls.Add(this.chkContentRanking);
            this.grpTSVOutput.Controls.Add(this.chkAgeDistinction);
            this.grpTSVOutput.Controls.Add(this.chkHitParadeList);
            this.grpTSVOutput.Controls.Add(this.chkContents);
            this.grpTSVOutput.Controls.Add(this.chkSinger);
            this.grpTSVOutput.Controls.Add(this.chkSongNameInEnglish);
            this.grpTSVOutput.Controls.Add(this.chkRecommendSong);
            this.grpTSVOutput.Controls.Add(this.chkSelectAll);
            this.grpTSVOutput.Location = new System.Drawing.Point(9, 255);
            this.grpTSVOutput.Name = "grpTSVOutput";
            this.grpTSVOutput.Size = new System.Drawing.Size(755, 273);
            this.grpTSVOutput.TabIndex = 15;
            this.grpTSVOutput.TabStop = false;
            this.grpTSVOutput.Text = "<<出力対象TSV>>";
            // 
            // chkSingerRanking
            // 
            this.chkSingerRanking.AutoSize = true;
            this.chkSingerRanking.Checked = true;
            this.chkSingerRanking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingerRanking.Location = new System.Drawing.Point(336, 148);
            this.chkSingerRanking.Name = "chkSingerRanking";
            this.chkSingerRanking.Size = new System.Drawing.Size(95, 17);
            this.chkSingerRanking.TabIndex = 23;
            this.chkSingerRanking.Text = "歌手ランキング";
            this.chkSingerRanking.UseVisualStyleBackColor = true;
            // 
            // chkUpdateDay
            // 
            this.chkUpdateDay.AutoSize = true;
            this.chkUpdateDay.Checked = true;
            this.chkUpdateDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateDay.Location = new System.Drawing.Point(336, 171);
            this.chkUpdateDay.Name = "chkUpdateDay";
            this.chkUpdateDay.Size = new System.Drawing.Size(74, 17);
            this.chkUpdateDay.TabIndex = 24;
            this.chkUpdateDay.Text = "更新日付";
            this.chkUpdateDay.UseVisualStyleBackColor = true;
            // 
            // chkHitPradeInfo
            // 
            this.chkHitPradeInfo.AutoSize = true;
            this.chkHitPradeInfo.Checked = true;
            this.chkHitPradeInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHitPradeInfo.Location = new System.Drawing.Point(336, 194);
            this.chkHitPradeInfo.Name = "chkHitPradeInfo";
            this.chkHitPradeInfo.Size = new System.Drawing.Size(112, 17);
            this.chkHitPradeInfo.TabIndex = 25;
            this.chkHitPradeInfo.Text = "ヒッパレテーマ情報";
            this.chkHitPradeInfo.UseVisualStyleBackColor = true;
            // 
            // chkServiceInfo
            // 
            this.chkServiceInfo.AutoSize = true;
            this.chkServiceInfo.Checked = true;
            this.chkServiceInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkServiceInfo.Location = new System.Drawing.Point(336, 55);
            this.chkServiceInfo.Name = "chkServiceInfo";
            this.chkServiceInfo.Size = new System.Drawing.Size(87, 17);
            this.chkServiceInfo.TabIndex = 19;
            this.chkServiceInfo.Text = "サービス情報";
            this.chkServiceInfo.UseVisualStyleBackColor = true;
            // 
            // chkGeneralInfo
            // 
            this.chkGeneralInfo.AutoSize = true;
            this.chkGeneralInfo.Checked = true;
            this.chkGeneralInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGeneralInfo.Location = new System.Drawing.Point(336, 78);
            this.chkGeneralInfo.Name = "chkGeneralInfo";
            this.chkGeneralInfo.Size = new System.Drawing.Size(87, 17);
            this.chkGeneralInfo.TabIndex = 20;
            this.chkGeneralInfo.Text = "ジャンル情報";
            this.chkGeneralInfo.UseVisualStyleBackColor = true;
            // 
            // chkSingerNameInEnglish
            // 
            this.chkSingerNameInEnglish.AutoSize = true;
            this.chkSingerNameInEnglish.Checked = true;
            this.chkSingerNameInEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingerNameInEnglish.Location = new System.Drawing.Point(336, 101);
            this.chkSingerNameInEnglish.Name = "chkSingerNameInEnglish";
            this.chkSingerNameInEnglish.Size = new System.Drawing.Size(109, 17);
            this.chkSingerNameInEnglish.TabIndex = 21;
            this.chkSingerNameInEnglish.Text = "歌手名英数読み";
            this.chkSingerNameInEnglish.UseVisualStyleBackColor = true;
            // 
            // chkSingingStart
            // 
            this.chkSingingStart.AutoSize = true;
            this.chkSingingStart.Checked = true;
            this.chkSingingStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingingStart.Location = new System.Drawing.Point(336, 125);
            this.chkSingingStart.Name = "chkSingingStart";
            this.chkSingingStart.Size = new System.Drawing.Size(69, 17);
            this.chkSingingStart.TabIndex = 22;
            this.chkSingingStart.Text = "歌い出し";
            this.chkSingingStart.UseVisualStyleBackColor = true;
            // 
            // chkTieUp
            // 
            this.chkTieUp.AutoSize = true;
            this.chkTieUp.Checked = true;
            this.chkTieUp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTieUp.Location = new System.Drawing.Point(593, 55);
            this.chkTieUp.Name = "chkTieUp";
            this.chkTieUp.Size = new System.Drawing.Size(92, 17);
            this.chkTieUp.TabIndex = 28;
            this.chkTieUp.Text = "タイアップ情報";
            this.chkTieUp.UseVisualStyleBackColor = true;
            // 
            // chkGenreList
            // 
            this.chkGenreList.AutoSize = true;
            this.chkGenreList.Checked = true;
            this.chkGenreList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenreList.Location = new System.Drawing.Point(593, 101);
            this.chkGenreList.Name = "chkGenreList";
            this.chkGenreList.Size = new System.Drawing.Size(87, 17);
            this.chkGenreList.TabIndex = 30;
            this.chkGenreList.Text = "ジャンルリスト";
            this.chkGenreList.UseVisualStyleBackColor = true;
            // 
            // chkSingerIdChangeHistories
            // 
            this.chkSingerIdChangeHistories.AutoSize = true;
            this.chkSingerIdChangeHistories.Checked = true;
            this.chkSingerIdChangeHistories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSingerIdChangeHistories.Location = new System.Drawing.Point(336, 240);
            this.chkSingerIdChangeHistories.Name = "chkSingerIdChangeHistories";
            this.chkSingerIdChangeHistories.Size = new System.Drawing.Size(109, 17);
            this.chkSingerIdChangeHistories.TabIndex = 27;
            this.chkSingerIdChangeHistories.Text = "歌手ID変更履歴";
            this.chkSingerIdChangeHistories.UseVisualStyleBackColor = true;
            // 
            // chkAddSong
            // 
            this.chkAddSong.AutoSize = true;
            this.chkAddSong.Checked = true;
            this.chkAddSong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddSong.Location = new System.Drawing.Point(50, 240);
            this.chkAddSong.Name = "chkAddSong";
            this.chkAddSong.Size = new System.Drawing.Size(62, 17);
            this.chkAddSong.TabIndex = 18;
            this.chkAddSong.Text = "追加曲";
            this.chkAddSong.UseVisualStyleBackColor = true;
            // 
            // chkTieUpRanking
            // 
            this.chkTieUpRanking.AutoSize = true;
            this.chkTieUpRanking.Checked = true;
            this.chkTieUpRanking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTieUpRanking.Location = new System.Drawing.Point(593, 78);
            this.chkTieUpRanking.Name = "chkTieUpRanking";
            this.chkTieUpRanking.Size = new System.Drawing.Size(113, 17);
            this.chkTieUpRanking.TabIndex = 29;
            this.chkTieUpRanking.Text = "タイアップランキング";
            this.chkTieUpRanking.UseVisualStyleBackColor = true;
            // 
            // chkMedleyCompositionSong
            // 
            this.chkMedleyCompositionSong.AutoSize = true;
            this.chkMedleyCompositionSong.Checked = true;
            this.chkMedleyCompositionSong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMedleyCompositionSong.Location = new System.Drawing.Point(336, 219);
            this.chkMedleyCompositionSong.Name = "chkMedleyCompositionSong";
            this.chkMedleyCompositionSong.Size = new System.Drawing.Size(98, 17);
            this.chkMedleyCompositionSong.TabIndex = 26;
            this.chkMedleyCompositionSong.Text = "メドレー構成曲";
            this.chkMedleyCompositionSong.UseVisualStyleBackColor = true;
            // 
            // chkMovieContents
            // 
            this.chkMovieContents.AutoSize = true;
            this.chkMovieContents.Checked = true;
            this.chkMovieContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMovieContents.Location = new System.Drawing.Point(593, 124);
            this.chkMovieContents.Name = "chkMovieContents";
            this.chkMovieContents.Size = new System.Drawing.Size(94, 17);
            this.chkMovieContents.TabIndex = 31;
            this.chkMovieContents.Text = "動画コンテンツ";
            this.chkMovieContents.UseVisualStyleBackColor = true;
            // 
            // chkDisRecordSong
            // 
            this.chkDisRecordSong.AutoSize = true;
            this.chkDisRecordSong.Checked = true;
            this.chkDisRecordSong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisRecordSong.Location = new System.Drawing.Point(50, 148);
            this.chkDisRecordSong.Name = "chkDisRecordSong";
            this.chkDisRecordSong.Size = new System.Drawing.Size(99, 17);
            this.chkDisRecordSong.TabIndex = 14;
            this.chkDisRecordSong.Text = "DISC版収録曲";
            this.chkDisRecordSong.UseVisualStyleBackColor = true;
            // 
            // chkContentRanking
            // 
            this.chkContentRanking.AutoSize = true;
            this.chkContentRanking.Checked = true;
            this.chkContentRanking.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContentRanking.Location = new System.Drawing.Point(50, 171);
            this.chkContentRanking.Name = "chkContentRanking";
            this.chkContentRanking.Size = new System.Drawing.Size(127, 17);
            this.chkContentRanking.TabIndex = 15;
            this.chkContentRanking.Text = "コンテンツランキング用";
            this.chkContentRanking.UseVisualStyleBackColor = true;
            // 
            // chkAgeDistinction
            // 
            this.chkAgeDistinction.AutoSize = true;
            this.chkAgeDistinction.Checked = true;
            this.chkAgeDistinction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAgeDistinction.Location = new System.Drawing.Point(50, 194);
            this.chkAgeDistinction.Name = "chkAgeDistinction";
            this.chkAgeDistinction.Size = new System.Drawing.Size(107, 17);
            this.chkAgeDistinction.TabIndex = 16;
            this.chkAgeDistinction.Text = "年代別ランキング";
            this.chkAgeDistinction.UseVisualStyleBackColor = true;
            // 
            // chkHitParadeList
            // 
            this.chkHitParadeList.AutoSize = true;
            this.chkHitParadeList.Checked = true;
            this.chkHitParadeList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHitParadeList.Location = new System.Drawing.Point(50, 217);
            this.chkHitParadeList.Name = "chkHitParadeList";
            this.chkHitParadeList.Size = new System.Drawing.Size(112, 17);
            this.chkHitParadeList.TabIndex = 17;
            this.chkHitParadeList.Text = "ヒッパレテーマリスト";
            this.chkHitParadeList.UseVisualStyleBackColor = true;
            // 
            // chkContents
            // 
            this.chkContents.AutoSize = true;
            this.chkContents.Checked = true;
            this.chkContents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContents.Location = new System.Drawing.Point(50, 55);
            this.chkContents.Name = "chkContents";
            this.chkContents.Size = new System.Drawing.Size(70, 17);
            this.chkContents.TabIndex = 10;
            this.chkContents.Text = "コンテンツ";
            this.chkContents.UseVisualStyleBackColor = true;
            // 
            // chkSinger
            // 
            this.chkSinger.AutoSize = true;
            this.chkSinger.Checked = true;
            this.chkSinger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSinger.Location = new System.Drawing.Point(50, 78);
            this.chkSinger.Name = "chkSinger";
            this.chkSinger.Size = new System.Drawing.Size(50, 17);
            this.chkSinger.TabIndex = 11;
            this.chkSinger.Text = "歌手";
            this.chkSinger.UseVisualStyleBackColor = true;
            // 
            // chkSongNameInEnglish
            // 
            this.chkSongNameInEnglish.AutoSize = true;
            this.chkSongNameInEnglish.Checked = true;
            this.chkSongNameInEnglish.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSongNameInEnglish.Location = new System.Drawing.Point(50, 101);
            this.chkSongNameInEnglish.Name = "chkSongNameInEnglish";
            this.chkSongNameInEnglish.Size = new System.Drawing.Size(109, 17);
            this.chkSongNameInEnglish.TabIndex = 12;
            this.chkSongNameInEnglish.Text = "楽曲名英数読み";
            this.chkSongNameInEnglish.UseVisualStyleBackColor = true;
            // 
            // chkRecommendSong
            // 
            this.chkRecommendSong.AutoSize = true;
            this.chkRecommendSong.Checked = true;
            this.chkRecommendSong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecommendSong.Location = new System.Drawing.Point(50, 124);
            this.chkRecommendSong.Name = "chkRecommendSong";
            this.chkRecommendSong.Size = new System.Drawing.Size(78, 17);
            this.chkRecommendSong.TabIndex = 13;
            this.chkRecommendSong.Text = "おすすめ曲";
            this.chkRecommendSong.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.Location = new System.Drawing.Point(27, 29);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(55, 17);
            this.chkSelectAll.TabIndex = 9;
            this.chkSelectAll.Text = "すべて";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // lblServiceDateNotify
            // 
            this.lblServiceDateNotify.AutoSize = true;
            this.lblServiceDateNotify.Location = new System.Drawing.Point(8, 11);
            this.lblServiceDateNotify.Name = "lblServiceDateNotify";
            this.lblServiceDateNotify.Size = new System.Drawing.Size(80, 13);
            this.lblServiceDateNotify.TabIndex = 9;
            this.lblServiceDateNotify.Text = "サービス発表日";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputPath.Location = new System.Drawing.Point(113, 34);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.ReadOnly = true;
            this.txtOutputPath.Size = new System.Drawing.Size(662, 20);
            this.txtOutputPath.TabIndex = 3;
            // 
            // chkSerialNumberOutput
            // 
            this.chkSerialNumberOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSerialNumberOutput.AutoSize = true;
            this.chkSerialNumberOutput.Location = new System.Drawing.Point(456, 8);
            this.chkSerialNumberOutput.Name = "chkSerialNumberOutput";
            this.chkSerialNumberOutput.Size = new System.Drawing.Size(74, 17);
            this.chkSerialNumberOutput.TabIndex = 2;
            this.chkSerialNumberOutput.Text = "連番出力";
            this.chkSerialNumberOutput.UseVisualStyleBackColor = true;
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Location = new System.Drawing.Point(308, 10);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(31, 13);
            this.lblSerialNumber.TabIndex = 15;
            this.lblSerialNumber.Text = "連番";
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(10, 37);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(43, 13);
            this.lblOutputPath.TabIndex = 14;
            this.lblOutputPath.Text = "出力先";
            // 
            // numSerial
            // 
            this.numSerial.Location = new System.Drawing.Point(345, 7);
            this.numSerial.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSerial.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSerial.Name = "numSerial";
            this.numSerial.Size = new System.Drawing.Size(83, 20);
            this.numSerial.TabIndex = 1;
            this.numSerial.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dtServicesDate
            // 
            this.dtServicesDate.CustomFormat = "yyyy/MM/dd";
            this.dtServicesDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtServicesDate.Location = new System.Drawing.Point(113, 9);
            this.dtServicesDate.Name = "dtServicesDate";
            this.dtServicesDate.Size = new System.Drawing.Size(158, 20);
            this.dtServicesDate.TabIndex = 0;
            this.dtServicesDate.Value = new System.DateTime(2020, 3, 4, 0, 0, 0, 0);
            // 
            // OrchTSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 671);
            this.Controls.Add(this.numSerial);
            this.Controls.Add(this.dtServicesDate);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblServiceDateNotify);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.chkSerialNumberOutput);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblOutputPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OrchTSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Orch TSV 出力";
            this.Load += new System.EventHandler(this.OrchTSV_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpTSVOutput.ResumeLayout(false);
            this.grpTSVOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSerial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTSVOutput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnImportRecommendSong;
        private System.Windows.Forms.Label lblImportRankingList;
        private System.Windows.Forms.Label lblImportRecomment;
        private System.Windows.Forms.Label lblRecommendSong;
        private System.Windows.Forms.Button btnImportRanking;
        private System.Windows.Forms.Button btnConfirmLog;
        private System.Windows.Forms.Label lblServiceDateNotify;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.CheckBox chkSerialNumberOutput;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Label lblCheckDataOutput;
        private System.Windows.Forms.Label lblConfirmDate;
        private System.Windows.Forms.Label lblConfirmNumberOfOutputFile;
        private System.Windows.Forms.NumericUpDown numSerial;
        private System.Windows.Forms.DateTimePicker dtServicesDate;
        private System.Windows.Forms.GroupBox grpTSVOutput;
        private System.Windows.Forms.CheckBox chkSingerRanking;
        private System.Windows.Forms.CheckBox chkUpdateDay;
        private System.Windows.Forms.CheckBox chkHitPradeInfo;
        private System.Windows.Forms.CheckBox chkServiceInfo;
        private System.Windows.Forms.CheckBox chkGeneralInfo;
        private System.Windows.Forms.CheckBox chkSingerNameInEnglish;
        private System.Windows.Forms.CheckBox chkSingingStart;
        private System.Windows.Forms.CheckBox chkTieUp;
        private System.Windows.Forms.CheckBox chkGenreList;
        private System.Windows.Forms.CheckBox chkSingerIdChangeHistories;
        private System.Windows.Forms.CheckBox chkAddSong;
        private System.Windows.Forms.CheckBox chkTieUpRanking;
        private System.Windows.Forms.CheckBox chkMedleyCompositionSong;
        private System.Windows.Forms.CheckBox chkMovieContents;
        private System.Windows.Forms.CheckBox chkDisRecordSong;
        private System.Windows.Forms.CheckBox chkContentRanking;
        private System.Windows.Forms.CheckBox chkAgeDistinction;
        private System.Windows.Forms.CheckBox chkContents;
        private System.Windows.Forms.CheckBox chkSinger;
        private System.Windows.Forms.CheckBox chkSongNameInEnglish;
        private System.Windows.Forms.CheckBox chkRecommendSong;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRanking;
        private System.Windows.Forms.CheckBox chkHitParadeList;
    }
}