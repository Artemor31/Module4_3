using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, ISaveLoadEntity<PlayerSaveData>
{
    public bool IsDead => _health.IsDead;
    public Experience Experience { get; private set; }
    public Gold Gold { get; private set; }
    public Inventory Inventory { get; private set; }

    [SerializeField] private Attacker _attacker;
    [SerializeField] private Motion _mover;
    [SerializeField] private Health _health;
    [SerializeField] private Caster _caster;
    [SerializeField] private LootPicker _lootPicker;
    [SerializeField] private LayerMask _layerMask;
    private InputManager _input;
    private SaveService _saveService;

    public void Construct(Weapon weapon, float startHealth, Camera camera, Progression progression, SaveService saveService)
    {
        var data = Restore();
        _input = new InputManager(camera, _layerMask);
        _saveService = saveService;
        Inventory = new Inventory();
        Experience = new Experience(progression, data);
        Gold = new Gold(data);

        _mover.Construct(camera);

        if (data.Health.Max == 0)
        {
            _health.Construct(startHealth, startHealth);
        }
        else
        {
            _health.Construct(data.Health.Current, data.Health.Max);
        }

        _attacker.Construct(weapon);
        Inventory.TryEquipItem(weapon);

        _lootPicker.OnItemPicked += OnItemPicked;
        Inventory.OnEquipmentChanged += OnEquipmentChanged;
        _saveService.AddEntity(this);
        
    }

    private void OnEquipmentChanged(Dictionary<Slot, Item> equipment)
    {
        if (equipment.TryGetValue(Slot.Weapon, out var newWeapon) && newWeapon is Weapon weapon) 
        {
            _attacker.Construct(weapon);
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
        else if (_input.Casting && _input.TryGetPointerWorldPosition(out var point))
        {
            _caster.CastFireball(point);
        }

        if (!_attacker.AttackProcessing())
        {
            _mover.Move(_input.Motion);
        }
    }

    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData
        {
            Exp = Experience.CurrentExp,
            Gold = Gold.CurrentGold,
            Level = Experience.CurrentLevel,
            Health = new PlayerHealthData
            {
                Current = _health.CurrentHealth,
                Max = _health.MaxHealth
            }
        };

        string data = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("Player", data);
    }

    public PlayerSaveData Restore()
    {
        if (PlayerPrefs.HasKey("Player"))
        {
            string json = PlayerPrefs.GetString("Player");
            return JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
        {
            return new PlayerSaveData();
        }
    }
}

[Serializable]
public class PlayerSaveData : SaveData
{
    public int Exp;
    public int Level;
    public int Gold;
    public PlayerHealthData Health;
}

[Serializable]
public struct PlayerHealthData
{
    public float Current;
    public float Max;
}