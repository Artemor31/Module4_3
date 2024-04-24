using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<SlotCell> _equipment;
    [SerializeField] private List<Cell> _inventory;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _closeButton.onClick.AddListener(OnCloseClicked);
        _player.Inventory.OnEquipmentChanged += OnEquipmentChanged;
        _player.Inventory.OnInventoryChanged += OnInventoryChanged;

        OnEquipmentChanged(_player.Inventory.Equipment);
        OnInventoryChanged(_player.Inventory.InventoryStash);
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
        _closeButton.onClick.RemoveListener(OnCloseClicked);
    }
}

[Serializable]
public class SlotCell
{
    public Slot Slot;
    public Cell Cell;
}
