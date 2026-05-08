using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Test", menuName = "InventorTest/ItemSO")]
public class ItemSO : ScriptableObject
{
    public enum ItemType
    {
        Coin,
        Square,
        Triangle,
        GoToInventory,
        Heart
    }

    public ItemType itemType;

    [field: SerializeField] public ItemType Type { get; private set; }
    [field: SerializeField] public string itemName { get; private set; } 
    [field: SerializeField] public Sprite itemSprite { get; private set; }
    [field: SerializeField] public int itemValue { get; private set; }
}
