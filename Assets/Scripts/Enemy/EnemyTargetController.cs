using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private TARGET attackTarget;
    private string target;
    // Start is called before the first frame update
    void Start()
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        enemyController = enemy.GetComponent<EnemyController>();
        attackTarget = enemy.enemyData.attackTarget;
        target = attackTarget == TARGET.PLAYER ? "Player" : "Objective";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
        {
            if (enemyController.isTargetDetected) return;
            enemyController.isTargetDetected = true;
            enemyController.attackTarget = collision.transform;
            enemyController.StartCoroutine(enemyController.AttackTarget());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(target))
        {
            enemyController.isTargetDetected = false;
        }
    }
}
