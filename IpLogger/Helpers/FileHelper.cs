namespace IpLogger.Console.Helpers
{
    public static class FileHelper
    {
        public static void CanCreateOrThrowException(FileInfo file)
        {
            try
            {
                using (file.Create())
                    ExistsOrThrowException(file);

                file.Delete();
            }
            catch
            {
                throw;
            }
        }

        public static void ExistsOrThrowException(FileInfo file)
        {
            if (!file.Exists)
                throw new IOException();
        }
    }
}
