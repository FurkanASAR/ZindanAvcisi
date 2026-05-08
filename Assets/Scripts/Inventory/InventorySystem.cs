using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySystem
{
    public event EventHandler OnItemPickedUp;


    private List<Item> itemList;
    private int totalValue = 0;
    public InventorySystem()
    {
        itemList = new List<Item>();                
    }

    public List<Item> ItemList=> itemList;

    public void AddItem(ItemSO item)
    {
        Item newItem = new Item { itemData = item };
        Debug.Log($"Adding item: {newItem.itemData.itemName} to inventory");
        itemList.Add(newItem);
        Debug.Log("Item picked up event fired");

        foreach (Item itemm in itemList)
        { 
        Debug.Log(itemm.itemData.itemName);

        }


        OnItemPickedUp?.Invoke(this, EventArgs.Empty);
    }

    public int CalculateTotalValue()
    {        
        foreach (Item item in itemList)
        {
            totalValue += item.itemData.itemValue;
        }
        return totalValue;
    }

}
