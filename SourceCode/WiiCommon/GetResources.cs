namespace WiiCommon
{
    public class GetResources
    {
        public static string GetResourceLable(string name)
        {
            try
            {
                return Resources.Resources.ResourceManager.GetString(name);
            }
            catch
            {
                return null;
            }
        }

        public static string GetResourceMesssage(string messageCode)
        {
            try
            {
                return Resources.Resource_Messages.ResourceManager.GetString(messageCode);
            }
            catch
            {
                return null;
            }
        }        
    }
}
