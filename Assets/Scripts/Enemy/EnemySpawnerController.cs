using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject enemyReference;
    [SerializeField] private bool canSpawn = true;
    private float cooldownTime = 0;
    private Coroutine spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemy()
    {
        while (canSpawn)
        {

            float delay = Random.Range(5, 8);
            float countdown = delay;

            while (countdown > 0f)
            {
                Debug.Log($"Spawn in: {Mathf.CeilToInt(countdown)}");
                yield return new WaitForSeconds(1f);
                countdown -= 1f;
            }
            Instantiate(enemyReference, transform.position, transform.rotation);

        }
    }
    void Update()
    {

    }
}
