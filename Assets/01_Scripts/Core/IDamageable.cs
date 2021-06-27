using UnityEngine;

public interface IDamageable
{
    // 데미지, 피격부위, 피격 노말백터
    void OnDamage(int damage, Vector2 hitPoint, Vector2 normal);
}