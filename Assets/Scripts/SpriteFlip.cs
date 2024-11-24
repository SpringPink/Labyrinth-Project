using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour

{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flipInterval = 0.5f;
    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        StartCoroutine(FlipSpriteRoutine());
    }

    private IEnumerator FlipSpriteRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(flipInterval);

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }
}
