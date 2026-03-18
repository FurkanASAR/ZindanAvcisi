using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Tile floorTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tilemap floorTileMap;
    [SerializeField] private Tilemap wallTileMap;
    [SerializeField] private Vector2 mapSize;
    [SerializeField] private int maxAgent;
    [SerializeField] private int minAgnet;
    [SerializeField] private float gridFillPercentage;

    public enum MapGrid
    {
        Empty,
        Floor,
        Wall
    }
    private MapGrid[,] mapGrid;
    private int floorTileCount;

    private List<Agent> agentList;
    private List<Vector3Int> floorTileList;
    private void Start()
    {
        Initializer();
    }
    private void Initializer()
    {
        agentList = new List<Agent>();
        floorTileList = new List<Vector3Int>();

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
            currentAgent.position.x = Mathf.Clamp(currentAgent.position.x, 1, mapGrid.GetLength(0) - 2);
            currentAgent.position.y = Mathf.Clamp(currentAgent.position.y, 1, mapGrid.GetLength(1) - 2);
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
            }
            if (mapGrid[currentFloor.x, currentFloor.y + 1] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x, currentFloor.y + 1, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
            }
            if (mapGrid[currentFloor.x - 1, currentFloor.y] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x - 1, currentFloor.y, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
            }
            if (mapGrid[currentFloor.x, currentFloor.y - 1] == MapGrid.Empty)
            {
                Vector3Int wallTileVector = new Vector3Int(currentFloor.x, currentFloor.y - 1, 0);
                wallTileMap.SetTile(wallTileVector, wallTile);
            }
        }
    }
}