using System;

public class BreakStatement : INode
{
    public BreakStatement(IteratorTokening it)
    {
    }

    public INode run()
    {
        Interpreter.helper.Push(new BreakConditional());
        return null;
    }
}