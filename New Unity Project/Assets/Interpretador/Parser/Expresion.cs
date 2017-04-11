using System;
using UnityEngine;

public class Expresion
{
    public static INode expr(IteratorToken it)
    {
        INode iNode = term(it);
        while (it.hasCurrent() && (it.current().type == Type.PLUS || it.current().type == Type.MINUS))
        {
            Token token = it.current();
            if (token.type == Type.PLUS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.PLUS, term(it));
            }
            else if (token.type == Type.MINUS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MINUS, term(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode term(IteratorToken it)
    {
        INode iNode = factor(it);
        while (it.hasCurrent() && (it.current().type == Type.MUL || it.current().type == Type.DIV || it.current().type == Type.MOD))
        {
            Token token = it.current();
            if (token.type == Type.MUL)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MUL, factor(it));
            }
            else if (token.type == Type.DIV)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.DIV, factor(it));
            }
            else if (token.type == Type.MOD)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MOD, factor(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode factor(IteratorToken it)
    {
        Token token = it.current();
        it.next();
        if (token.type == Type.PLUS)
        {
            return new Signal(factor(it), 1);
        }
        else if (token.type == Type.MINUS)
        {
            return new Signal(factor(it), -1);
        }
        else if (token.type == Type.INT)
        {
            return new Integer((token.value as Number).valueI);
        }
        else if (token.type == Type.FLOAT)
        {
            return new Float((token.value as Number).getFloat());
        }
        else if (token.type == Type.LPAREN)
        {
            INode exp = expr(it);
            it.next();
            return exp;
        }
        else if (token.type == Type.RPAREN)
        {
            return new NoOperator();
        }
        return null;
    }
}