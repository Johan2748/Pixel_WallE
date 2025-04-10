public enum TokenType
{
    // Single character Token
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRACKET, RIGHT_BRACKET,

    // Arithmetics operators
    PLUS, MINUS, STAR, SLASH, MOD, STAR_STAR, ASSIGN,

    // Boolean operators
    AND, OR, NOT, GREATER, GREATER_EQUAL, LESS, LESS_EQUAL, EQUAL_EQUAL, NOT_EQUAL,

    // Literals
    NUMBER, ID,
    
    // Keywords
    GOTO, TRUE, FALSE,

    // End of file
    EOF
}