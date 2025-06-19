using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackInfo : MonoBehaviour
{
    private ScoreInfo attacker;
    private ScoreInfo defender;

    private TextMeshProUGUI resultText;
    private TextMeshProUGUI totalDamageText;

    private GameObject coinPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacker = transform.GetChild(0).GetComponent<ScoreInfo>();
        defender = transform.GetChild(1).GetComponent<ScoreInfo>();
        resultText = transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        totalDamageText = transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
        coinPrefab = Resources.Load<GameObject>("Prefabs/UI/Combat/Coin");
    }

    public void SetAttacker(List<bool> coinResults, int noCoins, int baseScore, int coinScore, int baseModifier, int coinModifier, int coinNumberModifier, int totalScore)
    {
        attacker.SetText(coinPrefab, coinResults, noCoins, baseScore, coinScore, baseModifier, coinModifier, coinNumberModifier, totalScore);
    }

    public void SetDefender(List<bool> coinResults, int noCoins, int baseScore, int coinScore, int baseModifier, int coinModifier, int coinNumberModifier, int totalScore)
    {
        defender.SetText(coinPrefab, coinResults, noCoins, baseScore, coinScore, baseModifier, coinModifier, coinNumberModifier, totalScore);
    }

    public void SetResult(SuccessLevel result) { resultText.text = $"Result:\n{result}"; }

    public void SetDamage(int totalDamage) { totalDamageText.text = $"Total Damage:\n{totalDamage}"; }

    public void Show(bool value)
    {
        attacker.transform.GetChild(0).gameObject.SetActive(value);
        defender.transform.GetChild(0).gameObject.SetActive(value);
        transform.GetChild(2).gameObject.SetActive(value);
    }
}
