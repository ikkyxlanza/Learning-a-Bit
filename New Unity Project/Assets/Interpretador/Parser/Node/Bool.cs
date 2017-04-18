using System;

public class Bool : IOperator
{
    public Type type { get { return Type.BOOL; } set {} }
    public string name { get; set; }
    public bool value { get; private set; }

    public Bool (bool value)
    {
        this.value = value;
    }

    public INode run()
    {
        return this;
    }
}