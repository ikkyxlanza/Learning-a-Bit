using System;

public class Integer : IOperator
{
    public Type type { get { return Type.INT; } set {} }
    public string name { get; set; }
    public int value { get; private set; }

    public Integer(int value)
    {
        this.value = value;
    }

    public INode run()
    {
        return this;
    }
}