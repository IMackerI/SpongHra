using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool Overlap;

    void Start()
    {
        Overlap = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Overlap == false)
        {
            Overlap = true;
        }
        Debug.Log("Collision");
    }

    private void OnCollisionExit(Collision collision)
    {
        Overlap = false;
    }
}
