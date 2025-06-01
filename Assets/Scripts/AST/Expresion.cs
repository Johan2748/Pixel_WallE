using System.Xml.Serialization;
using UnityEngine;

public abstract class Expresion : AST
{
    public object Value;
}

public abstract class Literal : Expresion
{
    public Token Token { get; protected set; }

    public override void Print()
    {
        Debug.Log(Token.Text);
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

public class UnaryExpresion : Expresion
{
    public Token Operation;
    public Expresion Expresion;

    public UnaryExpresion(Token op, Expresion expresion)
    {
        Operation = op;
        Expresion = expresion;
    }

    public override void Print()
    {
        Debug.Log($"{Operation.Text} ");
        Expresion.Print();
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

    public override void Print()
    {
        Left.Print();
        Debug.Log($" {Operation.Text} ");
        Right.Print();
    }


}














