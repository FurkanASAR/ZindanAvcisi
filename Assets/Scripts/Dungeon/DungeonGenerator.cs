using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public enum MapGrid
    {
        Empty,
        Floor,
        Wall
    }
    public Vector3 PlayerWorldPosition
    {
        get
        {
            return playerWorldPosition;
        }
    }
    public event EventHandler OnBrokenDoorGenerated;

    [SerializeField] private GameObject brokenDoor;
    [SerializeField] private GameObject dungeonDoor;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject goldOre;
    [SerializeField] private Tile floorTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private int maxAgent;
    [SerializeField] private int minAgnet;
    [SerializeField] private float gridFillPercentage;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private int maxOreCount;

    private MapGrid[,] mapGrid;
    private int floorTileCount;
    private List<Agent> agentList;
    private List<Vector3Int> floorTileList;
    private List<Vector3Int> wallTileList;
    private Vector3 playerWorldPosition;
    private int enemyCount = 0;
    private int oreCount = 0;

    private void Start()
    {
        Initializer();
    }
    private void Initializer()
    {
        agentList = new List<Agent>();
        floorTileList = new List<Vector3Int>();
        wallTileList = new List<Vector3Int>();

        mapGrid = new MapGrid[(int)mapSize.x, (int)mapSize.y];

        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                mapGrid[i, j] = MapGrid.Empty;
            }
        }
        FloorGenerator();
    }
    private void FloorGenerator()
    {
        while ((float)floorTileCount / mapGrid.Length < gridFillPercentage)
        {

            HandleAgents();

            foreach (Agent currentAgent in agentList)
            {
                Vector3Int currentPosition = new Vector3Int((int)currentAgent.position.x, (int)currentAgent.position.y, 0);
                if (mapGrid[currentPosition.x, currentPosition.y] != MapGrid.Floor)
                {
                    floorTileMap.SetTile(currentPosition, floorTile);
                    floorTileCount++;
                    mapGrid[currentPosition.x, currentPosition.y] = MapGrid.Floor;
                    floorTileList.Add(currentPosition);
                }
            }
        }
        CreateWalls();
    }
    private void HandleAgents()
    {
        int chance = 60;

        if (agentList.Count == 0)
        {
            Vector3Int mapCenter = new Vector3Int((int)mapSize.x, (int)mapSize.y, 0);
            Agent agent = new Agent(mapCenter, NewDirection());

            agentList.Add(agent);
        }

        HandlePosition();
        HandleDirection();
        HandleDeleting();
        HandleGeneration();

        void HandleGeneration()
        {
            int walkersCount = agentList.Count;
            for (int i = 0; i < walkersCount; i++)
            {
                if (UnityEngine.Random.value > chance && agentList.Count < maxAgent)
                {
                    Vector3 currentWalkerPosition = agentList[i].position;

                    Agent newAgent = new Agent(currentWalkerPosition, NewDirection());
                    agentList.Add(newAgent);
                }
            }
        }

        void HandleDeleting()
        {
            int walkerCount = agentList.Count;

            for (int i = 0; i < walkerCount; i++)
            {
                if (UnityEngine.Random.value > chance && agentList.Count > 1)
                {
                    agentList.RemoveAt(i);
                    break;
                }
            }

        }

        void HandleDirection()
        {
            foreach (Agent currentAgent in agentList)
            {
                if (UnityEngine.Random.Range(0, 100) > chance)
                {
                    currentAgent.direction = NewDirection();
                }
            }
        }

        void HandlePosition()
        {
            foreach (Agent currentAgent in agentList)
            {
            currentAgent.position += currentAgent.direction;
            currentAgent.position.x = Mathf.Clamp(currentAgent.position.x, 1, mapGrid.GetLength(0) - 3);
            currentAgent.position.y = Mathf.Clamp(currentAgent.position.y, 1, mapGrid.GetLength(1) - 3);
            }
        }

        Vector3 NewDirection()
        {
            int randomNumber = UnityEngine.Random.Range(0, 4);

            switch (randomNumber)
            {
                case 0: return Vector3.up;

                case 1: return Vector3.right;

                case 2: return Vector3.down;

                case 3: return Vector3.left;

                default: return Vector3.zero;
            }
        }
    }
    private void CreateWalls()
    {
        foreach (Vector3Int currentFloor in floorTileList)
        {
            if (mapGrid[currentFloor.x + 1, currentFloor.y] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x + 1, currentFloor.y, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
                wallTileList.Add(wallTileVector);
            }
            if (mapGrid[currentFloor.x, currentFloor.y + 1] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x, currentFloor.y + 1, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
                wallTileList.Add(wallTileVector);   
            }
            if (mapGrid[currentFloor.x - 1, currentFloor.y] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x - 1, currentFloor.y, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
                wallTileList.Add(wallTileVector);
            }
            if (mapGrid[currentFloor.x, currentFloor.y - 1] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x, currentFloor.y - 1, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
                wallTileList.Add(wallTileVector);
            }
        }
        CreateDungeonDoors();
    }
    private void CreateDungeonDoors()
    {
        Vector3Int brokenDoorPosition = Utils.GetAndRemoveRandomInList(wallTileList);
        Vector3Int dungeonDoorPosition = Utils.GetAndRemoveRandomInList(wallTileList);

        Vector3 brokenDoorWorldPosition = GetWorldPosition(brokenDoorPosition);
        Vector3 dungeonDoorWorldPosition = GetWorldPosition(dungeonDoorPosition);

        Instantiate(brokenDoor, brokenDoorWorldPosition, Quaternion.identity);
        Instantiate(dungeonDoor, dungeonDoorWorldPosition, Quaternion.identity);

        SetPlayerSpawn(Vector3Int.FloorToInt(brokenDoorWorldPosition));
        CreateEnemies();
        CreateOres();
    }
    private void SetPlayerSpawn(Vector3Int brokenDoorWorldPosition)
    {
        if (mapGrid[brokenDoorWorldPosition.x + 1, brokenDoorWorldPosition.y] == MapGrid.Floor)
        {         
            Vector3Int pos = new Vector3Int(brokenDoorWorldPosition.x + 1, brokenDoorWorldPosition.y);            
            this.playerWorldPosition = GetWorldPosition(pos);
        }
        else if (mapGrid[brokenDoorWorldPosition.x -1, brokenDoorWorldPosition.y] == MapGrid.Floor)
        {           
            Vector3Int pos = new Vector3Int(brokenDoorWorldPosition.x - 1, brokenDoorWorldPosition.y);
            this.playerWorldPosition = GetWorldPosition(pos);

        }
        else if (mapGrid[brokenDoorWorldPosition.x, brokenDoorWorldPosition.y +1] == MapGrid.Floor)
        {
            Vector3Int pos = new Vector3Int(brokenDoorWorldPosition.x, brokenDoorWorldPosition.y + 1);
            this.playerWorldPosition = GetWorldPosition(pos);

        }
        else if (mapGrid[brokenDoorWorldPosition.x, brokenDoorWorldPosition.y -1] == MapGrid.Floor)
        {
            Vector3Int pos = new Vector3Int(brokenDoorWorldPosition.x, brokenDoorWorldPosition.y - 1);
            this.playerWorldPosition = GetWorldPosition(pos);
        }
        else
        {
            Debug.Log("Dungeon Generator PlayeSpawn: Else executed!");
        }
        OnBrokenDoorGenerated?.Invoke(this, EventArgs.Empty);
    }
    private void CreateEnemies()
    {
        while (enemyCount < maxEnemyCount)
        {
            CreateObject(enemy);
            enemyCount++;
        }
    }
    private void CreateOres()
    {
        while (oreCount < maxOreCount)
        {
            CreateObject(goldOre);
            oreCount++;
        }
    }
    private void CreateObject(GameObject gameObject)
    {
        Vector3Int objectPosition = Utils.GetAndRemoveRandomInList(floorTileList);
        Vector3 objectWorldPosition = GetWorldPosition(objectPosition);

        Instantiate(gameObject, objectWorldPosition, Quaternion.identity);
    }
    private Vector3 GetWorldPosition(Vector3Int position)
    {
        return Vector3Int.FloorToInt(position) + wallTileMap.cellSize / 2;
    }
}