using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private ProgressBar hpBar;
    private TempHPLabel tempHPLabel;
    private ActionDisplay actionDisplay;

    private Player displayedPlayer;

    private Dictionary<UIUpdateType, Action> updateDictionary;

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

        updateDictionary = new Dictionary<UIUpdateType, Action>
        {
            { UIUpdateType.Health, UpdateHealth },
            { UIUpdateType.Actions, UpdateActions },
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
}

public enum UIUpdateType
{
    Health, Actions 
}
