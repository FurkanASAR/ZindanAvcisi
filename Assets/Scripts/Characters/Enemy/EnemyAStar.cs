using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Controller : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public int panicMultiplier = 1;


    public Node currentNode;
    public List<Node> path = new List<Node>();

    public enum StateMachine
    {
        Patrol,
        Engage,
        Evade
    }

    public StateMachine currentState;

    public Player player;

    public float speed = 3f;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            /*case StateMachine.Engage:
                ..Engage();
                break;
            case StateMachine.Evade:
                Evade();
                break;*/
        }

        bool playerSeen = Vector2.Distance(transform.position, player.transform.position) < 5.0f;

        if (!playerSeen && currentState != StateMachine.Patrol && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Patrol;
            path.Clear();
        }
        else if (playerSeen && currentState != StateMachine.Engage && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Engage;
            path.Clear();
        }
        else if (currentState != StateMachine.Evade && curHealth <= (maxHealth * 20) / 100)
        {
            panicMultiplier = 2;
            currentState = StateMachine.Evade;
            path.Clear();
        }

        CreatePath();
    }

    void Patrol()
    {
        if (path.Count == 0)
        {
            Debug.Log(currentNode);            
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.nodesList[Random.Range(0, AStarManager.instance.nodesList.Count)]);
        }
    }

   /* void Engage()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
    }

    void Evade()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(player.transform.position));
        }
    }*/

    public void CreatePath()
    {
        if (path.Count > 0)
        {
            int x = 0;
            Vector3 currentPostion = new Vector3(path[x].gridX, path[x].gridY, -2);
            transform.position = Vector3.MoveTowards(transform.position, currentPostion, (speed * panicMultiplier) * Time.deltaTime);

            if (Vector2.Distance(transform.position, new Vector2(path[x].gridX, path[x].gridY)) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);
            }
        }
    }
}
