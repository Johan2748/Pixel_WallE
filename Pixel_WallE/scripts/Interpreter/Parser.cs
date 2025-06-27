public class Parser
{
    public List<Token> Tokens;
    public Scope Scope { get; private set; }

    private int current = 0;

    public Parser(Lexer lexer)
    {
        Tokens = lexer.Tokens;
        Scope = new();
    }

    private Token Current()
    {
        return Tokens[current];
    }

    private Token Previous()
    {
        return Tokens[current - 1];
    }

    private Token Peek()
    {
        return Tokens[current + 1];
    }

    private void Advance()
    {
        current++;
    }

    private bool IsAtEnd()
    {
        if ((current >= Tokens.Count) || (Current().Type == TokenType.EOF)) return true;
        return false;
    }

    private bool CheckType(TokenType tokenType)
    {
        if (tokenType == Current().Type)
        {
            return true;
        }
        return false;
    }

    private bool Match(TokenType tokenType)
    {
        if (CheckType(tokenType))
        {
            Advance();
            return true;
        }
        return false;
    }


    private void Eat(TokenType tokenType, string error)
    {
        if (CheckType(tokenType)) Advance();
        else throw new Error(Current().Line, error);
    }

    private void Synchronize()
    {
        while (!IsAtEnd())
        {
            Advance();
            if (Previous().Type == TokenType.EO_LINE) break;
        }
    }



    public Program ParseProgram()
    {
        Program program = new Program();

        while (!IsAtEnd())
        {
            while(Match(TokenType.EO_LINE)){}
            if(!IsAtEnd())program.Body.Add(ParseStatement());
        }

        return program;
    }




    // PARSER DE EXPRESIONES

    public Expresion ParseExpresion()
    {
        try
        {
            Expresion expr = ParseBooleanTerm();
            while (Match(TokenType.AND))
            {
                Token op = Previous();
                expr = new BinaryExpresion(expr, op, ParseBooleanTerm());

            }
            return expr;
        }
        catch (Error error)
        {
            ErrorManager.AddError(error);
            return null!;
        }

    }

    private Expresion ParseBooleanTerm()
    {
        Expresion expr = ParseComparison();
        while (Match(TokenType.OR))
        {
            Token op = Previous();
            expr = new BinaryExpresion(expr, op, ParseComparison());
        }
        return expr;
    }

    private Expresion ParseComparison()
    {
        Expresion expr = ParseArithmeticExpresion();
        while (Match(TokenType.GREATER) || Match(TokenType.GREATER_EQUAL) || Match(TokenType.LESS) || Match(TokenType.LESS_EQUAL) || Match(TokenType.EQUAL_EQUAL) || Match(TokenType.NOT_EQUAL))
        {
            Token op = Previous();
            expr = new BinaryExpresion(expr, op, ParseArithmeticExpresion());
        }
        return expr;
    }

    private Expresion ParseArithmeticExpresion()
    {
        Expresion expr = ParseTerm();
        while (Match(TokenType.PLUS) || Match(TokenType.MINUS))
        {
            Token op = Previous();
            expr = new BinaryExpresion(expr, op, ParseTerm());
        }
        return expr;
    }

    private Expresion ParseTerm()
    {
        Expresion expr = ParsePow();
        while (Match(TokenType.STAR) || Match(TokenType.SLASH) || Match(TokenType.MOD))
        {
            Token op = Previous();
            expr = new BinaryExpresion(expr, op, ParsePow());
        }
        return expr;
    }

    private Expresion ParsePow()
    {
        Expresion expr = ParseFactor();
        while (Match(TokenType.STAR_STAR))
        {
            Token op = Previous();
            expr = new BinaryExpresion(expr, op, ParseFactor());
        }
        return expr;
    }

    private Expresion ParseFactor()
    {
        if (Match(TokenType.NUMBER)) return new Number(Previous());
        else if (Match(TokenType.FALSE) || Match(TokenType.TRUE)) return new Bool(Previous());
        else if (Match(TokenType.MINUS) || Match(TokenType.PLUS) || Match(TokenType.NOT)) return ParseUnaryExpr();
        else if (Match(TokenType.COLOR)) return new PixelColor(Previous());
        else if (CheckType(TokenType.ID) && Peek().Type == TokenType.LEFT_PAREN)
        {
            Match(TokenType.ID);
            return ParseFunction();
        }
        else if (Match(TokenType.ID))
        {
            Token var = Previous();
            if (!Scope.ContainsVar(var)) throw new Error(var.Line, $"The name '{var.Text}' does not exist in the current context");
            return Scope.GetVar(var);
        }
        else if (Match(TokenType.LEFT_PAREN))
        {
            Expresion expr = ParseExpresion();
            Eat(TokenType.RIGHT_PAREN, "Expected ')' after expresion");
            return expr;
        }
        else throw new Error(Current().Line, "Invalid Expresion");
    }

    private UnaryExpresion ParseUnaryExpr()
    {
        Token op = Previous();
        return new UnaryExpresion(op, ParseFactor());
    }

    private Function ParseFunction()
    {
        Token id = Previous();
        if (!BuiltInFunctions.IsFunction(id.Text)) throw new Error(id.Line, "Unknown function");
        var function = BuiltInFunctions.GetFunction(id.Text);
        Eat(TokenType.LEFT_PAREN, "Expected '(' before arguments");
        List<Expresion> arguments = [];
        if (!CheckType(TokenType.RIGHT_PAREN))
        {
            do
            {
                arguments.Add(ParseExpresion());
            }
            while (Match(TokenType.COMMA));
        }
        Eat(TokenType.RIGHT_PAREN, "Expected ')' after arguments");

        if (arguments.Count != function.Arity) throw new Error(id.Line, $"function {id.Text} expects {function.Arity} arguments");
        if (!function.CheckArguments(arguments)) throw new Error(id.Line, "Invalid argument in function");

        return new Function(id, arguments, function);
    }

    // PARSER DE DECLARACIONES


    public Statement? ParseStatement()
    {

        try
        {
            Statement? stmt = null;
            if (Match(TokenType.GOTO)) stmt = ParseGoTo();
            else
            {
                Eat(TokenType.ID, "Only assignment, call and label declaration can be used as a statement");
                if (CheckType(TokenType.EO_LINE) || CheckType(TokenType.EOF)) stmt = ParseLabel();
                else if (CheckType(TokenType.ASSIGN)) stmt = ParseAssign();
                else stmt = ParseInstructionCall();
            }
            if (!IsAtEnd()) Eat(TokenType.EO_LINE, "Expected end of line after a statment");
            return stmt;
        }
        catch (Error error)
        {
            ErrorManager.AddError(error);
            Synchronize();
            return null;
        }
    }

    public Label ParseLabel()
    {
        Label label = new(Previous());
        if (Scope.ContainsLabel(label))
        {
            throw new Error(label.Location, $"A label named '{label.Id.Text}' is alredy define");
        }
        Scope.AddLabel(label);
        return label;
    }

    public AssignStatement? ParseAssign()
    {
        Token id = Previous();
        Advance();
        Expresion expr = ParseExpresion();
        if (expr is not null)
        {
            Var var = new(id, expr);
            Scope.AddVar(var);
            return new AssignStatement(var,expr);
        }
        return null;
    }

    public GoToStatement ParseGoTo()
    {
        Eat(TokenType.LEFT_BRACKET, "Expected '[' after GoTo instruction");
        Eat(TokenType.ID, "Expected label");
        Label label = new(Previous());
        Eat(TokenType.RIGHT_BRACKET, "Expected ']' after label");
        Eat(TokenType.LEFT_PAREN, "Expected '(' before condition");
        Expresion expr = ParseExpresion();
        if (expr is null) throw new Error(label.Location, "Invalid condition");
        if (expr.Type != AstType.BOOL) throw new Error(expr.Location, "Invalid condition");
        Eat(TokenType.RIGHT_PAREN, "Expected ')' after condition");
        return new GoToStatement(label, expr);
    }

    public InstructionCall ParseInstructionCall()
    {
        Token id = Previous();
        if (!BuiltInFunctions.IsInstruction(id.Text)) throw new Error(id.Line, "Unknown instruction");
        var instruction = BuiltInFunctions.GetInstruction(id.Text);
        Eat(TokenType.LEFT_PAREN, "Expected '(' before arguments");
        List<Expresion> arguments = new();
        if (!CheckType(TokenType.RIGHT_PAREN))
        {
            do
            {
                arguments.Add(ParseExpresion());
            }
            while (Match(TokenType.COMMA));
        }
        Eat(TokenType.RIGHT_PAREN, "Expected ')' after arguments");

        if (arguments.Count != instruction.Arity) throw new Error(id.Line, $"Instruction {id.Text} expects {instruction.Arity} arguments");
        if (!instruction.CheckArguments(arguments)) throw new Error(id.Line, "Invalid argument in instruction");

        return new InstructionCall(id, arguments, instruction);
    }







}