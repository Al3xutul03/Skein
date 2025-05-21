using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    private IAbility ability;
    public IAbility Ability { get { return ability; } }

    private AbilityDescription description;
    private TextMeshProUGUI abilityName;

    public void Initialize(IAbility ability, AbilityDescription description)
    {
        abilityName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        this.description = description;

        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => description.SetAbility(ability));

        this.ability = ability;
        abilityName.text = Ability.Name;
    }
}
