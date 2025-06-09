using System;
using System.Collections.Generic;
using UnityEngine;

public class RunCode : MonoBehaviour
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

        BuiltInFunctions.RegisterFunction("Sum", new Sum());
        /*
        BuiltInFunctions.RegisterInstruction("Spawn", new Spawn());
        BuiltInFunctions.RegisterInstruction("Spawn", new Spawn());
        BuiltInFunctions.RegisterInstruction("Spawn", new Spawn());
        */
    }


    public void Run()
    {
        InitializeFunctionsAndInstructions();
        ErrorManager.Restart();
        Lexer lexer = new Lexer(ReadInputText.input);
        Parser parser = new Parser(lexer);
        Debug.Log(parser.Tokens.Count);
        foreach (Token token in parser.Tokens)
        {
            Debug.Log(token.ToString());
        }
        Program program = parser.ParseProgram();
        ErrorManager.ShowErrors();
        Debug.Log(program.ToString());

    }


}
