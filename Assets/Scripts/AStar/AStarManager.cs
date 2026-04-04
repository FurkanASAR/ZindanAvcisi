using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public static AStarManager instance {  get; private set; }
    public List<Node> nodesList;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        nodesList = new List<Node>();
    }

    private void Start()
    {
        nodesList = dungeonGenerator.nodesList;
    }

    public List<Node> GeneratePath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();
        foreach(Node node in dungeonGenerator.nodesList)
        {
            node.gCost = float.MaxValue;
        }

        Debug.Log("first debug!");

        Debug.Log("start.gridx " + start.gridX);
        Debug.Log("start.gridy " + start.gridY);

        Vector2 startVector = new Vector2(start.gridX, start.gridY);
        Vector2 endVector = new Vector2(end.gridX, end.gridY);

        Debug.Log("Second debug");

        start.gCost = 0f;
        start.hCost = Vector2.Distance(startVector, endVector);
        openList.Add(start);

        while (openList.Count > 0)
        {
            int lowestF = 0;

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < openList[lowestF].FCost)
                {
                    lowestF = i;
                }
            }
            Node currentNode = openList[lowestF];
            openList.Remove(currentNode);

            if(currentNode == end)
            {
                List<Node> path = new List<Node>();
                path.Insert(0, end);

                while (currentNode != start)
                {
                    currentNode = currentNode.cameFrom;
                    path.Add(currentNode);
                }
                path.Reverse();
                return path;
            }

            foreach (Node connectedNode in currentNode.connections)
            {
                Vector2 currentVector = new Vector2(currentNode.gridX, currentNode.gridY);
                Vector2 connectedVector = new Vector2(connectedNode.gridX, connectedNode.gridY);

                float heldGCost = currentNode.gCost + Vector2.Distance(currentVector, connectedVector);

                if(heldGCost < connectedNode.gCost)
                {
                    connectedNode.cameFrom = currentNode;
                    connectedNode.gCost = heldGCost;
                    connectedNode.hCost = Vector2.Distance(connectedVector, endVector);

                    if (!openList.Contains(connectedNode))
                    {
                        openList.Add(connectedNode);
                    }
                }
            }
        }

        return null;
    }

}
