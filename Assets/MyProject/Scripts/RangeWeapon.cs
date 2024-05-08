using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "Config/RangeWeapon", order = 0)]
public class RangeWeapon : Weapon
{
    [field: SerializeField] public Projectile Projectile { get; private set; }
    [field: SerializeField] public float ProjectileSpeed { get; private set; }
}
