using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] private Coin coin;
    [SerializeField] private Player player;
    public static CollectableManager Instance {  get; private set; }


    private InventorySystem playerinventory;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        playerinventory = player.Inventory;
    }

    public void RegisterEvent(ICollectable collectable)
    {
        collectable.OnCollected += (sender, e) =>
        {            
            switch (collectable.GetItem)
            {
                case ItemSO item when item.itemType == ItemSO.ItemType.GoToInventory:
                    playerinventory.AddItem(collectable.GetItem);
                    break;

                case ItemSO item when item.itemType == ItemSO.ItemType.Heart:
                     Debug.Log("CollectableManager: Heart collected!");
                    break;
                    


                default: Debug.Log("Defaul!");
                    break;
            }
        };
    }

}
