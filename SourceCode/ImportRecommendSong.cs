using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Utilities;
using WiiCommon;
using WiiController;
using WiiObjects;

namespace Wii
{
    public partial class ImportRecommendSong : WiiSystemBase
    {
        // private BackgroundWorker bgwImport = null;

        private delegate void ProcessPercent(int value);
        private event ProcessPercent progressBar;
        private ImportRecommendSongController importRecommendSongController;
        private int countTotalLine = 0;
        private string fileInputPath = string.Empty;

        public ImportRecommendSong()
        {
            InitializeComponent();
            importRecommendSongController = new ImportRecommendSongController();
        }

        private void ImportRecommendSong_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            txtInputFilePath.Text = Properties.Settings.Default.UTSV出力_おすすめ曲 + DateTime.Now.ToString("yyyyMMdd") + WiiConstant.TSV_EXTENSION;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = WiiConstant.FILE_FILTER_EXTENSION;
            var rst = openFile.ShowDialog();
            if (rst == DialogResult.OK)
            {
                txtInputFilePath.Text = openFile.FileName;
                fileInputPath = txtInputFilePath.Text;
            }
        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ProgreesImportData();
        }

        /// <summary>
        /// Progress import data
        /// </summary>
        private void ProgreesImportData()
        {
            if (!Valid())
                return;
            bgwProcess = CreateThread();
            bgwProcess.DoWork += bgwProcess_DoWork;
            bgwProcess.RunWorkerCompleted += BgwProcess_RunWorkerCompleted;
            bgwProcess.RunWorkerAsync(fileInputPath);
            this.ShowWating();
        }

        private void BgwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bgwProcess != null)
            {
                bgwProcess.DoWork -= bgwProcess_DoWork;
                bgwProcess.RunWorkerCompleted -= BgwProcess_RunWorkerCompleted;
                bgwProcess = null;
            }
            GC.Collect();
            if (isActive)
                btnClosed_Click(null, null);
        }

        private bool Valid()
        {
            if (!File.Exists(fileInputPath))
            {
                MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGE001), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check file no data
            try
            {
                var dataAll = File.ReadAllLines(fileInputPath);
                countTotalLine = dataAll.Count();

                FileInfo fileInfo = new FileInfo(fileInputPath);
                if (fileInfo.Length == 0 ||
                    (fileInfo.Length > 0
                    && !dataAll.Where(dat => !String.IsNullOrWhiteSpace(dat.Trim())).Any())
                )
                {
                    MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE026), "おすすめ曲"), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Check number of columns in file
                // Lager than 21 error
                // var data = File.ReadAllLines(fileInputPath);
                // int rowTotalCount = data.Count();
                int comLumns = 0;

                for (int rowIndex = 0; rowIndex < countTotalLine; rowIndex++)
                {
                    comLumns = dataAll[rowIndex].Split('\t').Count();
                    var dataRow = dataAll[rowIndex].Split('\t');

                    if (comLumns > 21)
                    {
                        MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE027), rowIndex, dataRow[0]), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
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

            return true;
        }

        private void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportData(e.Argument.ToString());
        }

        /// <summary>
        /// Import data to database
        /// </summary>
        private void ImportData(string filePath)
        {
            int columnCount = 0;

            string tmp_path = Utils.CreateFilePath(new string[] { Path.GetDirectoryName(filePath), WiiConstant.IMPORT_RECOMMEND_SONG_TMP_FILE_NAME });

            try
            {
                if (File.Exists(tmp_path))
                {
                    File.Delete(tmp_path);
                }

                var dataAll = File.ReadAllLines(filePath);
                var countTotalLine = dataAll.Count();

                // Write data to file
                using (FileStream file = new FileStream(tmp_path, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding("shift_jis")))
                {
                    sw.NewLine = "\r\n";

                    for (int rowIndex = 0; rowIndex < countTotalLine; rowIndex++)
                    {
                        var dataLine = dataAll[rowIndex];

                        // If First Line contains ID ignore it in new file
                        if (rowIndex == 0)
                        {
                            if (dataLine.Contains("ID"))
                                continue;
                        }

                        // Get column in row
                        columnCount = dataLine.Split('\t').Count();

                        if (columnCount < 21)
                        {
                            // Create row with 21 columns
                            for (int index = 0; index < 21 - columnCount; index++)
                            {
                                dataLine += "\t";
                            }
                        }

                        // Write to file
                        sw.WriteLine(dataLine);

                        if (progressBar != null)
                            progressBar(rowIndex);
                    }

                    sw.Close();
                }

                // Truncate table recommend song
                importRecommendSongController.TruncateTableRecommendSong();

                // Create sever path
                string server_filePath = string.Format(Properties.Settings.Default.ImportRecommendSongServerFilePath, Properties.Settings.Default.CONNECT_Server);
                // Copy file to server
                File.Copy(tmp_path, server_filePath, true);

                // Bulk insert recommend song
                importRecommendSongController.BulkInsertTableRecommendSong(server_filePath);

                // Delete file in server after update database
                if (File.Exists(server_filePath))
                    File.Delete(server_filePath);

                // Delete file tmp
                if (File.Exists(tmp_path))
                    File.Delete(tmp_path);

                // update utsv table
                importRecommendSongController.UpdateTableUTSVLabel(filePath);
                isActive = true;
                this.ClosedWaiting();
                Invoke(new Action(() =>
                {
                    MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGI003), GetResources.GetResourceMesssage(WiiConstant.INFO_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.I)
            {
                btnImport_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
