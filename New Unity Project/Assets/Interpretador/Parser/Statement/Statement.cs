using System;

public class Statement : INode
{
    public INode current { get; private set; }
    public INode next { get; set; }

    public Statement(INode current)
    {
        this.current = current;
        this.next = null;
    }

    public INode run()
    {
        IConditional iCond = Interpreter.helper.Count > 0 ? Interpreter.helper.Peek() : new TrueConditional();
        if (iCond.check(Type.NONE)) current.run();
        if (next != null) next.run();
        return null;
    }
}