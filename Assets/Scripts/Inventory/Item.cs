using System;
using UnityEngine;

public class Item : MonoBehaviour, ICollectable
{
    public event EventHandler OnCollected;
    public ItemSO itemData;

    public ItemSO GetItem => RunTimeItem;
    private ItemSO RunTimeItem;


    private void Awake()
    {
        RunTimeItem = ScriptableObject.Instantiate(itemData);
    }

    private void Start()
    {
        RegisterObject();
    }
    private void RegisterObject()
    {
        CollectableManager.Instance.RegisterEvent(this);
    }
    public void GetRunTimeItem(ItemSO itemSo)
    {
        itemSo = ScriptableObject.Instantiate(itemData);
    }
    public virtual void Collect()
    {
        OnCollected?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
}
