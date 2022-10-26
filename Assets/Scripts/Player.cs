using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool _active;
    GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _active = false;
    }

    void Moved()
    {
        _gameManager.Switchstate(GameManager.State.PLAYING);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.Reselect();
            _active = true;
        }
    }

    void Update()
    {
        if (_active)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                Moved();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                Moved();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Moved();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                Moved();
            }
        }
    }
}
