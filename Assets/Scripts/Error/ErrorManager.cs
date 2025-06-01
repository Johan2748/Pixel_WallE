using System.Collections.Generic;

public static class ErrorManager
{
    public static bool hadError { get; private set; } = false;
    private static List<Error> errors = new();

    public static void AddError(Error error)
    {
        hadError = true;
        errors.Add(error);
    }

    public static void ShowErrors()
    {
        foreach (Error error in errors)
        {
            error.Report();
        }
    }

}