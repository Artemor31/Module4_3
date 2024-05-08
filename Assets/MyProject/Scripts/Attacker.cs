using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;
    public bool AttackProcessing => _weapon.AttackCooldown - _attackTime <= 1.2f;

    [SerializeField] private Animator _animator;
    [SerializeField] protected LayerMask _damageMask;
    [SerializeField] protected Transform _handRight;
    [SerializeField] protected Transform _handLeft;

    protected Weapon _weapon;
    private GameObject _weaponInstance;
    private float _attackTime;

    public virtual void Construct(Weapon weapon)
    {
        if (_weaponInstance != null)        
            Destroy(_weaponInstance);        

        _weapon = weapon;
        _weaponInstance = Instantiate(_weapon.Prefab, _weapon.Hand == Hand.Right ? _handRight : _handLeft);
        ResetAttackTimer();

        if (weapon.Animator != null)
            _animator.runtimeAnimatorController = weapon.Animator;
    }

    private void Update() => _attackTime -= Time.deltaTime;
    public bool InRange(Vector3 position) => Vector3.Distance(transform.position, position) <= _weapon.Range;
    public void AttackEvent() => AttackOnEvent();
    protected virtual void AttackOnEvent() { }

    public virtual void Attack()
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

    private void ResetAttackTimer() => _attackTime = _weapon.AttackCooldown;

    private void OnDrawGizmosSelected()
    {
        if (_weapon == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _weapon.Range);
    }
}