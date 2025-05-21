using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private List<Image> valueTokens = new List<Image>();
    private int value;
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            SetColors();
        }
    }

    private static readonly Dictionary<int, bool[]> valueDictionary = new Dictionary<int, bool[]>
    {
        { 0, new bool[]{ false, false, false, false } },
        { 1, new bool[]{ true, false, false, false } },
        { 2, new bool[]{ true, true, false, false } },
        { 3, new bool[]{ true, true, true , false } },
        { 4, new bool[]{ true, true, true , true } }
    };

    private static readonly Dictionary<bool, Color> tokenColor = new Dictionary<bool, Color>
    {
        { false, Color.black },
        { true, Color.white }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in transform)
        {
            Image currentToken = child.GetChild(1).GetComponent<Image>();
            valueTokens.Add(currentToken);
        }
        value = 0;

        SetColors();
    }

    private void SetColors()
    {
        int index = 0;
        foreach (Image token in valueTokens)
        {
            token.color = tokenColor[valueDictionary[value][index]];
            index++;
        }
    }
}
