using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Core.Game;
public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed;
    public int FireRateLevel;
    public int MagazineCapacityLevel;
    public int AttackLevel;
    public int ReloadSpeedLevel;

    public bool isRight = true;
    public bool canPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        GameController.OnGamePaused += ChangePlayState;
        FireRateLevel = 1;
        MagazineCapacityLevel = 1;
        AttackLevel = 1;
        ReloadSpeedLevel = 1;

    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        if (canPlay) { Move(); }

    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * Time.fixedDeltaTime * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            canPlay = !canPlay;
        }
    }

    public void AddStats(StatsUpgradeData upgradeData)
    {
        AttackLevel += upgradeData.attackValue;
        FireRateLevel += upgradeData.fireRateValue;
        MagazineCapacityLevel += upgradeData.magazineCapacityValue;
        ReloadSpeedLevel += upgradeData.reloadSpeedValue;
    }
    private void ChangePlayState(GAME_STATE state)
    {
        canPlay = state == GAME_STATE.PLAY || state == GAME_STATE.DAYSTART || state == GAME_STATE.DAYEND;
        Debug.Log(canPlay);
    }

    public void Flip(Transform weaponPivot)
    {
        isRight = !isRight;
        Vector3 aScale = weaponPivot.transform.localScale;
        aScale.x *= -1;
        aScale.y *= -1;
        weaponPivot.transform.localScale = aScale;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;


        // Vector3 wScale = currentWeapon.transform.localScale;
        // wScale.x *= -1;
        // wScale.y *= -1;
        // currentWeapon.transform.localScale = wScale;
    }
}
