using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State { PLAYING, LEVELCOMPLETE };
    State _state;
    public GameObject[] levels;
    GameObject _currentLevel;
    int _levelIndex;

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
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }
        Instance = this;
        _currentLevel = levels[0];
        _currentLevel.SetActive(true);
        _levelIndex = 0;
        getObjects();
        Switchstate(State.PLAYING);
    }

    void getObjects()
    {
        foreach(Transform t in _currentLevel.transform)
        {
            if(t.tag == "Player")
            {
                players.Add(t.gameObject);
            }
        }
    }

    public void Reselect()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>()._active = false;
        }
    }

    void BeginState(State newState)
    {
        switch (_state)
        {
            case State.PLAYING:
                Debug.Log("PlayingSTART");
                break;
            case State.LEVELCOMPLETE:
                _currentLevel.SetActive(false);
                _levelIndex++;
                _currentLevel = levels[_levelIndex];
                _currentLevel.SetActive(true);
                Switchstate(State.PLAYING);
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.PLAYING:
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.PLAYING:
                Debug.Log("PlayingEND");
                foreach(GameObject player in players)
                {
                    player.GetComponent<Player>()._active = false;
                }
                break;
            case State.LEVELCOMPLETE:
                getObjects();
                break;
        }
    }
}
