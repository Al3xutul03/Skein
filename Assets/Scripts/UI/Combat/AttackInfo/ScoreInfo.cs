using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreInfo : MonoBehaviour
{
    private int baseScore;
    private int coinScore;
    private int noCoins;
    private int totalScore;

    private int baseModifier;
    private int coinModifier;
    private int coinNumberModifier;

    private GameObject coinPrefab;
    private List<Coin> coins = new List<Coin>();
    private List<bool> coinResults = new List<bool>();

    private Transform coinResultTransform;
    private TextMeshProUGUI valueText;
    private TextMeshProUGUI baseModifierText;
    private TextMeshProUGUI coinModifierText;
    private TextMeshProUGUI coinNumberModifierText;
    private TextMeshProUGUI totalScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valueText = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        baseModifierText = transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>();
        coinModifierText = transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>();
        coinNumberModifierText = transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>();
        totalScoreText = transform.GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>();

        coinResultTransform = transform.GetChild(0).GetChild(2).GetChild(1);
    }

    public void SetText(GameObject coinPrefab, List<bool> coinResults, int noCoins, int baseScore, int coinScore,
        int baseModifier, int coinModifier, int coinNumberModifier, int totalScore)
    {
        this.coinPrefab = coinPrefab;
        this.coinResults = coinResults;
        this.noCoins = noCoins;
        this.baseScore = baseScore;
        this.coinScore = coinScore;
        this.baseModifier = baseModifier;
        this.coinModifier = coinModifier;
        this.coinNumberModifier = coinNumberModifier;
        this.totalScore = totalScore;

        foreach (var coin in coins) { Destroy(coin.gameObject); }
        coins.Clear();

        foreach (var coinResult in coinResults)
        {
            GameObject newPrefab = Instantiate(coinPrefab, coinResultTransform);
            Coin newCoin = newPrefab.GetComponent<Coin>();
            newCoin.Initialize();
            newCoin.SetCoin(coinResult);
            coins.Add(newCoin);
        }

        valueText.text = $"Base Score: {baseScore}\nCoin Score: {coinScore}";
        baseModifierText.text = $"Base: {baseModifier}";
        coinModifierText.text = $"Coin: {coinModifier}";
        coinNumberModifierText.text = $"Coin Number: {coinNumberModifier}";
        totalScoreText.text = $"Total Score: {totalScore}";
    }
}
