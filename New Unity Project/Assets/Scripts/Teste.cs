using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Interpreter i = new Interpreter("a = 1\n" +
                                        "b = 0\n" +
                                        "c = 0\n" +
                                        "while b <= 5:\n" +
                                        "  a += a\n" +
                                        "  b += 1\n" +
                                        "  if b == 3:\n" +
                                        "    break\n" +
                                        "  :\n" +
                                        "  c += 2\n" +
                                        ":");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
