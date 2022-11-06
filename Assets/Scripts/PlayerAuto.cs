using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuto : MonoBehaviour
{
    bool CanDoW, CanDoA, CanDoS, CanDoD;

    //0 - W, 1 - A, 2 - S, 3 - D
    public int lastMoved;
    public enum Type { Forward, Right, Left, Back, Oposite, Stand }
    public Type type;
    public GameObject WMarker, AMarker, SMarker, DMarker, OMarker;

    private void Start()
    {
        WMarker.SetActive(false);
        AMarker.SetActive(false);
        SMarker.SetActive(false);
        DMarker.SetActive(false);
        OMarker.SetActive(false);
        if(type == Type.Forward)
        {
            WMarker.SetActive(true);
        }
        else if(type == Type.Left)
        {
            AMarker.SetActive(true);
        }
        else if(type == Type.Back)
        {
            SMarker.SetActive(true);
        }
        else if(type == Type.Right)
        {
            DMarker.SetActive(true);
        }
        else if(type == Type.Oposite)
        {
            OMarker.SetActive(true);
        }
    }

    private void Update()
    {
        RaycastHit hit;
        CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1);
        CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1);
        CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1);
        CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1);
    }

    void MoveForward()
    {
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

    void MoveLeft()
    {
        if (CanDoA)
        {
            transform.Rotate(Vector3.forward * 90);
            transform.position += transform.up;
        }
        else if (CanDoW)
        {
            transform.position += transform.up;
        }
        else if (CanDoD)
        {
            transform.Rotate(Vector3.forward * -90);
            transform.position += transform.up;
        }
        else if (CanDoS)
        {
            transform.Rotate(Vector3.forward * 180);
            transform.position += transform.up;
        }
    }

    void MoveRight()
    {
        if (CanDoD)
        {
            transform.Rotate(Vector3.forward * -90);
            transform.position += transform.up;
        }
        else if (CanDoW)
        {
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

    void MoveBack()
    {
        if (CanDoS)
        {
            transform.Rotate(Vector3.forward * 180);
            transform.position += transform.up;
        }
        else if (CanDoA)
        {
            transform.Rotate(Vector3.forward * 90);
            transform.position += transform.up;
        }
        else if (CanDoD)
        {
            transform.Rotate(Vector3.forward * -90);
            transform.position += transform.up;
        }
        else if (CanDoW)
        {
            transform.position += transform.up;
        }
    }

    void MoveOposite()
    {
        if (lastMoved == 0) MoveBack();
        if (lastMoved == 1) MoveRight();
        if (lastMoved == 2) MoveForward();
        if (lastMoved == 3) MoveLeft();
    }

    public void MoveAuto()
    {
        RaycastHit hit;
        CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1);
        CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1);
        CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1);
        CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1);

        switch (type) 
        {
            case Type.Forward:
                MoveForward();
                break;
            case Type.Right:
                MoveRight();
                break;
            case Type.Left:
                MoveLeft();
                break;
            case Type.Back:
                MoveBack();
                break;
            case Type.Oposite:
                MoveOposite();
                break;
            case Type.Stand:
                break;
        }


    }
}
