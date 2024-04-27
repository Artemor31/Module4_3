using System;
using System.Collections.Generic;

public class Inventory
{
    private const int MaxInventoryItems = 12;

    public event Action<Dictionary<Slot, Item>> OnEquipmentChanged;
    public event Action<List<Item>> OnInventoryChanged;

    private Dictionary<Slot, Item> _equipment;
    private List<Item> _inventory;

    public Dictionary<Slot, Item> Equipment { get => _equipment; set => _equipment = value; }
    public List<Item> InventoryStash { get => _inventory; set => _inventory = value; }

    public Inventory()
    {
        Equipment = new Dictionary<Slot, Item>();
        _inventory = new List<Item>();
    }

    public bool TryEquipItem(Item item)
    {
        if (Equipment.TryGetValue(item.Slot, out var equip))
        {
            return false;
        }
        else
        {
            Equipment[item.Slot] = item;
            OnEquipmentChanged?.Invoke(Equipment);
            return true;
        }
    }

    public bool TryUnequipItem(Slot slot, out Item item)
    {
        if (Equipment.TryGetValue(slot, out var equip))
        {
            Equipment.Remove(slot);
            OnEquipmentChanged?.Invoke(Equipment);
            item = equip;
            return true;            
        }

        item = null;
        return false;
    }

    public bool TryAddToInventory(Item item)
    {
        if (_inventory.Count >= MaxInventoryItems)
        {
            return false;
        }

        _inventory.Add(item);
        OnInventoryChanged?.Invoke(InventoryStash);
        return true;
    }

    public Item RemoveFromInventory(Item item)
    {
        if (_inventory.Remove(item))
        {
            return item;
        }

        return null;
    }
}