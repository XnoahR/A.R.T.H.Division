using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float speed;
    public bool isRight = true;
    public bool canPlay = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        if (canPlay) { Move();}
        
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
