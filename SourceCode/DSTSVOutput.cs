using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Utilities;
using WiiCommon;
using WiiController;
using WiiObjects;
using System.Linq;
using System.Text;

namespace Wii
{
    public partial class DSTSVOutput : WiiSystemBase
    {
        private FilesCollection filesCollection = null;

        private ThreeDSTSVController threeDSTSVController = null;
        // private int countExport = 0;
        private ItemCollection serchItemSelected = new ItemCollection();
        private BackgroundWorker bgwExport = null;
        private BackgroundWorker bgwCheckSqlConnection = null;

        // List item collection
        public DSTSVOutput()
        {
            InitializeComponent();
            threeDSTSVController = new ThreeDSTSVController();
        }

        private void CheckSqlConnectionProcess()
        {
            bgwCheckSqlConnection = CreateThread();
            bgwCheckSqlConnection.DoWork += BgwCheckSqlConnection_DoWork;
            bgwCheckSqlConnection.RunWorkerAsync();
        }

        private void BgwCheckSqlConnection_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLable();
        }

        private void LoadLable()
        {
            Invoke(new Action(() =>
            {
                try
                {
                    ParameterObjet parameterObjet = threeDSTSVController.GetParameterByFunctioNameAndParamterName(WiiConstant.FUNCTION_VALUE_3DSTSV, WiiConstant.PARAMTER__VALUE_NAME_3DSTSV);

                    lblParameter.Text = "済み：" + parameterObjet.Parameters;

                    // Delete table  WiiTmp.[dbo.tbl_Wrk_Wiiコンテンツ_{strComputerName}]
                    threeDSTSVController.TruncateTableContentByPCName();
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
            }));

        }

        /// <summary>
        /// Load init form
        /// </summary>
        private void LoadInitData()
        {
            txtOutputPath.Text = Properties.Settings.Default.ThreeDSTSV_出力パス;
            dtServicesDate.Value = DateTime.Now.AddDays(1);
            try
            {
                // Remove and Create temp folder default
                // ThreeDSTSV_出力パス
                string workFolderPath = Application.StartupPath + "//" + Properties.Settings.Default.Wii_WorkFolder;
                if (!Directory.Exists(workFolderPath))
                {
                    Directory.CreateDirectory(workFolderPath);
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
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                chkContents.Checked = true;
                chkServiceInfo.Checked = true;
                chkEnglishSong.Checked = true;
                chkCollection.Checked = true;

                chkSinger.Checked = true;
                chkGeneralInfo.Checked = true;
                chkTieUp.Checked = true;
                chkRankingBroad.Checked = true;
                chkSingerNameInEnglish.Checked = true;
                chkSearchingBySingerName.Checked = true;
            }
            else
            {
                chkContents.Checked = false;
                chkSinger.Checked = false;
                chkGeneralInfo.Checked = false;
                chkTieUp.Checked = false;
                chkServiceInfo.Checked = false;
                chkRankingBroad.Checked = false;
                chkEnglishSong.Checked = false;
                chkSingerNameInEnglish.Checked = false;
                chkSearchingBySingerName.Checked = false;
                chkCollection.Checked = false;
            }
        }

        private void btnTSVOutput_Click(object sender, EventArgs e)
        {
            // Create file Name to export
            InitFileName3DS();

            if (!Valid())
                return;

            Export();


        }

        private bool Valid()
        {
            //1.サービス発表日が未入力											
            if (String.IsNullOrEmpty(dtServicesDate.Text))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE012), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtServicesDate.Focus();
                return false;
            }

            if (Utils.DateTimeCompare(dtServicesDate.Value, DateTime.Now) < 0)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE009), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtServicesDate.Focus();
                return false;
            }

            //2. 収集リストは、コンテンツとセットチェック	
            if (chkCollection.Checked && !chkContents.Checked)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE013), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                chkContents.Focus();
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

            //3.  チェックボックスがすべて未チェック											
            if (isEmptyCheck)
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE014), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //4. "連番出力がチェック 連番が未入力"																			
            if (chkSerialOutput.Checked && string.IsNullOrWhiteSpace(numSerial.Text))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE015), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                numSerial.Focus();
                return false;
            }

            //5. 出力先パスの存在チェック											
            if (!Directory.Exists(txtOutputPath.Text))
            {
                MessageBox.Show(String.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), txtOutputPath.Text), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOutputPath.Focus();
                return false;
            }

            //6. "ランキングボード用パスの存在チェック iniから取得"	
            string fileRankingPath = Properties.Settings.Default.ThreeDSTSV_ランキングボード用パス;
            if (chkRankingBroad.Checked && !Directory.Exists(fileRankingPath))
            {
                MessageBox.Show(String.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), fileRankingPath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //7. "収集リストパスの存在チェック iniから取得"		
            string fileCollectionPath = Properties.Settings.Default.ThreeDSTSV_収集リストパス;
            if (chkCollection.Checked && !Directory.Exists(fileCollectionPath))
            {
                MessageBox.Show(String.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), fileCollectionPath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //8. "ログファイルパスの存在チェック  iniから取得"
            if (!InitDirectory())
            {
                return false;
            }

            string workFolderPath = string.Format(Application.StartupPath + "\\{0}", Properties.Settings.Default.Wii_WorkFolder);
            //9.  ワークフォルダー・出力先が存在しない											
            if (!Directory.Exists(workFolderPath))
            {
                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE017), workFolderPath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Delete File Folder work
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

            // 10. Valid file overwrite
            //ThreeDSTSV_歌手取込配置ファイル
            if (!CheckExistFile())
            {
                return false;
            }

            // If 選曲番号で出力 ( rdSelectById checked) notify E014 check count database count table WiiTmp.dbo.[tbl_Wrk_Wiiコンテンツ_{strComputerName}]
            if (chkSelectById.Checked)
            {
                try
                {
                    if (threeDSTSVController.CountContentsByPCName() == 0)
                    {
                        MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE036), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }

            return true;
        }
        //Check exist file in system
        private bool CheckExistFile()
        {
            string msg = string.Empty;
            int fileExist = 0;
            string fileName = string.Empty;
            string filePath = string.Empty;
            var listItem = grpTSVOutput.Controls.Cast<CheckBox>().ToList().OrderBy(r => r.TabIndex);

            foreach (var chkItem in listItem)
            {
                if (chkItem.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkItem.Name);

                    fileName = existItem == null ? string.Empty : existItem.FileName;
                    filePath = existItem == null ? string.Empty : existItem.FileOutPutPath;

                    if (!string.IsNullOrEmpty(fileName) && File.Exists(filePath))
                    {
                        msg += filePath + "\n";
                        fileExist++;
                    }
                }
            }

            if (fileExist > 0)
            {
                DialogResult dialogResult = MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGA001), msg), GetResources.GetResourceMesssage(WiiConstant.ALERT_TITLE_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }

        private void btnSelectById_Click(object sender, EventArgs e)
        {
            SearchByID searchByID = new SearchByID();
            searchByID.serchItemSelected = serchItemSelected;
            searchByID.ShowDialog();
            serchItemSelected = searchByID.serchItemSelected;
        }

        //3DS form file names
        private void InitFileName3DS()
        {
            filesCollection = new FilesCollection();
            string date = dtServicesDate.Value.ToString("yyyyMMdd");
            string timeCurrent = DateTime.Now.ToString("HHmmss");

            string fileNameSuffix = WiiConstant.TSV_EXTENSION;
            if (chkSerialOutput.Checked)
            {
                fileNameSuffix = "_" + numSerial.Value.ToString() + fileNameSuffix;
            }
            string folderOutPutPath = Properties.Settings.Default.ThreeDSTSV_出力パス;
            string fileName = string.Empty;
            string fileNameWork = string.Empty;

            string folder_Work = Application.StartupPath + "\\" + Properties.Settings.Default.Wii_WorkFolder + "\\";

            fileName = "3DSコンテンツ" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkContents.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            fileName = "3DS歌手" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSinger.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            fileName = "3DSジャンル情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkGeneralInfo.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            fileName = "3DSタイアップ情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkTieUp.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            fileName = "3DSサービス情報" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkServiceInfo.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            // Exception out put path

            fileName = date + "_" + timeCurrent + "rnk3DSsong" + fileNameSuffix;
            fileName = fileName.Substring(fileName.Length - 29, 29);
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkRankingBroad.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = Properties.Settings.Default.ThreeDSTSV_ランキングボード用パス + fileName,
            });


            if (chkSerialOutput.Checked)
            {
                fileName = date + "_楽曲名英数読み" + fileNameSuffix;
            }
            else
            {
                fileName = "3DS楽曲名英数読み" + fileNameSuffix;
            }

            fileNameWork = "3DS楽曲名英数読み" + fileNameSuffix;

            filesCollection.AddItem(new FileEntity
            {
                InputName = chkEnglishSong.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileNameWork,
                FileOutPutPath = folderOutPutPath + fileName
            });

            if (chkSerialOutput.Checked)
            {
                fileName = date + "_歌手名英数読み" + fileNameSuffix;
            }
            else
            {
                fileName = "3DS歌手名英数読み" + fileNameSuffix;
            }

            fileNameWork = "3DS歌手名英数読み" + fileNameSuffix;

            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSingerNameInEnglish.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileNameWork,
                FileOutPutPath = folderOutPutPath + fileName
            });

            fileName = "3DS歌手名複数名検索" + fileNameSuffix;
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkSearchingBySingerName.Name,
                FileName = fileName,
                FullFilePath_FolderWork = folder_Work + fileName,
                FileOutPutPath = folderOutPutPath + fileName
            });

            // Exception out put path
            fileName = "list.txt";
            filesCollection.AddItem(new FileEntity
            {
                InputName = chkCollection.Name,
                FileName = fileName,
                FullFilePath_FolderWork = Properties.Settings.Default.ThreeDSTSV_収集リストパス + fileName,
                FileOutPutPath = Properties.Settings.Default.ThreeDSTSV_収集リストパス + fileName,
            });
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            Finished();
        }

        /// <summary>
        /// Finished
        /// </summary>
        private void Finished()
        {
            DialogResult rst = MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGA002), GetResources.GetResourceMesssage(WiiConstant.ALERT_TITLE_MESSAGE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rst != DialogResult.Yes)
                return;

            try
            {
                // Content checked
                // chkコンテンツがチェックされる場合											
                if (chkContents.Checked)
                    threeDSTSVController.Replace3DSContentsBaseTable();

                // Service checked
                // chkサービス情報がチェックされる場合											
                if (chkServiceInfo.Checked)
                    threeDSTSVController.Replace3DSServiceBaseTable();

                // Ranking checked
                // chkランキングボードがチェックされる場合											
                if (chkRankingBroad.Checked)
                    threeDSTSVController.Replace3DSRankingBoardBaseTable();

                // English song checked
                // chk楽曲名英数読みがチェックされる場合											
                if (chkEnglishSong.Checked)
                    threeDSTSVController.Replace3DSSongNameEisuuBaseTable();

                // Update
                // 出力完了日付の更新											
                threeDSTSVController.Update3DSTSVTable();

                // Load data from db
                ParameterObjet parameterObjet = threeDSTSVController.GetParameterByFunctioNameAndParamterName(WiiConstant.FUNCTION_VALUE_3DSTSV, WiiConstant.PARAMTER__VALUE_NAME_3DSTSV);

                lblParameter.Text = "済み：" + parameterObjet.Parameters;

                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGI002), GetResources.GetResourceMesssage(WiiConstant.INFO_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //init directories
        private bool InitDirectory()
        {
            try
            {
                // FolderPath
                string pathImport = Properties.Settings.Default.ThreeDSTSV_出力パス;

                // If path is not directory or not exist notify message
                if (!Utils.CheckPathIsFolder(pathImport))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), pathImport), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //pathImport = Properties.Settings.Default.ThreeDSTSV_出力パス + @"1";
                if (!Directory.Exists(pathImport))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), pathImport), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //remove + create log file
                string filePathLog = Properties.Settings.Default.ThreeDSTSV_ログファイル;

                // Directory not exist notify message                
                if (!Directory.Exists(Path.GetDirectoryName(filePathLog)))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE016), Path.GetDirectoryName(filePathLog)), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // If files exist delete and create a new file
                Utils.DeleteExistAndCreateNewFile(filePathLog);

                return true;

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
        }

        private void Export()
        {
            bgwExport = CreateThread();
            bgwExport.DoWork += BgwExport_DoWork;
            bgwExport.RunWorkerAsync();

            this.ShowWating();
        }

        private void BgwExport_DoWork(object sender, DoWorkEventArgs e)
        {

            //add start status to log
            AddLog("【表示楽曲TSV出力開始】" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), LOG_THREEDSTV);

            ExportProcess();

            //add susscess status to log
            AddLog("【表示楽曲TSV出力終了】" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), LOG_THREEDSTV);
        }

        /// <summary>
        /// Export process
        /// </summary>
        private void ExportProcess()
        {
            string dateFrm = dtServicesDate.Value.ToString("yyyyMMdd");
            // countExport = 0;

            try
            {
                //コンテンツ
                if (chkContents.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkContents.Name);
                    ExportContents(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkContents.Text);
                }

                //歌手
                if (chkSinger.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSinger.Name);
                    ExportSinger(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkSinger.Text);
                }

                //ジャンル情報
                if (chkGeneralInfo.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkGeneralInfo.Name);
                    ExportGenre(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkGeneralInfo.Text);
                }

                //タイアップ情報
                if (chkTieUp.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkTieUp.Name);
                    ExportTieup(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkTieUp.Text);
                }

                //サービス情報
                if (chkServiceInfo.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkServiceInfo.Name);
                    ExportServices(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkServiceInfo.Text);
                }

                //ランキングボード
                if (chkRankingBroad.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkRankingBroad.Name);
                    ExportRankingBoard(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkRankingBroad.Text);
                }

                //楽曲名英数読み
                if (chkEnglishSong.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkEnglishSong.Name);
                    ExportSongNameInEisuu(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkEnglishSong.Text);
                }

                //歌手名英数読み
                if (chkSingerNameInEnglish.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSingerNameInEnglish.Name);
                    ExportSingerEisuu(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkSingerNameInEnglish.Text);
                }
                //歌手名複数名検索
                if (chkSearchingBySingerName.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkSearchingBySingerName.Name);
                    ExportSingerPeopleSearch(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkSearchingBySingerName.Text);
                }
                //収集リスト
                if (chkCollection.Checked)
                {
                    var existItem = filesCollection.FindByInputName(chkCollection.Name);
                    ExportCollectionList(existItem.FileOutPutPath, existItem.FullFilePath_FolderWork, dateFrm, chkCollection.Text);
                }
                // 変換バッチをコールします
                int rst = RunBatchFiles();

                if (rst == -1)
                {
                    this.ClosedWaiting();
                    AddLog(string.Format("--- {0} -> 変換バッチ：失敗", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")), LOG_THREEDSTV);
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE019), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
                else if (rst == 0)
                {
                    AddLog(string.Format("--- {0} -> 変換バッチ：成功", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")), LOG_THREEDSTV);
                }

                //ファイルをコピーします
                CopyFileFromWorkFolderToOutputFolder(filesCollection);

                this.ClosedWaiting();

                Invoke(new Action(() =>
                {
                    MessageBox.Show(String.Format(GetResources.GetResourceMesssage(WiiConstant.MSGI001), Properties.Settings.Default.ThreeDSTSV_出力パス), GetResources.GetResourceMesssage(WiiConstant.INFO_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        ///1,2. ExportContents
        /// </summary>
        private void ExportContents(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            string contents = string.Empty;

            //出力前にファイルを削除しておきます
            string pathFileDelete = string.Format(Properties.Settings.Default.ThreeDSTSV_出力パス + "{0}", WiiConstant.EXPORT_CONTENT_DELETE_FILE_NAME);

            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
                Utils.DeleteFile(pathFileDelete);
            }
            catch
            {

            }

            try
            {
                DataTable exportTable = new DataTable();

                //処理対象のテーブルがあればそちらから出力
                if (!chkSelectById.Checked)
                {
                    //比較対象のテーブルを作成
                    threeDSTSVController.Create3DSContentsTable(dateTime);

                    //対象データ出力（追加）
                    exportTable.Merge(threeDSTSVController.Select3DSContentsAdd());

                    //対象データ出力（更新）
                    exportTable.Merge(threeDSTSVController.Select3DSContentsDiff());

                    //対象データ出力（削除）
                    DataTable content3DSDeleteTable = threeDSTSVController.Select3DSContentsDel();

                    FileExportEntity file3Ds = new FileExportEntity()
                    {
                        DataExport = content3DSDeleteTable,
                        FilePath = pathFileDelete,
                        FunctionName = "コンテンツ削除",
                        LogPathFile = logFilePathThreeDSTV
                    };

                    TsvConvert.ExportToTSV(file3Ds);
                }
                else if (chkSelectById.Checked)
                {
                    exportTable.Merge(threeDSTSVController.ContentSelectQuery(dateTime));
                }

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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
                AddLog(string.Format("--- {0} -> {1}： 出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 3. SingerExport
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSinger(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSSingerTable(dateTime);

                //対象データ出力（全件）
                DataTable singerTable = threeDSTSVController.Select3DSSingerAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = singerTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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
                AddLog(string.Format("--- {0} -> {1}： 出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 5. GenreExport
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportGenre(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSGenreTable(dateTime);

                //対象データ出力（全件）
                DataTable genreTable = threeDSTSVController.Select3DSGenreAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = genreTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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
                AddLog(string.Format("--- {0} -> {1}： 出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 6. TieupExport
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportTieup(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSTieupTable(dateTime);

                //対象データ出力（全件）
                DataTable tieupTable = threeDSTSVController.Select3DSTieupAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = tieupTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 9. ServicesExport
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportServices(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            string contents = string.Empty;
            string pathFileDelete = string.Format(Properties.Settings.Default.ThreeDSTSV_出力パス + "{0}", WiiConstant.EXPORT_SERVICE_NAME_DELETE_FILE_NAME);
            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
                Utils.DeleteFile(pathFileDelete);
            }
            catch
            {

            }

            try
            {
                DataTable exportTable = new DataTable();

                if (!chkSelectById.Checked)
                {
                    //比較対象のテーブルを作成
                    threeDSTSVController.Create3DSServiceTable(dateTime);

                    //対象データ出力（追加）
                    exportTable.Merge(threeDSTSVController.Select3DSServiceAdd());

                    //対象データ出力（更新）
                    exportTable.Merge(threeDSTSVController.Select3DSServiceDiff());

                    //対象データ出力（削除）

                    DataTable service3DSDeleteTable = threeDSTSVController.Select3DSServiceDel();

                    FileExportEntity fileInfo = new FileExportEntity()
                    {
                        DataExport = service3DSDeleteTable,
                        FilePath = pathFileDelete,
                        FunctionName = "サービス情報削除",
                        LogPathFile = logFilePathThreeDSTV
                    };

                    TsvConvert.ExportToTSV(fileInfo);

                }
                else if (chkSelectById.Checked)
                {    // Select ServiceSelectQuery Table
                    exportTable.Merge(threeDSTSVController.ServiceSelectQuery(dateTime));
                }

                FileExportEntity fileInfoExport = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
                };

                TsvConvert.ExportToTSV(fileInfoExport);

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
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 10. RankingBoardExport
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportRankingBoard(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSRankingBoardTable(dateTime);

                //対象データ出力（全件
                DataTable rankingBoardAllTable = threeDSTSVController.Select3DSRankingBoardAll();

                FileExportEntity fileInfoExport = new FileExportEntity()
                {
                    DataExport = rankingBoardAllTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV,
                    IsHeader = true
                };

                TsvConvert.ExportToTSV(fileInfoExport);

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
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 11_12. SongNameInEisuuExport 楽曲名英数読み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSongNameInEisuu(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
            string contents = string.Empty;
            string songNameDelFilePath = string.Format(Properties.Settings.Default.ThreeDSTSV_出力パス + "{0}", WiiConstant.EXPORT_SONG_NAME_ENGLISH_DELETE_FILE_NAME);

            try
            {
                Utils.DeleteFile(deleteFileOutPutPath);
                Utils.DeleteFile(songNameDelFilePath);
            }
            catch
            {

            }

            try
            {
                DataTable exportTable = new DataTable();


                //処理対象のテーブルがあればそちらから出力
                if (!chkSelectById.Checked)
                {
                    //比較対象のテーブルを作成
                    threeDSTSVController.Create3DSSongNameEisuuTable(dateTime);

                    //対象データ出力（追加）
                    exportTable.Merge(threeDSTSVController.Select3DSSongNameEisuuAdd());


                    //対象データ出力（更新）
                    exportTable.Merge(threeDSTSVController.Select3DSSongNameEisuuDiff());

                    //対象データ出力（削除）
                    DataTable dataDelTable = threeDSTSVController.Select3DSSongNameEisuuDel();

                    FileExportEntity fileInfoExport = new FileExportEntity()
                    {
                        DataExport = dataDelTable,
                        FilePath = songNameDelFilePath,
                        FunctionName = "楽曲名英数読み削除",
                        LogPathFile = logFilePathThreeDSTV
                    };

                    TsvConvert.ExportToTSV(fileInfoExport);

                    //contents = TsvConvert.OutputTSV(dataDelTable);

                    //AddCSV(songNameDelFilePath, contents, "楽曲名英数読み削除", dataDelTable.Rows.Count, LOG_THREEDSTV);

                }
                else if (chkSelectById.Checked)
                {
                    // 一時テーブルに表示されたものを出力
                    exportTable.Merge(threeDSTSVController.SongNameEnglishSelectQuery(dateTime));
                }


                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
                };

                TsvConvert.ExportToTSV(fileInfo);

                //contents = TsvConvert.OutputTSV(exportTable);
                //AddCSV(filePath, contents, functionName, exportTable.Rows.Count, LOG_THREEDSTV);

                //countExport++;

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
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 13. SingerEisuuExport 歌手名英数読み
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSingerEisuu(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSSingerEisuuTable(dateTime);

                // 対象データ出力（全件）
                DataTable singerEisuuAllTable = threeDSTSVController.Select3DSSingerEisuuAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = singerEisuuAllTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
                };

                TsvConvert.ExportToTSV(fileInfo);

                //contents = TsvConvert.OutputTSV(singerEisuuAllTable);
                //AddCSV(filePath, contents, functionName, singerEisuuAllTable.Rows.Count, LOG_THREEDSTV);

                //countExport++;

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
                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 12. SingerPeopleSearchExport 歌手名複数名検索
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportSingerPeopleSearch(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                threeDSTSVController.Create3DSSingerPeopleSearchTable(dateTime);

                // 対象データ出力（全件）
                DataTable singerPeopleSearchAllTable = threeDSTSVController.Select3DSSingerPeopleSearchAll();

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = singerPeopleSearchAllTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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

                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// 18_19. CollectionListExport 収集リスト
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        /// <param name="functionName"></param>
        private void ExportCollectionList(string deleteFileOutPutPath, string filePath, string dateTime, string functionName)
        {
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
                DataTable exportTable = new DataTable();
                if (!chkSelectById.Checked)
                {
                    // 対象データ出力（全件）
                    exportTable.Merge(threeDSTSVController.Select3DSFileGetList(dateTime));
                }
                else if (chkSelectById.Checked)
                {    // Select Select3DSFileGetList Table
                    exportTable.Merge(threeDSTSVController.CollectionSelectQuery(dateTime));
                }

                FileExportEntity fileInfo = new FileExportEntity()
                {
                    DataExport = exportTable,
                    FilePath = filePath,
                    FunctionName = functionName,
                    LogPathFile = logFilePathThreeDSTV
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


                AddLog(string.Format("--- {0} -> {1}：出力失敗", DateTime.Now.ToString(), functionName), LOG_THREEDSTV);
            }
        }

        /// <summary>
        /// BatchFilesExport
        /// </summary>
        private int RunBatchFiles()
        {
            string logInfo = string.Empty;
            int rst = -1;
            string filePath = Properties.Settings.Default.ThreeDSTSV_変換バッチファイル;
            try
            {
                if (!File.Exists(filePath))
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE018), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));

                    return -2;
                }

                string workingDirectory = Path.GetDirectoryName(Properties.Settings.Default.ThreeDSTSV_変換バッチファイル);
                string pathLog = Properties.Settings.Default.ThreeDSTSV_変換バッチログファイル;
                string folderWorkPath = string.Format(Application.StartupPath + "\\{0}", Properties.Settings.Default.Wii_WorkFolder);

                //if (Utils.IsDirectoryEmpty(folderWorkPath))
                //{
                //    return -1;
                //}

                //oShell.Run strArgs, 0, false

                var startArgs = new ProcessStartInfo
                {
                    Arguments = folderWorkPath,
                    FileName = Properties.Settings.Default.ThreeDSTSV_変換バッチファイル,
                    WorkingDirectory = workingDirectory,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var shellProcess = Process.Start(startArgs);
                rst = 0;
                shellProcess.WaitForExit();
                shellProcess.Close();

                // Reading log batch file
                try
                {
                    if (File.Exists(pathLog))
                    {
                        var dataTotal = File.ReadAllLines(pathLog, Encoding.GetEncoding("shift_jis"));
                        var totalRowsCount = dataTotal.Count();

                        for (int index = 0; index < totalRowsCount; index++)
                        {
                            string data = dataTotal[index];
                            if (data.Contains("該当ファイルが1件もありませんでした") ||
                                data.Contains("件のエラーが発生しました") ||
                                 data.Contains("パラメータ引数の指定の仕方が間違っています") ||
                                 data.Contains("処理に必要なファイルが足りないため、処理を中止します") ||
                                  data.Contains("必要なファイルが存在しません！")
                                )
                            {
                                return -1;
                            }
                        }
                    }
                }
                catch
                {

                }
                return rst;
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
                this.ClosedWaiting();
                return rst;
            }
        }

        /// <summary>
        /// CopyFileWorkFolderToOutputFolder
        /// </summary>
        private void CopyFileFromWorkFolderToOutputFolder(FilesCollection fileList)
        {
            string sourceFile = string.Empty;// Work folder in created file list 

            string desFile = string.Empty;

            foreach (var file in filesCollection.GetListItem())
            {
                try
                {
                    sourceFile = file.FullFilePath_FolderWork;
                    desFile = file.FileOutPutPath;

                    if (!File.Exists(sourceFile))
                        continue;

                    //収集リストコピー不要
                    if (file.FileName.Equals("list.txt")) continue;

                    File.Copy(sourceFile, desFile, true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void btnConfirmLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(logFilePathThreeDSTV))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE010), "ログファイル", logFilePathThreeDSTV), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Utils.OpenFileByNotepad(logFilePathThreeDSTV);
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
            if (keyData == Keys.O)
            {
                btnTSVOutput_Click(null, null);
            }
            else if (keyData == Keys.S)
            {
                btnFinish_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnConfirmConversionPathLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(logFilePathBatch))
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE010), WiiConstant.TEXT_CONVERSION_BATCH_LOG, logFilePathBatch), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Utils.OpenFileByNotepad(logFilePathBatch);
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

        private void DSTSVOutput_Load(object sender, EventArgs e)
        {
            LoadInitData();
            CheckSqlConnectionProcess();
        }

        private void chkSelectById_CheckedChanged(object sender, EventArgs e)
        {
            btnSelectById.Enabled = chkSelectById.Checked;
            btnFinish.Enabled = chkSelectById.Checked ? false : true;
        }
    }
}
