using System;
using UnityEngine;

public class IteratorTokening
{
    private Tokening[] tokening { get; set; }
    private int index { get; set; }

    public IteratorTokening(Tokening[] tokening)
    {
        this.tokening = tokening;
        this.index = -1;
    }

    public Tokening current()
    {
        Tokening tk = index == -1 ? tokening[0] : tokening[index];
        while (tk.token == null && ++index < tokening.Length)
        {
            if (Interpreter.debug) Debug.Log("JUMP LINE");
            tk = tokening[index];
        }
        return tk;
    }

    public Tokening lookNext()
    {
        return index + 1 < tokening.Length ? tokening[index + 1] : null;
    }

    public Tokening next()
    {
        Tokening tk = tokening[++index];
        while (tk.token == null && ++index < tokening.Length)
        {
            if (Interpreter.debug) Debug.Log("JUMP LINE");
            tk = tokening[index];
        }
        return tk;
    }

    public bool hasNext()
    {
        while (index + 1 < tokening.Length && tokening[index + 1].token == null)
        {
            index++;
            if (Interpreter.debug) Debug.Log("JUMP LINE");
        }
        return (index + 1) < tokening.Length;
    }
}