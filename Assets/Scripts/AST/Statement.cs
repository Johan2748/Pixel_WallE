

public abstract class Statement : AST
{

}


public class Label : Statement
{
    public Token Id;

    public Label(Token id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return Id.Text;
    }

}

public class GoToStatement : Statement
{
    public Expresion Condition;
    public Label Label;

    public GoToStatement(Label label, Expresion expresion)
    {
        Label = label;
        Condition = expresion;
    }

    public override string ToString()
    {
        return "GoTo [" + Label.ToString() + "](" + Condition.ToString() + ")";
    }
}


public class Assign
{
    
}