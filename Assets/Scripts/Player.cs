using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager _gameManager;

    public bool active;
    public bool won;
    public bool key;
    public GameObject WCube, ACube, SCube, DCube;


    bool moved;
    int lastMoved;
    public bool CanDoW, CanDoA, CanDoS, CanDoD;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        active = false;
        won = false;
        moved = false;
        key = false;
        WCube.SetActive(false);
        ACube.SetActive(false);
        SCube.SetActive(false);
        DCube.SetActive(false);
    }

    public void CheckWon()
    {
        if (Vector3.Distance(_gameManager._currentTarget, transform.position) < 0.8f)
        {
            won = true;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void HasKey() //implemented now
    {
        if(_gameManager._currenthasKey)
        {
            if (Vector3.Distance(_gameManager._currentKeyTarget.transform.position, transform.position) < 0.8f)
            {
                key = true;
            }
        }
    }

    void Moved()
    {
        if(key)
        {
            _gameManager._currentKeyTarget.transform.position = transform.position;
        }
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

    /*
    private void FixedUpdate()
    {
        RaycastHit hit;
        CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1);
        CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1);
        CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1);
        CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1);
    }
    */

    void Update()
    { 
        if (active && !_gameManager.moving && !won)
        {
            RaycastHit hit;
            CanDoW = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 1);
            CanDoD = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 1);
            CanDoA = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 1);
            CanDoS = !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1);


            if (CanDoW)
            {
                WCube.SetActive(true);
            }
            if (WCube.GetComponent<DirectionScript>().selected)
            {
                transform.position += transform.up;
                moved = true;
                lastMoved = 0;
                WCube.GetComponent<DirectionScript>().selected = false;
            }

            if (CanDoA)
            {
                ACube.SetActive(true);
            }
            if (ACube.GetComponent<DirectionScript>().selected)
            {
                transform.Rotate(Vector3.forward * 90);
                transform.position += transform.up;
                moved = true;
                lastMoved = 1;
                ACube.GetComponent<DirectionScript>().selected = false;
            }

            if (CanDoS)
            {
                SCube.SetActive(true);
            }
            if (SCube.GetComponent<DirectionScript>().selected)
            {
                transform.Rotate(Vector3.forward * 180);
                transform.position += transform.up;
                moved = true;
                lastMoved = 2;
                SCube.GetComponent<DirectionScript>().selected = false;
            }

            if (CanDoD)
            {
                DCube.SetActive(true);
            }
            if (DCube.GetComponent<DirectionScript>().selected)
            {
                transform.Rotate(Vector3.forward * -90);
                transform.position += transform.up;
                moved = true;
                lastMoved = 3;
                DCube.GetComponent<DirectionScript>().selected = false;
            }

            if (moved)
            {
                WCube.SetActive(false);
                ACube.SetActive(false);
                SCube.SetActive(false);
                DCube.SetActive(false);
                _gameManager._lastMoved = lastMoved;
                CheckWon();
                HasKey();
                Moved();
                moved = false;
            }
        }
        else
        {
            WCube.SetActive(false);
            ACube.SetActive(false);
            SCube.SetActive(false);
            DCube.SetActive(false);
        }
    }
}
