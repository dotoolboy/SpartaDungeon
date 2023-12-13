using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> _items = new();

    public event Action OnChanged;


    public Item this[string key]
    {
        get
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Key == key) return _items[i];
            }
            return null;
        }
    }

    public void Add(Item item)
    {
        _items.Add(item);
        OnChanged?.Invoke();
    }
    public void Remove(Item item)
    {
        _items.Remove(item);
        OnChanged?.Invoke();
    }
}
