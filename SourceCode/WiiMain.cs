using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Utilities;
using WiiCommon;
using WiiController;
using WiiObjects;
using System.Linq;
using System.Text;

namespace Wii
{
    public partial class WiiMain : WiiSystemBase
    {

        private WorkController workController = null;
        private bool isConnected = false;
        private UC.UCSqlConnection ucSqlConnection = new Wii.UC.UCSqlConnection();


        public WiiMain()
        {
            InitializeComponent();
            CreateWaitingControl();

        }

        private void Test()
        {


            string dateString = "20200219 0:00:00";
            var rst = Utils.ConvertDate(dateString);

            //var orchTSCVController = new OrchTSCVController();
            //DataTable exportTable = new DataTable();
            //string contents = string.Empty;

            //exportTable.Merge(orchTSCVController.Select3DSContentsAdd());

            //FileExportEntity fileInfo = new FileExportEntity()
            //{
            //    DataExport = exportTable,
            //    FilePath = Application.StartupPath + "//h001.tsv",
            //    FunctionName = "222",
            //    LogPathFile = Application.StartupPath + "//h001.txt",
            //};

            //TsvConvert.ExportToTSV(fileInfo);


            //string file1 = Application.StartupPath + "//WUコンテンツ_tsv_20200430//WUコンテンツ_current222.tsv";

            //string tmp22 = "テレビ朝日系木曜ドラマ「リーガルＶ～元弁護士・小鳥遊翔子～」主題歌";

            //string strVatoBa = Utils.ToWideDakuten(tmp22);


            //string tmp2 = Utils.ToWideDakuten(tmp22);


            //using (FileStream file = new FileStream(file1, FileMode.Append, FileAccess.Write, FileShare.Read))
            //using (StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding("shift_jis")))
            //{

            //    writer.WriteLine(strVatoBa);

            //    // End write file
            //    writer.Close();
            //}

            string filename = "WUcontents_ranking.tsv";
            filename = "WUsinger_ranking.tsv";
            filename = "WUsong_id_change_history.tsv";
            filename = "WUtieup_ranking.tsv";
            filename = "WUyears_ranking.tsv";
            filename = "WUコンテンツ.tsv";
            filename = "WUサービス情報.tsv";
            filename = "WUジャンルリスト.tsv";
            filename = "WUジャンル情報.tsv";
            filename = "WUタイアップ情報.tsv";
            filename = "WUディスク版収録曲.tsv";
            filename = "WUランキング集計期間.tsv";
            filename = "WU歌いだし.tsv";
            filename = "WU歌手.tsv";
            filename = "WU歌手名英数読み.tsv";
            filename = "WU楽曲名英数読み.tsv";
            filename = "WU更新日付.tsv";
            filename = "WU追加曲.tsv";
            //filename = "WU動画コンテンツ.tsv";
            string fileVba = Application.StartupPath + "//WUコンテンツ_tsv_20200430//Current//" + filename;
            string fileC = Application.StartupPath + "//WUコンテンツ_tsv_20200430//New//" + filename;

            var dataVba = File.ReadAllLines(fileVba, System.Text.Encoding.GetEncoding("shift_jis"));
            var totalVbaCount = dataVba.Count();


            var dataC = File.ReadAllLines(fileC, System.Text.Encoding.GetEncoding("shift_jis"));
            var totalCCount = dataC.Count();

            int isOkie = 1;

            if (totalVbaCount != totalCCount)
            {
                isOkie = 0;
            }
            else
            {

                for (int index = 0; index < totalVbaCount; index++)
                {
                    string tmpstr1 = dataVba[index];
                    string tmpstr2 = dataC[index];
                    if (!tmpstr1.Equals(tmpstr2))
                    {
                        isOkie = 0;
                        break;
                    }
                }
            }




            DataTable dt = new DataTable();
            dt.Columns.Add("楽曲名");
            dt.Columns.Add("楽曲名検索用かな");
            dt.Columns.Add("3DSサービス発表日");
            dt.Columns.Add("楽曲発売日(整備用)");

            dt.Rows.Add(string.Empty, string.Empty, " ", " ");
            dt.Rows.Add(DateTime.Now.ToString("yyyy/MM/dd"), 1, 2, DateTime.Now.ToString("yyyy/MM/dd"));
            dt.Rows.Add("1", 1, DateTime.Now.ToString("yyyy/MM/dd"), null);
            dt.Rows.Add(3, null, DateTime.Now.ToString("yyyy/MM/dd"), null);

            FileExportEntity export = new FileExportEntity()
            {
                FilePath = Application.StartupPath + "//test.log",
                DataExport = dt,
                IsHeader = true,
                FunctionName = "aaa",
                LogPathFile = Application.StartupPath + "//error.log"
            };

            TsvConvert.ExportToTSV(export);
        }

        private void WiiMain_Load(object sender, EventArgs e)
        {
            LoadDefault();
            ThreadCheckConnection();
        }

        private void ThreadCheckConnection()
        {
            bgwProcess = CreateThread();
            bgwProcess.DoWork += BgwProcess_DoWork;
            bgwProcess.RunWorkerAsync();
        }

        private void CreateWaitingControl()
        {
            this.panelSqlChecking.Controls.Add(ucSqlConnection);
            ucSqlConnection.BackColor = System.Drawing.Color.Transparent;
            ucSqlConnection.Location = new System.Drawing.Point(111, 18);
            ucSqlConnection.Name = "ucSqlConnection";
            ucSqlConnection.Size = new System.Drawing.Size(161, 159);
            ucSqlConnection.TabIndex = 5;
        }

        private void BgwProcess_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CheckConnection();
        }

        private void CheckConnection()
        {
            Invoke(new Action(() =>
            {
                ucSqlConnection.sqlConnectedEvent += HideCheckConnect;
                ucSqlConnection.ProgeressCheckDatabase();
                panelSqlChecking.Left = (this.Width - panelSqlChecking.Width) / 2;
                panelSqlChecking.Top = (this.Height - panelSqlChecking.Height) / 2;
                Thread.Sleep(1000);
                this.panelSqlChecking.Visible = true;
            }));
        }

        private void HideCheckConnect(bool isConnected, string databaseName)
        {
            panelSqlChecking.Visible = false;

            this.isConnected = isConnected;
            if (isConnected)
            {
                workController = new WorkController();

                CreateTableWork();

                // Check role
                Authority();

                panelMain.Visible = true;
                return;
            }
            MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE044), Properties.Settings.Default.CONNECT_Server, databaseName), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);

            btnFinish_Click(null, null);
        }

        private void CreateTableWork()
        {
            try
            {
                workController.CreateWorkTable();
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

                //LogException(error);
            }
        }

        private void Authority()
        {
            btn3DSTSVOutput.Enabled = false;
            btnOrchTSVOutPut.Enabled = false;
            string userAuthority = string.Empty;
            try
            {
                DataTable dtRole = workController.GetRole();

                int rowIndex = 0;

                int countRole = 0;

                foreach (DataRow row in dtRole.Rows)
                {
                    if (rowIndex == 0)
                    {
                        userAuthority = row.Field<string>(0).ToString();
                    }
                    var value1 = row.Field<int?>(1);
                    var value2 = row.Field<int?>(2);
                    if (value2 == 1 || value2 == 2)
                    {
                        if (value1 == int.Parse(btn3DSTSVOutput.Tag.ToString()))
                        {
                            btn3DSTSVOutput.Enabled = true;
                            countRole++;
                            continue;
                        }

                        if (value1 == int.Parse(btnOrchTSVOutPut.Tag.ToString()))
                        {
                            btnOrchTSVOutPut.Enabled = true;
                            countRole++;
                            continue;
                        }
                    }
                    rowIndex++;
                }

                if (countRole == 0)
                {
                    userAuthority = string.Empty;
                }
                if (string.IsNullOrEmpty(userAuthority))
                {
                    MessageBox.Show(GetResources.GetResourceMesssage(WiiConstant.MSGI006), GetResources.GetResourceMesssage(WiiConstant.INFO_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                this.Text = string.Format("Wii メニュー << {0}({1}) >>", userAuthority.Trim(), Environment.MachineName);

                // update table

                // workController.UpdateTableLog();
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

        private void btnOrchTSVOutPut_Click(object sender, EventArgs e)
        {
            OrchTSV orchTSV = new OrchTSV();
            orchTSV.ShowDialog();
        }

        private void DropTableWork()
        {
            try
            {
                // Drop table after closed form
                workController.DropWorkTable();
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
            }
        }

        private void btn3DSTSVOutput_Click(object sender, EventArgs e)
        {
            DSTSVOutput dSTSVOutput = new DSTSVOutput();
            dSTSVOutput.ShowDialog();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVersionInfo_Click(object sender, EventArgs e)
        {

        }

        private void LoadDefault()
        {
            panelMain.Left = (this.Width - panelMain.Width) / 2;
            panelMain.Top = (this.Height - panelMain.Height) / 2;
            panelMain.Visible = false;

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.I)
            {
                btn3DSTSVOutput_Click(null, null);
            }
            else if (keyData == Keys.G)
            {
                btnOrchTSVOutPut_Click(null, null);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void WiiMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isConnected)
                DropTableWork();
        }
    }
}
