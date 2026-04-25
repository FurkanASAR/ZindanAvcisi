using System;
using UnityEngine;

public interface ICollectable
{
    //public void GetRunTimeItem(ItemSO itemSo);

    public ItemSO GetItem { get; }

    public event EventHandler OnCollected;
    public void Collect();
}
