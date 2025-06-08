using System;
using MyTools;

public abstract class Expresion : AST
{
    public abstract object Value { get; protected set; }    
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
    public override object Value { get => int.Parse(Token.Text); protected set {} }
    public Number(Token token)
    {
        Token = token;
        Location = token.Line;
        Type = AstType.INT;
    }
}

public class Bool : Literal
{
    public override object Value { get => bool.Parse(Token.Text); protected set {} }
    public Bool(Token token)
    {
        Token = token;
        Location = token.Line;
        Type = AstType.BOOL;
    }
}

public class PixelColor : Literal
{
    public override object Value { get => Token.Text; protected set { } }
    public PixelColor(Token token)
    {
        Token = token;
        Location = token.Line;
        Type = AstType.COLOR;
    }
}


public class UnaryExpresion : Expresion
{
    public Token Operation;
    public Expresion Expresion;

    public override object Value
    {
        get
        {
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

    private void CheckType()
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

    public override object Value
    {
        get
        {
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

    private void CheckType()
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














