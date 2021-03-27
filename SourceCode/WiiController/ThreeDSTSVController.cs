using System;
using System.Data;
using WiiCommon;
using WiiObjects;

namespace WiiController
{
    public class ThreeDSTSVController
    {
        private string connectionString = string.Empty;
        public ThreeDSTSVController()
        {
            connectionString = GetConnection.GetSqlConnectionString();
            SqlHelpers.CommandTimeOut = GetConnection.CommandTimeOut;
        }

        /// <summary>
        /// GetParameterByFunctioNameAndParamterName
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public ParameterObjet GetParameterByFunctioNameAndParamterName(string functionName, string parameter)
        {
            ParameterObjet parameterObjet = new ParameterObjet();
            try
            {
                string sqlString = string.Format("SELECT TOP 1 * FROM dbo.[{0}] WHERE [{1}] = '{2}' AND [{3}] = '{4}'", WiiConstant.TABLE_3DSTSV_SUCCESS_DATE, WiiConstant.COLUMN_FUNCTION_NAME, functionName, WiiConstant.COLUMN_PARAMETER_NAME, parameter);

                var data = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
                if (data.Rows.Count > 0)
                {
                    parameterObjet.FunctionName = data.Rows[0][WiiConstant.COLUMN_FUNCTION_NAME] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_FUNCTION_NAME].ToString();
                    parameterObjet.ParameterName = data.Rows[0][WiiConstant.COLUMN_PARAMETER_NAME] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_PARAMETER_NAME].ToString();
                    parameterObjet.Parameters = data.Rows[0][WiiConstant.COLUMN_PARAMETERS] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_PARAMETERS].ToString();
                    parameterObjet.ParameterRemarks = data.Rows[0][WiiConstant.COLUMN_PARAMETER_REMARKS] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_PARAMETER_REMARKS].ToString();
                    parameterObjet.LastModified = data.Rows[0][WiiConstant.COLUMN_LAST_MODIFIED] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_LAST_MODIFIED].ToString();
                    parameterObjet.LastUpdateBy = data.Rows[0][WiiConstant.COLUMN_LAST_UPDATED_BY] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_LAST_UPDATED_BY].ToString();
                    parameterObjet.UpdateLastUpdatePCName = data.Rows[0][WiiConstant.COLUMN_LAST_UPDATED_PC_NAME] == null ? string.Empty : data.Rows[0][WiiConstant.COLUMN_LAST_UPDATED_PC_NAME].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return parameterObjet;
        }

        /// <summary>
        /// Replace3DSContentsBaseTable
        /// </summary>
        public void Replace3DSContentsBaseTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_Replace3DSContentsBaseTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Replace3DSServiceBaseTable
        /// </summary>
        public void Replace3DSServiceBaseTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_Replace3DSServiceBaseTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Replace3DSRankingBoardBaseTable
        /// </summary>
        public void Replace3DSRankingBoardBaseTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_Replace3DSRankingBoardBaseTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Replace3DSSongNameEisuuBaseTable
        /// </summary>
        public void Replace3DSSongNameEisuuBaseTable()
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, "exec usp_Replace3DSSongNameEisuuBaseTable");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update3DSTSVTable 
        /// </summary>
        public void Update3DSTSVTable()
        {
            string userPc = Environment.UserName;
            string PcName = Environment.MachineName;

            string sqlString = string.Format("UPDATE dbo.[パラメータ] SET [パラメータ] = CONVERT(VARCHAR, GETDATE(), 120), [最終更新日時] = CONVERT(VARCHAR, GETDATE(), 120),  [最終更新者] = '{0}',  [最終更新PC名] ='{1}' WHERE [機能名] = '{2}' AND [パラメータ名] = '{3}'", userPc, PcName, WiiConstant.FUNCTION_VALUE_3DSTSV, WiiConstant.PARAMTER__VALUE_NAME_3DSTSV);
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, sqlString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //START content checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成	
        ///更新されたデータを取得			
        /// </summary>
        public void Create3DSContentsTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSContentsTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（追加）					
        /// 追加データを出力			
        /// </summary>
        public DataTable Select3DSContentsAdd()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSContentsAdd").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（更新）		
        /// 更新データを出力						
        /// </summary>
        public DataTable Select3DSContentsDiff()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSContentsDiff").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（削除）	
        /// 削除データを出力			
        /// </summary>
        public DataTable Select3DSContentsDel()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSContentsDel").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// content option select 
        /// </summary>
        public DataTable ContentSelectQuery(string Date)
        {
            string PcName = Environment.MachineName;

            string sqlString = string.Format("SELECT [Wii(デジドコ)選曲番号], [楽曲名], [楽曲名検索用カナ], [楽曲名ソート用カナ], [PU_ARTIST_ID] as [歌手ID], [カラオケ選曲番号], [タイアップ情報欄], [歌い出し], [楽曲発売日(整備用)], (CASE WHEN [アレンジコード] IS NOT NULL THEN [アレンジコード] Else N'1' END) AS [アレンジコード], [Lowキー], [Highキー], (CASE WHEN [原曲比2] IS NOT NULL THEN [原曲比2] WHEN [原曲比] = N'N' THEN N'0' WHEN [原曲比] IS NULL THEN N'0' Else [原曲比] END) AS [原曲比] FROM WiiTmp.dbo.tbl_Wrk_Wiiコンテンツ_{0} WHERE [3DSサービス発表日] <= '{1}' ORDER BY [Wii(デジドコ)選曲番号]", PcName, Date);

            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END content checkbox option

        //START single checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成
        /// </summary>
        public void Create3DSSingerTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSSingerTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（全件）
        /// </summary>
        public DataTable Select3DSSingerAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSingerAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //END single checkbox option

        //START general info checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成
        /// </summary>
        public void Create3DSGenreTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSGenreTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（全件）
        /// ファイル出力処理 
        /// </summary>
        public DataTable Select3DSGenreAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSGenreAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END general info checkbox option

        //START Tieup all checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成					
        /// </summary>
        public void Create3DSTieupTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSTieupTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（全件）
        /// "ファイル出力処理	 
        /// </summary>
        public DataTable Select3DSTieupAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSTieupAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Tieup all checkbox option

        //START Service checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成					
        /// </summary>
        public void Create3DSServiceTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSServiceTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（更新）
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSServiceAdd()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSServiceAdd").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（更新）
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSServiceDiff()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSServiceDiff;").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（削除）					
        /// 追加データを出力			
        /// </summary>
        public DataTable Select3DSServiceDel()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSServiceDel").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Service option select 
        /// </summary>
        public DataTable ServiceSelectQuery(string Date)
        {
            string PcName = Environment.MachineName;

            string sqlString = string.Format("select [Wii(デジドコ)選曲番号], [3DSサービス発表日], ( case when [3DS取消フラグ] is null then 0 else [3DS取消フラグ] end ) from WiiTmp.dbo.tbl_Wrk_Wiiコンテンツ_{0} where [3DSサービス発表日] <= '{1}' order by [Wii(デジドコ)選曲番号]", PcName, Date);

            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Service checkbox option


        //START Ranking board checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成					
        /// </summary>
        public void Create3DSRankingBoardTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSRankingBoardTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 対象データ出力（全件）				
        /// ファイル出力処理			
        /// </summary>
        public DataTable Select3DSRankingBoardAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSRankingBoardAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Ranking board checkbox option

        //START song name checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成					
        /// </summary>
        public void Create3DSSongNameEisuuTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSSongNameEisuuTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（追加）				
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSSongNameEisuuAdd()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSongNameEisuuAdd").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（更新）				
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSSongNameEisuuDiff()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSongNameEisuuDiff").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（削除）			
        /// 削除データを出力			
        /// </summary>
        public DataTable Select3DSSongNameEisuuDel()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSongNameEisuuDel").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SongName option select 
        /// </summary>
        public DataTable SongNameEnglishSelectQuery(string Date)
        {
            string PcName = Environment.MachineName;

            string sqlString = string.Format("SELECT t1.[Wii(デジドコ)選曲番号], t2.[楽曲名検索ソート用英字] AS [楽曲名検索用英数], t2.[楽曲名検索ソート用英字] AS [楽曲名ソート用英数] FROM WiiTmp.dbo.tbl_Wrk_Wiiコンテンツ_{0} AS t1 LEFT OUTER JOIN [v_デジ・ドココンテンツ] AS t2 ON t1.[デジドココンテンツID] = t2.[デジドココンテンツID] WHERE [3DSサービス発表日] <= '{1}' AND t2.[楽曲名検索ソート用英字] IS NOT NULL ORDER BY [Wii(デジドコ)選曲番号]", PcName, Date);

            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Service checkbox option
        //END song name checkbox option

        //START Singer Eisuu checkbox option
        /// <summary>
        /// 比較対象のテーブルを作成					
        /// </summary>
        public void Create3DSSingerEisuuTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSSingerEisuuTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（追加）				
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSSingerEisuuAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSingerEisuuAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Singer Eisuu checkbox option

        //START Singer People checkbox option
        /// <summary>
        /// 対象データ出力（更新）				
        /// 更新データを出力			
        /// </summary>
        public void Create3DSSingerPeopleSearchTable(string dateTime)
        {
            try
            {
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, string.Format("exec usp_Create3DSSingerPeopleSearchTable {0}", dateTime));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 対象データ出力（削除）			
        /// 削除データを出力			
        /// </summary>
        public DataTable Select3DSSingerPeopleSearchAll()
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, "exec usp_Select3DSSingerPeopleSearchAll").Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END Singer People checkbox option

        //START collection checkbox option
        /// <summary>
        /// 対象データ出力（更新）				
        /// 更新データを出力			
        /// </summary>
        public DataTable Select3DSFileGetList(string dateTime)
        {
            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, string.Format("exec usp_Select3DSFileGetList {0}", dateTime)).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Collection option select 
        /// </summary>
        public DataTable CollectionSelectQuery(string dateTime)
        {
            string PcName = Environment.MachineName;

            string sqlString = string.Format("select [Wii(デジドコ)選曲番号] from WiiTmp.dbo.tbl_Wrk_Wiiコンテンツ_{0} as t where [3DSサービス発表日] <= '{1}' order by [Wii(デジドコ)選曲番号]", PcName, dateTime);

            try
            {
                return SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, sqlString).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insert search item
        /// </summary>
        /// <param name="serchItemSelected"></param>
        public void InsertWiiTmp(ItemCollection serchItemSelected)
        {
            try
            {
                string PcName = Environment.MachineName;

              
                string queryTruncade = string.Format("truncate table WiiTmp.dbo.[tbl_Wrk_Wiiコンテンツ_{0}]", PcName);
                SqlHelpers.ExecuteNonQuery(GetConnection.GetConnectionShorTimeOut(), CommandType.Text, queryTruncade);

                string sqlQuery = string.Format("insert into WiiTmp.dbo.[tbl_Wrk_Wiiコンテンツ_{0}] ( [デジドココンテンツID], [未公開理由], [Wii(デジドコ)選曲番号], [楽曲名], [楽曲名検索用かな], [楽曲名ソート用かな],[楽曲名ソート用かな_補正], [歌手ID], [PU_ARTIST_ID], [歌手名], [歌手名検索用かな], [歌手名ソート用かな], [洋楽フラグ], [演歌フラグ], [JV演歌POPS], [カラオケ選曲番号], [タイアップ情報欄], [JV映像区分(背景映像区分)], [Wii映像区分], [歌い出し], [楽曲分類区分], [ドコカラフラグ], [ドコカラサービス発表日], [ドコカラ管理(詞)], [ドコカラ管理(曲)], [Wiiアップ予定日], [Wiiサービス発表日], [Wii可否フラグ], [Wii停止日], [Wii削除情報], [Wii取消フラグ], [Wii_US_アップ予定日], [Wii_US_サービス発表日], [Wii_US_可否フラグ], [Wii_US_停止日], [Wii_US_削除情報], [Wii_US_取消フラグ], [PSアップ予定日], [PSサービス発表日], [PS可否フラグ], [PS停止日], [PS削除情報], [PS取消フラグ], [PS試聴フラグ], [PS試聴開始日], [PS試聴終了日], [PS試聴備考], [発売日], [Wii制作完了日], [Wii_US_制作完了日], [PS制作完了日], [カラオケ完パケ予定日], [権利確認日], [著作権備考], [Wii備考], [PS備考], [楽曲分類コード], [増曲候補フラグ], [登録日時], [DistCode], [選択], [削除], [歌い出し_Ca], [原曲比], [原曲比2], [Wii_DISCID], [Wii_U_アップ予定日], [Wii_U_可否フラグ], [Wii_U_録画可否フラグ], [Wii_U_録音可否フラグ], [Wii_U_無料配信フラグ], [Wii_U_サービス発表日], [Wii_U_取消フラグ], [Wii_U_停止日], [Wii_U_削除情報], [Wii_U_制作完了日], [FESサービス発表日], [アレンジコード], [背景映像コード], [Wii_U_アレンジコード], [3DSアップ予定日], [3DS可否フラグ], [3DSサービス発表日], [3DS取消フラグ], [3DS停止日], [3DS削除情報], [3DS備考], [3DS制作完了日], [アップ予定日], [公開開始日], [Lowキー], [Highキー], [楽曲発売日(整備用)] ) select[デジドココンテンツID], [未公開理由], [Wii(デジドコ)選曲番号], [楽曲名], [楽曲名検索用かな], [楽曲名ソート用かな],[楽曲名ソート用かな_補正], [歌手ID], [PU_ARTIST_ID], [歌手名], [歌手名検索用かな], [歌手名ソート用かな], [洋楽フラグ], [演歌フラグ], [JV演歌POPS], [カラオケ選曲番号], [タイアップ情報欄], [JV映像区分(背景映像区分)], [Wii映像区分], [歌い出し], [楽曲分類区分], [ドコカラフラグ], [ドコカラサービス発表日], [ドコカラ管理(詞)], [ドコカラ管理(曲)], [Wiiアップ予定日], [Wiiサービス発表日], [Wii可否フラグ], [Wii停止日], [Wii削除情報], [Wii取消フラグ], [Wii_US_アップ予定日], [Wii_US_サービス発表日], [Wii_US_可否フラグ], [Wii_US_停止日], [Wii_US_削除情報], [Wii_US_取消フラグ], [PSアップ予定日], [PSサービス発表日], [PS可否フラグ], [PS停止日], [PS削除情報], [PS取消フラグ], [PS試聴フラグ], [PS試聴開始日], [PS試聴終了日], [PS試聴備考], [発売日], [Wii制作完了日], [Wii_US_制作完了日], [PS制作完了日], [カラオケ完パケ予定日], [権利確認日], [著作権備考], [Wii備考], [PS備考], [楽曲分類コード], [増曲候補フラグ], [登録日時], [DistCode], 0, 0, [歌い出し_Ca], [原曲比], [原曲比2], [Wii_DISCID], [Wii_U_アップ予定日], [Wii_U_可否フラグ], [Wii_U_録画可否フラグ], [Wii_U_録音可否フラグ], [Wii_U_無料配信フラグ], [Wii_U_サービス発表日], [Wii_U_取消フラグ], [Wii_U_停止日], [Wii_U_削除情報], [Wii_U_制作完了日], [FESサービス発表日], [アレンジコード], [背景映像コード], [Wii_U_アレンジコード], [3DSアップ予定日], [3DS可否フラグ], [3DSサービス発表日], [3DS取消フラグ], [3DS停止日], [3DS削除情報], [3DS備考], [3DS制作完了日], [アップ予定日], [公開開始日], [Lowキー], [Highキー], [楽曲発売日(整備用)] from[v_Wiiコンテンツ] where[Wii(デジドコ)選曲番号] in ({1})", PcName, serchItemSelected.ListItemString());
                SqlHelpers.ExecuteNonQuery(GetConnection.GetConnectionShorTimeOut(), CommandType.Text, sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //END collection checkbox option

        /// <summary>
        /// Count song by pc
        /// </summary>
        /// <returns></returns>
        public int CountContentsByPCName()
        {
            try
            {
                string query = string.Format("SELECT top(1) * FROM WiiTmp.dbo.[tbl_Wrk_Wiiコンテンツ_{0}]", Environment.MachineName);
                DataTable table = SqlHelpers.ExecuteDataset(connectionString, CommandType.Text, query).Tables[0];
                return table.Rows.Count;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete all data in table Contents
        /// </summary>
        public void TruncateTableContentByPCName()
        {
            try
            {
                string query = string.Format("TRUNCATE TABLE WiiTmp.dbo.[tbl_Wrk_Wiiコンテンツ_{0}]", Environment.MachineName);
                SqlHelpers.ExecuteNonQuery(connectionString, CommandType.Text, query);             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
