using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamListCell : MonoBehaviour
{
    private Player player;

    private Image background;
    private TextMeshProUGUI info;
    private TextMeshProUGUI hp;

    public void Initialize(Player player)
    {
        background = transform.GetChild(0).GetComponent<Image>();
        info = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        hp = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        background.color = Color.cyan;

        this.player = player;
        UpdateCell();
    }

    public void UpdateCell()
    {
        info.text = $"ID:\n{player.ID}";
        hp.text = player.CurrentHP.ToString() + "/" + player.MaxHP.ToString();
    }
}
