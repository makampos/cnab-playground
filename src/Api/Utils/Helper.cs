namespace Api.Utils
{
    public static class Helper
    {
        public static async Task<string> ReadFormFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return await Task.FromResult((string)null);
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static bool IsTxtExtension(IFormFile file)
        {
            FileInfo fileInfo = new FileInfo(file.FileName);
            if (fileInfo.Extension == ".txt")
                return true;
            return false;
        }
    }
}
