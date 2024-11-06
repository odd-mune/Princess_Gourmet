using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    IDLE,
    COOKING,
    COUNT,
}

public class GameStateManager : MonoBehaviour
{
    private static GameState CurrentGameState = GameState.IDLE;

    public static void ChangeState(GameState state)
    {
        CurrentGameState = state;
    }

    public static GameState GetState()
    {
        return CurrentGameState;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentGameState = GameState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
