using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TeamList : MonoBehaviour
{
    private TurnManager turnManager;
    private Dictionary<Player, TeamListCell> teamListDictionary;

    public void UpdatePlayer(Player player) { teamListDictionary[player].UpdateCell(); }

    public void Initialize(IEnumerable<Player> players)
    {
        GameObject teamListCellPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/TeamList/TeamListCell");
        teamListDictionary = new Dictionary<Player, TeamListCell>();

        foreach ( Player player in players )
        {
            GameObject newPrefab = Instantiate(teamListCellPrefab, this.transform);
            RectTransform rectTransform = newPrefab.GetComponent<RectTransform>();
            float cellWidth = rectTransform.rect.width;

            float newPosition = cellWidth * teamListDictionary.Count;
            rectTransform.anchoredPosition = new Vector2(newPosition, 0);

            TeamListCell newCell = newPrefab.GetComponent<TeamListCell>();
            newCell.Initialize(player);

            teamListDictionary[player] = newCell;
        }
    }
}
