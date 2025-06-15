
public class Error : Exception
{
    protected int line;
    protected string message;

    public Error(int line, string message)
    {
        this.line = line;
        this.message = message;
    }

    public string GetReport()
    {
        return $"Error: {message} [Line: {line}]\n";
    }
}

public class UnexpectedCharacterError : Error
{
    public UnexpectedCharacterError(int line, char c) : base(line, $"Unexpected character '{c}'") { }
}

public class UnaryExprError : Error
{
    public UnaryExprError(Token op, Expresion expr) : base(op.Line, $"Operator '{op.Text}' cannot be applied to operand of type '{expr.Type}'") { }

}

public class BinOpError : Error
{
    public BinOpError(Expresion left, Token op, Expresion right) : base(op.Line, $"Operator '{op.Text}' cannot be applied to operands of type '{left.Type}' and '{right.Type}'") { }
}

public class PoorlyImplementedFunctionError : Error
{
    public PoorlyImplementedFunctionError(int line, ICallable function) : base(line, $"This function {function} is poorly implemented") { }
}

public class IndexOutOfRangeError : Error
{
    public IndexOutOfRangeError() : base(-1, "Index out of range") { }
}