using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Vector2Int gridSize;
    [SerializeField]
    private int unityGridSize;

    public int UnityGridSize { get { return unityGridSize; } }

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); 
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private Dictionary<Vector2Int, Player> playerCoords = new Dictionary<Vector2Int, Player>();
    public Dictionary<Vector2Int, Player> PlayerCoords { get { return playerCoords; } }

    private Dictionary<Vector2Int, Enemy> enemyCoords = new Dictionary<Vector2Int, Enemy>();
    public Dictionary<Vector2Int, Enemy> EnemyCoords { get { return enemyCoords; } }

    private TurnManager turnManager;

    private void Awake()
    {
        CreateGrid();
    }

    private void Start()
    {
        turnManager = GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        foreach (Transform transform in turnManager.transform)
        {
            ICharacter character = transform.GetComponent<ICharacter>();
            Vector2Int coords = GetCoordinatesFromPosition(transform.position);

            if (character is Player)
            {
                playerCoords.Add(coords, (Player)character);
                GetNode(coords).isWalkable = false;
            }
            else
            {
                enemyCoords.Add(coords, (Enemy)character);
                GetNode(coords).isWalkable = false;
            }
        }
    }

    public Node GetNode(Vector2Int position)
    {
        if (grid.ContainsKey(position)) return grid[position];
        return null;
    }

    public void BlockNode(Vector2Int position)
    {
        if (grid.ContainsKey(position)) grid[position].isWalkable = false;
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * unityGridSize;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                grid.Add(coords, new Node(coords, true));
            }
        }
    }
}
