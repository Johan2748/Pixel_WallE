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

        public static bool IsBooleanOp(TokenType type)
        {
            return type == TokenType.AND || type == TokenType.OR;
        }

        public static bool IsComparerOp(TokenType type)
        {
            return type == TokenType.LESS || type == TokenType.LESS_EQUAL || type == TokenType.GREATER || type == TokenType.GREATER_EQUAL || type == TokenType.EQUAL_EQUAL || type == TokenType.NOT_EQUAL;
        }

        public static bool IsArithmeticOp(TokenType type)
        {
            return type == TokenType.PLUS || type == TokenType.MINUS || type == TokenType.MOD || type == TokenType.SLASH || type == TokenType.STAR || type == TokenType.STAR_STAR;
        }

        public static bool IsValidColor(string color)
        {
            color = color[1..^1];
            return color == "Red" || color == "Blue" || color == "Green" || color == "Yellow" || color == "Orange" || color == "Purple" || color == "Black" || color == "White" || color == "Transparent";
        }

    }


}
