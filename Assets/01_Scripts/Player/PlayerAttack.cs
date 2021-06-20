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
    }
}
