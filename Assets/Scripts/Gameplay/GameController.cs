using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;

namespace Core.Game
{
    public enum GAME_STATE
    {
        INIT,
        DAYSTART,
        DAYEND,
        GAMEOVER,
        VICTORY,
        PLAY,
        PAUSE,
        UPGRADE,

    }
}
public class GameController : MonoBehaviour
{
    public GAME_STATE currentGameState;
    private GAME_STATE previousGameState;
    private DayController dayController;
    [SerializeField] PlayerController playerController;
    public static event Action<GAME_STATE> OnGamePaused;
    public static event Action OnGameStart;
    public static event Action OnGameRestart;
    public static event Action OnGameUpgrade;
    public static event Action<bool> OnGameOver;

    public static GameController Instance { get; private set; }

    public GAME_STATE CurrentState { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        dayController = GetComponent<DayController>();
    }

    public int GetDay()
    {
        return dayController.currentDay;
    }
    void Start()
    {
        SetState(GAME_STATE.DAYSTART);
    }
    public void SetState(GAME_STATE state)
    {
        if (currentGameState == state)
        {
            return;
        }
        previousGameState = currentGameState;
        currentGameState = state;
        Debug.Log($"Current Game State: {currentGameState}");
        EnterState(state);
    }

    void EnterState(GAME_STATE state)
    {
        switch (state)
        {
            case GAME_STATE.INIT:
                break;
            case GAME_STATE.DAYSTART:
                playerController.SetPositionSpawn();
                OnGameStart?.Invoke();
                dayController.StartDay();
                OnGamePaused?.Invoke(state);
                break;
            case GAME_STATE.DAYEND:
                OnGamePaused?.Invoke(state);
                break;
            case GAME_STATE.PAUSE:
                Time.timeScale = 0;
                OnGamePaused?.Invoke(state);
                break;
            case GAME_STATE.PLAY:
                Time.timeScale = 1;
                OnGamePaused?.Invoke(state);
                break;
            case GAME_STATE.UPGRADE:
                OnGamePaused?.Invoke(state);
                OnGameUpgrade?.Invoke();
                break;
            case GAME_STATE.GAMEOVER:
                Time.timeScale = 0;
                OnGamePaused?.Invoke(state);
                OnGameOver?.Invoke(true);
                break;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        dayController.RestartCurrentDay();
        OnGameRestart?.Invoke();

        SetState(GAME_STATE.DAYSTART);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (currentGameState == GAME_STATE.PLAY || currentGameState == GAME_STATE.PAUSE))
        {
            if (currentGameState == GAME_STATE.PLAY)
            {
                SetState(GAME_STATE.PAUSE);
            }
            else if (currentGameState == GAME_STATE.PAUSE)
            {
                SetState(GAME_STATE.PLAY);
            }
        }
    }

}