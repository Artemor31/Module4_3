using System.Collections.Generic;
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

    public void Construct(Weapon weapon, float startHealth, Camera camera)
    {
        _input = new InputManager();
        Inventory = new Inventory();

        _mover.Construct(camera);
        _health.SetStartHealth(startHealth);
        _attacker.SetWeapon(weapon);
        Inventory.TryEquipItem(weapon);

        _lootPicker.OnItemPicked += OnItemPicked;
        Inventory.OnEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Dictionary<Slot, Item> equipment)
    {
        if (equipment.TryGetValue(Slot.Weapon, out var newWeapon) && newWeapon is Weapon weapon) 
        {
            _attacker.SetWeapon(weapon);
        }        
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
