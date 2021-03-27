using System;
using System.IO;
using System.Text;

namespace Utilities
{
    public static class LogWriter
    {
       
        public static void Write(string filePath, string message)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                using (FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }                
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
