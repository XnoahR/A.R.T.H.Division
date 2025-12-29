using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectiveController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = gameObject.GetComponentInParent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            enemyController.isTargetDetected = true;
            enemyController.attackTarget = collision.transform;
            enemyController.StartCoroutine(enemyController.AttackTarget());
        }
    }
}
