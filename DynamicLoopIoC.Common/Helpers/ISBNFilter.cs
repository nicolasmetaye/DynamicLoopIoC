namespace DynamicLoopIoC.Common.Helpers
{
    public static class ISBNFilter
    {
        public static string Filter(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return isbn;
            return isbn.ToLower().Replace("isbn", "").Replace(" ", "").Replace("-", "");
        }
    }
}