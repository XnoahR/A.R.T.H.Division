using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image enemyHealthBar;
    public Image enemyHealthBarFill;
    private EnemyController enemyController;
    private bool isToggled;
    private int max_health;

    void Awake()
    {
        enemyController = GetComponentInParent<EnemyController>();
        enemyHealthBar.gameObject.SetActive(false);
        enemyHealthBarFill.gameObject.SetActive(false);
        max_health = enemyController.health;
    }
    private void ShowEnemyHealthBar()
    {
        isToggled = true;
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBarFill.gameObject.SetActive(true);
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
        enemyHealthBarFill.fillAmount = (float)health / max_health;
    }
}
