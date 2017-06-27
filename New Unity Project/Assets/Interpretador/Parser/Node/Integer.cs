using System;

public class Integer : IOperator
{
    public Type type { get { return Type.INT; } set { } }
    public string name { get; set; }
    public int value { get; private set; }
    public int lineNumber { get; set; }

    public Integer(int value, int line)
    {
        lineNumber = line;
        this.value = value;
    }

    public INode run()
    {
        return this;
    }

    public IVariable clone()
    {
        return new Integer(value, lineNumber);
    }
}