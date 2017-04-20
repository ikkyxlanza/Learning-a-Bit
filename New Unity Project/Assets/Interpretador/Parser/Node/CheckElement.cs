using System;

public class CheckElement : INode
{
    private string name { get; set; }
    private INode index { get; set; }

    public CheckElement(string name, INode index)
    {
        this.name = name;
        this.index = index;
    }

    public INode run()
    {
        Vector vector = (Interpreter.variables.getVariable(name) as IOperator) as Vector;
        return vector.value[(index.run() as Integer).value];
    }
}