using System;

public class ContinueStatement : INode
{
    public int lineNumber { get; set; }
    public ContinueStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        lineNumber = ite.current().lineNumber;
    }

    public INode run()
    {
        Interpreter.helper.Push(new ContinueConditional());
        return null;
    }
}