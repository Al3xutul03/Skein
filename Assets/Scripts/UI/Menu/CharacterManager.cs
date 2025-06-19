using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private CharacterBuilder characterBuilder;
    private List<Character> characterList;

    private Dictionary<Character, List<IAbility>> abilityMap = new Dictionary<Character, List<IAbility>>();
    private Dictionary<Button, CharacterPannel> pannelMap = new Dictionary<Button, CharacterPannel>();
    private List<string> names = new List<string> { "Fighter", "Rogue", "Cleric"};

    public void SelectPannel(Button button)
    {
        foreach (var p in pannelMap.Values)
        {
            p.gameObject.SetActive(false);
        }

        pannelMap[button].gameObject.SetActive(true);
    }

    public void SaveChanges()
    {
        int characterIndex = 0;
        foreach (var characterPannel in pannelMap.Values)
        {
            var selectedAbilities = characterPannel.SelectedAbilities;
            foreach (Transform abilityButton in selectedAbilities.transform)
            {
                IAbility ability = abilityButton.GetComponent<SelectedAbilityButton>().Ability;
                characterList[characterIndex].Abilities.Add(ability);
            }

            characterIndex++;
        }

        CharacterInfo.Instance.SetCharacters(characterList);
        SceneManager.LoadScene("PrototypeScene");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterBuilder = GetComponent<CharacterBuilder>();
        characterList = characterBuilder.BuildParty();

        var abilityFactory = new AbilityFactory();

        abilityMap.Add(characterList[0], abilityFactory.BuildFighterAbilities(characterList[0]));
        abilityMap.Add(characterList[1], abilityFactory.BuildRogueAbilities(characterList[1]));
        abilityMap.Add(characterList[2], abilityFactory.BuildClericAbilities(characterList[2]));

        for (var i = 0; i < 3; i++)
        {
            var currentPannel = transform.GetChild(i).GetComponent<CharacterPannel>();
            currentPannel.Initialize(abilityMap[characterList[i]], characterList[i], names[i]);

            pannelMap.Add(transform.parent.GetChild(2).GetChild(i).GetComponent<Button>(), currentPannel);
        }

        SelectPannel(transform.parent.GetChild(2).GetChild(0).GetComponent<Button>());
    }
}
