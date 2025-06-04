using System;
using UnityEngine;

public class Error : Exception
{
    private int line;
    private string message;

    public Error(int line, string message)
    {
        this.line = line;
        this.message = message;
    }

    public void Report()
    {
        Debug.LogError($"Error: {message} [Line: {line}]");
    }
}