using System.Collections.Generic;
using UnityEngine;

public class Parser
{
    public List<Token> Tokens;

    private int current = 0;

    public Parser(Lexer lexer)
    {
        Tokens = lexer.Tokens;
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
        if (Current().Type == TokenType.EOF) return true;
        return false;
    }

    private bool Match(TokenType tokenType)
    {
        if (tokenType == Current().Type)
        {
            Advance();
            return true;
        }
        return false;
    }


    private void Eat(TokenType tokenType, string error)
    {
        if (Current().Type == tokenType) Advance();
        else throw new Error(Current().Line, error);
    }




    private Program ParseProgram()
    {
        Program program = new Program();

        while (!IsAtEnd())
        {
            Token current = Current();
            if (current.Type == TokenType.ID) program.Body.Add(ParseStatement());
            else program.Body.Add(ParseExpresion());
        }

        return program;
    }

    public Statement ParseStatement()
    {
        return null;
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
        }catch (Error error)
        {
            error.Report();
            return null;
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
        return new UnaryExpresion(op, ParseExpresion());
    }













































































































































































































































































































}