using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LivingEntity : MonoBehaviour, IDamageable
{
    // Ã¤·Â
    [SerializeField] protected int maxHP;
                     protected int curHP;


    protected virtual void Start()
    {
        curHP = maxHP;
    }

    public virtual void OnDamage(int damage, Vector2 hitPoint, Vector2 normal)
    {
        curHP -= damage;

        if (curHP <= 0)
        {
            OnDie();
        }
    }

    abstract protected void OnDie();
}
