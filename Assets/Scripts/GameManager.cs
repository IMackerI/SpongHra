using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State { MOVING, MOVED, LEVELCOMPLETE, LEVELLOST };
    public State _state;
    public GameObject[] levels;
    public int[] levelMoves;
    GameObject _currentLevel;
    public float moveDelay;
    
    int _levelIndex;
    [HideInInspector]
    public Vector3 _currentTarget;
    public Vector3 _currentKeyTarget; //done now
    public bool moving = false;
    public bool outOfMoves = false;

    public int _currentMoves;
    public List<GameObject> players;
    [HideInInspector]
    public int _lastMoved;

    public void Switchstate(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
    }
    void Start()
    {
        Instance = this;
        Restart(0);
    }

    void Restart(int levelIndex)
    {
        Debug.Log("Restarting on level:" + _levelIndex);
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
        }
        _levelIndex = levelIndex;
        _currentLevel = Instantiate(levels[_levelIndex]);

        players.Clear();
        foreach(Transform t in _currentLevel.transform)
        {
            if(t.tag == "Player")
            {
                players.Add(t.gameObject);
            }
        }
        _currentMoves = levelMoves[_levelIndex];
        _currentTarget = _currentLevel.transform.Find("Finish").position;
        _currentKeyTarget = _currentLevel.transform.Find("Key").position; //implemented now

        Switchstate(State.MOVED);
    }

    public void Reselect()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().active = false;
        }
    }


    IEnumerator MovedDelay()
    {
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player>().active == false && player.GetComponent<Player>().won == false)
            {
                //Move other Players
                yield return new WaitForSeconds(moveDelay);
                player.GetComponent<PlayerAuto>().lastMoved = _lastMoved;
                player.GetComponent<PlayerAuto>().MoveAuto();
                player.GetComponent<Player>().CheckWon();
                //Deactivate Players
                player.GetComponent<Player>().active = false;
            }
        }
        bool won = true;
        foreach (GameObject player in players)
        {
            if (player.GetComponent<Player>().won == false)
                won = false;
        }
        _currentMoves--;
        if (won)
        {
            Debug.Log("Won");
            Switchstate(State.LEVELCOMPLETE);
        }
        else if (_currentMoves == 0)
        {
            outOfMoves = true;
        }
    }
    void BeginState(State newState)
    {
        switch (_state)
        {
            case State.MOVING:
                Debug.Log("Started Playing");
                moving = true;
                Switchstate(State.MOVED, players.Count * moveDelay);
                StartCoroutine(MovedDelay());
                break;
            case State.LEVELCOMPLETE:
                Restart(_levelIndex + 1);
                break;
            case State.LEVELLOST:
                Restart(_levelIndex);
                break;
        }
    }

    private void Update()
    {
        switch (_state)
        {
            case State.MOVED:
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    foreach(GameObject player in players)
                    {
                        player.GetComponent<Player>().active = false;
                    }
                    _currentMoves++;
                    Switchstate(State.MOVING);
                }
                if(Input.GetKeyDown(KeyCode.R))
                {
                    Switchstate(State.LEVELLOST);
                }
                break;
            case State.MOVING:
                if(Input.GetKeyDown(KeyCode.R))
                {
                    Switchstate(State.LEVELLOST);
                }
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MOVING:
                moving = false;
                break;
        }
    }
}
