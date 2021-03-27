/*
 * Create by Nguyen Tat Thanh
 * Created date: Mar-20
 * Email: thanhntt89bk@gmail.com
 */

using System.Configuration;
using System.Data.SqlClient;

namespace WiiController
{
    public class GetConnection
    {
        private static string shortConnection = string.Empty;

        public static int CommandTimeOut { get; set; }
        /// <summary>
        /// Check Sql Connection String
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnectionString(string sqlConnectionString)
        {
            try
            {
                SettingConnectionApplication(sqlConnectionString);
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetConnectionShorTimeOut()
        {
            return Properties.Settings.Default.WiiConnectionStringShortTimeOut;
        }

        /// <summary>
        /// Check Sql Connection String
        /// </summary>
        /// <param name="serverName">serverName</param>
        /// <param name="userName">userName</param>
        /// <param name="passwords">passwords</param>
        /// <param name="databaseName">databaseName</param>
        /// <param name="timeOut">timeOut</param>
        /// <returns></returns>
        public static bool CheckConnectionString(string serverName, string userName, string passwords, string databaseName, int timeOut, int commandTimeOut)
        {
            string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connection Timeout={4};", serverName, databaseName, userName, passwords, timeOut);
            shortConnection = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connect Timeout={4};", serverName, databaseName, userName, passwords, 10);
            CommandTimeOut = commandTimeOut;
            try
            {
                SettingConnectionApplication(sqlConnectionString);
                SettingConnectionShortApplication(shortConnection);

                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check WiiTmp database
        /// </summary>
        /// <param name="serverName">serverName</param>
        /// <param name="userName">userName</param>
        /// <param name="passwords">passwords</param>
        /// <param name="databaseName">databaseName</param>
        /// <param name="timeOut">timeOut</param>
        /// <returns></returns>
        public static bool CheckWiiTmpConnectionString(string serverName, string userName, string passwords, string databaseName, int timeOut)
        {
            string sqlConnectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connect Timeout={4};", serverName, "WiiTmp", userName, passwords, timeOut);

            try
            {
                SettingWiiTmpConnectionApplication(sqlConnectionString);
                SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Setting sql connection
        /// </summary>
        public static void SettingConnectionApplication(string sqlConnectString)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Properties.Settings.Default["WiiConnectionString"] = sqlConnectString;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("WiiConnectionString");
        }

        /// <summary>
        /// Setting sql connection
        /// </summary>
        public static void SettingConnectionShortApplication(string sqlConnectString)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Properties.Settings.Default["WiiConnectionStringShortTimeOut"] = sqlConnectString;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("WiiConnectionStringShortTimeOut");
        }

        /// <summary>
        /// Setting sql connection
        /// </summary>
        public static void SettingWiiTmpConnectionApplication(string sqlConnectString)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Properties.Settings.Default["WiiTmpConnectionString"] = sqlConnectString;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("WiiTmpConnectionString");
        }

        /// <summary>
        /// Get connection
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(GetSqlConnectionString());
        }

        /// <summary>
        /// Get sql connection string
        /// </summary>
        /// <returns></returns>
        public static string GetSqlConnectionString()
        {
            return Properties.Settings.Default.WiiConnectionString;
        }

        public static string GetWiiTmpSqlConnectionString()
        {
            return Properties.Settings.Default.WiiTmpConnectionString;
        }
    }
}
