using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TextAsset asset = Resources.Load<TextAsset>("teste");
        //Interpreter i = new Interpreter(asset.text, this);
        StartCoroutine(key());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator key()
    {
        while (true)
        {
            transform.Rotate(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
            yield return null;
        }
    }
}
