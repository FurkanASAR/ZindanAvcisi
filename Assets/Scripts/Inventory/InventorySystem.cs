using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem
{
    public event EventHandler OnItemPickedUp;
    public event EventHandler OnClearInventory;


    private List<Item> itemList;
    public int totalValue = 0;
    private const int MAX_INVENTROY_SIZE = 5;
    private int currentInventroySize = 0;
    public InventorySystem()
    {
        itemList = new List<Item>();                
    }

    public List<Item> ItemList=> itemList;

    public void AddItem(ItemSO item)
    {
        if (currentInventroySize < MAX_INVENTROY_SIZE)
        {
            Item newItem = new Item { itemData = item };
            itemList.Add(newItem);
            currentInventroySize++;

            OnItemPickedUp?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Inventory is full. Cannot add more items.");
        }
    }

    public int CalculateTotalValue()
    {        
        foreach (Item item in itemList)
        {
            totalValue += item.itemData.itemValue;
        }
        return totalValue;
    }

    public void ClearInventory()
    {
        itemList.Clear();
        currentInventroySize = 0;
        OnClearInventory?.Invoke(this, EventArgs.Empty);
    }
}
