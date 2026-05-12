using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] Transform itemParent;

    private InventorySystem characterInventory;
    private IHasInventory character;

    private void Awake()
    {        
        slots = itemParent.GetComponentsInChildren<InventorySlot>();
        character = GetComponentInParent<IHasInventory>();
    }

    private void OnEnable()
    {
        
    }
    private void Start()
    {
              
        characterInventory = character.Inventory;       
        characterInventory.OnItemPickedUp += UpdateUi;
        characterInventory.OnClearInventory += ClearUi;
    }

    InventorySlot[] slots;


    private void UpdateUi(object sender, System.EventArgs e)
    {        
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < characterInventory.ItemList.Count)
            {               
                slots[i].AddItem(characterInventory.ItemList[i].itemData);
            }
        }
    }

    private void ClearUi(object sender, EventArgs e)
    {
        for(int i = 0;i < slots.Length; i++)
        {
            slots[i].RemoveItem();
        }
    }

}
