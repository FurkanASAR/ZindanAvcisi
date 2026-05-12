using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image image;

    private ItemSO item;
    public void AddItem(ItemSO item)
    {        
        this.item = item;
        this.image.sprite = this.item.itemSprite;
        image.enabled = true;
    }

    public void RemoveItem() 
    {
        this.item = null;
        this.image.sprite = null;
        image.enabled = false;
    }

}
