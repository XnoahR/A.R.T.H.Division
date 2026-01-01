using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [Header("Spawner Setup")]
    [SerializeField] public SpawnerData spawnerReference;
    [SerializeField] private bool canSpawn = true;

    [Header("Delay")]
    [Range(0.0f, 10.0f)]
    [SerializeField] private float delayMin;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float delayMax;
    private int MAX_SPAWNED_ENEMIES;
    private int spawnCounter = 0;
    private Coroutine spawner;
    // Start is called before the first frame update
    public void Init()
    {
        MAX_SPAWNED_ENEMIES = spawnerReference.enemies.Count;
        spawnCounter = 0;
    }

    void Start()
    {
    }

    public void StartSpawn()
    {
        canSpawn = true;
        StartCoroutine(SpawnEnemy());
    }
    public void StopSpawn()
    {
        StopCoroutine(SpawnEnemy());
        spawnCounter = 0;
    }
    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        while (canSpawn)
        {

            float delay = Random.Range(delayMin, delayMax);
            float countdown = delay;
            float YSpawnPos = -1f;
            while (countdown > 0f)
            {
                Debug.Log($"Spawn in: {Mathf.CeilToInt(countdown)}");
                yield return new WaitForSeconds(1f);
                countdown -= 1f;
            }
            GameObject enemyReference = spawnerReference.enemies[spawnCounter];
            YSpawnPos = enemyReference.GetComponent<IFlyable>() != null ? 3.5f: YSpawnPos;
            Vector3 spawnPosition = new Vector3(transform.position.x, YSpawnPos, transform.position.z);
            GameObject enemyGO = Instantiate(enemyReference, spawnPosition, transform.rotation);
            spawnCounter++;
            if(spawnCounter >= MAX_SPAWNED_ENEMIES)
            {
                canSpawn = false;
                Debug.Log("Limit reached, spawner off.");
            }
        }
    }
    void Update()
    {

    }
}
