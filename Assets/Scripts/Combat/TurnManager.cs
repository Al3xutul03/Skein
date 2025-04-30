using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private TurnArrow turnArrow;
    private InitiativeTracker initiativeTracker;
    private PlayerUI playerUI;
    private List<ICharacter> characters = new List<ICharacter>();
    private ICharacter currentCharacter;
    private int currentCharacterIndex = 0;

    public void ChangeTurn()
    {
        currentCharacter.EndTurn();
        currentCharacterIndex++;
        if (currentCharacterIndex == characters.Count)
        {
            currentCharacterIndex = 0;
        }

        currentCharacter = characters[currentCharacterIndex];
        turnArrow.ChangeCharacter(currentCharacter.GameObject);
        currentCharacter.StartTurn();
        initiativeTracker.ChangeTurn(currentCharacterIndex);
        if (IsPlayersTurn()) playerUI.ChangeDisplayedPlayer(currentCharacter as Player);
    }

    public bool IsPlayersTurn()
    {
        return currentCharacter.IsControllable();
    }

    public bool IsCurrentCharacter(GameObject character)
    {
        return currentCharacter.GameObject == character;
    }

    public void ChangeCurrentPlayerActions(int value) { currentCharacter.NoActions += value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnArrow = GameObject.FindWithTag("TurnArrow").GetComponent<TurnArrow>();
        initiativeTracker = GameObject.FindWithTag("InitiativeTracker").GetComponent<InitiativeTracker>();
        playerUI = GameObject.FindWithTag("PlayerUI").GetComponent<PlayerUI>();
        
        characters = gameObject.GetComponentsInChildren<ICharacter>().ToList();
        characters.Sort((c1, c2) => { return c2.CompareTo(c1); });

        initiativeTracker.Initialize(characters);

        currentCharacter = characters.First();
        turnArrow.ChangeCharacter(currentCharacter.GameObject);
        currentCharacter.StartTurn();
        if (IsPlayersTurn()) playerUI.ChangeDisplayedPlayer(currentCharacter as Player);
    }
}
