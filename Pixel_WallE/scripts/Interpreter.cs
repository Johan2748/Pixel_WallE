public class Interpreter
{
    private int current;
    private Parser parser;
    public Program program;
    public Dictionary<string, int> CheckPoints = [];
    public Dictionary<string, object> Variables = [];

    public Interpreter(Parser parser)
    {
        this.parser = parser;
        program = parser.ParseProgram();
        SetCheckPoints();
    }

    private void SetCheckPoints()
    {
        for (int i = 0; i < program.Body.Count; i++)
        {
            if (program.Body[i] is Label label) CheckPoints[label.Id.Text] = i;
        }
    }




    public void EvaluateProgram()
    {
        if (ErrorManager.HadError || program.Body.Count == 0) return;

        try
        {
            EvaluateSpawn();

            for (current = 0; current < program.Body.Count; current++)
            {
                Statement stmt = program.Body[current]!;
                if (stmt is InstructionCall) EvaluateInstruction(stmt);
                else if (stmt is GoToStatement) EvaluateGoto(stmt);
            }
        }
        catch (Error error)
        {
            ErrorManager.AddError(error);
        }
        

        
    }



    private void EvaluateSpawn()
    {
        Statement start = program.Body[0]!;
        if (start.Id.Text != "Spawn") throw new Error(1, "The program should start whith a instruction Spawn(int,int)");
        
    }

    public void EvaluateVar(Statement statement)
    {
        Var var = null!;
        var = (Var)statement;
    }    

    public void EvaluateInstruction(Statement statement)
    {

    }


    public void EvaluateGoto(Statement statement)
    {
        GoToStatement goTo = null!;
        goTo = (GoToStatement)statement;
        if ((bool)goTo.Condition.Value!)
        {
            current = CheckPoints[goTo.Label.Id.Text];
            MessageBox.Show($"Entro en el bucle {goTo.Label} y ahore nos vamos al lugar {current} ");
        }
    }

















}
