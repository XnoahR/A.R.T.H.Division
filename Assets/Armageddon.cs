using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armageddon : SpecialBase
{
    [SerializeField] GameObject boomPS;
    [SerializeField] BoxCollider2D col;
    [SerializeField] Rigidbody2D rb;

    public int totalRows = 7;
    public int boomsPerRow = 3;

    public float startX = -5f;
    public float rowXOffset = 2f;
    public float xStep = 1f;
    public float yStep = 1.5f;

    public float rowDelay = 0.5f;
    float[] offsets = { -5f, -2f, 0f, 2f, 5f };
    int offsetIndex = 0;
    public void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Play()
    {
         StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        yield return new WaitForSeconds(2);
        for (int row = 0; row < totalRows; row++)
        {
            Vector2 colOffset = col.offset;
            colOffset.x = offsets[offsetIndex];
            col.offset = colOffset;
            offsetIndex++;

            for (int i = 0; i < boomsPerRow; i++)
            {
                Vector3 localPos = new Vector3(
                    startX + row * rowXOffset + i * xStep,
                    i * yStep,
                    0
                );

                Vector3 worldPos = transform.TransformPoint(localPos);
                GameObject boomGO = Instantiate(boomPS, worldPos, Quaternion.identity);

                // IMPORTANT: small gap so burst registers per position
                yield return null;
            }

            yield return new WaitForSeconds(rowDelay);
        }
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1000);
            }
        }
    }
}
