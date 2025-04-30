using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InitiativeTracker : MonoBehaviour
{
    private List<CharacterCell> characterCells = new List<CharacterCell>();
    private List<GameObject> characterCellObjects = new List<GameObject>();
    private float cellWidth = 0;

    private GameObject initiativeList;
    private GameObject turnMarker;

    public void Initialize(IEnumerable<ICharacter> characters)
    {
        GameObject characterCellPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/InitiativeTracker/CharacterCell");

        foreach (ICharacter character in characters)
        {
            GameObject newPrefab = Instantiate(characterCellPrefab, initiativeList.transform);
            RectTransform rectTransform = newPrefab.GetComponent<RectTransform>();
            cellWidth = rectTransform.rect.width;

            float newPosition = cellWidth * (characterCells.Count - characters.Count() / 2.0f);
            newPosition += (characters.Count() % 2 == 1) ? cellWidth / 2 : 0;
            rectTransform.anchoredPosition = new Vector2(newPosition, 0);

            CharacterCell newCell = newPrefab.GetComponent<CharacterCell>();
            newCell.Initialize(character);
            characterCells.Add(newCell);
            characterCellObjects.Add(newPrefab);
        }
        
        MoveMarker(0);
    }

    public void ChangeTurn(int turn)
    {
        turn %= characterCells.Count;
        MoveMarker(turn);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initiativeList = transform.GetChild(0).gameObject;
        turnMarker = transform.GetChild(1).gameObject;
    }

    private void MoveMarker(int index)
    {
        float newPosition = characterCellObjects[index].GetComponent<RectTransform>().anchoredPosition.x;
        RectTransform rectTransform = turnMarker.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(newPosition, rectTransform.anchoredPosition.y);
    }
}
