using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microlight.MicroBar;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] MicroBar healthBar;
    private EnemyController enemyController;
    private bool isToggled;
    private int max_health;

    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        healthBar.gameObject.SetActive(false);
        max_health = enemyController.health;
        healthBar.Initialize(max_health);
    }
    private void ShowEnemyHealthBar()
    {
        isToggled = true;
        healthBar.gameObject.SetActive(true);
        // enemyHealthBar.gameObject.SetActive(true);
        // enemyHealthBarFill.gameObject.SetActive(true);
    }

    void Start()
    {
        healthBar.Initialize(max_health);

    }
    void OnEnable()
    {
        enemyController.onEnemyDamaged += setHealth;
    }

    void OnDisable()
    {
        enemyController.onEnemyDamaged -= setHealth;
    }

    void setHealth(int health)
    {
        if (!isToggled) ShowEnemyHealthBar();
        healthBar.UpdateBar(health);
    }
}
