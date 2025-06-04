

public abstract class Expresion : AST
{
    public object Value;
}

public abstract class Literal : Expresion
{
    public Token Token { get; protected set; }

    public override string ToString()
    {
        return Token.Text;
    }

}

public class Number : Literal
{
    public Number(Token token)
    {
        Token = token;
        Value = int.Parse(token.Text);
        Location = token.Line;
        Type = AstType.INT;
    }
}

public class Bool : Literal
{
    public Bool(Token token)
    {
        Token = token;
        Value = bool.Parse(token.Text);
        Location = token.Line;
        Type = AstType.BOOL;
    }
}

public class PixelColor : Literal
{
    public PixelColor(Token token)
    {
        Token = token;
        Value = token.Text;
        Location = token.Line;
        Type = AstType.COLOR;
    }
}

public class UnaryExpresion : Expresion
{
    public Token Operation;
    public Expresion Expresion;

    public UnaryExpresion(Token op, Expresion expresion)
    {
        Operation = op;
        Expresion = expresion;
    }

    public override string ToString()
    {
        return Operation.Text + Expresion.ToString();
    }
}

public class BinaryExpresion : Expresion 
{
    public Expresion Left;
    public Token Operation;
    public Expresion Right;

    public BinaryExpresion(Expresion left, Token op, Expresion right)
    {
        Left = left;
        Operation = op;
        Right = right;
    }

    public override string ToString()
    {
        return Left.ToString() + " " + Operation.Text + " " + Right.ToString();
    }


}














