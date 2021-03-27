namespace WiiObjects
{
    public class ErrorEntity
    {
        public string LogTime { get; set; }
        public string ModuleName { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorNumber { get; set; }
        public string FilePath { get; set; }
        public string GetLogInfo()
        {
            return string.Format("***************************************************************\n【発生日時】 {0}\n【発生箇所】 {1}\n【障害内容】 {2}\n", this.LogTime, this.ModuleName, this.ErrorMessage);
        }
    }
}
