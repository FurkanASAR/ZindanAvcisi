using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour, ICollectable
{
    public ItemSO GetItem => throw new NotImplementedException();

    public event EventHandler OnCollected;

    public void Collect()
    {
        SceneFader.Instance.FadeToScene("Dungeon");        
        OnCollected?.Invoke(this, EventArgs.Empty);
        
    }

    public void GetRunTimeItem(ItemSO itemSo)
    {
        throw new NotImplementedException();
    }
}
