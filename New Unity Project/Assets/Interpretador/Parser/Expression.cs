using System;
using UnityEngine;

public class Expression
{
    public static INode expr(IteratorToken it)
    {
        INode iNode = term1(it);
        while (it.hasCurrent())
        {
            Token token = it.current();
            if (token.type == Type.AND)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.AND, term1(it));
            }
            else if (token.type == Type.OR)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.OR, term1(it));
            }
            else break;
        }
        return iNode;
    }
    private static INode term1(IteratorToken it)
    {
        INode iNode = term2(it);
        while (it.hasCurrent())
        {
            Token token = it.current();
            if (token.type == Type.GREATER)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.GREATER, term2(it));
            }
            else if (token.type == Type.LESS_THAN)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.LESS_THAN, term2(it));
            }
            else if (token.type == Type.EQUALS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.EQUALS, term2(it));
            }
            else if (token.type == Type.DIFFERENT)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.DIFFERENT, term2(it));
            }
            else if (token.type == Type.GREATER_EQUALS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.GREATER_EQUALS, term2(it));
            }
            else if (token.type == Type.LESS_THAN_EQUALS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.LESS_THAN_EQUALS, term2(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode term2(IteratorToken it)
    {
        INode iNode = term3(it);
        while (it.hasCurrent())
        {
            Token token = it.current();
            if (token.type == Type.PLUS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.PLUS, term3(it));
            }
            else if (token.type == Type.MINUS)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MINUS, term3(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode term3(IteratorToken it)
    {
        INode iNode = term4(it);
        while (it.hasCurrent())
        {
            Token token = it.current();
            if (token.type == Type.MUL)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MUL, term4(it));
            }
            else if (token.type == Type.DIV)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.DIV, term4(it));
            }
            else if (token.type == Type.MOD)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.MOD, term4(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode term4(IteratorToken it)
    {
        INode iNode = factor(it);
        while (it.hasCurrent())
        {
            Token token = it.current();
            if (token.type == Type.POW)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.POW, factor(it));
            }
            else if (token.type == Type.SQRT)
            {
                it.next();
                iNode = new BinaryOperator(iNode, Type.SQRT, factor(it));
            }
            else break;
        }
        return iNode;
    }

    private static INode factor(IteratorToken it)
    {
        Token token = it.current();
        int line = it.current().lineNumber;
        if (token.type == Type.COLON || token.type == Type.COMMA || token.type == Type.RBRACKET || token.type == Type.PIPE)
            return new NoOperator(line);
        it.next();
        if (token.type == Type.PLUS)
            return new InvertSignal(factor(it), 1);
        else if (token.type == Type.MINUS)
            return new InvertSignal(factor(it), -1);
        else if (token.type == Type.NOT)
            return new InvertSignal(factor(it));
        else if (token.type == Type.INT)
            return new Integer((token.value as Number).valueI, line);
        else if (token.type == Type.FLOAT)
            return new Float((token.value as Number).getFloat(), line);
        else if (token.type == Type.TRUE)
            return new Bool(true, line);
        else if (token.type == Type.FALSE)
            return new Bool(false, line);
        else if (token.type == Type.ID)
        {
            IVariable iVariable = Interpreter.variables.getVariable((token.value as KeyWord).word);
            if (it.current() != null && it.current().type == Type.LBRACKET)
            {
                it.next();
                INode check = new CheckElement(iVariable.name, Expression.expr(it));
                if (it.current().type != Type.RBRACKET)
                    throw new Error("Expression RBRACKET");
                it.next();
                return check;
            }
            else if (iVariable != null)
                return new CheckVariable(iVariable.name, line);
            else
                throw new Error("Variable " + (token.value as KeyWord).word + " need be create before used!");
        }
        else if (token.type == Type.HASH)
        {
            if (it.current().type == Type.ID)
            {
                IVariable iVariable = Interpreter.variables.getVariable((it.current().value as KeyWord).word);
                it.next();
                if (iVariable != null)
                    return new CheckLength(iVariable.name, line);
                else
                    throw new Error("Variable " + (token.value as KeyWord).word + " need be create before used!");
            }
            else
                throw new Error("Expression ID");

        }
        else if (token.type == Type.LPAREN)
        {
            INode exp = expr(it);
            it.next();
            return exp;
        }
        else if (token.type == Type.RPAREN)
            return new NoOperator(line);
        return null;
    }
}