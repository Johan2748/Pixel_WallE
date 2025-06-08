using UnityEngine;

public class RunCode : MonoBehaviour
{
    public void Run()
    {
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
