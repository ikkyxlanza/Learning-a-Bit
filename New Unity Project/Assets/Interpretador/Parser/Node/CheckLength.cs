using System;

public class CheckLength : INode
{
    private string vector { get; set; }

    public CheckLength(string vector)
    {
        this.vector = vector;
    }

    public INode run()
    {
        return (Interpreter.variables.getVariable(vector) as Vector).Length.run();
    }
}