using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Roller
{
    private static System.Random rand = new System.Random();

    private int baseModifier;
    private List<int> baseBonuses = new List<int>();

    private int noCoins;
    private int coinModifier;
    private List<int> coinBonuses = new List<int>();

    public Roller(int baseModifier, List<int> baseBonuses, int noCoins, int coinModifier, List<int> coinBonuses)
    {
        this.baseModifier = baseModifier;
        this.baseBonuses = baseBonuses;
        this.noCoins = noCoins;
        this.coinModifier = coinModifier;
        this.coinBonuses = coinBonuses;
    }

    public Roller(int baseModifier, int noCoins, int coinModifier)
    {
        this.baseModifier = baseModifier;
        this.noCoins = noCoins;
        this.coinModifier = coinModifier;
    }

    public int Roll(out List<bool> coinResults)
    {
        coinResults = new List<bool>();

        int finalBaseModifier = baseModifier;
        foreach(int baseBonus in baseBonuses)
        {
            finalBaseModifier += baseBonus;
        }

        int finalCoinModifier = coinModifier;
        foreach(int coinBonus in coinBonuses)
        {
            finalCoinModifier += coinBonus;
        }

        int finalResult = finalBaseModifier;
        for (int currentCoin = 0; currentCoin < noCoins; currentCoin++)
        {
            bool coinFlip = rand.Next(0, 2) == 1;
            if (coinFlip) finalResult += finalCoinModifier;
            coinResults.Add(coinFlip);
        }

        return finalResult;
    }
}
