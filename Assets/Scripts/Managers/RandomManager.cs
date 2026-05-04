using System;
using Unity.VectorGraphics;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public static RandomManager Instance { get; private set; }
    [SerializeField] private BrokenDungeonDoor brokenDungeonDoor;
    [SerializeField] private DungeonDoor dungeonDoor;
    [SerializeField] private Player player;

    Vector3 playerPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerPosition = Vector3.zero;
        player.transform.position = playerPosition;
    }
    private void OnEnable()
    {
        dungeonDoor.OnCollected += HandleDungeonDoorEvent;
        dungeonDoor.OnCollected += SubscribeToEvents;
        brokenDungeonDoor.OnCollected += HandleBrokenDungeonDoorEvent;
    }

    private void OnDisable()
    {
        dungeonDoor.OnCollected -= HandleDungeonDoorEvent;
        brokenDungeonDoor.OnCollected -= HandleBrokenDungeonDoorEvent;
    }

    private void HandleBrokenDungeonDoorEvent(object sender, EventArgs e)
    {
        Debug.Log("Broken door");
        player.transform.position = playerPosition;
    }

    private void HandleDungeonDoorEvent(object sender, EventArgs e)
    {
        Debug.Log("Dungeon door");
        playerPosition = player.transform.position + Vector3.up * 4;
        Debug.Log("Player position: " + playerPosition);
    }

    private void SubscribeToEvents(object sender, EventArgs e) 
    {
        dungeonDoor = FindFirstObjectByType<DungeonDoor>();
        brokenDungeonDoor = FindFirstObjectByType<BrokenDungeonDoor>();

        dungeonDoor.OnCollected += HandleDungeonDoorEvent;
        brokenDungeonDoor.OnCollected += HandleBrokenDungeonDoorEvent;
    }

}
