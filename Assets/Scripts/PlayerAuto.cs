using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuto : MonoBehaviour
{
    public bool CanDoW, CanDoA, CanDoS, CanDoD;

    private void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
        {
            //Debug.Log("Did Hit");
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1000, Color.yellow);
        if (CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
    }
    public void MoveAuto()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }


        //Debug.Log("MoveAuto Called");
        if (CanDoW)
        {
            transform.position += transform.up;
        }
        else if (CanDoD)
        {
            transform.Rotate(Vector3.forward * -90);
            transform.position += transform.up;
        }
        else if (CanDoA)
        {
            transform.Rotate(Vector3.forward * 90);
            transform.position += transform.up;
        }
        else if (CanDoS)
        {
            transform.Rotate(Vector3.forward * 180);
            transform.position += transform.up;
        }
    }
}
