using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDisplay : MonoBehaviour
{
    private List<Image> actionTokens = new List<Image>();
    private int noActions;
    public  int NoActions
    {
        get { return noActions; }
        set
        {
            noActions = value;
            SetColors();
        }
    }

    private static readonly Dictionary<int, bool[]> actionDictionary = new Dictionary<int, bool[]>
    {
        { 0, new bool[]{ false, false, false } },
        { 1, new bool[]{ true, false, false } },
        { 2, new bool[]{ true, true, false } },
        { 3, new bool[]{ true, true, true } }
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
            actionTokens.Add(currentToken);
        }
        noActions = 3;

        SetColors();
    }

    private void SetColors()
    {
        int index = 0;
        foreach (Image token in actionTokens)
        {
            token.color = tokenColor[actionDictionary[noActions][index]];
            index++;
        }
    }
}
