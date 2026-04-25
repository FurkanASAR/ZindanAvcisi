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
    }

    InventorySlot[] slots;


    private void UpdateUi(object sender, System.EventArgs e)
    {
        Debug.Log("UI_InventoryUpdate UI executed");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < characterInventory.ItemList.Count)
            {
                Debug.Log("UI_InventoryUpdate UI executed : " + characterInventory.ItemList.Count);
                Debug.Log("Eklenecek item: " + characterInventory.ItemList[i].itemData.itemName);
                slots[i].AddItem(characterInventory.ItemList[i].itemData);
            }
        }
    }


}
