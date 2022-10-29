using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool active;
    public bool won;
    GameManager _gameManager;
    bool CantDoW, CantDoA, CantDoS, CantDoD;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        active = false;
        won = false;
    }

    void Moved()
    {
        _gameManager.Switchstate(GameManager.State.MOVED);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.Reselect();
            active = true;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (CantDoW = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CantDoD = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CantDoA = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        if (CantDoS = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }

    }

    void Update()
    {
        if (active)
        {
            if (CantDoD == false)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    Moved();
                }
            }

            if (CantDoA == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    Moved();
                }
            }

            if (CantDoW == false)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    Moved();
                }
            }

            if (CantDoS == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    //Moved();
                }
            }
        }
    }
}
