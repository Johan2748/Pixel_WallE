using System.Collections.Generic;

public static class BuiltInFunctions
{
    private static Dictionary<string, ICallable> functions = new();

    public static void Register(string name, ICallable function)
    {
        functions[name] = function;
    }

    public static bool IsAlreadyDeclared(string name)
    {
        return functions.ContainsKey(name);
    }

    public static ICallable GetFuction(string name)
    {
        return functions[name];
    }

}
