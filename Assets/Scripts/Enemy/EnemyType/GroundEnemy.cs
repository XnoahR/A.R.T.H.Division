
using System.Collections;
using UnityEngine;


public class GroundEnemy : Enemy
{
    [SerializeField] private GameObject attackArea;


    public override void Attack(Transform attackTarget, int damage)
    {
        StartCoroutine(spawnAttackArea());
    }

    IEnumerator spawnAttackArea()
    {
        yield return new WaitForSeconds(0.4f);
        attackArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        attackArea.gameObject.SetActive(false);
    }
}