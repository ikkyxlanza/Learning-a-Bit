using System;

public class WhileStatement : INode
{
    private Statement body { get; set; }
    private INode expr { get; set; }
    public int lineNumber { get; set; }
    
    public WhileStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        lineNumber = ite.current().lineNumber;
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
            } while (it.current().token.type != Type.COLON || it.current().token.end);
            it.current().token.end = true;
            state.current.lineNumber = lineNumber;
            body = body.next as Statement;
        }
        else
            throw new Error("Syntax error!");
    }

    public INode run()
    {
        IConditional check = new TrueConditional();
        while (check.check(Type.WHILE) && (expr.run() as Bool).value)
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