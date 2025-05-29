using System.Collections.Generic;

public static class ErrorManager
{
    public static List<Error> errors { get; private set; } = new List<Error>();

    public static void AddError(Error error)
    {
        errors.Add(error);   
    }

    
}