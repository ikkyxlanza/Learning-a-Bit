using System;

public class Float : IOperator
{
    public Type type { get { return Type.FLOAT; } set {} }
    public string name { get; set; }
    public float value { get; private set; }

    public Float(float value)
    {
        this.value = value;
    }

    public INode run()
    {
        return this;
    }
}