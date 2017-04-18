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
        IOperator a = (left.run() as IOperator);
        IOperator b = (right.run() as IOperator);
        if (a == null || b == null)
            throw new Error("Syntax error!");
        if (a.type == Type.BOOL && b.type == Type.BOOL)
            return calcBool((a as Bool).value, (b as Bool).value);
        else if (a.type == Type.INT && b.type == Type.INT)
            return calcInt((a as Integer).value, (b as Integer).value);
        else if (a.type == Type.INT && b.type == Type.FLOAT)
            return calcFloat((float)(a as Integer).value, (b as Float).value);
        else if (a.type == Type.FLOAT && b.type == Type.INT)
            return calcFloat((a as Float).value, (float)(b as Integer).value);
        else if (a.type == Type.FLOAT && b.type == Type.FLOAT)
            return calcFloat((a as Float).value, (b as Float).value);
        else 
            throw new Error("Can't operator " + type + " with " + a.type + " and " + b.type);
    }

    private IOperator calcInt(int a, int b)
    {
        Debug.Log(a + " " + type + " " + b);
        switch (type)
        {
            case Type.PLUS:
                return new Integer(a + b);
            case Type.MINUS:
                return new Integer(a - b);
            case Type.MUL:
                return new Integer(a * b);
            case Type.DIV:
                return new Integer(a / b);
            case Type.MOD:
                return new Integer(a % b);
            case Type.POW:
                return new Integer((int)Math.Pow(a, b));
            case Type.SQRT:
                return new Float((float)Math.Pow((float)a, (1/(float)b)));
            case Type.GREATER:
                return new Bool(a > b);
            case Type.LESS_THAN:
                return new Bool(a < b);
            case Type.EQUALS:
                return new Bool(a == b);
            case Type.DIFFERENT:
                return new Bool(a != b);
            case Type.GREATER_EQUALS:
                return new Bool(a >= b);
            case Type.LESS_THAN_EQUALS:
                return new Bool(a <= b);
            default:
                return new Integer(0);
        }
    }

    private IOperator calcFloat(float a, float b)
    {
        Debug.Log(a + " " + type + " " + b);
        switch (type)
        {
            case Type.PLUS:
                return new Float(a + b);
            case Type.MINUS:
                return new Float(a - b);
            case Type.MUL:
                return new Float(a * b);
            case Type.DIV:
                return new Float(a / b);
            case Type.MOD:
                return new Float(a % b);
            case Type.POW:
                return new Float((float)Math.Pow(a, b));
            case Type.SQRT:
                return new Float((float)Math.Pow(a, (1/b)));
            case Type.GREATER:
                return new Bool(a > b);
            case Type.LESS_THAN:
                return new Bool(a < b);
            case Type.EQUALS:
                return new Bool(a == b);
            case Type.DIFFERENT:
                return new Bool(a != b);
            case Type.GREATER_EQUALS:
                return new Bool(a >= b);
            case Type.LESS_THAN_EQUALS:
                return new Bool(a <= b);
            default:
                return new Float(0);
        }
    }

    private IOperator calcBool(bool a, bool b)
    {
        Debug.Log(a + " " + type + " " + b);
        switch (type)
        {
            case Type.AND:
                return new Bool(a && b);
            case Type.OR:
                return new Bool(a || b);
            case Type.EQUALS:
                return new Bool(a == b);
            case Type.DIFFERENT:
                return new Bool(a != b);
            default:
                return new Bool(false);
        }
    }
}