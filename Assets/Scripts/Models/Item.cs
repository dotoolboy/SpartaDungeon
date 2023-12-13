using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item
{
    public ItemData Data { get; private set; }
    public string Key { get; private set; }
    public string ItemName { get; private set; }
    public string ItemDescription { get; private set; }
    public float Cost { get; private set; }
    public List<StatModifier> Modifiers { get; private set; }

    public Item(ItemData data)
    {
        this.Data = data;
        this.Key = data.key;
        this.ItemName = data.itemname;
        this.ItemDescription = data.itemdescription;
        this.Cost = data.cost;
        this.Modifiers = data.modifiers;
    }
}
