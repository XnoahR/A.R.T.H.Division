using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Game;
public class DayController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<SpawnerData> spawnerList;
    [SerializeField] private EnemySpawnerController enemySpawnerController;
    [SerializeField] private GameController gameController;
    [SerializeField] private PlayerEconomy playerEconomy;
    public int currentDay;
    public event Action<int> OnDayChanged;
    public static event Action<int, int> OnGameEnd;
    public static event Action OnDayEnded;
    [SerializeField] private int enemiesLeft;
    [SerializeField] private int currentDayEnemies;
    [SerializeField] private int maxDay => spawnerList.Count;
    public bool hasNextDay => currentDay + 1 <= maxDay;
    bool isEnding;

    void Awake()
    {
        gameController = GetComponent<GameController>();
    }
    void Start()
    {
        currentDay = 1;
        OnDayChanged?.Invoke(currentDay);
    }
    public void StartDay()
    {
        isEnding = false;
        StartCoroutine(StartCount());
    }

    void OnEnable()
    {
        EnemyController.OnEnemyDead += ReduceEnemies;
    }

    void OnDisable()
    {
        EnemyController.OnEnemyDead -= ReduceEnemies;
    }
    public void ReduceEnemies()
    {
        enemiesLeft--;
        Debug.Log($"Enemies Left: {enemiesLeft}");
        if (enemiesLeft <= 0)
        {
            StartCoroutine(EndCount());
        }
    }

    private void EndDay()
    {
        enemySpawnerController.StopSpawn();
        if (hasNextDay)
        {
            // UI P1
            OnDayEnded?.Invoke();

            return;
        }
        Debug.Log("Game End");
    }

    public void UpgradePage()
    {
        currentDay++;
        OnDayChanged?.Invoke(currentDay);
        gameController.SetState(GAME_STATE.UPGRADE);
    }
    private IEnumerator StartCount()
    {
        yield return new WaitForSeconds(3);
        gameController.SetState(GAME_STATE.PLAY);
        enemiesLeft = spawnerList[currentDay - 1].enemies.Count;
        currentDayEnemies = enemiesLeft;
        enemySpawnerController.spawnerReference = spawnerList[currentDay - 1];
        enemySpawnerController.Init();
        enemySpawnerController.StartSpawn();
    }

    private IEnumerator EndCount()
    {
        if(isEnding) yield break;
        isEnding = true;
        yield return new WaitForSeconds(3);
        int income = playerEconomy.AddMoney(50);
        int total = playerEconomy.GetMoney();

        OnGameEnd?.Invoke(income, total);
        gameController.SetState(GAME_STATE.DAYEND);
        EndDay();
    }
}