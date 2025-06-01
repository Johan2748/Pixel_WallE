using System.Collections.Generic;
using UnityEditor.Purchasing;

public class Parser
{
    public List<Token> Tokens;

    private int current = 0;

    public Parser(Lexer lexer)
    {
        Tokens = lexer.Tokens;
    }

    private Token Advance()
    {
        if (!IsAtEnd()) current++;
        return Previous();
    }

    private Token Peek()
    {
        return Tokens[current];
    }

    private Token Previous()
    {
        return Tokens[current - 1];
    }

    private bool IsAtEnd()
    {
        if (Peek().Type is TokenType.EOF) return true;
        return false;
    }

    private bool Eat(TokenType tokenType)
    {
        if (tokenType == Peek().Type) return true;
        return false;
    }





    private Program ParseProgram()
    {
        Program program = new Program();

        while (!IsAtEnd())
        {
            if (Advance().Type == TokenType.ID) program.Body.Add(ParseStatement());
            else program.Body.Add(ParseExpresion());
        }

        return program;
    }

    public Statement ParseStatement()
    {
        return null;
    }

    public Expresion ParseExpresion()
    {
        return null;
    }







    public Tag ParseTag()
    {
        if (Eat(TokenType.ID)) return new Tag(Peek());
        return null;
    }
}