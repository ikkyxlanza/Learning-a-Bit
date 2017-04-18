using System;
using UnityEngine;

public class IteratorTokening
{
    private Tokening[] tokening { get; set; }
    private int index { get; set; }

    public IteratorTokening (Tokening[] tokening)
    {
        this.tokening = tokening;
        this.index = -1;
    }

    public Tokening current()
    {
        Tokening tk = index == -1 ? tokening[0] : tokening[index];
        while(tk.token == null && ++index < tokening.Length)
        {
            Debug.Log("NEXT");
            tk = tokening[index];
        } 
        return tk;
    }

    public Tokening next()
    {
        Tokening tk = tokening[++index];
        while(tk.token == null && ++index < tokening.Length)
        {
            Debug.Log("NEXT");
            tk = tokening[index];
        } 
        return tk;
    }

    public bool hasNext()
    {
        return (index + 1) < tokening.Length;
    }
}