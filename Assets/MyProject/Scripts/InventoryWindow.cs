using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : Window
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<SlotCell> _equipment;
    [SerializeField] private List<Cell> _inventory;
    private Inventory _playerInv;

    public override void Construct(Player player)
    {
        base.Construct(player);
        _closeButton.onClick.AddListener(OnCloseClicked);
        _playerInv = _player.Inventory;

        _playerInv.OnEquipmentChanged += OnEquipmentChanged;
        _playerInv.OnInventoryChanged += OnInventoryChanged;
        OnEquipmentChanged(_playerInv.Equipment);
        OnInventoryChanged(_playerInv.InventoryStash);

        foreach (Cell cell in _inventory)
            cell.OnCellClicked += OnCellClicked;
    }

    private void OnCellClicked(Item item)
    {
        if (item == null) return;

        if (item.Slot != Slot.None)
        {
            _playerInv.RemoveFromInventory(item);

            if (_playerInv.TryUnequipItem(item.Slot, out var equip))
            {
                _playerInv.TryAddToInventory(equip);
            }

            _playerInv.TryEquipItem(item);
        }
    }

    private void OnInventoryChanged(List<Item> items)
    {
        foreach (Cell cell in _inventory)
        {
            cell.Clear();
        }

        for (int i = 0; i < items.Count; i++)
        {
            _inventory[i].SetItem(items[i]);
        }
    }

    private void OnEquipmentChanged(Dictionary<Slot, Item> items)
    {
        foreach (SlotCell cell in _equipment)
        {
            cell.Cell.Clear();
            if (items.TryGetValue(cell.Slot, out var item))
            {
                cell.Cell.SetItem(item);
            }
        }
    }

    private void OnCloseClicked()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_player == null) return;

        _closeButton.onClick.RemoveListener(OnCloseClicked);
        _playerInv.OnEquipmentChanged -= OnEquipmentChanged;
        _playerInv.OnInventoryChanged -= OnInventoryChanged;

        foreach (Cell cell in _inventory)
            cell.OnCellClicked -= OnCellClicked;
    }
}

[Serializable]
public class SlotCell
{
    public Slot Slot;
    public Cell Cell;
}
