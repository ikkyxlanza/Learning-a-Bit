using System;

public class Bool : IOperator
{
    public Type type { get { return Type.BOOL; } set { } }
    public string name { get; set; }
    public bool value { get; private set; }
    public int lineNumber { get; set; }

    public Bool(bool value, int line)
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
        return new Bool(value, lineNumber);
    }
}