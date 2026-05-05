using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    [SerializeField] private Coin coin;
    //[SerializeField] private Player player;
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerinventory = FindObjectOfType<Player>().Inventory;
    }

    private void Start()
    {
        //playerinventory = player.Inventory;
    }

    public void RegisterEvent(ICollectable collectable)
    {
        collectable.OnCollected += (sender, e) =>
        {            
            switch (collectable.GetItem)
            {
                
                case ItemSO item when item.itemType == ItemSO.ItemType.GoToInventory:
                    Debug.Log("CollectableManager: Item" + collectable.GetItem);
                    Debug.Log("CollectableManager: playerInventory" + playerinventory);
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
