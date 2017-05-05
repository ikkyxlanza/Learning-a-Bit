using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour
{
    private bool isMoving;
    private GameObject point;
    private int ignoreLayer;
    // Use this for initialization
    void Start()
    {
        point = GameObject.FindGameObjectWithTag("Point");
        isMoving = false;
        Debug.Log("Init");
        ignoreLayer = 1;
        ignoreLayer = (ignoreLayer << 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(point.transform.position, Vector3.forward * 10, Color.red);
            if (Physics.Raycast(point.transform.position, Vector3.forward, out hit, float.MaxValue))
                if (hit.collider.tag.Equals("Component"))
                {
                    isMoving = true;
                    StartCoroutine(move(hit.transform));
                }
        }
        if (Input.GetMouseButtonDown(1))
        {
            isMoving = false;
        }
    }

    private IEnumerator move(Transform toMove)
    {
        Debug.Log(toMove);
        while (isMoving)
        {
            RaycastHit hit;
            Debug.DrawRay(point.transform.position, Vector3.forward * 10, Color.red);
            Physics.Raycast(point.transform.position, Vector3.forward, out hit, float.MaxValue, ignoreLayer);
            Debug.Log(hit.collider);
            if (hit.collider && hit.collider.tag.Equals("Louza"))
            {
                Vector3 newPoint = new Vector3(0, 0, 0);
                newPoint.x = hit.point.x - toMove.position.x;
                Debug.Log(hit.point + " | " + toMove.position);
                toMove.Translate(newPoint);
            }
            yield return null;
        }
    }
}
