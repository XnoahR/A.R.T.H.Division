using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private TARGET attackTarget;
    private Enemy enemy;
    private string targetTag;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        enemy = enemyController.enemyScript;
        attackTarget = enemy.enemyData.attackTarget;
        targetTag = attackTarget == TARGET.PLAYER ? "Player" : "Objective";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        enemy.Stop(); // stop movement immediately
        enemyController.isTargetDetected = true; // tetap true selama di trigger
        enemyController.StartAttackSafe(collision.transform);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag)) return;

        enemyController.isTargetDetected = false; // baru reset detect saat keluar
        enemyController.StopAttackSafe();
    }

}
