using System;

public class CheckLength : INode
{
    private string vector { get; set; }
    public int lineNumber { get; set; }

    public CheckLength(string vector, int line)
    {
        lineNumber = line;
        this.vector = vector;
    }

    public INode run()
    {
        return (Interpreter.variables.getVariable(vector) as Vector).Length.run();
    }
}