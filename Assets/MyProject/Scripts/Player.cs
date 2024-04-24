using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsDead => _health.IsDead;
    public Inventory Inventory { get; private set; }

    [SerializeField] private Attacker _attacker;
    [SerializeField] private Motion _mover;
    [SerializeField] private Health _health;
    [SerializeField] private LootPicker _lootPicker;
    private InputManager _input;

    private void Awake()
    {
        _input = new InputManager();
        Inventory = new Inventory();

        _health.SetStartHealth(StaticData.PlayerRole.StartHealth);
        _attacker.SetWeapon(StaticData.PlayerRole.Weapon);
        Inventory.TryEquipItem(StaticData.PlayerRole.Weapon);
        _lootPicker.OnItemPicked += OnItemPicked;
    }

    private void OnItemPicked(Item item)
    {
        Inventory.TryAddToInventory(item);
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
