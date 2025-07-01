public static class BuiltInFunctions
{
    private static Dictionary<string, ICallable> functions = [];
    private static Dictionary<string, ICallable> instructions = [];

    public static void RegisterFunction(string name, ICallable function)
    {
        functions[name] = function;
    }

    public static void RegisterInstruction(string name, ICallable instruction)
    {
        instructions[name] = instruction;
    }


    public static bool IsFunction(string name)
    {
        return functions.ContainsKey(name);
    }

    public static bool IsInstruction(string name)
    {
        return instructions.ContainsKey(name);
    }

    public static ICallable GetFunction(string name)
    {
        return functions[name];
    }

    public static ICallable GetInstruction(string name)
    {
        return instructions[name];
    }

}
