namespace IpLogger.Console.Helpers
{
    public static class FileExtensions
    {
        public static bool CanCreate(this FileInfo file)
        {
            try
            {
                using (file.Create()) { }

                file.Delete();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
