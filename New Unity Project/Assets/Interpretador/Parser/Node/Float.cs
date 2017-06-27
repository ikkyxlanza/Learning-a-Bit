using System;

public class Float : IOperator
{
    public Type type { get { return Type.FLOAT; } set { } }
    public string name { get; set; }
    public float value { get; private set; }
    public int lineNumber { get; set; }

    public Float(float value, int line)
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
        return new Float(value, lineNumber);
    }
}