using UnityEngine;

public class Error
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
        Debug.Log($"Error: {message} [Line: {line}]");
    }
}