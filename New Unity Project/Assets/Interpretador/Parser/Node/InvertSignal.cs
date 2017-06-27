using System;

public class InvertSignal : INode
{
    public int value { get; private set; }
    public INode iNode { get; private set; }
    public bool invert { get; private set; }
    public int lineNumber { get; set; }

    public InvertSignal(INode iNode, int value)
    {
        lineNumber = iNode.lineNumber;
        this.value = value;
        this.iNode = iNode;
        this.invert = false;
    }

    public InvertSignal(INode iNode)
    {
        lineNumber = iNode.lineNumber;
        this.iNode = iNode;
        this.invert = true;
    }

    public INode run()
    {
        IOperator iOperator = iNode.run() as IOperator;
        if (invert)
            return new Bool(!(iOperator as Bool).value, lineNumber);
        else if (iOperator.type == Type.INT)
            return new Integer((iOperator as Integer).value * value, lineNumber);
        return new Float((iOperator as Float).value * value, lineNumber);
    }
}