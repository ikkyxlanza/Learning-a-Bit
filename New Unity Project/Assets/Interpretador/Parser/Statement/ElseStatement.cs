using System;

public class ElseStatement : INode
{
    public Statement senao { get; private set; }

    public ElseStatement(IteratorTokening it)
    {
        IteratorToken ite = new IteratorToken(it.current().token);
        if (ite.current().type == Type.COLON)
            ite.next();
        Token token = ite.next();

        if (token.type == Type.COLON)
        {
            senao = new Statement(null);
            Statement state = senao;
            do
            {
                INode iNode = Line.statement(it);
                Statement newSta = new Statement(iNode);
                state.next = newSta;
                state = newSta;
            } while (it.current().token.type != Type.COLON);
            senao = senao.next as Statement;
        }
        else
            throw new Error("Syntax error!");
    }

    public INode run()
    {
        senao.run();
        return null;
    }
}