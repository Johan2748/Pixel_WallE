public enum TokenType
{
    // Single character Token
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRACKET, RIGHT_BRACKET, COMMA,

    // Arithmetics operators
    PLUS, MINUS, STAR, SLASH, MOD, STAR_STAR, ASSIGN,

    // Boolean operators
    AND, OR, NOT, GREATER, GREATER_EQUAL, LESS, LESS_EQUAL, EQUAL_EQUAL, NOT_EQUAL,

    // Literals
    NUMBER, ID, COLOR,
    
    // Keywords
    GOTO, TRUE, FALSE,

    // End of line
    EO_LINE,

    // End of file
    EOF
}