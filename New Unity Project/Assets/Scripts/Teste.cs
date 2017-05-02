using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TextAsset asset = Resources.Load<TextAsset>("teste");
        Interpreter i = new Interpreter(asset.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
