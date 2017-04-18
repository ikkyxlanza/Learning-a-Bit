using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Interpreter i = new Interpreter("a = 1 == 1\n" +
                                        "if ~a:\n" +
                                        "  b = 1\n" +
                                        ": else :\n" +
                                        "  if a:\n" +
                                        "    c = 1\n"+
                                        "  : else :\n"+
                                        "    d = 1\n"+
                                        "  :\n"+
                                        ":");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
