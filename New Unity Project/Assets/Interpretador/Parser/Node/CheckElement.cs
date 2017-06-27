using System;
using UnityEngine;

public class CheckElement : INode
{
    private string name { get; set; }
    private INode index { get; set; }
    public int lineNumber { get; set; }

    public CheckElement(string name, INode index)
    {
        lineNumber = index.lineNumber;
        this.name = name;
        this.index = index;
    }

    public INode run()
    {
        Vector vector = (Interpreter.variables.getVariable(name) as IOperator) as Vector;
        int position = (index.run() as Integer).value;
        if (position < 0) position = (vector.Length.run() as Integer).value + position;
        if (Interpreter.debug) Debug.Log("ACESS POSITION " + position + " OF VECTOR " + name);
        return vector.value[position];
    }
}