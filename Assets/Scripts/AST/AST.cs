using System.Collections.Generic;
public abstract class AST
{
    public int Location { get; protected set; }

    public AstType Type { get; protected set; } = AstType.UNKNOWN;

    public abstract override string ToString();

}

public class Program : AST
{
    public List<AST> Body;

    public Program()
    {
        Body = new List<AST>();
    }

    public override string ToString()
    {
        throw new System.NotImplementedException();
    }

}



public enum AstType
{
    UNKNOWN, INT, BOOL, COLOR, RETURNFUNCTION, VOIDFUNCTION
}

public interface IEvaluable
{
    public int Evaluate();
}