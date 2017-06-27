using System;

public class BreakStatement : INode
{
    public int lineNumber { get; set; }
    public BreakStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        lineNumber = ite.current().lineNumber;
    }

    public INode run()
    {
        Interpreter.helper.Push(new BreakConditional());
        return null;
    }
}