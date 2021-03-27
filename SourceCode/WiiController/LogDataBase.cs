using Utilities;
using WiiObjects;

namespace WiiController
{
    public class LogDataBase
    {
        public static void WriteLog(ErrorEntity error)
        {
            try
            {
                LogWriter.Write(error.FilePath, error.GetLogInfo());
            }
            catch
            {

            }
        }
    }
}
