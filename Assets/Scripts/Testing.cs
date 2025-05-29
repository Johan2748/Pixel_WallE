using UnityEngine;

public class Testing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string test = "lol<\"po\n00";

        Lexer lexer = new Lexer(test);
        lexer.GetTokens();
        Debug.Log(test);
        Debug.Log(lexer.Tokens.Count);
        foreach (Token token in lexer.Tokens)
        {
            Debug.Log(token.ToString());
        }
        foreach (Error e in ErrorManager.errors)
        {
            e.Report();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
