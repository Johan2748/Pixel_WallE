
public abstract class Statement : AST
{
    public abstract Token Id { get; protected set; }
}


public class Label : Statement
{
    public override Token Id { get; protected set; }

    public Label(Token id)
    {
        Id = id;
        Location = Id.Line;
    }

    public override string ToString()
    {
        return Id.Text;
    }

}

public class GoToStatement : Statement
{
    public override Token Id { get; protected set; }
    public Expresion Condition;
    public Label Label;

    public GoToStatement(Label label, Expresion expresion)
    {
        Label = label;
        Condition = expresion;
        Location = label.Location;
        Id = new Token(TokenType.GOTO, "GoTo", Location);
    }

    public override string ToString()
    {
        return "GoTo [" + Label.ToString() + "](" + Condition.ToString() + ")";
    }
}

public class InstructionCall : Statement
{
    public override Token Id { get; protected set; }
    public List<Expresion> Arguments;

    public ICallable Function;

    public InstructionCall(Token id, List<Expresion> arguments, ICallable fuction)
    {
        Id = id;
        Arguments = arguments;
        Function = fuction;
        Location = id.Line;
    }

    public override string ToString()
    {
        string text = Id.Text + "(";
        for (int i = 0; i < Arguments.Count; i++)
        {
            if (i == Arguments.Count - 1) text += Arguments[^1];
            else text += Arguments[i].ToString() + ",";
        }
        text += ")";

        return text;
    }

}

public class AssignStatement : Statement
{
    public Var Var;
    public Expresion Expresion;

    public override Token Id { get; protected set; }

    public AssignStatement(Var var, Expresion expresion)
    {
        Var = var;
        Expresion = expresion;
        Location = var.Location;
        Id = new Token(TokenType.ASSIGN, "<-", Location);
    }

    public override string ToString()
    {
        return Var.ToString() + " <- " + Expresion.ToString();
    }
}