using System;
using UnityEngine;

public interface ICollectable
{
    public event EventHandler OnCollected;
    public void Collect();
}
