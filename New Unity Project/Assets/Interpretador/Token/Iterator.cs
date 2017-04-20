using System;
using UnityEngine;

public class Iterator
{
    private string line;
    private int i;

    public Iterator(string st)
    {
        line = st + " ";
        i = 0;
    }

    public char current()
    {
        return line[i];
    }

    public char next()
    {
        return line[++i];
    }

    public char previous()
    {
        return line[--i];
    }

    public bool hasNext()
    {
        return (i + 1) < line.Length;
    }

    public bool hasPrevious()
    {
        return i > 0;
    }
}