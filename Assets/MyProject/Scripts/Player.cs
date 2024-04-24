using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsDead => _health.IsDead;

    [SerializeField] private Attacker _attacker;
    [SerializeField] private Motion _mover;
    [SerializeField] private Health _health;
    [SerializeField] private InputManager _input;

    private void Awake()
    {
        _health.SetStartHealth(StaticData.PlayerRole.StartHealth);
        _attacker.SetWeapon(StaticData.PlayerRole.Weapon);
    }

    private void Update()
    {
        if (_health.IsDead) return;

        if (_input.Attacking)
        {
            _attacker.Attack();
        }

        if (!_attacker.AttackProcessing)
        {
            _mover.Move(_input.Motion);
        }
    }
}
