using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool active;
    public bool won;
    GameManager _gameManager;
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

    void Update()
    {
        if (active)
        {
            GameObject W1 = transform.Find("Detector+Y").gameObject; //Meno v zatvorke je nazov objektu v Unity
            Detector W2 = W1.GetComponent<Detector>();
            bool CantDoW = W2.Overlap;

            GameObject A1 = transform.Find("Detector-X").gameObject;
            Detector A2 = A1.GetComponent<Detector>();
            bool CantDoA = A2.Overlap;

            GameObject S1 = transform.Find("Detector-Y").gameObject;
            Detector S2 = S1.GetComponent<Detector>();
            bool CantDoS = S2.Overlap;

            GameObject D1 = transform.Find("Detector+X").gameObject;
            Detector D2 = D1.GetComponent<Detector>();
            bool CantDoD = D2.Overlap;

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
                    Moved();
                }
            }
        }
    }
}
