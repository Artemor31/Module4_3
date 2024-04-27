﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public event Action<Item> OnCellClicked;

    [SerializeField] private Image _icon;
    private Item _item;

    public void SetItem(Item item)
    {
        _item = item;
        _icon.gameObject.SetActive(true);
        _icon.sprite = _item.Icon;
    }

    public void Clear()
    {
        _item = null;
        _icon.gameObject.SetActive(false);
        _icon.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData) => OnCellClicked?.Invoke(_item);
}
