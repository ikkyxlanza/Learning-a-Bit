using System;

public class ContinueConditional : IConditional
{
    public bool check(Type type)
    {
        if (type == Type.WHILE || type == Type.FOR) return true;
        return false;
    }
}