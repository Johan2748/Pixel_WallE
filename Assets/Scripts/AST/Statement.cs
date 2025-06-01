using UnityEngine;

public abstract class Statement : AST
{
    
}


public class Tag : Statement
{
    public Token Id;

    public Tag(Token id)
    {
        Id = id;
    }

    public override void Print()
    {
        Debug.Log(Id.Text);
    }

}

public class GoToStatement
{
    
}