using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public GameObject key;
    void Update()
    {
        if(key != null)
        {
            if(Vector3.Distance(key.transform.position, transform.position) < 1.5)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
