
using System.Collections;
using UnityEngine;


public class GroundEnemy : Enemy
{
    [SerializeField] private GameObject attackArea;


    public override void Attack(Transform attackTarget, int damage)
    {
        if (attackTarget.TryGetComponent<IDamageable>(out var dmg))
        {
            dmg.TakeDamage(damage);
        }

        StartCoroutine(spawnAttackArea()); // hanya visual
    }

    IEnumerator spawnAttackArea()
    {
        yield return new WaitForSeconds(0.4f);

        attackArea.SetActive(false); // reset trigger
        yield return null;           // tunggu 1 frame
        attackArea.SetActive(true);

        yield return new WaitForSeconds(0.6f);
        attackArea.SetActive(false);
    }
}