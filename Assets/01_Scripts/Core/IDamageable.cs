using UnityEngine;

public interface IDamageable
{
    // ������, �ǰݺ���, �ǰ� �븻����
    void OnDamage(int damage, Vector2 hitPoint, Vector2 normal);
}