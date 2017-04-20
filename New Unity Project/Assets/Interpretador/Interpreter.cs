using System;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{
    public static Variable variables { get; private set; }
    public static Stack<IConditional> helper { get; private set; }
    public static bool debug
    {
        get { return true; }
        private set { }
    }
    private INode iNode { get; set; }

    public Interpreter(string program)
    {
        variables = new Variable(20);
        helper = new Stack<IConditional>();
        iNode = Parser.parser(program);
        iNode.run();
        if (debug) Debug.Log(variables.state());
    }
}