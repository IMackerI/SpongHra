using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool active;
    public bool won;
    bool moved;
    int lastMoved;
    GameManager _gameManager;
    public bool CanDoW, CanDoA, CanDoS, CanDoD;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        active = false;
        won = false;
        moved = false;
    }

    public void CheckWon()
    {
        if (Vector3.Distance(_gameManager._currentTarget, transform.position) < 0.8f)
        {
            won = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Moved()
    {
        _gameManager.Switchstate(GameManager.State.MOVING);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.Reselect();
            active = true;
        }
    }

    private void FixedUpdate()
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
    }

    void Update()
    { 
        if (active && !_gameManager.moving)
        {
            if (CanDoD)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.Rotate(Vector3.forward * -90);
                    transform.position += transform.up;
                    moved = true;
                    lastMoved = 3;
                }
            }

            if (CanDoA)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.Rotate(Vector3.forward * 90);
                    transform.position += transform.up;
                    moved = true;
                    lastMoved = 1;
                }
            }

            if (CanDoW)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.position += transform.up;
                    moved = true;
                    lastMoved = 0;
                }
            }

            if (CanDoS)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.Rotate(Vector3.forward * 180);
                    transform.position += transform.up;
                    moved = true;
                    lastMoved = 2;
                }
            }

            if (moved)
            {
                _gameManager._lastMoved = lastMoved;
                CheckWon();
                Moved();
                moved = false;
            }
        }
    }
}
