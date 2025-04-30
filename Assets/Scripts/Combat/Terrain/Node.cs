using UnityEngine;

public class Node
{
    public Vector2Int coords;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coords, bool isWalkable)
    {
        this.coords = coords;
        this.isWalkable = isWalkable;
    }
}
