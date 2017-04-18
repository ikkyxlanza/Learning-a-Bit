using System;

public class ContinueStatement : INode
{
    public ContinueStatement(IteratorTokening it)
    {
    }

    public INode run()
    {
        Interpreter.helper.Push(new ContinueConditional());
        return null;
    }
}