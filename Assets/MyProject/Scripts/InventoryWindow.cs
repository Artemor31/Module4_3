using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<SlotCell> _equipment;
    [SerializeField] private List<Cell> _inventory;

    private void Start()
    {
        _closeButton.onClick.AddListener(OnCloseClicked);
        foreach (SlotCell cell in _equipment)
        {
            cell.Cell.Clear();
        }

        foreach (Cell cell in _inventory)
        {
            cell.Clear();
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
