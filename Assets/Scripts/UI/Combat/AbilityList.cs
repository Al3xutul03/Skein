using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityList : MonoBehaviour
{
    private Player player;
    private List<AbilityButton> abilityButtons = new List<AbilityButton>();

    private Transform content;

    public void Initialize(Player player, AbilityDescription description)
    {
        this.player = player;
        content = transform.GetChild(0);

        GameObject abilityButtonPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/AbilityButton");
        PlayerInput playerInput = GameObject.FindWithTag("PlayerInput").GetComponent<PlayerInput>();

        if (player.Abilities.Count == 0) return;
        foreach (IAbility ability in player.Abilities)
        {
            GameObject newPrefab = Instantiate(abilityButtonPrefab, content);

            Button newButton = newPrefab.GetComponent<Button>();
            newButton.interactable = true;
            newButton.onClick.RemoveAllListeners();
            newButton.onClick.AddListener(() => playerInput.ChangeSelectedAbility(newButton));
            
            AbilityButton newAbilityButton = newPrefab.GetComponent<AbilityButton>();
            newAbilityButton.Initialize(ability, description);
            abilityButtons.Add(newAbilityButton);
        }
    }
}
