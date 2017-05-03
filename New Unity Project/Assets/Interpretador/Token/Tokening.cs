using System;
using UnityEngine;

public class Tokening
{
    public Token token { get; private set; }

    public Tokening(string line)
    {
        Iterator it = new Iterator(line);
        token = new Token(Type.NONE, null);
        Token lastToken = token;
        while (it.hasNext())
        {
            if (skipSpace(it)) continue;
            if (skipComment(it)) break;
            Token tk = Number.get(it);
            if (tk != null)
            {
                lastToken.next = tk;
                lastToken = tk;
                continue;
            }

            tk = Symbol.get(it);
            if (tk != null)
            {
                lastToken.next = tk;
                lastToken = tk;
                continue;
            }

            tk = KeyWord.get(it);
            if (token != null)
            {
                lastToken.next = tk;
                lastToken = tk;
                continue;
            }
        }
        token = token.next;
        if (Interpreter.debug) Debug.Log(print());
    }

    private bool skipSpace(Iterator it)
    {
        if ((int)it.current() <= 32)
        {
            it.next();
            return true;
        }
        return false;
    }

    private bool skipComment(Iterator it)
    {
        if ((int)it.current() == 62 && (int)it.lookNext() == 62)
            return true;
        return false;
    }

    public string print()
    {
        string v = "";
        Token t = token;
        while (t != null)
        {
            v += t.type + "\n";
            t = t.next;
        }
        return v;
    }
}