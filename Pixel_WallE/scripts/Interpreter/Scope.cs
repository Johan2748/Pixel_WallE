public class Scope
{
    private List<Label> labels;
    private List<Var> variables { get; }

    public Scope()
    {
        labels = [];
        variables = [];
    }

    public void AddLabel(Label label)
    {
        labels.Add(label);
    }

    public bool ContainsLabel(Label label)
    {
        for (int i = 0; i < labels.Count; i++)
        {
            if (label.Id.Text == labels[i].Id.Text) return true;
        }
        return false;
    }

    public void AddVar(Var var)
    {
        if (ContainsVar(var.Id))
        {
            variables.Remove(var);
        }
        variables.Add(var);
    }

    public bool ContainsVar(Token var)
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (var.Text == variables[i].Id.Text) return true;
        }
        return false;
    }

    public Var GetVar(Token var)
    {
        Var result = null!;
        for (int i = 0; i < variables.Count; i++)
        {
            if (var.Text == variables[i].Id.Text) result = variables[i];
        }
        return result;
    }
}
