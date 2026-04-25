using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;

    private ItemSO item;
    public void AddItem(ItemSO item)
    {
        Debug.Log("InventorySlot: Adding item executed " + item.itemName);  
        this.item = item;
        this.image.sprite = this.item.itemSprite;
        image.enabled = true;
    }



}
