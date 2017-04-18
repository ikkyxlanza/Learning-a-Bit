using System;

public class Statement : INode
{
    public INode current { get; private set; }
    public INode next { get; set; }

    public Statement (INode current)
    {
        this.current = current;
        this.next = null;
    }

    public INode run ()
    {
        current.run();
        if(next != null) next.run();
        return null;
    }
}