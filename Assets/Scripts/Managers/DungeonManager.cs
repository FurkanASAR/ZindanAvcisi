using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonGenerator dungeonGenerator;


    private GameObject player;
    private const string PLAYER_TAG = "Player";
    private Vector3 brokenDoorPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG);

        dungeonGenerator.OnBrokenDoorGenerated += DungeonGenerator_OnBrokenDoorGenerated;
    }

    private void OnDisable()
    {
        dungeonGenerator.OnBrokenDoorGenerated -= DungeonGenerator_OnBrokenDoorGenerated;
    }

    private void DungeonGenerator_OnBrokenDoorGenerated(object sender, System.EventArgs e)
    {
        brokenDoorPosition = dungeonGenerator.PlayerWorldPosition;
        player.transform.position = brokenDoorPosition;
    }
}