using System;
using System.Text;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualBasic;

namespace Utilities
{
    public class Utils
    {

        /// <summary>
        /// Compare datetme
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns>
        /// value lower 0 − If date1 is earlier than date2
        /// value equal 0 − If date1 is the same as date2
        /// value greater 0 − If date1 is later than date2
        /// </returns>
        public static int DateTimeCompare(DateTime dt1, DateTime dt2)
        {
            int compare = 0;
            DateTime d1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
            DateTime d2 = new DateTime(dt2.Year, dt2.Month, dt2.Day);
            compare = DateTime.Compare(d1, d2);
            return compare;
        }

        // Convert string to WideDakuten
        public static string ToWideDakuten(string str)
        {

            bool bIsDakuten = false;
            bool bIsHanDakuten = false;
            string result = str;

            try
            {
                result = ConvertDakuten(result);

                if (result.Contains("゛"))
                {
                    bIsDakuten = true;
                    result = result.Replace("゛", @"\濁点\");
                }

                if (result.Contains("゜"))
                {
                    bIsHanDakuten = true;
                    result = result.Replace("゜", @"\半濁点\");
                }
                result = Strings.StrConv(result, VbStrConv.Wide);

                if (bIsDakuten)
                {
                    result = result.Replace(@"\濁点\", "゛");
                }

                if (bIsHanDakuten)
                {
                    result = result.Replace(@"\半濁点\", "゜");
                }
            }
            catch
            {

            }
            return result;
        }

        // Convert Date from YYYYMMDD to YYYY/MM/DD
        public static string ConvertDate(string date)
        {
            string result = string.Empty;
            try
            {
                result = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
                DateTime outValue = DateTime.MinValue;
                DateTime.TryParse(result, out outValue);
                if (outValue == DateTime.MinValue)
                {
                    result = null;
                }
            }
            catch
            {

            }
            return result;
        }

        // Convert katakana characters into hiragana.
        public static string ToHiragana(string str)
        {
            string result = string.Empty;
            try
            {
                result = Strings.StrConv(str, VbStrConv.Hiragana);
            }
            catch
            {

            }
            return result;
        }

        // Unicodeでは濁点や半濁点を別扱いしてることがあるので結合した
        public static string ConvertDakuten(string str)
        {
            string result = str;
            try
            {
                string[,] replaceArr = new string[50, 2] {
                    { "ヂ", "ヂ"},
                    { "グ", "グ"},
                    { "ボ", "ボ"},
                    { "ぎ", "ぎ"},
                    { "ず", "ず"},
                    { "プ", "プ"},
                    { "デ", "デ"},
                    { "パ", "パ"},
                    { "ゼ", "ゼ"},
                    { "ぴ", "ぴ"},
                    { "ぞ", "ぞ"},
                    { "ブ", "ブ"},
                    { "ギ", "ギ"},
                    { "だ", "だ"},
                    { "バ", "バ"},
                    { "ぽ", "ぽ"},
                    { "ズ", "ズ"},
                    { "ぷ", "ぷ"},
                    { "ポ", "ポ"},
                    { "じ", "じ"},
                    { "ぢ", "ぢ"},
                    { "べ", "べ"},
                    { "ぱ", "ぱ"},
                    { "ジ", "ジ"},
                    { "ザ", "ザ"},
                    { "び", "び"},
                    { "げ", "げ"},
                    { "が", "が"},
                    { "ビ", "ビ"},
                    { "ベ", "ベ"},
                    { "ぶ", "ぶ"},
                    { "ば", "ば"},
                    { "ざ", "ざ"},
                    { "ペ", "ペ"},
                    { "ぼ", "ぼ"},
                    { "ヅ", "ヅ"},
                    { "ゲ", "ゲ"},
                    { "ぺ", "ぺ"},
                    { "ガ", "ガ"},
                    { "ゴ", "ゴ"},
                    { "ゾ", "ゾ"},
                    { "ピ", "ピ"},
                    { "で", "で"},
                    { "ぜ", "ぜ"},
                    { "ぐ", "ぐ"},
                    { "ド", "ド"},
                    { "ど", "ど"},
                    { "ダ", "ダ"},
                    { "づ", "づ"},
                    { "ご", "ご"},
                    };


                for (int i = 0; i < replaceArr.GetLength(0); i++)
                {
                    result = result.Replace(replaceArr[i, 0], replaceArr[i, 1]);
                }
            }
            catch
            {

            }
            return result;
        }

        // Convert　タイアップ表示名
        public static string ConverTieup(string str)
        {
            string result = str;
            try
            {
                string[,] replaceArr = new string[50, 2] {
                    { "ヂ", "チ゛"},
                    { "グ", "ク゛"},
                    { "ボ", "ホ゛"},
                    { "ぎ", "き゛"},
                    { "ず", "す゛"},
                    { "プ", "フ゛"},
                    { "デ", "テ゛"},
                    { "パ", "ハ゛"},
                    { "ゼ", "セ゛"},
                    { "ぴ", "ひ゛"},
                    { "ぞ", "そ゛"},
                    { "ブ", "フ゛"},
                    { "ギ", "キ゛"},
                    { "だ", "た゛"},
                    { "バ", "ハ゛"},
                    { "ぽ", "ほ゛"},
                    { "ズ", "ス゛"},
                    { "ぷ", "ふ゛"},
                    { "ポ", "ホ゛"},
                    { "じ", "し゛"},
                    { "ぢ", "ち゛"},
                    { "べ", "へ゛"},
                    { "ぱ", "は゛"},
                    { "ジ", "シ゛"},
                    { "ザ", "サ゛"},
                    { "び", "ひ゛"},
                    { "げ", "け゛"},
                    { "が", "か゛"},
                    { "ビ", "ヒ゛"},
                    { "ベ", "へ゛"},
                    { "ぶ", "ふ゛"},
                    { "ば", "は゛"},
                    { "ざ", "さ゛"},
                    { "ペ", "へ゛"},
                    { "ぼ", "ほ゛"},
                    { "ヅ", "そ゛"},
                    { "ゲ", "ケ゛"},
                    { "ぺ", "へ゛"},
                    { "ガ", "カ゛"},
                    { "ゴ", "コ゛"},
                    { "ゾ", "ソ゛"},
                    { "ピ", "ヒ゛"},
                    { "で", "て゛"},
                    { "ぜ", "せ゛"},
                    { "ぐ", "く゛"},
                    { "ド", "ト゛"},
                    { "ど", "と゛"},
                    { "ダ", "タ゛"},
                    { "づ", "つ゛"},
                    { "ご", "こ゛"},
                    };


                for (int i = 0; i < replaceArr.GetLength(0); i++)
                {
                    result = result.Replace(replaceArr[i, 0], replaceArr[i, 1]);
                }
            }
            catch
            {

            }
            return result;
        }

        // Convert string to Vatoba
        public static string ConvertVatoBa(string str)
        {
            //string strdakuon = "バビブベボ";
            //string strSeionConvert_1 = "ヴァヴィヴヴェヴォ";

            // ヴァ→バ
            // ヴィ→ビ
            // ヴェ→ベ
            // ヴォ→ボ
            // ヴ→ブ
            string result = str;
            try
            {
                string[,] replaceArr = new string[5, 2] {
                    { "ヴァ", "バ" },
                    { "ヴィ", "ビ" },
                    { "ヴェ", "ベ" },
                    { "ヴォ", "ボ" },
                    { "ヴ", "ブ" },                    
                    };


                for (int i = 0; i < replaceArr.GetLength(0); i++)
                {
                    result = result.Replace(replaceArr[i, 0], replaceArr[i, 1]);
                }
            }
            catch
            {

            }
            return result;
        }

        /// <summary>
        /// Create file path
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string CreateFilePath(string[] array = null)
        {
            if (array == null)
                return string.Empty;

            string filePath = string.Empty;

            foreach (var item in array)
            {
                filePath += item + "\\";
            }

            return filePath.Remove(filePath.Length - 1);
        }

        public static void OpenFileByPath(string filePath)
        {
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete all file in folder
        /// </summary>
        /// <param name="folderPath"></param>
        public static void DeleteAllFileInFolder(string folderPath)
        {
            try
            {
                // Delete files
                string[] listFile = Directory.GetFiles(folderPath);
                foreach (var file in listFile)
                {
                    File.Delete(file);
                }

                // Get sub directory
                string[] subDirectory = Directory.GetDirectories(folderPath);
                // Loop
                foreach (var folder in subDirectory)
                {
                    DeleteAllFileInFolder(folder);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check file exist in folder
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool CheckFileExistInFolder(string folderPath)
        {
            try
            {
                string[] listFile = Directory.GetFiles(folderPath);
                return listFile.Length > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckPathIsFolder(string folderPath)
        {

            return Path.GetDirectoryName(folderPath) == folderPath.TrimEnd(Path.DirectorySeparatorChar.ToString().ToCharArray());
        }

        public static void DeleteExistAndCreateNewFile(string filePath)
        {
            try
            {
                // Directory Exist delete file 
                if (File.Exists(filePath) && !CheckFileInUsed(filePath))
                {
                    File.Delete(filePath);
                }
                // Create a new file
                File.Create(filePath).Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                // Directory Exist delete file 
                if (File.Exists(filePath) && !CheckFileInUsed(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool CheckFileInUsed(string filePath)
        {
            FileStream stream = null;
            try
            {
                FileInfo file = new FileInfo(filePath);
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                // File in used
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();

            }
            // file free
            return false;
        }


        public static int OpenFileByNotepad(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return -1;
                }

                //Set oShell = CreateObject("Wscript.Shell")
                var shellType = Type.GetTypeFromProgID("Wscript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                //oShell.Run strArgs, 0, false

                var startArgs = new ProcessStartInfo
                {
                    Arguments = filePath,
                    FileName = "notepad.exe",
                    WorkingDirectory = Path.GetDirectoryName(filePath),
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var shellProcess = Process.Start(startArgs);
                shellProcess.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
    }
}
