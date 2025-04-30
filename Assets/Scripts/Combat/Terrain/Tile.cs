using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private bool blocked;
    public Vector2Int coords;

    GridManager gridManager;

    private void Start()
    {
        SetCoords();
        if (blocked) gridManager.GetNode(coords).isWalkable = false;
    }

    private void SetCoords()
    {
        gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();

        coords = new Vector2Int(
            (int)transform.position.x / gridManager.UnityGridSize,
            (int)transform.position.z / gridManager.UnityGridSize);
    }
}
