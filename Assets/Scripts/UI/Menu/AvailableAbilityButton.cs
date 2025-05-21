using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvailableAbilityButton : MonoBehaviour
{
    private IAbility ability;
    public IAbility Ability { get { return ability; } }

    private Button button;
    private CharacterPannel characterPannel;

    public void Initialize(CharacterPannel characterPannel, IAbility ability)
    {
        button = GetComponent<Button>();

        this.characterPannel = characterPannel;
        this.ability = ability;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => characterPannel.ChangeSelectedAbilityButton(this));

        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ability.Name;
    }
}
