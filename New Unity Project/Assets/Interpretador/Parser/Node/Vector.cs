using System;
using UnityEngine;

public class Vector : IOperator
{
    public Type type { get { return Type.VECTOR; } set { } }

    public string name { get; set; }
    public INode length { get; private set; }
    public INode Length
    {
        get { return length; }
    }
    public IOperator[] value { get; private set; }
    private EachElement elem { get; set; }

    public Vector(IteratorToken it)
    {
        Token token = it.current();
        if (it.lookNext().type == Type.HASH)
        {
            it.next();
            it.next();
            length = Expression.expr(it);
            elem = null;
        }
        else
        {
            int len = 0;
            elem = new EachElement();
            EachElement e = elem;
            do
            {
                it.next();
                e.element = Expression.expr(it);
                e.nextElent = new EachElement();
                e = e.nextElent;
                len++;
                Debug.Log(it.current().type);
            } while (it.current().type != Type.RBRACKET);
            length = new Integer(len);
        }
    
        if (it.current().type != Type.RBRACKET)
            throw new Error("Erro no código!");
    }

    public INode run()
    {
        int len = (length.run() as Integer).value;
        value = new IOperator[len];
        if (elem == null)
            for (var i = 0; i < len; i++)
                value[i] = new Integer(0);
        else
            for (var i = 0; i < len; i++)
            {
                value[i] = elem.run() as IOperator;
                elem = elem.nextElent;
            }
        length = new Integer(len);
        return this;
    }

    private class EachElement : INode
    {
        public INode element { get; set; }
        public EachElement nextElent { get; set; }

        public INode run()
        {
            return element.run();
        }
    }
}