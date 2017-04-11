using System;

public class Float : INumber
{
    public bool isInteger { get { return false; } private set { } }
    public int valueI { get; private set; }
    public float valueF { get; private set; }

    public Float(float value)
    {
        this.valueF = value;
    }

    public INode run()
    {
        return this;
    }
}