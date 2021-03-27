using System;
using System.Data;

namespace WiiController
{
    public class WorkController
    {
        private string connectionString = string.Empty;
        private string connectionShortTimeoutString = string.Empty;

        public WorkController()
        {
            connectionString = GetConnection.GetSqlConnectionString();
            connectionShortTimeoutString = GetConnection.GetConnectionShorTimeOut();
            SqlHelpers.CommandTimeOut = GetConnection.CommandTimeOut;
        }

        public void CreateWorkTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec Wii.dbo.usp_CreateWorkTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DropWorkTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionShortTimeoutString, CommandType.Text, "exec Wii.dbo.usp_DropWorkTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void UpdateTableLog()
        {
            string computerName = Environment.MachineName;
            string userName = Environment.UserName;
            string query = string.Format("update dbo.[tbl_ロック] set [状態] = 0 where [担当者] ='{0}'  and [PC名] ='{1}' and ID = 'WiiTmp.dbo.tbl_Wrk_Wiiコンテンツ_{2}';", userName, computerName, computerName);
            try
            {

                SqlHelpers.ExecuteNonQuery(connectionShortTimeoutString, CommandType.Text, query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRole()
        {
            DataTable dtRole = new DataTable();

            string userName = Environment.UserName;
            string query1 = string.Format("select t2.[権限名], t3.[機能ID], t3.[更新タイプ] from ( [利用者] as t1 left join [権限グループ] as t2 on t1.[権限グループ] = t2.[権限グループ] ) left join [権限グループ機能割当] as t3 on t2.[権限グループ] = t3.[権限グループ] where t1.[利用者ID] = '{0}';", userName);

            string query2 = "select t2.[権限名], t3.[機能ID], t3.[更新タイプ] from ( [利用者] as t1 left join [権限グループ] as t2 on t1.[権限グループ] = t2.[権限グループ] ) left join [権限グループ機能割当] as t3 on t2.[権限グループ] = t3.[権限グループ] where t1.[利用者ID] = '__Guest__';";
            try
            {
                dtRole = SqlHelpers.ExecuteDataset(connectionShortTimeoutString, CommandType.Text, query1).Tables[0];

                if (dtRole.Rows.Count == 0)
                {
                    dtRole = SqlHelpers.ExecuteDataset(connectionShortTimeoutString, CommandType.Text, query2).Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtRole;
        }

    }
}
