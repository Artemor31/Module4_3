using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Role _role;
    [SerializeField] private Loot _loot;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _attacker.SetWeapon(_role.Weapon);
        _health.SetStartHealth(_role.StartHealth);
        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(Health health)
    {
        if (health.IsDead)
        {
            StartCoroutine(Dessapear());
        }
    }

    private IEnumerator Dessapear()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        SpawnLoot();
    }

    private void SpawnLoot()
    {
        var loot = Instantiate(_loot, transform.position, Quaternion.identity);
        loot.Init(_role.Weapon);
    }

    private void Update()
    {
        if (_health.IsDead) return;
        if (_player.IsDead) return;

        if (_attacker.InRange(_player.transform.position))
        {
            _mover.Stop();
            _attacker.Attack();
        }
        else
        {
            _mover.MoveTo(_player.transform.position);
        }
    }
}
