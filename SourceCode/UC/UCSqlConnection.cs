using System;
using System.ComponentModel;
using System.Windows.Forms;
using WiiCommon;
using WiiController;
using WiiObjects;

namespace Wii.UC
{
    public partial class UCSqlConnection : UserControl
    {
        private BackgroundWorker bgwCheckDatabase = null;

        public delegate void SqlConnected(bool status, string databaseName);
        public event SqlConnected sqlConnectedEvent;
        private string dataBaseName = string.Empty;
        private bool isConnected = false;
           

        public UCSqlConnection()
        {
            InitializeComponent();
        }


        public void ProgeressCheckDatabase()
        {
            if (bgwCheckDatabase == null)
            {
                bgwCheckDatabase = new BackgroundWorker();
                bgwCheckDatabase.RunWorkerCompleted += BgwCheckDatabase_RunWorkerCompleted;
                bgwCheckDatabase.DoWork += BgwCheckDatabase_DoWork;
            }

            bgwCheckDatabase.RunWorkerAsync();
        }

        private void BgwCheckDatabase_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckDatabase();
        }

        private void BgwCheckDatabase_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (bgwCheckDatabase == null)
            {
                bgwCheckDatabase.Dispose();
            }
            bgwCheckDatabase = null;

            if (this.sqlConnectedEvent != null)
            {
                this.sqlConnectedEvent(isConnected, dataBaseName);
            }
        }

        /// <summary>
        /// Check database connection
        /// </summary>
        private void CheckDatabase()
        {
            if (!GetConnection.CheckConnectionString(Properties.Settings.Default.CONNECT_Server, Properties.Settings.Default.CONNECT_UserID, Properties.Settings.Default.CONNECT_Password, Properties.Settings.Default.CONNECT_DBName, Properties.Settings.Default.CONNECT_DDETimeout_CheckConnecting, Properties.Settings.Default.CONNECT_CmmandTimeOut))
            {
                dataBaseName = Properties.Settings.Default.CONNECT_DBName;
                return;
            }

            if (!GetConnection.CheckWiiTmpConnectionString(Properties.Settings.Default.CONNECT_Server, Properties.Settings.Default.CONNECT_UserID, Properties.Settings.Default.CONNECT_Password, WiiConstant.WiiTmp_Database, Properties.Settings.Default.CONNECT_DDETimeout_CheckConnecting))
            {
                dataBaseName = WiiConstant.WiiTmp_Database;
                return;
            }

            // Set wii db connection
            GetConnection.CheckConnectionString(Properties.Settings.Default.CONNECT_Server, Properties.Settings.Default.CONNECT_UserID, Properties.Settings.Default.CONNECT_Password, Properties.Settings.Default.CONNECT_DBName, Properties.Settings.Default.CONNECT_DDETimeout, Properties.Settings.Default.CONNECT_CmmandTimeOut);

            // Set wii tmp db connection
            GetConnection.CheckWiiTmpConnectionString(Properties.Settings.Default.CONNECT_Server, Properties.Settings.Default.CONNECT_UserID, Properties.Settings.Default.CONNECT_Password, WiiConstant.WiiTmp_Database, Properties.Settings.Default.CONNECT_DDETimeout);         
           
            isConnected = true;
        }

        private void UCCheckLogin_SizeChanged(object sender, EventArgs e)
        {
            lblContent.Left = (this.Width - lblContent.Width) / 2;
        }
    }
}
