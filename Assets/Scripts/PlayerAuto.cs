using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuto : MonoBehaviour
{
    public void MoveAuto()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
}
