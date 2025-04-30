using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCell : MonoBehaviour, IComparable<CharacterCell>
{
    private ICharacter character;

    private Image background;
    private TextMeshProUGUI info;
    private TextMeshProUGUI initiative;

    private static readonly Dictionary<Type, Color> backgroundColors = new Dictionary<Type, Color>
    {
        { typeof(Player), Color.cyan },
        { typeof(Enemy), Color.red }
    };

    public void Initialize(ICharacter character)
    {
        this.character = character;
        UpdateCell();
    }

    public void UpdateCell()
    {
        background.color = backgroundColors[character.GetType()];
        info.text = $"ID:\n{character.ID}";
        initiative.text = character.Initiative.ToString();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        background = transform.GetChild(0).GetComponent<Image>();
        info = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        initiative = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public int CompareTo(CharacterCell other)
    {
        return character.CompareTo(other.character);
    }
}
