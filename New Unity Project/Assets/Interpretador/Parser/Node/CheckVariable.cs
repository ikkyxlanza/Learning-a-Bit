using System;

public class CheckVariable : INode
{
    private string name { get; set; }

    public CheckVariable(string name)
    {
        this.name = name;
    }

    public INode run()
    {
        return Interpreter.variables.getVariable(name) as IOperator;
    }
}