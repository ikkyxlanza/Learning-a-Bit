using System;

public class Signal : INode
{
    public int value { get; private set; }
    public INode iNode { get; private set; }

    public Signal(INode iNode, int value)
    {
        this.value = value;
        this.iNode = iNode;
    }

    public INode run()
    {
        return new Integer((iNode.run() as INumber).valueI * value);
    }
}