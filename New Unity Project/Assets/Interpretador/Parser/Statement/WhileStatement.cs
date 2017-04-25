using System;

public class WhileStatement : INode
{
    private Statement body { get; set; }
    private INode expr { get; set; }
    public WhileStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        ite.next();
        expr = Expression.expr(ite);
        Token token = ite.current();
        if (token.type == Type.COLON)
        {
            body = new Statement(null);
            Statement state = body;
            do
            {
                INode iNode = Line.statement(it);
                Statement newSta = new Statement(iNode);
                state.next = newSta;
                state = newSta;
                Debug.Log(it.current().token.end);
            } while (it.current().token.type != Type.COLON || it.current().token.end);
            it.current().token.end = true;
            body = body.next as Statement;
        }
        else
            throw new Error("Syntax error!");
    }

    public INode run()
    {
        IConditional check = new TrueConditional();
        while ((expr.run() as Bool).value && check.check(Type.WHILE))
        {
            body.run();
            if (Interpreter.helper.Count > 0)
                check = Interpreter.helper.Pop();
            else
                check = new TrueConditional();
        }
        return null;
    }
}