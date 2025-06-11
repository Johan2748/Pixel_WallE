public abstract class AST
{
    public int Location { get; protected set; }

    public AstType Type { get; protected set; } = AstType.NULL;

    public abstract override string ToString();

}

public class Program : AST
{
    public List<AST?> Body;

    public Program()
    {
        Body = [];
    }

    public override string ToString()
    {
        string text = "";
        foreach (AST? ast in Body)
        {
            if (ast is not null) text += ast.ToString() + "\n";
            else text += "ERROR\n";
        }
        return text;
    }

}


public enum AstType
{
    NULL, INT, BOOL, COLOR
}

