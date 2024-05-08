using System;
using UnityEngine;

public class RangeAttacker : Attacker
{
    private Transform _target;
    private RangeWeapon _rangeWeapon;

    public override void Construct(Weapon weapon)
    {
        if (weapon is RangeWeapon range)
        {
            _rangeWeapon = range;
            base.Construct(weapon);
        }
        else
        {
            throw new Exception("Melee weapon on range unit");
        }
    }

    public override void Attack()
    {
        transform.LookAt(_target);
        base.Attack();
    }

    public void PickTarget(Transform target) => _target = target;

    protected override void AttackOnEvent()
    {
        var projectile = Instantiate(_rangeWeapon.Projectile, _handRight.position, Quaternion.identity);
        projectile.transform.LookAt(_target.position + Vector3.up);
        projectile.Construct(_rangeWeapon.ProjectileSpeed, _rangeWeapon.Damage, _damageMask);
    }
}
