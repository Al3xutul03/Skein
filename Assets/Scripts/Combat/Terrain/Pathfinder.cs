using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private Vector2Int startCoords;
    public Vector2Int StartCoords { get { return startCoords; } }

    [SerializeField]
    private Vector2Int targetCoords;
    public Vector2Int TargetCoords { get { return targetCoords; } }

    private Node startNode;
    private Node targetNode;
    private Node currentNode;

    private Queue<Node> frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private static Vector2Int[] searchOrder =
    {
        Vector2Int.up,
        new Vector2Int(1, 1),
        Vector2Int.right,
        new Vector2Int(1, -1),
        Vector2Int.down,
        new Vector2Int(-1, -1),
        Vector2Int.left,
        new Vector2Int(-1, 1),
    };

    private void Awake()
    {
        gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        targetNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while (frontier.Count > 0 && isRunning == true)
        {
            currentNode = frontier.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbors();
            if (currentNode.coords == targetCoords)
            {
                isRunning = false;
                currentNode.isWalkable = false;
            }
        }
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighborCords = currentNode.coords + direction;

            if (grid.ContainsKey(neighborCords))
            {
                neighbors.Add(grid[neighborCords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentNode;
                reached.Add(neighbor.coords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    public void NotifyReceievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCoords = startCoordinates;
        targetCoords = targetCoordinates;
        startNode = grid[this.startCoords];
        targetNode = grid[this.targetCoords];
        GetNewPath();
    }
}
