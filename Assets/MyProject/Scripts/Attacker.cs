using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;
    public bool AttackProcessing => _weapon.AttackCooldown - _attackTime <= 1.2f;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;
    [SerializeField] private Transform _hand;

    private Collider[] _hits = new Collider[3];
    private Weapon _weapon;
    private GameObject _weaponInstance;
    private float _attackTime;

    public void SetWeapon(Weapon weapon)
    {
        if (_weaponInstance != null)        
            Destroy(_weaponInstance);        

        _weapon = weapon;
        _weaponInstance = Instantiate(_weapon.Prefab, _hand);
        ResetAttackTimer();

        if (weapon.Animator != null)
            _animator.runtimeAnimatorController = weapon.Animator;
    }

    void Update() => _attackTime -= Time.deltaTime;

    public void AttackEvent()
    {
        AttackNearEnemies();
    }

    public void Attack()
    {
        if (CanAttack)
        {
            AnimateAttack();
            ResetAttackTimer();
        }
    }

    private void AnimateAttack()
    {
        var index = Random.Range(0, 2);
        _animator.SetInteger("AttackIndex", index);
        _animator.SetTrigger("Attacking");
    }

    public bool InRange(Vector3 position) => Vector3.Distance(transform.position, position) <= _weapon.Range;
    private void ResetAttackTimer() => _attackTime = _weapon.AttackCooldown;

    private void AttackNearEnemies()
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

    private void OnDrawGizmosSelected()
    {
        if (_weapon == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _weapon.Range);
    }
}
