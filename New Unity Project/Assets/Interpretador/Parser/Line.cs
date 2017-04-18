using System;
using UnityEngine;

public class Line
{
    public static INode statement(IteratorTokening it)
    {
        if (it.hasNext())
            switch (it.next().token.type)
            {
                case Type.ID:
                    return new VariableStatement(it);
                case Type.IF:
                    return new IfStatement(it);
                case Type.WHILE:
                    return new WhileStatement(it);
                case Type.BREAK:
                    return new BreakStatement(it);
                case Type.CONTINUE:
                    return new ContinueStatement(it);
                case Type.COLON:
                    return new NoOperator();
                default:
                    throw new Error("Erro no código!");
            }
        return null;
    }
}