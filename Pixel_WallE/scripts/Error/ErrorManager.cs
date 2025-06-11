public static class ErrorManager
{
    public static bool HadError { get; private set; } = false;
    private static List<Error> errors = [];

    public static void AddError(Error error)
    {
        HadError = true;
        errors.Add(error);
    }

    public static void ShowErrors()
    {
        string errorList = "";
        foreach (Error error in errors)
        {
            errorList += error.GetReport();
        }        
        MessageBox.Show(errorList);
    }

    public static void Restart()
    {
        errors.Clear();
        HadError = false;
    }

}