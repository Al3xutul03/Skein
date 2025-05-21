using TMPro;
using UnityEngine;

public class AbilityDescription : MonoBehaviour
{
    private TextMeshProUGUI name;
    private TextMeshProUGUI level;
    private TextMeshProUGUI description;
    private TextMeshProUGUI actionCost;

    public void SetAbility(IAbility ability)
    {
        name.text = ability.Name;
        level.text = "Level: " + ability.Level.ToString();
        description.text = ability.Description;
        actionCost.text = "Action Cost: " + ability.ActionCost.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name        = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        level       = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        description = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        actionCost  = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }
}
