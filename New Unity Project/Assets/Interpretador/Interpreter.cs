using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interpreter
{
    private static MonoBehaviour mono { get; set; }
    public static Variable variables { get; private set; }
    public static Stack<IConditional> helper { get; private set; }
    public static bool debug
    {
        get { return true; }
        private set { }
    }
    public static bool nextLine { get; set; }
    public static bool debugging
    {
        get { return false; }
        private set { }
    }
    public static int timeInSleep
    {
        get { return 250; }
        private set { }
    }
    private INode iNode { get; set; }
    private static Thread thread { get; set; }

    public Interpreter(string program, MonoBehaviour mono)
    {
        variables = new Variable(20);
        helper = new Stack<IConditional>();
        if (Interpreter.debugging)
        {
            Interpreter.mono = mono;
            Interpreter.mono.StartCoroutine(readKey());
            nextLine = false;
            thread = new Thread(() =>
            {
                Debug.Log("RUNNING DEBUGGING!");
                iNode = Parser.parser(program);
                iNode.run();
                if (debug) Debug.Log(variables.state());
                if (debugging) Debug.Log("FIM DO DEBUG");
            });
            thread.Start();
        }
        else
        {
            iNode = Parser.parser(program);
            iNode.run();
            if (debug) Debug.Log(variables.state());
        }
    }

    private IEnumerator readKey()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F8))
                nextLine = true;
            if (Input.GetKeyDown(KeyCode.V))
                Debug.Log(variables.state());
            yield return null;
        }
    }
}