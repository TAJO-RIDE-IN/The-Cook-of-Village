using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public InventoryItem Data { get; private set; }

    public Item(InventoryItem data) => Data = data;
}
