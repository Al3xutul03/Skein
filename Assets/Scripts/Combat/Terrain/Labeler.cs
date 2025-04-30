using TMPro;
using UnityEngine;

public class Labeler : MonoBehaviour
{
    public TextMeshPro label;
    public Vector2Int coords = new Vector2Int();
    public GridManager gridManager;

    private void Awake()
    {
        gridManager = GameObject.FindWithTag("GridManager").GetComponent<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();
        DisplayCoords();
    }

    private void Update()
    {
        DisplayCoords();
        transform.name = coords.ToString();
    }

    private void DisplayCoords()
    {
        if (gridManager == null) { return; }

        coords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coords.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

        label.text = $"{coords.x}, {coords.y}";
    }
}
