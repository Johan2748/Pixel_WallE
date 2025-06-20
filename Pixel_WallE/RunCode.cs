public class RunCode
{

    public void InitializeFunctionsAndInstructions()
    {
        BuiltInFunctions.RegisterInstruction("Spawn", new Spawn());
        BuiltInFunctions.RegisterInstruction("Color", new ChangeColor());
        BuiltInFunctions.RegisterInstruction("Size", new Size());
        BuiltInFunctions.RegisterInstruction("DrawLine", new DrawLine());
        BuiltInFunctions.RegisterInstruction("DrawCircle", new DrawCircle());
        BuiltInFunctions.RegisterInstruction("DrawRectangle", new DrawRectangle());
        BuiltInFunctions.RegisterInstruction("Fill", new Fill());

        BuiltInFunctions.RegisterFunction("GetActualX", new GetActualX());
        BuiltInFunctions.RegisterFunction("GetActualY", new GetActualY());
        BuiltInFunctions.RegisterFunction("GetCanvasSize", new GetCanvasSize());
        BuiltInFunctions.RegisterFunction("GetColorCount", new GetColorCount());
        BuiltInFunctions.RegisterFunction("IsBrushColor", new IsBrushColor());
        BuiltInFunctions.RegisterFunction("IsBrushSize", new IsBrushSize());
        BuiltInFunctions.RegisterFunction("IsCanvasColor", new IsCanvasColor());

    }


    public RunCode(string text)
    {
        InitializeFunctionsAndInstructions();
        ErrorManager.Restart();
        Lexer lexer = new Lexer(text);
        Parser parser = new Parser(lexer);
        Interpreter interpreter = new Interpreter(parser);
        interpreter.EvaluateProgram();
        
        if (ErrorManager.HadError) ErrorManager.ShowErrors();
                

    }

}