using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHealth : LivingEntity
{
    public  Color          hitColor;
    private BoxCollider2D  col;
    private Rigidbody2D    rigid;
    private SlimeAnimation anim;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<SlimeAnimation>();
    }

    void OnEnable()
    {
        col.enabled = true;
        rigid.gravityScale = 1;
    }

    protected override void Start()
    {
        base.Start();


    }

    public override void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        rigid.AddForce(-normal * damage, ForceMode2D.Impulse);
        anim.SetHit();
        base.OnDamage(damage, hitPoint, normal);

        BloodParticle bp = PoolManager.GetItem<BloodParticle>();
        bp.SetRotation(normal);
        bp.SetParticleColor(hitColor);
        bp.Play(hitPoint);
    }

    protected override void OnDie()
    {
        rigid.gravityScale = 0;
        rigid.velocity = Vector2.zero;

        anim.SetDead();
        col.enabled = false;

        Invoke(nameof(DeadProcess), 1.0f);
    }

    private void DeadProcess()
    {
        gameObject.SetActive(false);
    }
}
