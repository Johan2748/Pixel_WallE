using UnityEngine;

public class RunCode : MonoBehaviour
{
    public void Run()
    {
        Lexer lexer = new Lexer(ReadInputText.input);
        Parser parser = new Parser(lexer);
        Debug.Log(parser.Tokens.Count);
        foreach (Token token in parser.Tokens)
        {
            Debug.Log(token.ToString());
        }
        Expresion e = parser.ParseExpresion();
        Debug.Log(e.ToString());
        Debug.Log(e.Value);

        ErrorManager.ShowErrors();
    }

}
