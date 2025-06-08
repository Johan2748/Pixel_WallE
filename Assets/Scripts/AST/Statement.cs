

using System.Collections.Generic;
using System.IO;

public abstract class Statement : AST
{

}


public class Label : Statement
{
    public Token Id { get; private set; }

    public Label(Token id)
    {
        Id = id;
        Location = Id.Line;
        Type = AstType.LABEL;
    }

    public override string ToString()
    {
        return Id.Text;
    }

}

public class Var : Statement
{
    public Token Id { get; private set; }
    public Expresion Expresion;
    public object Value { get => Expresion.Value; protected set { } }

    public Var(Token id, Expresion expr)
    {
        Id = id;
        Location = Id.Line;
        Expresion = expr;
        Type = expr.Type;
    }

    public override string ToString()
    {
        return $"{Id.Text}({Value})";
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
        Location = label.Location;
    }

    public override string ToString()
    {
        return "GoTo [" + Label.ToString() + "](" + Condition.ToString() + ")";
    }
}

public class FunctionCall : Statement
{
    public Token Id;
    public List<Expresion> Arguments;

    public FunctionCall(Token id, List<Expresion> arguments)
    {
        Id = id;
        Arguments = arguments;
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