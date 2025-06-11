using MyTools;

public abstract class Expresion : AST
{
    public abstract object? Value { get; protected set; }

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
    public override object? Value { get => int.Parse(Token.Text); protected set {} }

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
    public override object? Value { get => bool.Parse(Token.Text); protected set {} }

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
    public override object? Value { get => Token.Text; protected set { } }

    protected override void CheckType() => Type = AstType.COLOR;

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

    public override object? Value
    {
        get
        {
            if (Expresion.Value is null) return null;
            if (Operation.Type == TokenType.NOT) return !(bool)Expresion.Value;
            else return -(int)Expresion.Value;
        }
        protected set { }
    }

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

    public override object? Value
    {
        get
        {
            if (Left.Value is null || Right.Value is null) return null;
            if (Operation.Type == TokenType.PLUS) return (int)Left.Value + (int)Right.Value;
            else if (Operation.Type == TokenType.MINUS) return (int)Left.Value - (int)Right.Value;
            else if (Operation.Type == TokenType.STAR) return (int)Left.Value * (int)Right.Value;
            else if (Operation.Type == TokenType.SLASH) return (int)Left.Value / (int)Right.Value;
            else if (Operation.Type == TokenType.MOD) return (int)Left.Value % (int)Right.Value;
            else if (Operation.Type == TokenType.STAR_STAR)
            {
                int solve = 1;
                for (int i = 0; i < (int)Right.Value; i++)
                {
                    solve *= (int)Left.Value;
                }
                return solve;
            }
            else if (Operation.Type == TokenType.GREATER) return (int)Left.Value > (int)Right.Value;
            else if (Operation.Type == TokenType.GREATER_EQUAL) return (int)Left.Value >= (int)Right.Value;
            else if (Operation.Type == TokenType.LESS) return (int)Left.Value < (int)Right.Value;
            else if (Operation.Type == TokenType.LESS_EQUAL) return (int)Left.Value <= (int)Right.Value;
            else if (Operation.Type == TokenType.EQUAL_EQUAL) return Left.Value == Right.Value;
            else if (Operation.Type == TokenType.NOT_EQUAL) return Left.Value != Right.Value;
            else if (Operation.Type == TokenType.AND) return (bool)Left.Value && (bool)Right.Value;
            else if (Operation.Type == TokenType.OR) return (bool)Left.Value || (bool)Right.Value;
            else throw new Error(Location, "Invalid Expresion");
        }

        protected set { }
    }

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

public class Function : Expresion
{
    public ICallable fuction;

    public Token Id;

    public List<Expresion> Arguments;

    public override object? Value
    {
        get
        {
            object?[] args = new object[fuction.Arity];
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = Arguments[i].Value;
            }
            return fuction.Call(args!);
        }

        protected set { }
    }

    public Function(Token id, List<Expresion> arguments, ICallable fuction)
    {
        Id = id;
        Arguments = arguments;
        this.fuction = fuction;
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
        if (Value is int) Type = AstType.INT;
        else if (Value is bool) Type = AstType.BOOL;
        else if (Value is string) Type = AstType.COLOR;
    }

}

