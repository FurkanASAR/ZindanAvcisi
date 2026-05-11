using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenDungeonDoor : MonoBehaviour, ICollectable
{
    private Player player;
    public ItemSO GetItem => throw new NotImplementedException();

    public event EventHandler OnCollected;

    public void Collect()
    {
        SceneFader.Instance.FadeToScene("Village");         
    }

    public void GetRunTimeItem(ItemSO itemSo)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            player.transform.position = Vector3.zero;
            ScoreScript.instance.UpdateScore();
            player.Inventory.ClearInventory();
        }
    }
}
