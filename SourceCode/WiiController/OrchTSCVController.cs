using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WiiCommon;
using WiiObjects;

namespace WiiController
{
    public class OrchTSCVController
    {
        private string connectionString = string.Empty;
        
        private string logSqlPath = string.Empty;

        public OrchTSCVController(string logFilePath)
        {
            connectionString = GetConnection.GetSqlConnectionString();
            this.logSqlPath = logFilePath;
            SqlHelpers.CommandTimeOut = GetConnection.CommandTimeOut;
        }

        public OrchTSCVController()
        {
            connectionString = GetConnection.GetSqlConnectionString();
            SqlHelpers.CommandTimeOut = GetConnection.CommandTimeOut;
        }

        /// <summary>
        /// Get last uts
        /// </summary>
        /// <returns></returns>
        public UtsvLabelObject GetLastUtvLabel()
        {
            UtsvLabelObject utsvLabelObject = new UtsvLabelObject();
            try
            {
                string sqlString = string.Format("SELECT TOP 1 * FROM {0} ORDER BY ID DESC;", WiiConstant.TABLE_UTSV_LABEL);
                var data = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
                if (data.Rows.Count > 0)
                {
                    utsvLabelObject.RecommendSong = data.Rows[0][WiiConstant.COLUMN_RECOMMEND_SONG] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_RECOMMEND_SONG].ToString();
                    utsvLabelObject.Ranking = data.Rows[0][WiiConstant.COLUMN_RANKING] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_RANKING].ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }

            return utsvLabelObject;
        }

        /// <summary>
        /// Count data in table recommned song
        /// </summary>
        /// <returns></returns>
        public int RecommendSongCount()
        {
            try
            {
                string sqlString = string.Format("SELECT top(1) * FROM {0};", WiiConstant.TABLE_RECOMMEND_SONG);
                var dt = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// KaraokeYearRakingCount
        /// </summary>
        /// <returns></returns>
        public int KaraokeYearRakingCount()
        {
            try
            {
                string sqlString = string.Format("SELECT top(1) * FROM {0};", WiiConstant.TABLE_KARAOKE_YEAR_RANKING);
                var dt = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
                return dt.Rows.Count;
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 1. Select3DSContentsDiff
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>

        public void CreateUContentsTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUContentsTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable Select3DSContentsAdd()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUContentsAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 2. SelectUServiceAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUServiceTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUServiceTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// SelectUServiceAll
        /// </summary>
        /// <returns></returns>
        public DataTable SelectUServiceAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUServiceAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 3. SelectUGenreAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUGenreTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUGenreTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUGenreAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUGenreAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 4. SelectUGenreListAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUGenreListTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUGenreListTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUGenreListAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUGenreListAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }



        /// <summary>
        /// 5. SelectUTieupAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUTieupTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUTieupTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUTieupAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUTieupAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 6. SelectUSingerAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUSingerTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUSingerTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSingerAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSingerAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);

                throw ex;
            }
        }


        /// <summary>
        /// 7. SelectUSongNameEisuuAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUSongNameEisuuTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUSongNameEisuuTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSongNameEisuuAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSongNameEisuuAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 8. SelectUSingerEisuuAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUSingerEisuuTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUSingerEisuuTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSingerEisuuAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSingerEisuuAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 9. SelectURecomendSongAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public DataTable SelectURecomendSongAll(string dateTime)
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectURecomendSongAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 10. SelectUSingStartAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUSingStartTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUSingStartTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSingStartAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSingStartAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 11. SelectUSingerIDChangeHistryAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUSingerIDChangeHistryTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUSingerIDChangeHistryTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSingerIDChangeHistryAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSingerIDChangeHistryAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 12. SelectUDiscRecordSongAll
        /// </summary>
        /// <param name="dateTime">yyyyDDmm</param>
        /// <returns>DataTable</returns>
        public void CreateUDiscRecordSongTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUDiscRecordSongTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUDiscRecordSongAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUDiscRecordSongAll").Tables[0];

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 13. SelectUContentsRankAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateUContentsRankTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUContentsRankTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUContentsRankAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUContentsRankAll").Tables[0];

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }



        /// <summary>
        /// 14. SelectUSingerRankAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateUSingerRankTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUSingerRankTable");

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUSingerRankAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUSingerRankAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 15. SelectUTieupRankAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateUTieupRankTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUTieupRankTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUTieupRankAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUTieupRankAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 16. SelectUAgeDistinctionAll
        /// </summary>
        /// <param name="dateTime">yyyyMMdd</param>
        /// <returns>DataTable</returns>
        public void CreateUAgeDistinctionTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUAgeDistinctionTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUAgeDistinctionAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUAgeDistinctionAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// 17. SelectUUpdateAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateUUpdateTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUUpdateTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUUpdateAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUUpdateAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 18. SelectURankSumDurationAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateURankSumDurationTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateURankSumDurationTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectURankSumDurationAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectURankSumDurationAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 19. SelectUAddSongAll
        /// </summary>
        /// <returns>DataTable</returns>
        public void CreateUAddSongTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_CreateUAddSongTable");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUAddSongAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUAddSongAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// 20. SelectUMovieContentAll
        /// </summary>
        /// <param name="dateTime">yyyyMMdd</param>
        /// <returns>DataTable</returns>
        public void CreateUMovieContentTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_CreateUMovieContentTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public DataTable SelectUMovieContentAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_SelectUMovieContentAll").Tables[0];
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// SetContentsRankDate
        /// </summary>
        public void SetContentsRankDate()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("truncate table Wii.dbo.[{0}]", WiiConstant.TABLE_UPDATE_DATE_CONTENTS));

                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("INSERT INTO Wii.dbo.[{0}] ([{1}]) VALUES ('{2}')", WiiConstant.TABLE_UPDATE_DATE_CONTENTS, WiiConstant.COLUMN_WUCONTENTS_RANKING, DateTime.Now.ToString("yyyyMMdd")));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// SetSingerRankDate
        /// </summary>
        public void SetSingerRankDate()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("truncate table Wii.dbo.[{0}]", WiiConstant.TABLE_UPDATEDATE_SINGER));

                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("INSERT INTO Wii.dbo.[{0}] ([{1}]) VALUES ('{2}')", WiiConstant.TABLE_UPDATEDATE_SINGER, WiiConstant.COLUMN_WUSINGER_RANKING, DateTime.Now.ToString("yyyyMMdd")));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// SetTieupRankDate
        /// </summary>
        public void SetTieupRankDate()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("truncate table Wii.dbo.[{0}]", WiiConstant.TABLE_UPDATEDATE_TIEUP));

                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("INSERT INTO Wii.dbo.[{0}] ([{1}]) VALUES ('{2}')", WiiConstant.TABLE_UPDATEDATE_TIEUP, WiiConstant.COLUMN_WUTIEUP_RANKING, DateTime.Now.ToString("yyyyMMdd")));
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// SetRecommendSong
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        public DataTable SetRecommendSong(string dateTime)
        {
            try
            {
                // Search
                DataTable tbKaraokeSong = GetKaraokeByDateTime(dateTime);
                //tbKaraokeSong.Columns.Add("A");
                //tbKaraokeSong.Columns.Add("B");

                //tbKaraokeSong.Rows.Add(1, 2);
                //tbKaraokeSong.Rows.Add(2, 4);
                //tbKaraokeSong.Rows.Add(3, 5);
                //tbKaraokeSong.Rows.Add("", null);

                // Data compare
                DataTable dtRecommendSong = GetRecommendSong();

                //dtRecommendSong.Columns.Add("A");
                //dtRecommendSong.Columns.Add("B");
                //dtRecommendSong.Columns.Add("C");
                //dtRecommendSong.Columns.Add("D");

                //dtRecommendSong.Rows.Add(1, 5, 5, 7);
                //dtRecommendSong.Rows.Add(1, 5, 3, 7);
                //dtRecommendSong.Rows.Add(1, 5, 2, null);


                DataTable dtResut = new DataTable();
                for(int i = 0; i <= 20; i++)
                {
                    dtResut.Columns.Add("Col" + i);
                }

                int columnIndex = 0;

                bool recommandFirstColNull = false;
   
                string value1;
                string value2;
                string valueRecomend1;


                var myDictionary = new Dictionary<string, string>();

                foreach (DataRow karaRow in tbKaraokeSong.Rows)
                {
                    value1 = karaRow.IsNull(WiiConstant.COLUMN_FIRST_INDEX) ? null : karaRow.Field<object>(WiiConstant.COLUMN_FIRST_INDEX).ToString();
                    value2 = karaRow.IsNull(WiiConstant.COLUMN_SECOND_INDEX) ? null : karaRow.Field<object>(WiiConstant.COLUMN_SECOND_INDEX).ToString();
                    if (string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(value2))
                    {
                        continue;
                    }
                    myDictionary.Add(value1, value2);
                }

                int current_row = 0;

                // おすすめ曲
                foreach (DataRow recomRow in dtRecommendSong.Rows)
                {
                    var myList = new List<string>();

                    recommandFirstColNull = recomRow.IsNull(WiiConstant.COLUMN_FIRST_INDEX);
                   
                    if (recommandFirstColNull)
                    {
                        continue;
                    }

                    valueRecomend1 = recomRow.Field<object>(WiiConstant.COLUMN_FIRST_INDEX).ToString();

                    columnIndex = 0;

                    foreach (DataColumn recomCol in dtRecommendSong.Columns)
                    {

                        if (columnIndex >= 20)
                        {
                            break;
                        }

                        if (columnIndex == 0 && !myDictionary.ContainsKey(valueRecomend1))
{
                            break;
                        }

                        if (columnIndex == 0)
                        {

                            myList.Add(myDictionary[valueRecomend1]);
                            columnIndex++;
                            continue;
                        }

                        valueRecomend1 = recomRow.IsNull(recomCol.ColumnName) ? null : recomRow.Field<object>(recomCol.ColumnName).ToString();

                        if (!string.IsNullOrEmpty(valueRecomend1) && myDictionary.ContainsKey(valueRecomend1))
                        {
                            myList.Add(myDictionary[valueRecomend1]);
                        }
                        columnIndex++;
                    }
                    if(myList.Count > 0)
                    {
                        dtResut.Rows.Add(myList.ToArray());
                        current_row++;
                    }
                }

                return dtResut;
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// TruncadeRecommendSong
        /// </summary>
        public void TruncadeRecommendSong()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "truncate table WiiTmp.dbo.tbl_URecommendSong");
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        /// <summary>
        /// Get recommend song  Capioからのおすすめ曲のデータファイル取込を行う。
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommendSong()
        {
            try
            {
                string query = "SELECT [選曲番号], [おすすめ曲1], [おすすめ曲2], [おすすめ曲3], [おすすめ曲4], [おすすめ曲5], [おすすめ曲6], [おすすめ曲7],    [おすすめ曲8], [おすすめ曲9], [おすすめ曲10], [おすすめ曲11], [おすすめ曲12], [おすすめ曲13], [おすすめ曲14], [おすすめ曲15],[おすすめ曲16], [おすすめ曲17], [おすすめ曲18], [おすすめ曲19], [おすすめ曲20] FROM Wii.dbo.[tbl_RecommendSong]";

                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, query).Tables[0];

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        // <summary>
        /// GetKaraokeByDateTime
        /// </summary>
        /// <returns></returns>
        public DataTable GetKaraokeByDateTime(string dateTime)
        {
            try
            {
                string query = string.Format("SELECT [カラオケ選曲番号], [Wii(デジドコ)選曲番号] FROM dbo.[v_Wiiコンテンツ] WHERE [Wii_U_サービス発表日] <= {0} And [Wii_U_取消フラグ] is null Order By [カラオケ選曲番号]", dateTime);

                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, query).Tables[0];

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }


        /// <summary>
        /// SetKaraokeYearRanking
        /// </summary>
        public void SetKaraokeYearRanking()
        {
            SqlConnection connectSql = new SqlConnection(connectionString);

            try
            {
                DataTable dtAges = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, string.Format("select [{0}] from WiiTmp.dbo.[{1}] group by [{2}] order by [{3}]", WiiConstant.COLUMN_TIME, WiiConstant.TABLE_WURANKING_BY_AGE, WiiConstant.COLUMN_TIME, WiiConstant.COLUMN_TIME)).Tables[0];

                // Update data table WiiTmp.dbo.[tbl_U年代別ランク]              
                //connectSql.Open();
                //sqlTrans = connectSql.BeginTransaction();
                string updateSql = string.Empty;
                int rank = 1;

                foreach (DataRow row in dtAges.Rows)
                {
                    rank = 1;

                    if (row.IsNull(0))
                    {
                        continue;
                    }

                    object value = row.Field<object>(0);

                    DataTable dtAgeArr = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, string.Format("select [年代], [ランキング], [選曲番号] from WiiTmp.dbo.[{0}] WHERE [{1}] = '{2}'", WiiConstant.TABLE_WURANKING_BY_AGE, WiiConstant.COLUMN_TIME, value)).Tables[0];

                    foreach (DataRow row2 in dtAgeArr.Rows)
                    {
                        string value1 = row2.IsNull(0) ? null : row2.Field<object>(0).ToString();
                        int value2 = row2.IsNull(1) ? -1 : (int)row2.Field<object>(1);
                        string value3 = row2.IsNull(2) ? null : row2.Field<object>(2).ToString();

                        if (value2 == -1) continue;

                        updateSql = string.Format("UPDATE WiiTmp.dbo.[tbl_U年代別ランク] SET [ランキング] = {0} Where  [年代] = '{1}' and [ランキング] = {2} and [選曲番号] = '{3}'",

                            rank, value1, value2, value3);

                        SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, updateSql);
                        rank++;
                    }                  
                }

                //sqlTrans.Commit();
                //connectSql.Close();
            }
            catch (Exception ex)
            {
                //sqlTrans.Rollback();
                //connectSql.Close();

                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);

                throw ex;
            }
        }

        /// <summary>
        /// BulkInsertRecommendSong
        /// </summary>
        /// <param name="filePath"></param>
        public void BulkInsertRecommendSong(string filePath)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("BULK INSERT WiiTmp.dbo.tbl_URecommendSong FROM '{0}'", filePath));

            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = logSqlPath
                };

                LogDataBase.WriteLog(error);
                throw ex;
            }
        }

        public string GetClassName()
        {
            return this.GetType().Name;
        }
    }
}
