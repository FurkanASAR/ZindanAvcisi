using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance {  get; private set; }

    [SerializeField] private Coin coin;

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

    public void RegisterEvent(ICollectable collectable)
    {
        collectable.OnCollected += (sender, e) =>
        {
            switch (collectable)
            {
                case Coin: Debug.Log("Coin!");
                    break;

                default: Debug.Log("Defaul!");
                    break;
            }
        };
    }

}
