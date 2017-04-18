using System;

public class ContinueConditional : IConditional
{
    public bool check(Type type)
    {
        if (type == Type.WHILE) return true;
        return false;
    }
}