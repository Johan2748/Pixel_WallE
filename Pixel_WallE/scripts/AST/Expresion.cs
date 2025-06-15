using MyTools;

public abstract class Expresion : AST
{
        protected abstract void CheckType();
}

public abstract class Literal : Expresion
{
    public Token Token { get; protected set; } = null!;

    public override string ToString()
    {
        return Token.Text;
    }

}

public class Number : Literal
{

    protected override void CheckType() => Type = AstType.INT;

    public Number(Token token)
    {
        Token = token;
        Location = token.Line;
        CheckType();
    }
}

public class Bool : Literal
{

    protected override void CheckType() => Type = AstType.BOOL;

    public Bool(Token token)
    {
        Token = token;
        Location = token.Line;
        CheckType();
    }
}

public class PixelColor : Literal
{
    protected override void CheckType()
    {
        if (!Check.IsValidColor(Token.Text)) throw new Error(Location, "Invalid color");
        Type = AstType.COLOR;
    }

    public PixelColor(Token token)
    {
        Token = token;
        Location = token.Line;
        CheckType();
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
        Location = op.Line;
        CheckType();
    }

    public override string ToString()
    {
        return Operation.Text + Expresion.ToString();
    }

    protected override void CheckType()
    {
        if (Operation.Type == TokenType.NOT)
        {
            if (Expresion.Type != AstType.BOOL) throw new UnaryExprError(Operation, Expresion);
            Type = AstType.BOOL;
        }
        if (Operation.Type == TokenType.MINUS)
        {
            if (Expresion.Type != AstType.INT) throw new UnaryExprError(Operation, Expresion);
            Type = AstType.INT;
        }
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
        Location = op.Line;
        CheckType();
    }

    public override string ToString()
    {
        return Left.ToString() + " " + Operation.Text + " " + Right.ToString();
    }

    protected override void CheckType()
    {
        if (Check.IsArithmeticOp(Operation.Type))
        {
            if (Left.Type != AstType.INT || Right.Type != AstType.INT) throw new BinOpError(Left, Operation, Right);
            Type = AstType.INT;
        }

        if (Check.IsBooleanOp(Operation.Type))
        {
            if (Left.Type != AstType.BOOL || Right.Type != AstType.BOOL) throw new BinOpError(Left, Operation, Right);
            Type = AstType.BOOL;
        }

        if (Check.IsComparerOp(Operation.Type))
        {
            if (Left.Type != AstType.INT || Right.Type != AstType.INT) throw new BinOpError(Left, Operation, Right);
            Type = AstType.BOOL;
        }
    }

}

public class Var : Expresion
{
    public Token Id { get; protected set; }

    public Var(Token id, Expresion expr)
    {
        Id = id;
        Location = Id.Line;
        Type = expr.Type;

    }

    public override string ToString()
    {
        return $"{Id.Text}({Type})";
    }

    protected override void CheckType(){}

}

public class Function : Expresion
{
    public ICallable function;

    public Token Id;

    public List<Expresion> Arguments;

    public Function(Token id, List<Expresion> arguments, ICallable function)
    {
        Id = id;
        Arguments = arguments;
        this.function = function;
        CheckType();
    }

    public override string ToString()
    {
        string text = Id.Text + "(";
        for (int i = 0; i < Arguments.Count; i++)
        {
            if (i == Arguments.Count - 1) text += Arguments[^1];
            else text += Arguments[i].ToString() + ",";
        }
        text += ")";

        return text;
    }

    protected override void CheckType()
    {
        Type = function.ReturnType;
    }

}

