using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffInfo : MonoBehaviour
{
    private int baseModifier;
    private int coinModifier;
    private int coinNumberModifier;

    private GameObject textPrefab;
    private List<TextMeshProUGUI> buffs = new List<TextMeshProUGUI>();

    private Transform buffTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/BuffText");
        buffTransform = transform.GetChild(0).GetChild(1);
    }

    public void SetText(IEnumerable<Modifier> modifiers)
    {
        foreach (var buff in buffs) { Destroy(buff.gameObject); }
        buffs.Clear();

        baseModifier = 0; coinModifier = 0; coinNumberModifier = 0;
        foreach (Modifier modifier in modifiers)
        {
            if (modifier.HasTag(ModifierTag.Base)) { baseModifier = modifier.AddModifier(baseModifier); }
            if (modifier.HasTag(ModifierTag.Coin)) { coinModifier = modifier.AddModifier(coinModifier); }
            if (modifier.HasTag(ModifierTag.CoinNumber)) { coinNumberModifier = modifier.AddModifier(coinNumberModifier); }
        }

        if (baseModifier != 0)
        {
            GameObject newPrefab = Instantiate(textPrefab, buffTransform);
            var newText = newPrefab.GetComponent<TextMeshProUGUI>();
            newText.text = $"Base: {baseModifier}";
            buffs.Add(newText);
        }
        if (coinModifier != 0)
        {
            GameObject newPrefab = Instantiate(textPrefab, buffTransform);
            var newText = newPrefab.GetComponent<TextMeshProUGUI>();
            newText.text = $"Coin: {coinModifier}";
            buffs.Add(newText);
        }
        if (coinNumberModifier != 0)
        {
            GameObject newPrefab = Instantiate(textPrefab, buffTransform);
            var newText = newPrefab.GetComponent<TextMeshProUGUI>();
            newText.text = $"Coin Number: {coinNumberModifier}";
            buffs.Add(newText);
        }
    }

    public void Show(bool value) { transform.GetChild(0).gameObject.SetActive(value); }
}
