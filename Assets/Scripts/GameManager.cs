using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public static NewBehaviourScript Instance { get; private set; }

    public enum State { PLAYERMOVE, PLAYERSELECT};
    State _state;
    public GameObject Player;
    public GameObject Level;

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
        Switchstate(State.PLAYERMOVE);
    }

    void BeginState(State newState)
    {
        switch (_state)
        {
            case State.PLAYERMOVE:
                break;
            case State.PLAYERSELECT:
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.PLAYERMOVE:
                break;
            case State.PLAYERSELECT:
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.PLAYERMOVE:
                break;
            case State.PLAYERSELECT:
                break;
        }
    }
}
