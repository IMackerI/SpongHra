using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State { INNIT, MOVED, LEVELCOMPLETE, LEVELLOST };
    State _state;
    public GameObject[] levels;
    public int[] levelMoves;
    GameObject _currentLevel;
    public int _currentMoves;
    public int _levelIndex;
    public Vector3 _currentTarget;

    public List<GameObject> players;

    bool _isSwitchingState;

    public void Switchstate(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
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

        Switchstate(State.INNIT);
    }

    public void Reselect()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().active = false;
        }
    }

    void BeginState(State newState)
    {
        switch (_state)
        {
            case State.MOVED:
                Debug.Log("Started Playing");
                //Move other Players
                foreach (GameObject player in players)
                {
                    if (player.GetComponent<Player>().active == false && player.GetComponent<Player>().won == false)
                    {
                        player.GetComponent<PlayerAuto>().MoveAuto();
                        player.GetComponent<Player>().CheckWon();
                    }
                }

                //Deactivate Players
                foreach (GameObject player in players)
                {
                    player.GetComponent<Player>().active = false;
                }

                //check if they lost/won
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
                    Debug.Log("Lost");
                    Switchstate(State.LEVELLOST);
                }
                break;
            case State.LEVELCOMPLETE:
                Restart(_levelIndex + 1);
                break;
            case State.LEVELLOST:
                Restart(_levelIndex);
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {

        }
    }
}
