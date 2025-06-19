using System.Collections.Generic;
using UnityEngine;

public class BaseDefenseComponent : IDefenseComponent
{
    private Dictionary<DefenseType, int> defenses;

    public int ArmorClass { get { return defenses[DefenseType.ArmorClass]; } }
    public int Fortitude { get { return defenses[DefenseType.Fortitude]; } }
    public int Reflex { get { return defenses[DefenseType.Reflex]; } }
    public int Will { get { return defenses[DefenseType.Will]; } }

    private int noCoins;
    private int coinModifier;

    public BaseDefenseComponent(int armorClass, int fortitude, int reflex, int will, int noCoins, int coinModifier)
    {
        this.defenses = new Dictionary<DefenseType, int>
        {
            { DefenseType.ArmorClass, armorClass },
            { DefenseType.Fortitude, fortitude },
            { DefenseType.Reflex, reflex },
            { DefenseType.Will, will}
        };

        this.noCoins = noCoins;
        this.coinModifier = coinModifier;
    }

    public SuccessLevel IsHit(DefenseType defenseType, int attackModifier, List<Modifier> defenseModifiers)
    {
        int baseModifier = defenses[defenseType];

        List<int> baseBonuses = new List<int>();
        List<int> coinBonuses = new List<int>();
        List<bool> coinResults;

        int finalCoinNumber = noCoins;
        foreach (Modifier modifier in defenseModifiers)
        {
            if (modifier.HasTag(ModifierTag.Base)) baseBonuses.Add(modifier.AddModifier(0));
            if (modifier.HasTag(ModifierTag.Coin)) coinBonuses.Add(modifier.AddModifier(0));
            if (modifier.HasTag(ModifierTag.CoinNumber)) finalCoinNumber += modifier.Value;
        }

        Roller roller = new Roller(baseModifier, baseBonuses, finalCoinNumber, coinModifier, coinBonuses);
        int finalDefenseModifier = roller.Roll(out coinResults);

        int finalDifference = attackModifier - finalDefenseModifier;

        int finalBaseBonus = 0, finalCoinBonus = 0;
        foreach (int bonus in baseBonuses) { finalBaseBonus += bonus; }
        foreach (int bonus in coinBonuses) { finalCoinBonus += bonus; }

        var attackInfo = GameObject.FindWithTag("AttackInfo").GetComponent<AttackInfo>();
        attackInfo.SetDefender(coinResults, finalCoinNumber, baseModifier, noCoins, finalBaseBonus, finalCoinBonus, finalCoinNumber - noCoins, finalDefenseModifier);

        switch (finalDifference)
        {
            case >= 5: attackInfo.SetResult(SuccessLevel.CriticalSuccess); return SuccessLevel.CriticalSuccess;
            case >= 0: attackInfo.SetResult(SuccessLevel.Success); return SuccessLevel.Success;
            case >= -5: attackInfo.SetResult(SuccessLevel.Failure); return SuccessLevel.Failure;
            case < -5: attackInfo.SetResult(SuccessLevel.CriticalFailure); return SuccessLevel.CriticalFailure;
        }
    }
}
