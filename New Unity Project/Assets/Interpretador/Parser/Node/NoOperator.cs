using System;

public class NoOperator : INode
{
    public int lineNumber { get; set; }

    public NoOperator (int line)
    {
        lineNumber = -1;
    }

    public INode run()
    {
        return null;
    }
}