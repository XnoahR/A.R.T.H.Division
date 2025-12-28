using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponAimRotator : MonoBehaviour
{
    public WeaponController weaponController;
    [SerializeField] private Transform weaponPivot;

    // Start is called before the first frame update
    void Awake()
    {
        weaponController = GetComponentInParent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = weaponController.aimDirection;
        float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponPivot.transform.rotation= Quaternion.Euler(0, 0, angle);
    }
}
