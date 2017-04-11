using System;
using UnityEngine;

public class BinaryOperator : INode
{
    public INode left { get; set; }
    public Type type { get; set; }
    public INode right { get; set; }

    public BinaryOperator(INode left, Type type, INode right)
    {
        this.left = left;
        this.type = type;
        this.right = right;
    }

    public INode run()
    {
        INumber a = left.run() as INumber;
        INumber b = right.run() as INumber;
        if (a == null || b == null)
            throw new Error("Syntax error!");
        if (a.isInteger && b.isInteger)
            return new Integer(calcInt(a.valueI, b.valueI));
        else if (a.isInteger && !b.isInteger)
            return new Float(calcFloat((float)a.valueI, b.valueF));
        else if (!a.isInteger && b.isInteger)
            return new Float(calcFloat(a.valueF, (float)b.valueI));
        return new Float(calcFloat(a.valueF, b.valueF));
    }

    private int calcInt(int a, int b)
    {
        Debug.Log(a + " " + type + " " + b);
        switch (type)
        {
            case Type.PLUS:
                return a + b;
            case Type.MINUS:
                return a - b;
            case Type.MUL:
                return a * b;
            case Type.DIV:
                return a / b;
            case Type.MOD:
                return a % b;
            default:
                return 0;
        }
    }

    private float calcFloat(float a, float b)
    {
        Debug.Log(a + " " + type + " " + b);
        switch (type)
        {
            case Type.PLUS:
                return a + b;
            case Type.MINUS:
                return a - b;
            case Type.MUL:
                return a * b;
            case Type.DIV:
                return a / b;
            case Type.MOD:
                return a % b;
            default:
                return 0;
        }
    }
}