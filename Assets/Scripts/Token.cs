public class Token
{
    public TokenType Type { get; private set; }
    public string Text { get; private set; }
    public int Line { get; private set; }

    public Token(TokenType type, string text,int line)
    {
        Type = type;
        Text = text;
        Line = line;
    }

    public override string ToString()
    {
        return Type.ToString() + " " + Text;
    }    
}
