using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private MeleeNavMeshMover _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Role _role;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _attacker.SetWeapon(_role.Weapon);
        _health.SetStartHealth(_role.StartHealth);
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
