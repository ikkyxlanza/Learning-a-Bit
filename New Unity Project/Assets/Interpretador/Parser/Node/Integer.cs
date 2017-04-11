using System;

public class Integer : INumber
{
    public bool isInteger { get { return true; } private set { } }
    public int valueI { get; private set; }
    public float valueF { get; private set; }

    public Integer(int value)
    {
        this.valueI = value;
    }

    public INode run()
    {
        return this;
    }
}