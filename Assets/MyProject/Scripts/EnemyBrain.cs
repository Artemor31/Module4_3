using System;
using System.Collections;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public event Action<EnemyBrain> OnDied;
    private bool InRange => _attacker.InRange(_player.transform.position);
    public bool IsDead => _health.IsDead;
    public int ExpReward => _data.Exp;
    public int GoldReward => _data.Gold;

    [SerializeField] private Attacker _attacker;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Loot _loot;
    private EnemyData _data;
    private Player _player;
    private State _state;

    public void Construct(Player player, EnemyData data)
    {
        _data = data;
        _player = player;
        _state = State.Idle;
        _attacker.Construct(_data.Weapon); 

        if (_attacker is RangeAttacker rangeAttacker)
            rangeAttacker.PickTarget(_player.transform);

        _health.Construct(_data.StartHealth, _data.StartHealth);
        _health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(Health health)
    {
        if (health.IsDead)
        {
            SetState(State.Dead);
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        SpawnLoot();
    }

    private void SpawnLoot()
    {
        if (_data.Loot.Count == 0) return;
        var loot = Instantiate(_loot, transform.position, Quaternion.identity);
        loot.Init(_data.Loot.Random());
    }

    public void SetState(State newState)
    {
        switch (newState)
        {
            case State.Idle:
                _mover.Stop();
                break;
            case State.Follow:
                _mover.MoveTo(_player.transform.position);
                break;
            case State.Attack:
                _mover.Stop();
                _attacker.Attack();
                break;
            case State.Dead:
                OnDied?.Invoke(this);
                StartCoroutine(Disappear());
                break;

        case State.None:
        default:
                break;
        }

        _state = newState;
    }

    private void Update()
    {
        if (_state == State.Dead) return;

        if (_attacker.CanAttack() && InRange)
        {
            SetState(State.Attack);
            return;
        }

        if (!InRange && !_attacker.AttackProcessing())
        {
            SetState(State.Follow);
            return;
        }

        SetState(State.Idle);
    }

    public enum State
    {
        None = 0,
        Idle = 1,
        Follow = 2,
        Attack = 3,
        Dead = 4
    }
}
