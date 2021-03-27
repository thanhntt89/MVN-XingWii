using System;

namespace WiiController
{
    public class ImportRecommendSongController
    {
        private string connectionString = string.Empty;

        public ImportRecommendSongController()
        {
            connectionString = GetConnection.GetSqlConnectionString();
            SqlHelpers.CommandTimeOut = GetConnection.CommandTimeOut;
        }

        /// <summary>
        /// Truncate table recommend song in database
        /// </summary>
        public void TruncateTableRecommendSong()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, System.Data.CommandType.Text, "truncate table Wii.dbo.tbl_RecommendSong");               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert copy file into database
        /// </summary>
        /// <param name="filePath">Server file path</param>
        public void BulkInsertTableRecommendSong(string filePath)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, System.Data.CommandType.Text, string.Format("BULK INSERT Wii.dbo.tbl_RecommendSong FROM '{0}'", filePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update table UTSV label
        /// </summary>
        public void UpdateTableUTSVLabel(string filePath)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, System.Data.CommandType.Text, string.Format("INSERT INTO dbo.tbl_utsv_label (ID,lbl_RecommendSong, lbl_Ranking) VALUES ((select count(*) +1 from dbo.tbl_utsv_label),'済み（{0}）', ' ({1}) ') ", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), filePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
