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
    private AbilityManager abilityManager;
    private List<ICharacter> characters = new List<ICharacter>();
    private ICharacter currentCharacter;
    private int currentCharacterIndex = 0;

    public void ChangeTurn()
    {
        currentCharacter.EndTurn();
        do
        {
            currentCharacterIndex++;
            if (currentCharacterIndex == characters.Count)
            {
                currentCharacterIndex = 0;
            }
            if (!characters[currentCharacterIndex].IsAlive()) characters[currentCharacterIndex].DeleteSelf();
        } while (!characters[currentCharacterIndex].IsAlive());

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
        abilityManager = playerUI.transform.GetChild(4).GetComponentInChildren<AbilityManager>();

        var builders = gameObject.GetComponentsInChildren<CharacterBuilder>();
        foreach (var builder in builders) builder.Build();

        characters = gameObject.GetComponentsInChildren<ICharacter>().ToList();
        characters.Sort((c1, c2) => { return c2.CompareTo(c1); });
        Debug.Log($"Registered {characters.Count} characters");

        initiativeTracker.Initialize(characters);
        List<Player> players = new List<Player>();
        foreach (ICharacter character in characters)
        {
            if (character is Player) players.Add((Player)character);
        }
        abilityManager.Initialize(players);

        currentCharacter = characters[0];
        turnArrow.ChangeCharacter(currentCharacter.GameObject);
        currentCharacter.StartTurn();
        if (currentCharacter is Player) playerUI.ChangeDisplayedPlayer(currentCharacter as Player);
    }
}
