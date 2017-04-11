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
    }

    private bool skipSpace(Iterator it)
    {
        if (it.current() == ' ')
        {
            it.next();
            return true;
        }
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