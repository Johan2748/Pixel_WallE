
using System.Collections.Generic;

public interface ICallable
{
    int Arity { get; }

    AstType[] Types { get; }

    bool CheckArguments(List<Expresion> arguments)
    {
        if (Arity != Types.Length) throw new Error(-1, "This function is poorly implemented");

        for (int i = 0; i < Types.Length; i++)
        {
            if (Types[i] != arguments[i].Type) return false;
        }
        return true;
    }

    object Call(object[] arguments);
}


// INSTRUCTIONS
public class Spawn : ICallable
{
    public int Arity => 2;

    public AstType[] Types => new[] { AstType.INT, AstType.INT };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class ChangeColor : ICallable
{
    public int Arity => 1;

    public AstType[] Types => new[] { AstType.COLOR };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class Size : ICallable
{
    public int Arity => 1;

    public AstType[] Types => new[] { AstType.INT };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class DrawLine : ICallable
{
    public int Arity => 3;

    public AstType[] Types => new[] { AstType.INT, AstType.INT, AstType.INT };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class DrawCircle : ICallable
{
    public int Arity => 3;

    public AstType[] Types => new[] { AstType.INT, AstType.INT, AstType.INT };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class DrawRectangle : ICallable
{
    public int Arity => 5;

    public AstType[] Types => new[] { AstType.INT, AstType.INT, AstType.INT, AstType.INT, AstType.INT };

    public object Call(object[] arguments)
    {
        return null;
    }
}

public class Fill : ICallable
{
    public int Arity => 0;

    public AstType[] Types => new AstType[0];

    public object Call(object[] arguments)
    {
        return null;
    }
}

// FUNCTIONS

public class Sum : ICallable
{
    public int Arity => 2;

    public AstType[] Types => new[] { AstType.INT, AstType.INT };

    public object Call(object[] Arguments)
    {
        return (int)Arguments[0] + (int)Arguments[1];
    }
}





