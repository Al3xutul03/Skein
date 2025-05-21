using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private ProgressBar hpBar;
    private TempHPLabel tempHPLabel;
    private ActionDisplay actionDisplay;
    private TeamList teamList;
    private AbilityManager abilityManager;

    private Player displayedPlayer;

    private Dictionary<UIUpdateType, Action> updateDictionary;

    public void Initialize(IEnumerable<Player> players)
    {
        UpdateTeamList(players);
    }

    public void ChangeDisplayedPlayer(Player player)
    {
        displayedPlayer = player;
        UpdateAllPlayerUI();
    }

    public void UpdateAllPlayerUI()
    {
        hpBar.ToggleText(true);
        foreach(Action update in updateDictionary.Values) { update(); }
    }

    public void UpdatePlayerUIElement(UIUpdateType uiUpdateType) { updateDictionary[uiUpdateType](); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar = GetComponentInChildren<ProgressBar>();
        tempHPLabel = GetComponentInChildren<TempHPLabel>();
        actionDisplay = GetComponentInChildren<ActionDisplay>();
        abilityManager = transform.GetChild(4).GetComponentInChildren<AbilityManager>();

        updateDictionary = new Dictionary<UIUpdateType, Action>
        {
            { UIUpdateType.Health, UpdateHealth },
            { UIUpdateType.Actions, UpdateActions },
            { UIUpdateType.Abilities, UpdateAbilities }
        };
    }

    private void UpdateHealth()
    {
        hpBar.MaxValue = displayedPlayer.MaxHP;
        hpBar.CurrentValue = displayedPlayer.CurrentHP;
        tempHPLabel.TempHP = displayedPlayer.TempHP;
    }

    private void UpdateActions()
    {
        actionDisplay.NoActions = displayedPlayer.NoActions;
    }

    private void UpdateTeamList(IEnumerable<Player> players)
    {
        teamList = transform.GetChild(3).GetComponent<TeamList>();
        teamList.Initialize(players);
    }

    private void UpdateAbilities()
    {
        abilityManager.ChangeList(displayedPlayer);
    }
}

public enum UIUpdateType
{
    Health, Actions, Abilities
}
