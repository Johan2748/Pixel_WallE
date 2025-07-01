public class Interpreter
{
    private int current;
    private int depth = 0;
    private Program program;
    private Dictionary<string, int> CheckPoints = [];
    private Dictionary<string, object> Variables = [];

    public Interpreter(Parser parser)
    {
        program = parser.ParseProgram();
        SetCheckPoints();
    }

    private void SetCheckPoints()
    {
        for (int i = 0; i < program.Body.Count; i++)
        {
            if (program.Body[i] is Label label) CheckPoints[label.Id.Text] = i;
        }
    }




    public void EvaluateProgram()
    {
        if (ErrorManager.HadError || program.Body.Count == 0) return;

        try
        {
            EvaluateSpawn();

            for (current = 1; current < program.Body.Count; current++)
            {
                Statement stmt = program.Body[current]!;
                if (stmt is InstructionCall) EvaluateInstruction(stmt);
                else if (stmt is AssignStatement) EvaluateAssign(stmt);
                else if (stmt is GoToStatement) EvaluateGoto(stmt);
            }
        }
        catch (Error error)
        {
            ErrorManager.AddError(error);
        }
    }


    // EVALUATE STATEMENTS

    private void EvaluateSpawn()
    {
        Statement start = program.Body[0]!;
        if (start.Id.Text != "Spawn") throw new Error(1, "The program should start whith a instruction Spawn(int,int)");

        InstructionCall c = (InstructionCall)start;
        object[] args = new object[c.Function.Arity];
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = EvaluateExpresion(c.Arguments[i]);
        }

        c.Function.Call(args, c.Location);
    }

    private void EvaluateInstruction(Statement statement)
    {
        InstructionCall c = (InstructionCall)statement;
        if (c.Id.Text == "Spawn") throw new Error(c.Location, "Instruction Spawn(int,int) should be at the beggining");

        object[] args = new object[c.Function.Arity];
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = EvaluateExpresion(c.Arguments[i]);
        }
        
        c.Function.Call(args, c.Location);
    }

    private void EvaluateAssign(Statement statement)
    {
        AssignStatement ass = (AssignStatement)statement;
        Variables[ass.Var.Id.Text] = EvaluateExpresion(ass.Expresion);
    }

    private void EvaluateGoto(Statement statement)
    {
        GoToStatement goTo = (GoToStatement)statement;
        if (!CheckPoints.ContainsKey(goTo.Label.Id.Text)) throw new Error(goTo.Location, $"Label {goTo.Label.Id.Text} does not exist");
        if ((bool)EvaluateExpresion(goTo.Condition))
        {
            current = CheckPoints[goTo.Label.Id.Text];
            depth++;
        }
        if (depth > 10000) throw new Error(goTo.Location, "StackOverflow");
    }


    // EVALUATE EXPRESION

    private object EvaluateExpresion(Expresion expresion)
    {
        if (expresion is BinaryExpresion) return EvaluateBinaryExpresion(expresion);
        if (expresion is UnaryExpresion) return EvaluateUnaryExpresion(expresion);
        if (expresion is Literal) return EvaluateLiteral(expresion);
        if (expresion is Function) return EvaluateFunction(expresion);
        else return EvaluateVar(expresion);
    }

    private object EvaluateBinaryExpresion(Expresion expresion)
    {
        BinaryExpresion b = (BinaryExpresion)expresion;
        if (b.Operation.Type == TokenType.PLUS) return (int)EvaluateExpresion(b.Left) + (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.MINUS) return (int)EvaluateExpresion(b.Left) - (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.STAR) return (int)EvaluateExpresion(b.Left) * (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.SLASH)
        {
            if ((int)EvaluateExpresion(b.Right) == 0) throw new Error(b.Location, "You cannot divide by zero");
            return (int)EvaluateExpresion(b.Left) / (int)EvaluateExpresion(b.Right);
        }
        if (b.Operation.Type == TokenType.MOD) return (int)EvaluateExpresion(b.Left) % (int)EvaluateExpresion(b.Right);

        if (b.Operation.Type == TokenType.STAR_STAR)
        {
            int result = 1;

            for (int i = 0; i < (int)EvaluateExpresion(b.Right); i++)
            {
                result *= (int)EvaluateExpresion(b.Left);
            }
            return result;
        }

        if (b.Operation.Type == TokenType.AND) return (bool)EvaluateExpresion(b.Left) && (bool)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.OR) return (bool)EvaluateExpresion(b.Left) || (bool)EvaluateExpresion(b.Right);

        if (b.Operation.Type == TokenType.LESS) return (int)EvaluateExpresion(b.Left) < (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.LESS_EQUAL) return (int)EvaluateExpresion(b.Left) <= (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.GREATER) return (int)EvaluateExpresion(b.Left) > (int)EvaluateExpresion(b.Right);
        if (b.Operation.Type == TokenType.GREATER_EQUAL) return (int)EvaluateExpresion(b.Left) >= (int)EvaluateExpresion(b.Right);

        if (b.Operation.Type == TokenType.EQUAL_EQUAL)
        {
            object left = EvaluateExpresion(b.Left);
            object right = EvaluateExpresion(b.Right);

            if (left is int && right is int) return (int)left == (int)right;
            if (left is bool && right is bool) return (bool)left == (bool)right;
            if (left is string && right is string) return left.ToString() == right.ToString(); 
            return false;
        }
        if (b.Operation.Type == TokenType.NOT_EQUAL)
        {
            object left = EvaluateExpresion(b.Left);
            object right = EvaluateExpresion(b.Right);

            if (left is int && right is int) return (int)left != (int)right;
            if (left is bool && right is bool) return (bool)left != (bool)right;
            if (left is string && right is string) return left.ToString() != right.ToString(); 
            return false;
        }
        else return null!;
    }

    private object EvaluateUnaryExpresion(Expresion expresion)
    {
        UnaryExpresion u = (UnaryExpresion)expresion;
        if (u.Operation.Type == TokenType.NOT) return !(bool)EvaluateExpresion(u.Expresion);
        else if (u.Operation.Type == TokenType.MINUS) return -(int)EvaluateExpresion(u.Expresion);
        else return null!;
    }

    private object EvaluateLiteral(Expresion expresion)
    {
        Literal l = (Literal)expresion;
        if (expresion is Number) return int.Parse(l.Token.Text);
        if (expresion is Bool) return bool.Parse(l.Token.Text);
        else if (expresion is PixelColor) return l.Token.Text;
        else return null!;
    }

    private object EvaluateFunction(Expresion expresion)

    {
        Function f = (Function)expresion;

        object[] args = new object[f.function.Arity];
        for (int i = 0; i < args.Length; i++)
        {
            args[i] = EvaluateExpresion(f.Arguments[i]);
        }

        object result = f.function.Call(args, f.Location)!;

        if (result is int && f.function.ReturnType != AstType.INT) throw new PoorlyImplementedFunctionError(f.function);
        if (result is bool && f.function.ReturnType != AstType.BOOL) throw new PoorlyImplementedFunctionError(f.function);
        if (result is string && f.function.ReturnType != AstType.COLOR) throw new PoorlyImplementedFunctionError(f.function);
        

        return result;
    }

    private object EvaluateVar(Expresion expresion)
    {
        Var v = (Var)expresion;
        if (!Variables.ContainsKey(v.Id.Text)) throw new Error(v.Location, $"The name {v.Id.Text} does not exist in the current context");
        return Variables[v.Id.Text];
    }

}
