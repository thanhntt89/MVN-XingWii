using System;
using System.Data;
using System.IO;
using System.Text;

namespace Utilities
{
    public class TsvConvert
    {
        public static void ExportToTSV(FileExportEntity fileExport)
        {
            string strVatoBa = string.Empty;
            if (fileExport == null)
                return;
            try
            {
                if (fileExport.DataExport.Rows.Count == 0)
                {
                    if (!fileExport.FunctionName.Contains("削除"))
                    {
                        LogWriter.Write(fileExport.LogPathFile, string.Format("--- {0} -> {1}：{2}件", DateTime.Now.ToString(), fileExport.FunctionName, fileExport.TotalRecord));
                    }

                    return;
                }

                int rowIndex = 0;
                int columnIndex = 0;
                string header = string.Empty;
                string result = string.Empty;
                string columName = string.Empty;
                string value = string.Empty;
                string encoding_str = "shift_jis";
                if (fileExport.FunctionName.Contains("ランキングボード"))
                {
                    encoding_str = "UTF-8";
                }

                if (!File.Exists(fileExport.FilePath))
                {
                    File.Create(fileExport.FilePath).Close();
                }

                using (FileStream file = new FileStream(fileExport.FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.GetEncoding(encoding_str)))
                {
                    writer.NewLine = "\r\n";
                    columnIndex = 0;

                    if (fileExport.IsHeader)
                    {
                        foreach (DataColumn column in fileExport.DataExport.Columns)
                        {
                            if (columnIndex == 0)
                            {
                                header += column.ColumnName;
                            }
                            else
                            {
                                header += "\t" + column.ColumnName;
                            }
                            columnIndex++;
                        }
                        // Save to file with mutilpart  
                        if (!string.IsNullOrEmpty(header))
                        {
                            writer.WriteLine(header);
                        }
                    }


                    foreach (DataRow row in fileExport.DataExport.Rows)
                    {
                        columnIndex = 0;
                        result = string.Empty;

                        #region loop column
                        foreach (DataColumn column in fileExport.DataExport.Columns)
                        {
                            columName = column.ColumnName;
                            value = row.IsNull(column.ColumnName) ? null : row.Field<object>(column.ColumnName).ToString();
                            
                            if (columnIndex > 0)
                            {
                                result += "\t";
                            }

                            switch (columName)
                            {
                                case "タイアップ表示名":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += value;
                                        }
                                        else
                                        {
                                            result += Utils.ConverTieup(value);
                                        }
                                    }
                                    break;
                                case "楽曲名":
                                case "歌手名":
                                case "タイアップ表示用":
                                case "タイアップ情報欄":
                                case "情報欄":
                                case "歌い出し":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += value;
                                        }
                                        else
                                        {
                                            result += Utils.ToWideDakuten(value);
                                        }
                                    }
                                    break;
                                case "楽曲名検索用かな":
                                case "楽曲名ソート用かな":
                                case "歌手名検索用かな":
                                case "歌手名ソート用かな":
                                case "タイアップ検索用かな":
                                case "タイアップソート用かな":
                                case "楽曲名検索用カナ":
                                case "楽曲名ソート用カナ":
                                case "歌手名検索用カナ":
                                case "歌手名ソート用カナ":
                                case "タイアップ検索用カナ":
                                case "タイアップソート用カナ":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += value;
                                        }
                                        else
                                        {
                                            strVatoBa = Utils.ConvertVatoBa(value);
                                            result += Utils.ToHiragana(strVatoBa);
                                        }
                                    }
                                    break;
                                case "サービス発表日":
                                case "Wiiサービス発表日":
                                case "3DSサービス発表日":
                                case "Uサービス発表日":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += value;
                                        }
                                        else
                                        {
                                            result += Utils.ConvertDate(value);
                                        }

                                    }
                                    break;
                                case "Lowキー":
                                case "Highキー":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += '0';
                                        }
                                        else
                                        {
                                            result += value;
                                        }
                                    }
                                    break;
                                case "楽曲発売日(整備用)":
                                case "発売日":
                                case "発表日":
                                case "公開日":
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            result += value;
                                        }
                                        else
                                        {
                                            DateTime outValue = DateTime.MinValue;
                                            DateTime.TryParse(value, out outValue);

                                            if (outValue != DateTime.MinValue)
                                            {
                                                result += (outValue.Year + "/" + outValue.Month.ToString().PadLeft(2, '0') + "/" + outValue.Day.ToString().PadLeft(2, '0'));
                                            }
                                            else
                                            {
                                                result += null;
                                            }
                                        }
                                    }
                                    break;
                                case "歌手ID":
                                    {
                                        if (columnIndex == 4)
                                        {
                                            if (string.IsNullOrEmpty(value))
                                            {
                                                //MsgBox("コンテンツTSV" + Constants.vbCrLf + rowIndex + 1 + "行目 " + "選曲番号:「" + withBlock.Fields.Item(0).Value + "」 の歌手IDが空です。", Constants.vbOKOnly, Constants.vbExclamation);

                                            }
                                            else
                                            {
                                                result += value;
                                            }
                                        }
                                        else if (columnIndex == 0)
                                        {
                                            if (string.IsNullOrEmpty(value))
                                            {
                                                //MsgBox("歌手TSVか歌手ランキングTSV" + Constants.vbCrLf + rowIndex + 1 + "行目 " + "選曲番号:「" + withBlock.Fields.Item(0).Value + "」 の歌手IDが空です。", Constants.vbOKOnly, Constants.vbExclamation);
                                            }
                                            else
                                            {
                                                result += value;
                                            }

                                        }
                                        else
                                        {
                                            result += value;
                                        }

                                    }
                                    break;
                                default:
                                    {
                                        result += value;
                                    }
                                    break;

                            }

                            columnIndex++;

                        }
                        #endregion

                        writer.WriteLine(result);

                        rowIndex++;
                    }

                    // End write file
                    writer.Close();
                }

                // Write log success
                LogWriter.Write(fileExport.LogPathFile, string.Format("--- {0} -> {1}：{2}件", DateTime.Now.ToString(), fileExport.FunctionName, fileExport.TotalRecord));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class FileExportEntity
    {
        public DataTable DataExport { get; set; }
        public bool IsHeader { get; set; }
        public string FilePath { get; set; }
        public string FunctionName { get; set; }
        public int TotalRecord { get { return DataExport.Rows.Count; } }
        public string LogPathFile { get; set; }
    }
}
