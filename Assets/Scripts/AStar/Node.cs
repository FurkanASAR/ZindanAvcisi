using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
public class Node
{
    public Node cameFrom;
    public List<Node> connections = new List<Node>();

    public float gridX;
    public float gridY;

    public float gCost;
    public float hCost;

    public float FCost => gCost + hCost;
}
