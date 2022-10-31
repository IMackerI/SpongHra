using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionScript : MonoBehaviour
{
    public bool selected = false;
    private void OnMouseDown()
    {
        selected = true;
        Debug.Log("Selected");
    }
}
