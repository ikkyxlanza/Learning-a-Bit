using System;
using System.Threading;
using UnityEngine;

public class Statement : INode
{
    public INode current { get; private set; }
    public INode next { get; set; }
    public int lineNumber { get; set; }

    public Statement(INode current)
    {
        this.current = current;
        this.next = null;
    }

    public INode run()
    {
        IConditional iCond = Interpreter.helper.Count > 0 ? Interpreter.helper.Peek() : new TrueConditional();
        if (iCond.check(Type.NONE))
        {
            Interpreter.lineNumber = current.lineNumber;
            if (Interpreter.debugging && current.lineNumber >= 0)
            {
                while (!Interpreter.nextLine) Thread.Sleep(Interpreter.timeInSleep);
                Interpreter.nextLine = false;
            }
            current.run();
        }
        if (next != null) next.run();
        return null;
    }
}