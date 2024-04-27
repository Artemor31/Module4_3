using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Loot _loot;
    private EnemyData _data;
    private Player _player;

    public void Construct(Player player, EnemyData data)
    {
        _data = data;
        _player = player;
        _attacker.SetWeapon(_data.Weapon);
        _health.SetStartHealth(_data.StartHealth);
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
        loot.Init(_data.Loot.Random());
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
