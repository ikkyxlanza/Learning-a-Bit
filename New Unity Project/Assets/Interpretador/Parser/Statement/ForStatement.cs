using System;
using UnityEngine;

public class ForStatement : INode
{
    private Statement body { get; set; }
    private INode variable { get; set; }
    private INode expr { get; set; }
    private INode pass { get; set; }
    public int lineNumber { get; set; }

    public ForStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        lineNumber = ite.current().lineNumber;
        Debug.Log(lineNumber + " vvvvv");
        ite.next();
        if (ite.current().type == Type.PIPE) variable = null;
        else variable = new VariableStatement(ite);
        if (ite.current().type == Type.PIPE)
        {
            ite.next();
            expr = Expression.expr(ite);
            if (ite.current().type == Type.PIPE)
            {
                ite.next();
                pass = new VariableStatement(ite);
                if (ite.current().type == Type.COLON)
                {
                    body = new Statement(null);
                    Statement state = body;
                    do
                    {
                        INode iNode = Line.statement(it);
                        Statement newSta = new Statement(iNode);
                        state.next = newSta;
                        state = newSta;
                    } while (it.current().token.type != Type.COLON || it.current().token.end);
                    it.current().token.end = true;
                    state.current.lineNumber = lineNumber;
                    body = body.next as Statement;
                }
                else
                    throw new Error("Faltou DOIS PONTOS!");
            }
            else
                throw new Error("Faltou PIPE!");
        }
        else
            throw new Error("Faltou PIPE!");
    }

    public INode run()
    {
        IConditional check = new TrueConditional();
        if (variable != null) variable.run();
        while (check.check(Type.FOR) && (expr.run() as Bool).value)
        {
            body.run();
            if (Interpreter.helper.Count > 0)
                check = Interpreter.helper.Pop();
            else
                check = new TrueConditional();
            pass.run();
        }
        return null;
    }
}