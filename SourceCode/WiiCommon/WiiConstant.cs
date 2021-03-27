namespace WiiCommon
{
    public class WiiConstant
    {

        // File Extension
        public static string TSV_EXTENSION = ".tsv";
        public static string TXT_EXTENSION = ".txt";
        public static string IMPORT_RECOMMEND_SONG_TMP_FILE_NAME = "Tmp.txt";
        public static string FILE_FILTER_EXTENSION = "(*.tsv)|*.tsv|(*.txt)|*.txt";
        public static string FILE_FILTER_IMPORT_RANKING_EXTENTION = "(*.txt) | *.txt";
        public static string ONE_OF_THREE_CHECKED_FILE_NAME = "ランキング集計期間";
        public static string PC_USER_ROLE_ADMIN = "システム管理者";
        public static string PC_USER_ROLE_USER = "一般ユーザー";
        public static string EXPORT_CONTENT_DELETE_FILE_NAME = "コンテンツ_削除.tsv";
        public static string EXPORT_SONG_NAME_ENGLISH_DELETE_FILE_NAME = "楽曲名英数読み_削除.tsv";
        public static string EXPORT_SERVICE_NAME_DELETE_FILE_NAME = "サービス情報_削除.tsv";
        public static string TEXT_CONVERSION_BATCH_LOG = "変換バッチログファイル";
        // FolderPath ORCH function
        public static string FOLDER_PATH_MAPRJ = "C:\\maprj\\";
        public static string FOLDER_PATH_MAPRJ_ORCH = "C:\\maprj\\Orch\\";
        public static string FOLDER_PATH_MAORJ_WII = "C:\\maprj\\Wii\\";

        public static string EXIST_FILE_TITLE = "既存";

        public enum TYPE_WAITING { CONTINOUS, MARQUEE };

        // Table Name in database
        public static string TABLE_TOTAL_RANKING = "tbl_Total_Ranking";
        public static string TABLE_KARAOKE_YEAR_RANKING = "tbl_Karaoke_Year_Ranking";
        public static string TABLE_RECOMMEND_SONG = "tbl_RecommendSong";        
        public static string TABLE_UPDATE_DATE_CONTENTS = "tbl_更新日時_コンテンツ";
        public static string TABLE_UPDATEDATE_SINGER = "tbl_更新日時_歌手";
        public static string TABLE_UPDATEDATE_TIEUP = "tbl_更新日時_タイアップ";
        public static string TABLE_WURANKING_BY_AGE = "tbl_U年代別ランク";
        //前回出力完了日
        // Using 3DSTSV
        public static string TABLE_3DSTSV_SUCCESS_DATE = "パラメータ";

        // COLUMN NAME
        public static string COLUMN_FUNCTION_NAME = "機能名";
        public static string COLUMN_PARAMETER_NAME = "パラメータ名";
        public static string COLUMN_PARAMETERS = "パラメータ";
        public static string COLUMN_PARAMETER_REMARKS = "備考";
        public static string COLUMN_LAST_MODIFIED = "最終更新日時";
        public static string COLUMN_LAST_UPDATED_BY = "最終更新者";
        public static string COLUMN_LAST_UPDATED_PC_NAME = "最終更新PC名";
        public static string COLUMN_WUCONTENTS_RANKING = "WUcontents_ranking";
        public static string COLUMN_WUSINGER_RANKING = "WUsinger_ranking";
        public static string COLUMN_WUTIEUP_RANKING = "WUtieup_ranking";
        public static string COLUMN_RECOMMEND_SONG = "lbl_RecommendSong";
        public static string COLUMN_RANKING = "lbl_Ranking";
        public static string COLUMN_TIME = "年代";
        public static int COLUMN_FIRST_INDEX = 0;
        public static int COLUMN_SECOND_INDEX = 1;
        // VALUE IN COLUMN
        public static string FUNCTION_VALUE_3DSTSV = "3DS";
        public static string PARAMTER__VALUE_NAME_3DSTSV = "出力完了日";
        public static string COLUMN_RANKING_BY_AGE = "ランキング";

        // ORCHTSV
        public static string TABLE_UTSV_LABEL = "tbl_utsv_label";

        public static string WiiTmp_Database = "WiiTmp";

        //Messagebox
        public static string ERROR_TITLE_MESSAGE = "ERROR_TITLE_MESSAGE";
        public static string ALERT_TITLE_MESSAGE = "ALERT_TITLE_MESSAGE";
        public static string INFO_TITLE_MESSAGE = "INFO_TITLE_MESSAGE";

        public static string MSGA001 = "A001";
        public static string MSGA002 = "A002";
        public static string MSGA003 = "A003";
        public static string MSGA004 = "A004";
        public static string MSGA005 = "E005";
        public static string MSGA006 = "E006";
        public static string MSGA007 = "E007";
        public static string MSGA008 = "A008";
        public static string MSGA009 = "A009";
        public static string MSGA010 = "A010";
        public static string MSGA014 = "A014";
        public static string MSGA015 = "A015";
        public static string MSGA013 = "A013";
        public static string MSGA100 = "A100";
        public static string MSGE000 = "E000";
        public static string MSGE001 = "E001";
        public static string MSGE002 = "E002";
        public static string MSGE003 = "E003";
        public static string MSGE012 = "E012";
        public static string MSGE013 = "E013";
        public static string MSGE014 = "E014";
        public static string MSGE015 = "E015";
        public static string MSGE016 = "E016";
        public static string MSGE017 = "E017";
        public static string MSGE026 = "E026";
        public static string MSGE027 = "E027";
        public static string MSGE029 = "E029";
        public static string MSGE031 = "E031";
        public static string MSGE032 = "E032";
        public static string MSGE044 = "E044";
        public static string MSGI001 = "I001";
        public static string MSGI002 = "I002";
        public static string MSGI003 = "I003";
        public static string MSGE009 = "E009";
        public static string MSGE010 = "E010";
        public static string MSGE020 = "E020";
        public static string MSGE021 = "E021";
        public static string MSGE022 = "E022";
        public static string MSGE023 = "E023";
        public static string MSGE024 = "E024";
        public static string MSGE025 = "E025";
        public static string MSGI005 = "I005";
        public static string MSGI004 = "I004";
        public static string MSGE037 = "E037";
        public static string MSGE036 = "E036";
        public static string MSGE008 = "E008";
        public static string MSGE011 = "E011";
        public static string MSGE038 = "E038";
        public static string MSGE035 = "E035";
        public static string MSGE034 = "E034";
        public static string MSGE033 = "E033";
        public static string MSGE030 = "E030";
        public static string MSGE018 = "E018";
        public static string MSGE019 = "E019";
        public static string MSGI006 = "I006";
        public static string MSGE039 = "E039";
    }
}
