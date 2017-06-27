using System;
using UnityEngine;

public class IfStatement : INode
{
    public INode expr { get; private set; }
    public Statement se { get; private set; }
    public INode senao { get; private set; }
    public int lineNumber { get; set; }

    public IfStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        lineNumber = ite.current().lineNumber;
        senao = null;
        ite.next();
        expr = Expression.expr(ite);
        Token token = ite.current();
        if (token.type == Type.COLON)
        {
            se = new Statement(null);
            Statement state = se;
            do
            {
                INode iNode = Line.statement(it);
                Statement newSta = new Statement(iNode);
                state.next = newSta;
                state = newSta;
            } while (it.current().token.type != Type.COLON || it.current().token.end);
            it.current().token.end = true;
            se = se.next as Statement;

            if (it.current().token.next != null && it.current().token.next.type == Type.ELSE)
                senao = new ElseStatement(it);
            else if (it.lookNext() != null && it.lookNext().token != null && it.lookNext().token.type == Type.ELSE)
            {
                it.next();
                senao = new ElseStatement(it);
            }
        }
        else
            throw new Error("Syntax error!");

    }

    public INode run()
    {
        if ((expr.run() as Bool).value)
            se.run();
        else if (senao != null)
            senao.run();
        return null;
    }
}