using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    [SerializeField] private List<SpawnerData> spawnerList;
    [SerializeField] private EnemySpawnerController enemySpawnerController;
    public int currentDay;
    public event Action<int> OnDayChanged;
    [SerializeField] private int enemiesLeft;
    [SerializeField] private int currentDayEnemies;
    [SerializeField] private int maxDay => spawnerList.Count;
    public bool hasNextDay => currentDay + 1 <= maxDay;

    void Start()
    {
        currentDay = 1;
        OnDayChanged?.Invoke(currentDay);
        StartDay();
    }
    public void StartDay()
    {
        
        Debug.Log($"Starting day: {currentDay}");
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
            currentDay++;
            OnDayChanged?.Invoke(currentDay);
            StartDay();
            return;
        }
        Debug.Log("Game End");
    }
    private IEnumerator StartCount()
    {
        yield return new WaitForSeconds(3);
        enemiesLeft = spawnerList[currentDay - 1].enemies.Count;
        currentDayEnemies = enemiesLeft;
        enemySpawnerController.spawnerReference = spawnerList[currentDay - 1];
        enemySpawnerController.Init();
        enemySpawnerController.StartSpawn();
    }

    private IEnumerator EndCount()
    {
        yield return new WaitForSeconds(3);
        EndDay();
    }
}