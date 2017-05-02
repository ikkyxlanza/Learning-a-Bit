using System;
using UnityEngine;

public class Parser
{
    public static Statement statement { get; set; }

    public static INode parser(string program)
    {
        char[] separator = { '\n' };
        string[] lines = program.Split(separator);
        Tokening[] tokening = new Tokening[lines.Length];
        for (var i = 0; i < lines.Length; i++)
            tokening[i] = new Tokening(lines[i]);
        IteratorTokening it = new IteratorTokening(tokening);
        statement = new Statement(null);
        Statement state = statement;
        while (it.hasNext())
        {
            INode iNode = Line.statement(it);
            Statement newSta = new Statement(iNode);
            state.next = newSta;
            state = newSta;
        }
        return statement.next;
    }
}