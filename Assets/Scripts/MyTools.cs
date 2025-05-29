namespace MyTools
{
    public static class Check
    {
        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
        }

        public static bool IsAlphaNum(char c)
        {
            return IsDigit(c) || IsAlpha(c);
        }
    }


}

