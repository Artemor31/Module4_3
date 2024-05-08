using UnityEngine;

public class MeleeAttacker : Attacker
{
    private Collider[] _hits = new Collider[3];

    protected override void AttackOnEvent()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, _weapon.Range, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_weapon.Damage);
            }
        }
    }
}
