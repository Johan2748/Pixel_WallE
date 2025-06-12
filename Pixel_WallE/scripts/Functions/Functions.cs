
public interface ICallable
{
    int Arity { get; }

    AstType[] Types { get; }

    AstType ReturnType{ get; }

    bool CheckArguments(List<Expresion> arguments)
    {
        if (Arity != Types.Length) throw new Error(-1, "This function is poorly implemented");

        for (int i = 0; i < Types.Length; i++)
        {
            if (arguments[i] is null) return false;
            if (Types[i] != arguments[i].Type) return false;
        }
        return true;
    }

    object? Call(object[] arguments);
}


// INSTRUCTIONS
public class Spawn : ICallable
{
    public int Arity => 2;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class ChangeColor : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.COLOR];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class Size : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class DrawLine : ICallable
{
    public int Arity => 3;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class DrawCircle : ICallable
{
    public int Arity => 3;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class DrawRectangle : ICallable
{
    public int Arity => 5;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT, AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class Fill : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

// FUNCTIONS

public class Sum : ICallable
{
    public int Arity => 2;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT, AstType.INT];

    public object Call(object[] arguments)
    {
        return (int)arguments[0] + (int)arguments[1];
    }
}

public class GetActualX : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class GetActualY : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class GetCanvasSize : ICallable
{
    public int Arity => 0;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class GetColorCount : ICallable
{
    public int Arity => 5;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.COLOR, AstType.INT, AstType.INT, AstType.INT, AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class IsBrushColor : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.COLOR];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class IsBrushSize : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.INT];

    public object? Call(object[] arguments)
    {
        return null;
    }
}

public class IsCanvasColor : ICallable
{
    public int Arity => 1;

    public AstType ReturnType => AstType.NULL; 

    public AstType[] Types => [AstType.COLOR];

    public object? Call(object[] arguments)
    {
        return null;
    }
}
