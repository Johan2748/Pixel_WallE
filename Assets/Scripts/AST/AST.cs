using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public abstract class AST
{
    public int Location { get; protected set; }

    public AstType Type { get; protected set; } = AstType.UNKNOWN;

    public abstract void Print();

}

public class Program : AST
{
    public List<AST> Body;

    public Program()
    {
        Body = new List<AST>();
    }

    public override void Print()
    {
        
    }
}





public enum AstType
{
    UNKNOWN, INT, BOOL
}