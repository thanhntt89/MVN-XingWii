using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Utilities;
using WiiObjects;

namespace Wii
{
    public partial class WiiSystemBase : Form
    {
        public bool isActive = false;
        public const string LOG_THREEDSTV = "LOG_THREEDSTV";
        public const string LOG_ORCHTSV = "LOG_ORCHTSV";
        public const string LOG_BATCH_FILE = "LOG_BATCH_FILE";

        public string logFilePathBatch = Properties.Settings.Default.ThreeDSTSV_変換バッチログファイル;
        public string logFilePathThreeDSTV = Properties.Settings.Default.ThreeDSTSV_ログファイル;
        public string logFilePathOrchTSV = Properties.Settings.Default.UTSV出力_ログファイル;

        public string logExceptionPath = Application.StartupPath + "\\error_log.txt";

        public WaitingForm waiting = null;
        public BackgroundWorker bgwProcess = null;

        public BackgroundWorker CreateThread()
        {
            bgwProcess = new BackgroundWorker();
            bgwProcess.WorkerSupportsCancellation = true;
            return bgwProcess;
        }

        /// <summary>
        /// Log entity
        /// </summary>
        /// <param name="error"></param>
        public void LogException(ErrorEntity error)
        {
            try
            {
                LogWriter.Write(logExceptionPath, error.GetLogInfo());
            }
            catch (Exception ex)
            {              
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Writer log files fail: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
            }
        }

        /// <summary>
        /// Add log file
        /// </summary>
        /// <param name="content"></param>
        public void AddLog(string content, string logModule)
        {
            try
            {
                string logFilePath = string.Empty;

                if (logModule.Equals(LOG_THREEDSTV))
                {
                    logFilePath = logFilePathThreeDSTV;
                }
                else if (logModule.Equals(LOG_ORCHTSV))
                {
                    logFilePath = logFilePathOrchTSV;
                }
                else if (logModule.Equals(LOG_BATCH_FILE))
                {
                    logFilePath = logFilePathBatch;
                }

                LogWriter.Write(logFilePath, content);
            }
            catch (Exception ex)
            {               
                //Invoke(new Action(() =>
                //{
                //    MessageBox.Show("Writer log files fail: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}));
            }

        }

        /// <summary>
        /// Export data to files
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public void AddCSV(string filePath, string content, string functionName, int totalRecord, string moduleName)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (totalRecord > 0)
                {
                    LogWriter.Write(filePath, content);
                    AddLog(string.Format("--- {0} -> {1}：{2} 件", DateTime.Now.ToString(), functionName, totalRecord), moduleName);
                }
                else if(!functionName.Contains("削除"))
                {
                    AddLog(string.Format("--- {0} -> {1}：{2} 件", DateTime.Now.ToString(), functionName, totalRecord), moduleName);
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete log files
        /// </summary>
        public void DeleteLog(string moduleName)
        {
            try
            {
                string logFilePath = string.Empty;
                if (moduleName.Equals(LOG_THREEDSTV))
                {
                    logFilePath = logFilePathThreeDSTV;
                }
                else if (moduleName.Equals(LOG_ORCHTSV))
                {
                    logFilePath = logFilePathOrchTSV;
                }
                else if (moduleName.Equals(LOG_BATCH_FILE))
                {
                    logFilePath = logFilePathBatch;
                }

                if (File.Exists(logFilePath))
                    File.Delete(logFilePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Return class Name
        /// </summary>
        /// <returns></returns>
        public string GetClassName()
        {
            return this.GetType().Name;
        }

        public void ShowWating()
        {
            if (waiting == null)
                waiting = new WaitingForm();
            waiting.ShowDialog();
        }

        public void ClosedWaiting()
        {
            Invoke(new Action(() =>
            {
                if (waiting != null)
                {
                    if (bgwProcess != null)
                    {
                        bgwProcess.CancelAsync();
                        bgwProcess.Dispose();
                        bgwProcess = null;
                    }
                    waiting.Close();
                    waiting = null;
                }
            }));
        }        
    }
}
