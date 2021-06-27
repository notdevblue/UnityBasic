using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("공격관련")]
    [SerializeField] private float timeBetAttack   = 0.8f;
    [SerializeField] private float lastAttackTime  = 0.0f;
    [SerializeField] private float atkRange        = 1.0f;
    [SerializeField] private int   atkDamage       = 2;
    [SerializeField] private LayerMask whatIsEnemy;

    private PlayerInput     playerInput;
    private PlayerAnimation playerAnim;
    private SpriteRenderer  spriteRenderer;

    

    private void Start()
    {
        playerInput    = GetComponent<PlayerInput>();
        playerAnim     = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerInput.isAttack && Time.time > lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        playerAnim.StartAttack();
        FireRay();
    }

    private void FireRay()
    {
        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;

        //Debug.DrawRay(transform.position, dir * atkRange, Color.red, 0.5f);

        // Physics2D.Raycast(위치, 방향, 길이, 레이어마스크)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, atkRange, whatIsEnemy);
        
        // 충돌 체크
        if (hit.collider != null)
        {
            IDamageable iDamage = hit.collider.GetComponent<IDamageable>();
            if (iDamage != null)
            {
                iDamage.OnDamage(atkDamage, hit.point, hit.normal);
            }
        }
    }
}
