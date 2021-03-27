using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Utilities;
using WiiCommon;
using WiiController;
using WiiObjects;
using System.Linq;

namespace Wii
{
    public partial class OrchTSV : WiiSystemBase
    {
        private FilesCollection filesCollection = null;
        private OrchTSCVController orchTSCVController = null;

        public OrchTSV()
        {
            InitializeComponent();

            orchTSCVController = new OrchTSCVController(this.logExceptionPath);

            // Display next day
            dtServicesDate.Value = DateTime.Now.AddDays(1);
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckSelectStatus();
        }

        private bool Valid()
        {
            // サービス発表日が未入力											
            if (String.IsNullOrEmpty(dtServicesDate.Text))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE020), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtServicesDate.Focus();
                return false;
            }

            if (Utils.DateTimeCompare(dtServicesDate.Value, DateTime.Now) < 0)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE009), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtServicesDate.Focus();
                return false;
            }

            bool isEmptyCheck = true;

            foreach (var item in grpTSVOutput.Controls)
            {
                if (((CheckBox)item).Checked)
                {
                    isEmptyCheck = false;
                    break;
                }
            }

            // 取得ファイルは一つも選択されない
            if (isEmptyCheck)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE014), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // "コンテンツランキング用がチェック コンテンツが未チェック"											
            if (chkContentRanking.Checked && !chkContents.Checked)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE021), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkContents.Focus();
                return false;
            }

            // "歌手ランキングがチェック 歌手が未チェック"	
            if (chkSingerRanking.Checked && !chkSinger.Checked)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE022), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkSinger.Focus();
                return false;
            }

            // "タイアップランキングがチェック タイアップ情報が未チェック"
            if (chkTieUpRanking.Checked && !chkTieUp.Checked)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE023), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkTieUp.Focus();
                return false;
            }

            // "歌手ID変更履歴がチェック 歌手が未チェック"											
            if (chkSingerIdChangeHistories.Checked && !chkSinger.Checked)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE024), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkSinger.Focus();
                return false;
            }

            // Check chkRecommendSong default check db
            try
            {
                //おすすめ曲チェック
                if (orchTSCVController.RecommendSongCount() == 0)
                {
                    var rst = MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGA003), GetResources.GetResourceMesssage(WiiConstant.ALERT_TITLE_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst != DialogResult.Yes)
                        return false;
                }

                //Rankingチェック
                if (orchTSCVController.KaraokeYearRakingCount() == 0)
                {
                    var rst = MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGA004), GetResources.GetResourceMesssage(WiiConstant.ALERT_TITLE_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rst != DialogResult.Yes)
                        return false;
                }
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // "連番出力がチェック 連番が未入力"																			
            if (chkSerialNumberOutput.Checked && string.IsNullOrWhiteSpace(numSerial.Text))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE015), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                numSerial.Focus();
                return false;
            }

            // 出力先が存在しない					
            string folderOutputPath = Properties.Settings.Default.UTSV出力_出力パス;
            if (!Directory.Exists(folderOutputPath))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE002), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOutputPath.Focus();
                return false;
            }

            string folderWork = string.Format(Application.StartupPath + "/{0}", Properties.Settings.Default.Wii_WorkFolder);

            // ワークフォルダー・出力先が存在しない											
            if (!Directory.Exists(folderWork))
            {
                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE025), folderWork), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check Directory logfie exist
            string fileOutPutLogFileName = string.Format(Properties.Settings.Default.UTSV出力_ログファイル, Properties.Settings.Default.CONNECT_Server);
            if (!Directory.Exists(Path.GetDirectoryName(fileOutPutLogFileName)))
            {
                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), Path.GetDirectoryName(Properties.Settings.Default.UTSV出力_ログファイル)), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string workFolderPath = string.Format(Application.StartupPath + "/{0}", Properties.Settings.Default.Wii_WorkFolder);

            try
            {
                //変換のために中身はすべて削除します
                Utils.DeleteAllFileInFolder(workFolderPath);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // Valid file
            if (!CheckExistFile())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// CheckExistFile
        /// </summary>
        /// <returns></returns>
        private bool CheckExistFile()
        {
            string existFile = string.Empty;
            string fileName = string.Empty;
            string filePath = string.Empty;
            int countRankSumDuration = 0;

            List<string> list = new List<string>();

            var listItem = grpTSVOutput.Controls.Cast<CheckBox>().ToList().OrderBy(r => r.TabIndex);

            foreach (CheckBox chkItem in listItem)
            {
                if (chkItem.Checked)
                {
                    if (chkItem.Name == chkContentRanking.Name || chkItem.Name == chkSingerRanking.Name || chkItem.Name == chkTieUpRanking.Name)
                    {
                        countRankSumDuration++;
                    }

                    var existItem = filesCollection.FindByInputName(chkItem.Name);

                    fileName = existItem == null ? string.Empty : existItem.FileName;

                    filePath = existItem == null ? string.Empty : existItem.FileOutPutPath;

                    if (!string.IsNullOrEmpty(fileName) && File.Exists(filePath))
                    {
                        existFile += filePath + "\n";
                    }

                }
            }

            // Check 1 of 3 ranking checked
            if (countRankSumDuration > 0)
            {
                var existItem = filesCollection.FindByInputName(WiiConstant.ONE_OF_THREE_CHECKED_FILE_NAME);
                fileName = existItem == null ? string.Empty : existItem.FileName;
                filePath = existItem == null ? string.Empty : existItem.FileOutPutPath;

                if (!string.IsNullOrEmpty(fileName) && File.Exists(filePath))
                {
                    existFile += filePath + "\n";
                }
            }

            if (!string.IsNullOrEmpty(existFile))
            {
                DialogResult dialogResult = MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGA001), existFile), GetResources.GetResourceMesssage(WiiConstant.ALERT_TITLE_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        private void OrchTSV_Load(object sender, EventArgs e)
        {
            InitFolder();

            LoadData();
        }

        private void test()
        {

            ExportRecommendSong("", "20200202");


            String str = String.Empty;

            String result = "か゛き゛く゛け゛こ゛ﾋﾞビ你好ﾃﾚﾋﾞ(2)エ゛－２９゜゛００５゜゛，マ゛゛゜ドリード，イス゛キ゜゜゜エ゛ルダ７゛゜－４゜，ロ゛ドリ゜ゴ　デ　ゲ゜バラ゛テレヒ？朝日系木曜ト？ラマ「リーカ？ルＶ～元弁護士・小鳥遊翔子～」主題歌";
            byte[] bytesData;
            var badstringFromDatabase = "ƒ`ƒƒƒlƒ‹ƒp[ƒgƒi[‚Ì‘I‘ð";

            var hopefullyRecovered = Encoding.GetEncoding(1252).GetBytes(badstringFromDatabase);
            var oughtToBeJapanese = Encoding.GetEncoding(932).GetString(hopefullyRecovered);
            //Shift JISとして文字列に変換
            bytesData = System.Text.Encoding.GetEncoding(932).GetBytes(result);

            //Shift JISとして文字列に変換
            str = System.Text.Encoding.GetEncoding(932).GetString(bytesData);


            //UTF-8として変換
            bytesData = System.Text.Encoding.UTF8.GetBytes(result);

            //UTF-8として変換
            str = System.Text.Encoding.UTF8.GetString(bytesData);



            var orchTSCVController = new OrchTSCVController();
            DataTable exportTable = new DataTable();
            string contents = string.Empty;

            exportTable.Merge(orchTSCVController.Select3DSContentsAdd());

            FileExportEntity fileInfo = new FileExportEntity()
            {
                DataExport = exportTable,
                FilePath = Application.StartupPath + "//20200504_content.tsv",
                FunctionName = "222",
                LogPathFile = Application.StartupPath + "//log.txt",
            };

            TsvConvert.ExportToTSV(fileInfo);
        }

        /// <summary>
        /// Load data init form
        /// </summary>
        private void LoadData()
        {
            UtsvLabelObject utsvLabelObject = new UtsvLabelObject();
            lblRecommendSong.Text = string.Empty;
            lblRanking.Text = string.Empty;
            txtOutputPath.Text = Properties.Settings.Default.UTSV出力_出力パス;

            try
            {
                utsvLabelObject = orchTSCVController.GetLastUtvLabel();

                lblRecommendSong.Text = utsvLabelObject.RecommendSong;
                lblRanking.Text = utsvLabelObject.Ranking;
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Init folder
        /// </summary>
        private void InitFolder()
        {
            try
            {
                if (!Directory.Exists(WiiConstant.FOLDER_PATH_MAPRJ))
                {
                    Directory.CreateDirectory(WiiConstant.FOLDER_PATH_MAPRJ);
                }

                if (!Directory.Exists(WiiConstant.FOLDER_PATH_MAPRJ_ORCH))
                {
                    Directory.CreateDirectory(WiiConstant.FOLDER_PATH_MAPRJ_ORCH);
                }

                if (!Directory.Exists(WiiConstant.FOLDER_PATH_MAORJ_WII))
                {
                    Directory.CreateDirectory(WiiConstant.FOLDER_PATH_MAORJ_WII);
                }
                // Remove and Create temp folder default
                // ThreeDSTSV_出力パス
                string workFolderPath = Application.StartupPath + "//" + Properties.Settings.Default.Wii_WorkFolder;
                if (!Directory.Exists(workFolderPath))
                {
                    Directory.CreateDirectory(workFolderPath);
                }
            }
            catch
            {

            }
        }

        private void btnImportRecommendSong_Click(object sender, EventArgs e)
        {
            ImportRecommendSong importRecommendSong = new ImportRecommendSong();
            importRecommendSong.ShowDialog();
            if (importRecommendSong.isActive)
            {
                LoadData();
            }
        }

        private void CheckSelectStatus()
        {
            if (chkSelectAll.Checked)
            {
                chkContents.Checked = true;
                chkSinger.Checked = true;
                chkSongNameInEnglish.Checked = true;
                chkRecommendSong.Checked = true;
                chkDisRecordSong.Checked = true;
                chkContentRanking.Checked = true;
                chkAgeDistinction.Checked = true;
                chkHitParadeList.Checked = true;
                chkAddSong.Checked = true;
                chkServiceInfo.Checked = true;
                chkGeneralInfo.Checked = true;
                chkSingerNameInEnglish.Checked = true;
                chkSingingStart.Checked = true;
                chkSingerRanking.Checked = true;
                chkUpdateDay.Checked = true;
                chkHitPradeInfo.Checked = true;
                chkTieUp.Checked = true;
                chkGenreList.Checked = true;
                chkSingerIdChangeHistories.Checked = true;
                chkTieUpRanking.Checked = true;
                chkMedleyCompositionSong.Checked = true;
                chkMovieContents.Checked = true;
            }
            else
            {
                chkContents.Checked = false;
                chkSinger.Checked = false;
                chkSongNameInEnglish.Checked = false;
                chkRecommendSong.Checked = false;
                chkDisRecordSong.Checked = false;
                chkContentRanking.Checked = false;
                chkAgeDistinction.Checked = false;
                chkHitParadeList.Checked = false;
                chkAddSong.Checked = false;
                chkServiceInfo.Checked = false;
                chkGeneralInfo.Checked = false;
                chkSingerNameInEnglish.Checked = false;
                chkSingingStart.Checked = false;
                chkSingerRanking.Checked = false;
                chkUpdateDay.Checked = false;
                chkHitPradeInfo.Checked = false;
                chkTieUp.Checked = false;
                chkGenreList.Checked = false;
                chkSingerIdChangeHistories.Checked = false;
                chkTieUpRanking.Checked = false;
                chkMedleyCompositionSong.Checked = false;
                chkMovieContents.Checked = false;
            }
        }

        private void btnTSVOutput_Click(object sender, EventArgs e)
        {
            InitFileNameOrch();

            if (!Valid())
                return;

            ExportTSVProcess();
        }


        /// <summary>
        /// TSVProcess when press button btnTSVOutput
        /// </summary>
        private void ExportTSVProcess()
        {
            try
            {
                // ログファイルを削除													
                DeleteLog(LOG_ORCHTSV);

                // Add log
                AddLog("【表示楽曲TSV出力開始】" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), LOG_ORCHTSV);

                // Process  csv              
                ExportFileProcess();

                // Add log
                AddLog("【表示楽曲TSV出力終了】" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), LOG_ORCHTSV);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                Invoke(new Action(() =>
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        /// <summary>
        /// Export file process
        /// </summary>
        private void ExportFileProcess()
        {
            bgwProcess = CreateThread();
            bgwProcess.DoWork += bgwProcess_DoWork;
            bgwProcess.RunWorkerAsync();
            this.ShowWating();
        }

        private void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            CreateCSVFile();
        }

        /// <summary>
        /// Export to csv files
        /// </summary>
        private void CreateCSVFile()
        {
            string dateTime = dtServicesDate.Value.ToString("yyyyMMdd");

            try
            {
                #region 1 to 10
                //1. コンテンツ
                if (chkContents.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkContents.Name);

                    ExportContent(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkContents.Text);
                }

                //2. サービス情報
                if (chkServiceInfo.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkServiceInfo.Name);

                    ExportServices(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkServiceInfo.Text);
                }

                //3. ジャンル情報
                if (chkGeneralInfo.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkGeneralInfo.Name);

                    ExportGenereInfo(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkGeneralInfo.Text);
                }

                //4. ジャンルリスト
                if (chkGenreList.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkGenreList.Name);
                    ExportGenereList(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkGenreList.Text);
                }

                //5. タイアップ情報
                if (chkTieUp.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkTieUp.Name);
                    ExportTieup(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkTieUp.Text);
                }

                //6. 歌手
                if (chkSinger.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSinger.Name);
                    ExportSinger(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSinger.Text);
                }

                //7. 楽曲名英数読み
                if (chkSongNameInEnglish.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSongNameInEnglish.Name);
                    ExportSongNameInEnglish(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSongNameInEnglish.Text);
                }

                //8. 歌手名英数読み
                if (chkSingerNameInEnglish.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSingerNameInEnglish.Name);

                    ExportSingerNameInEnglish(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSingerNameInEnglish.Text);
                }

                //9. おすすめ曲
                if (chkRecommendSong.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkRecommendSong.Name);

                    ExportRecommendSong(existItem.FileOutPutPath, dateTime);
                }

                //10. 歌い出し
                if (chkSingingStart.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSingingStart.Name);

                    ExportSingingStart(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSingingStart.Text);
                }
                #endregion

                #region 11 to 21

                //11. 歌手ID変更履歴
                if (chkSingerIdChangeHistories.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSingerIdChangeHistories.Name);

                    ExportSingerIdChangeHistories(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSingerIdChangeHistories.Text);
                }

                //12. DISC版収録曲
                if (chkDisRecordSong.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkDisRecordSong.Name);

                    ExportDiscRecordSong(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkDisRecordSong.Text);
                }

                //13. コンテンツランキング用
                if (chkContentRanking.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkContentRanking.Name);

                    ExportContentsRank(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkContentRanking.Text);
                }

                //14. 歌手ランキング
                if (chkSingerRanking.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSingerRanking.Name);

                    ExportSingerRank(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkSingerRanking.Text);
                }

                //15. タイアップランキング
                if (chkTieUpRanking.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkTieUpRanking.Name);

                    ExportTieupRank(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkTieUpRanking.Text);
                }

                // 16.年代別ランキング
                if (chkAgeDistinction.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkAgeDistinction.Name);

                    ExportAgeDistinction(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkAgeDistinction.Text);
                }

                //17. 更新日付
                if (chkUpdateDay.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkUpdateDay.Name);

                    ExportUpdateAll(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkUpdateDay.Text);
                }

                //18. ランキング集計期間
                // Checked 1 of three コンテンツランキング用 OR 歌手ランキング OR タイアップランキング 
                if (chkContentRanking.Checked || chkSingerRanking.Checked || chkTieUpRanking.Checked)
                {
                    var existItem = filesCollection.FindByInputName(WiiConstant.ONE_OF_THREE_CHECKED_FILE_NAME);

                    ExportRankSumDuration(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, existItem.InputName);
                }

                //19. 追加曲
                if (chkAddSong.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkAddSong.Name);

                    ExportAddSong(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkAddSong.Text);
                }

                //20. 動画コンテンツ
                if (chkMovieContents.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkMovieContents.Name);

                    ExportMovieContent(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateTime, chkMovieContents.Text);
                }

                // chkHitParadeList,chkHitPradeInfo, chkMedleyCompositionSong, None
                #endregion

                //ファイルをコピーします
                CopyFileFromWorkFolderToOutputFolder(filesCollection);

                this.ClosedWaiting();

                Invoke(new Action(() =>
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGI001), Properties.Settings.Default.UTSV出力_出力パス), GetResources.GetResourceMesssage(WiiConstant.INFO_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));

            }
            catch (Exception ex)
            {
                this.ClosedWaiting();
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                Invoke(new Action(() =>
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        /// <summary>
        /// ExportContent
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportContent(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                //出力前にファイルを削除しておきます
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUContentsTable(dateTime);

                //対象データ出力（全件）
                exportTable.Merge(orchTSCVController.Select3DSContentsAdd());

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportServices
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportServices(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }

            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUServiceTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUServiceAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportGenereInfo
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportGenereInfo(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                // 比較対象のテーブルを作成
                orchTSCVController.CreateUGenreTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUGenreAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportGenereList
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportGenereList(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {

                //比較対象のテーブルを作成
                orchTSCVController.CreateUGenreListTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUGenreListAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportTieup(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUTieupTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUTieupAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportSinger 歌手
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSinger(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUSingerTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSingerAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportSongNameInEnglish 楽曲名英数読み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSongNameInEnglish(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUSongNameEisuuTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSongNameEisuuAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportSingerNameInEnglish 歌手名英数読み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSingerNameInEnglish(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUSingerEisuuTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSingerEisuuAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }


        /// <summary>
        /// Create file data recommend song 
        /// </summary>
        /// <param name="filePath">filePath</param>
        /// <param name="dataTable">dataTable</param>
        public void CreateRecommendSongFile(string filePath, DataTable dataTable)
        {

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding("shift_jis")))
                {

                    sw.NewLine = "\r\n";

                    foreach (DataRow row in dataTable.Rows)
                    {

                        string dataLine = string.Empty;
                        int columnIndex = 0;

                        foreach (DataColumn recomCol in dataTable.Columns)
                        {
                            string value = row.IsNull(columnIndex) ? string.Empty : row.Field<string>(columnIndex).ToString();

                            if (columnIndex == 0)
                            {
                                dataLine += value;
                            }
                            else
                            {
                                dataLine += "\t" + value;
                            }
                            columnIndex++;
                        }
                        sw.WriteLine(dataLine);
                    }

                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SetRecommendSong おすすめ曲
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        private void ExportRecommendSong(string deleteFileOutPutPath, string dateTime)
        {
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }

            var existItem = filesCollection.FindByInputName(chkRecommendSong.Name);

            string filePath = existItem.FullFilePath_FolderWork;

            string contents = string.Empty;

            string now_ymd = DateTime.Now.ToString("yyyyMMdd");

            //最終取込パスからファイル名取得し、パス生成
            string recommendSongFilePath = string.Format(Properties.Settings.Default.UTSV出力_おすすめ曲 + "{0}_TBLInsert{1}", now_ymd, WiiConstant.TXT_EXTENSION);

            try
            {
                // ワークテーブルの内容削除
                orchTSCVController.TruncadeRecommendSong();

                // SetRecommendSong
                DataTable tbRecommendSong = orchTSCVController.SetRecommendSong(dateTime);
                if (tbRecommendSong.Rows.Count > 0)
                {
                    // Create file with 20 columns
                    CreateRecommendSongFile(recommendSongFilePath, tbRecommendSong);

                    //ワークテーブルの内容削除
                    orchTSCVController.TruncadeRecommendSong();

                    // Copy Recommend file to server                
                    string server_filePath = string.Format(Properties.Settings.Default.UTSVRecommendSongServerFilePath, Properties.Settings.Default.CONNECT_Server);

                    // Copy file to server
                    File.Copy(recommendSongFilePath, server_filePath, true);

                    // Bulk insert
                    orchTSCVController.BulkInsertRecommendSong(server_filePath);

                    // Delete file in server おすすめ曲取込ファイルがあれば削除
                    File.Delete(server_filePath);
                }

                //比較対象のテーブルを作成
                DataTable tbData = orchTSCVController.SelectURecomendSongAll(dateTime);

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = tbData,
                    FilePath = filePath,
                    FunctionName = chkRecommendSong.Text,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), chkRecommendSong.Text), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportStarSinging 歌い出し
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSingingStart(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {

                //比較対象のテーブルを作成
                orchTSCVController.CreateUSingStartTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSingStartAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        ///11. ExportSingerIdChangeHistories 歌手ID変更履歴
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>

        private void ExportSingerIdChangeHistories(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //対象のテ比較ーブルを作成
                orchTSCVController.CreateUSingerIDChangeHistryTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSingerIDChangeHistryAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportDiscRecordSong DISC版収録曲
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportDiscRecordSong(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }

            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUDiscRecordSongTable(dateTime);

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUDiscRecordSongAll();


                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportContentsRank コンテンツランキング用
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportContentsRank(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUContentsRankTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUContentsRankAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);


                //更新日時TSV出力用に出力日時をテーブルに保存
                orchTSCVController.SetContentsRankDate();

                //  countExport++;
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportSingerRank 歌手ランキング
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSingerRank(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUSingerRankTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUSingerRankAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

                //更新日時TSV出力用に出力日時をテーブルに保存
                orchTSCVController.SetSingerRankDate();
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportTieupRank タイアップランキング
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportTieupRank(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {

                //比較対象のテーブルを作成
                orchTSCVController.CreateUTieupRankTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUTieupRankAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);


                //更新日時TSV出力用に出力日時をテーブルに保存
                orchTSCVController.SetTieupRankDate();
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportAgeDistinction 年代別ランキング
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportAgeDistinction(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUAgeDistinctionTable(dateTime);

                //年代順に書き換え
                orchTSCVController.SetKaraokeYearRanking();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUAgeDistinctionAll();
                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportUpdateAll 更新日付
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportUpdateAll(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUUpdateTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUUpdateAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportRankSumDuration ランキング集計期間
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportRankSumDuration(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }

            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateURankSumDurationTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectURankSumDurationAll();
                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportAddSong 追加曲
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportAddSong(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;

            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }

            try
            {
                //対象のテーブルを作成
                orchTSCVController.CreateUAddSongTable();

                //対象データ出力（全件）
                exportTable = orchTSCVController.SelectUAddSongAll();
                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// ExportMovieContent 動画コンテンツ
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportMovieContent(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            DataTable exportTable = new DataTable();
            string contents = string.Empty;
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
            }
            catch
            {

            }
            try
            {
                //比較対象のテーブルを作成
                orchTSCVController.CreateUMovieContentTable(dateTime);

                //対象データ出力 (全件)
                exportTable = orchTSCVController.SelectUMovieContentAll();
                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathOrchTSV
                };

                TsvConvert.ExportToTSV(fileInfo);

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_ORCHTSV);
            }
        }

        /// <summary>
        /// CopyFileWorkFolderToOutputFolder
        /// </summary>
        private void CopyFileFromWorkFolderToOutputFolder(FilesCollection fileList)
        {
            string sourceFile = string.Empty;// Work folder in created file list 

            string desFile = Properties.Settings.Default.UTSV出力_出力パス + "{0}";// Output folder config

            foreach (var file in filesCollection.GetListItem())
            {
                try
                {
                    sourceFile = file.FullFilePath_FolderWork;

                    desFile = file.FileOutPutPath;

                    if (!File.Exists(sourceFile))
                        continue;

                    File.Copy(sourceFile, desFile, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //3DS form file names
        private void InitFileNameOrch()
        {
            string date = dtServicesDate.Value.ToString("yyyyMMdd");
            string timeCurrent = DateTime.Now.ToString("hhmmss");
            filesCollection = new FilesCollection();

            string fileNameSuffix = WiiConstant.TSV_EXTENSION;
            if (chkSerialNumberOutput.Checked)
            {
                fileNameSuffix = "_" + numSerial.Value.ToString() + fileNameSuffix;
            }

            string folderPath_FolderOutPut = Properties.Settings.Default.UTSV出力_出力パス;
            string fileName = string.Empty;

            fileName = "WUコンテンツ" + fileNameSuffix;
            string fileOutPut_Folder_Work = string.Format(Application.StartupPath + "\\{0}\\", Properties.Settings.Default.Wii_WorkFolder);

            filesCollection.AddItem(new FileEntity
            {
                InputName = chkContents.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUサービス情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkServiceInfo.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUタイアップ情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkTieUp.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU歌手" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSinger.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUジャンル情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkGeneralInfo.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUジャンルリスト" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkGenreList.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU楽曲名英数読み" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSongNameInEnglish.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU歌手名英数読み" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSingerNameInEnglish.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUおすすめ曲" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkRecommendSong.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU歌いだし" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSingingStart.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUsong_id_change_history" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSingerIdChangeHistories.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUディスク版収録曲" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkDisRecordSong.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUcontents_ranking" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkContentRanking.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUsinger_ranking" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSingerRanking.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUtieup_ranking" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkTieUpRanking.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WUyears_ranking" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkAgeDistinction.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU更新日付" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkUpdateDay.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU動画コンテンツ" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkMovieContents.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });

            fileName = "WU追加曲" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkAddSong.Name,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });


            fileName = "WUランキング集計期間" + fileNameSuffix;
            // コンテンツランキング用 OR 歌手ランキング OR タイアップランキング 					
            filesCollection.AddItem(new FileEntity
            {
                InputName = WiiConstant.ONE_OF_THREE_CHECKED_FILE_NAME,
                FileName = fileName,
                FullFilePath_FolderWork = fileOutPut_Folder_Work + fileName,
                FileOutPutPath = folderPath_FolderOutPut + fileName,
            });
        }

        private void btnImportRanking_Click(object sender, EventArgs e)
        {
            ImportRanking importRanking = new ImportRanking();
            importRanking.ShowDialog();
            if (importRanking.isActive)
            {
                LoadData();
            }
        }

        private void btnConfirmLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(logFilePathOrchTSV))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE010), "ログファイル", logFilePathOrchTSV), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Utils.OpenFileByNotepad(logFilePathOrchTSV);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D)
            {
                btnImportRecommendSong_Click(null, null);
            }
            else if (keyData == Keys.R)
            {
                btnImportRanking_Click(null, null);
            }
            else if (keyData == Keys.O)
            {
                btnTSVOutput_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}