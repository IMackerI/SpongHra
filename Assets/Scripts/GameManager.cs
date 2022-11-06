using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject mainMenu;
    public GameObject victoryScreen;
    public GameObject playingScreen;
    public GameObject levelsScreen;
    public GameObject loseScreen;
    public GameObject colectionScreen;

    public enum State { MOVING, MOVED, LEVELCOMPLETE, LEVELLOST, MAINMENU, VICTORYSCREEN, PAUSESCREEN, COLLECTION, LEVELSSCREEN };
    public State _state;
    public GameObject[] levels;
    public int[] levelMoves;
    public bool[] hasKey;
    public GameObject[] memes;
    public GameObject[] victoryMemes;
    GameObject _currentLevel;
    public float moveDelay;

    public int _maxLevelIndex
    {
        set 
        {
            PlayerPrefs.SetInt("MaxLevel", value);
        }
        get { return PlayerPrefs.GetInt("MaxLevel"); }
    }
    int _levelIndex = 0;
    [HideInInspector]
    public Vector3 _currentTarget;
    public GameObject _currentKeyTarget;
    public bool moving = false;
    public bool outOfMoves = false;

    public GameObject movesText;
    public int _cMoves;
    public int _currentMoves
    {
        set 
        {
            _cMoves = value; 
            if(_cMoves > 0)
            {
                movesText.GetComponent<TextMeshProUGUI>().SetText("MOVES REMAINING: " + _cMoves + " / " + levelMoves[_levelIndex]);
            }
            else
            {
                movesText.GetComponent<TextMeshProUGUI>().SetText("MOVES REMAINING: " + 0 + " / " + levelMoves[_levelIndex]);
            }
        }
        get { return _cMoves; }
    }

    public bool _currenthasKey;
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
        mainMenu.SetActive(false);
        victoryScreen.SetActive(false);
        levelsScreen.SetActive(false);
        loseScreen.SetActive(false);
        colectionScreen.SetActive(false);
        foreach(GameObject meme in memes)
        {
            meme.SetActive(false);
        }
        for(int i = 0; i < _maxLevelIndex; i++)
        {
            memes[i].SetActive(true);
        }
        Restart(0);
        Switchstate(State.MAINMENU);
    }

    public void hitMainLevels()
    {
        Switchstate(State.LEVELSSCREEN);
    }

    public void hitMenu()
    {
        Switchstate(State.MAINMENU);
    }

    public void hitExit()
    {
        _maxLevelIndex = 18;
    }

    public void hitResetAll()
    {
        _maxLevelIndex = 0;
    }

    public void hitRestart()
    {
        Restart(_levelIndex);
    }

    public void hitNextLevel()
    {
        Restart(_levelIndex);
    }

    public void hitLevel(int index)
    {
        if(_maxLevelIndex >= index)
        {
            Restart(index);
        }
    }

    public void hitCollection()
    {
        Switchstate(State.COLLECTION);
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
        _currenthasKey = hasKey[_levelIndex];
        if (_currenthasKey)
        {
            _currentKeyTarget = _currentLevel.transform.Find("Key").gameObject; //implemented now
        }

        playingScreen.SetActive(true);

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
                if (player.GetComponent<Player>().key)
                {
                    _currentKeyTarget.transform.position = player.transform.position;
                }
                player.GetComponent<Player>().CheckWon();
                if (_currenthasKey)
                {
                    player.GetComponent<Player>().HasKey();
                }
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
            if (!outOfMoves)
            {
                Switchstate(State.LEVELCOMPLETE, players.Count * moveDelay);
            }
            else
            {
                Switchstate(State.LEVELLOST, players.Count * moveDelay);
            }
        }
        else if (_currentMoves < 0)
        {
            outOfMoves = true;
        }
    }

    void BeginState(State newState)
    {
        switch (_state)
        {
            case State.MAINMENU:
                mainMenu.SetActive(true);
                playingScreen.SetActive(false);
                break;
            case State.LEVELSSCREEN:
                levelsScreen.SetActive(true);
                break;
            case State.MOVING:
                //Debug.Log("Started Playing");
                moving = true;
                Switchstate(State.MOVED, players.Count * moveDelay);
                StartCoroutine(MovedDelay());
                break;
            case State.LEVELCOMPLETE:
                playingScreen.SetActive(false);
                _levelIndex++;
                if(_levelIndex >= _maxLevelIndex)
                {
                    _maxLevelIndex = _levelIndex;
                    memes[_levelIndex].SetActive(true);
                }
                foreach(GameObject mem in victoryMemes)
                {
                    mem.SetActive(false);
                }
                victoryMemes[_levelIndex].SetActive(true);

                victoryScreen.SetActive(true);
                break;
            case State.LEVELLOST:
                playingScreen.SetActive(false);
                loseScreen.SetActive(true);
                break;
            case State.COLLECTION:
                colectionScreen.SetActive(true);
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
            case State.MAINMENU:
                mainMenu.SetActive(false);
                break;
            case State.LEVELCOMPLETE:
                victoryScreen.SetActive(false);
                break;
            case State.LEVELLOST:
                loseScreen.SetActive(false);
                break;
            case State.LEVELSSCREEN:
                levelsScreen.SetActive(false);
                break;
            case State.COLLECTION:
                colectionScreen.SetActive(false);
                break;
            case State.MOVING:
                moving = false;
                break;
        }
    }
}
