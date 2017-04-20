using System;
using UnityEngine;

public class KeyWord : IToken
{
    public string word { get; private set; }

    private KeyWord()
    {
        this.word = "";
    }

    private bool add(char value)
    {
        if ((int)value >= 97 && (int)value <= 122)
        {
            word += value;
            return true;
        }
        return false;
    }

    private Type type()
    {
        switch (word)
        {
            case "true":
                return Type.TRUE;
            case "false":
                return Type.FALSE;
            case "if":
                return Type.IF;
            case "else":
                return Type.ELSE;
            case "while":
                return Type.WHILE;
            case "break":
                return Type.BREAK;
            case "continue":
                return Type.CONTINUE;
            default:
                return Type.ID;
        }
    }

    public static Token get(Iterator it)
    {
        KeyWord v = new KeyWord();
        if (v.add(it.current()))
        {
            while (it.hasNext())
                if (!v.add(it.next()))
                    break;
            return new Token(v.type(), v);
        }
        return null;
    }
}