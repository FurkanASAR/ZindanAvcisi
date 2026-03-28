using UnityEngine;
using System;

public class Coin : MonoBehaviour, ICollectable
{

    private void Start()
    {
        RegisterObject();
    }

    public void Collect()
    {
        OnCollected?.Invoke(this, EventArgs.Empty);
        Debug.Log("Coin Collect exectued!");
    }
    public event EventHandler OnCollected;

    private void RegisterObject()
    {
        CollectableManager.Instance.RegisterEvent(this);
    }
}
