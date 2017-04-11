using System;
using UnityEngine;

public class IteratorToken
{
    public Token token;

    public IteratorToken(Token token)
    {
        this.token = token;
    }

    public Token current()
    {
        return token;
    }

    public Token next()
    {
        token = token.next;
        return token;
    }

    public Token lookNext()
    {
        return token.next;
    }

    public bool hasCurrent()
    {
        return token != null;
    }

    public bool hasNext()
    {
        return token.next != null;
    }
}