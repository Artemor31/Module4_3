using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Health> OnHealthChanged;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth {  get; private set; }
    public bool IsDead => CurrentHealth < 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _hitVFX;
    private float _maxHealth;
    private bool _isDead;

    public void SetStartHealth(float startHealth)
    {
        _maxHealth = startHealth;
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        if (_hitVFX != null)
            Instantiate(_hitVFX, transform.position + Vector3.up, Quaternion.identity, transform);

        _animator.Play("Hited");

        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(this);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _animator.SetTrigger("Dead");
    }
}
