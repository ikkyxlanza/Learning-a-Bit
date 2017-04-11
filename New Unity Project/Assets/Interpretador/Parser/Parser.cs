using System;
using UnityEngine;

public class Parser
{

    public Parser(string line)
    {
        Tokening tokening = new Tokening(line);
        Debug.Log(tokening.print());
        INode iNode = Expresion.expr(new IteratorToken(tokening.token));
        Debug.Log((iNode.run() as INumber).valueI);
    }
}