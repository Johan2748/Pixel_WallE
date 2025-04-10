using System.Collections.Generic;
using UnityEngine;

public class Lexer
{
    public List<Token> Tokens { get; private set; }

    private string Text;

    private Dictionary<string, Token> ReservedKewwords;

    public Lexer(string text)
    {
        Text = text;
    }
}
