using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPannel : MonoBehaviour
{
    private Character character;

    private StatView statView;
    private GameObject selectedAbilities;
    public GameObject SelectedAbilities { get { return selectedAbilities; } }
    private GameObject availableAbilities;
    private TextMeshProUGUI HP;
    private TextMeshProUGUI classText;

    private SelectedAbilityButton selectedAbilityButton;

    public void SetSelectedAbilityButton(SelectedAbilityButton selectedAbilityButton)
    {
        this.selectedAbilityButton = selectedAbilityButton;
    }

    public void ChangeSelectedAbilityButton(AvailableAbilityButton availableAbilityButton)
    {
        this.selectedAbilityButton.Ability = availableAbilityButton.Ability;
        this.selectedAbilityButton.transform.GetComponent<Button>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selectedAbilityButton.Ability.Name;
    }

    public void Initialize(IEnumerable<IAbility> abilities, Character character, string name)
    {
        statView = transform.GetChild(0).GetComponent<StatView>();
        selectedAbilities = transform.GetChild(1).GetChild(0).gameObject;
        availableAbilities = transform.GetChild(2).GetChild(0).gameObject;
        HP = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        classText = transform.GetChild(6).GetComponent<TextMeshProUGUI>();

        this.character = character;

        GameObject selectedAbility = Resources.Load<GameObject>("Prefabs/UI/Menu/SelectedAbilityButton");
        GameObject availableAbility = Resources.Load<GameObject>("Prefabs/UI/Menu/AvailableAbilityButton");

        GameObject currentAbility;
        SelectedAbilityButton selectedButton;

        for (int i = 0; i < 2; i++)
        {
            currentAbility = Instantiate(selectedAbility, selectedAbilities.transform);
            selectedButton = currentAbility.GetComponent<SelectedAbilityButton>();
            selectedButton.Initialize(this, new Ability(null, "Empty", 0, "", AbilityType.Basic, 0, 0));
        }

        AvailableAbilityButton availableButton;
        foreach (var ability in abilities)
        {
            currentAbility = Instantiate(availableAbility, availableAbilities.transform);
            availableButton = currentAbility.GetComponent<AvailableAbilityButton>();
            availableButton.Initialize(this, ability);
        }

        statView.SetStat(StatViewType.Meele, character.Meele);
        statView.SetStat(StatViewType.Ranged, character.Ranged);
        statView.SetStat(StatViewType.Casting, character.Casting);
        statView.SetStat(StatViewType.Armor, character.ArmorClass);
        statView.SetStat(StatViewType.Fortitude, character.Fortitude);
        statView.SetStat(StatViewType.Reflex, character.Reflex);
        statView.SetStat(StatViewType.Will, character.Will);

        HP.text = $"Max Health: {character.MaxHP}";
        classText.text = name;
    }
}
