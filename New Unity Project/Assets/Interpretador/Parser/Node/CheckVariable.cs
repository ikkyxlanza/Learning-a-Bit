using System;
using UnityEngine;

public class CheckVariable : INode
{
    private string name { get; set; }
    public int lineNumber { get; set; }

    public CheckVariable(string name, int line)
    {
        lineNumber = line;
        this.name = name;
    }

    public INode run()
    {
        return Interpreter.variables.getVariable(name) as IOperator;
    }
}