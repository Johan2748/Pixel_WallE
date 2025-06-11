using MyTools;

public class Lexer
{
    public List<Token> Tokens { get; private set; }

    private string text;
    private int current = 0;
    private int start = 0;
    private int line = 1;

    private Dictionary<string, TokenType> ReservedKewwords = [];

    public Lexer(string text)
    {
        this.text = text;
        Tokens = [];
        InitializeKeywords();
        GetTokens();
    }

    private void InitializeKeywords()
    {
        ReservedKewwords = new()
        {
            ["GoTo"] = TokenType.GOTO,
            ["true"] = TokenType.TRUE,
            ["false"] = TokenType.FALSE
        };
    }

    private bool IsAtEnd()
    {
        return current >= text.Length;
    }

    private char Advance()
    {
        current++;
        return text[current - 1];
    }

    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return text[current];
    }
    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (expected != text[current]) return false;

        current++;
        return true;
    }

    private void AddToken(TokenType token)
    {
        string text = this.text[start..current];
        Tokens.Add(new Token(token, text, line));
    }

    private void GetNumber()
    {
        while (Check.IsDigit(Peek())) Advance();
        AddToken(TokenType.NUMBER);
    }

    private void GetID()
    {
        while (Check.IsAlphaNum(Peek())) Advance();
        string text = this.text[start..current];
        if (ReservedKewwords.ContainsKey(text)) AddToken(ReservedKewwords[text]);
        else AddToken(TokenType.ID);
    }

    private void GetColor()
    {
        while (Peek() != '\"' && !IsAtEnd())
        {
            if (Peek() == '\n') {ErrorManager.AddError(new Error(line, "Unterminated string")); return; }
            Advance();
        }

        if (IsAtEnd()) { ErrorManager.AddError(new Error(line, "Unterminated string")); return; }

        Advance();
        AddToken(TokenType.COLOR);
    }

    public void GetTokens()
    {
        while (!IsAtEnd())
        {
            GetNextToken();
            start = current;
        }
        Tokens.Add(new Token(TokenType.EOF, "", line));
    }



    private void GetNextToken()
    {
        char c = Advance();
        switch (c)
        {
            //ignore whitespace
            case ' ': break;
            case '\r': break;
            case '\t': break;

            case '\n': Tokens.Add(new Token(TokenType.EO_LINE, "", line)); line++; break;

            //one character tokens
            case ',': AddToken(TokenType.COMMA); break;
            case '(': AddToken(TokenType.LEFT_PAREN); break;
            case ')': AddToken(TokenType.RIGHT_PAREN); break;
            case '[': AddToken(TokenType.LEFT_BRACKET); break;
            case ']': AddToken(TokenType.RIGHT_BRACKET); break;
            case '+': AddToken(TokenType.PLUS); break;
            case '-': AddToken(TokenType.MINUS); break;
            case '/': AddToken(TokenType.SLASH); break;
            case '%': AddToken(TokenType.MOD); break;
            case '_': ErrorManager.AddError(new UnexpectedCharacterError(line, c)); break;

            //one or two character tokens
            case '*':
                if (Match('*')) AddToken(TokenType.STAR_STAR);
                else AddToken(TokenType.STAR);
                break;
            case '>':
                if (Match('=')) AddToken(TokenType.GREATER_EQUAL);
                else AddToken(TokenType.GREATER);
                break;
            case '<':
                if (Match('=')) AddToken(TokenType.LESS_EQUAL);
                else if (Match('-')) AddToken(TokenType.ASSIGN);
                else AddToken(TokenType.LESS);
                break;
            case '!':
                if (Match('=')) AddToken(TokenType.NOT_EQUAL);
                else AddToken(TokenType.NOT);
                break;
            case '=':
                if (Match('=')) AddToken(TokenType.EQUAL_EQUAL);
                break;
            case '|':
                if (Match('|')) AddToken(TokenType.OR);
                break;
            case '&':
                if (Match('&')) AddToken(TokenType.AND);
                break;

            //longer tokens
            case '\"': GetColor(); break;

            default:

                if (Check.IsDigit(c)) GetNumber();

                else if (Check.IsAlphaNum(c)) GetID();

                else ErrorManager.AddError(new UnexpectedCharacterError(line, c));
                break;
                
        }
    }

}
