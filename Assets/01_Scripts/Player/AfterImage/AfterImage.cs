using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AfterImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite, bool flip, Vector3 pos)
    {
        transform.position    = pos;
        spriteRenderer.flipX  = flip;
        spriteRenderer.color  = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        spriteRenderer.sprite = sprite;
        spriteRenderer.DOFade(0.0f, 0.7f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    

}
