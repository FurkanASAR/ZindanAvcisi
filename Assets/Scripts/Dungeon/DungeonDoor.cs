using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonDoor : MonoBehaviour, ICollectable
{
    public event EventHandler OnCollected;

    public void Collect()
    {
        SceneManager.LoadScene("Dungeon");
    }
}
