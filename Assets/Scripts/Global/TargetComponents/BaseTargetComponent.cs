using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BaseTargetComponent : ITargetComponent
{
    private Dictionary<AttackType, int> attacks;

    public int Meele { get { return attacks[AttackType.Meele]; } }
    public int Ranged { get { return attacks[AttackType.Ranged]; } }
    public int Casting { get { return attacks[AttackType.Casting]; } }

    private int noCoins;
    private int coinModifier;

    public BaseTargetComponent(int meeleScore, int rangedScore, int castingScore, int noCoins, int coinModifier)
    {
        this.attacks = new Dictionary<AttackType, int>
        {
            { AttackType.Meele, meeleScore },
            { AttackType.Ranged, rangedScore },
            { AttackType.Casting, castingScore },
        };

        this.noCoins = noCoins;
        this.coinModifier = coinModifier;
    }

    public void Attack(IEnumerable<ICharacter> targets, AttackType attackType, DefenseType defenseType, List<Modifier> attackModifiers, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers)
    {
        foreach (var target in targets)
        {
            int baseModifier = attacks[attackType];

            List<int> baseBonuses = new List<int>();
            List<int> coinBonuses = new List<int>();
            List<bool> coinResults;

            int finalCoinNumber = noCoins;
            foreach (Modifier modifier in attackModifiers)
            {
                if (modifier.HasTag(ModifierTag.Base)) baseBonuses.Add(modifier.AddModifier(0));
                if (modifier.HasTag(ModifierTag.Coin)) coinBonuses.Add(modifier.AddModifier(0));
                if (modifier.HasTag(ModifierTag.CoinNumber)) finalCoinNumber += modifier.Value;
            }

            Roller roller = new Roller(baseModifier, baseBonuses, finalCoinNumber, coinModifier, coinBonuses);
            int finalAttackModifier = roller.Roll(out coinResults);

            target.Defend(defenseType, finalAttackModifier, damages, resultModifiers);
        }
    }

    public void Buff(IEnumerable<ICharacter> targets, IEnumerable<Modifier> modifiers)
    {
        foreach(var target in targets)
        {
            foreach(var modifier in modifiers)
            {
                var newModifier = new Modifier(target, modifier);
                target.Modifiers.Add(newModifier);
            }
        }
    }

    public void Heal(IEnumerable<ICharacter> targets, int value)
    {
        foreach (var target in targets)
        {
            target.ApplyHealing(value);
        }
    }

    public void Heal(IEnumerable<ICharacter> targets, float percentage)
    {
        foreach (var target in targets)
        {
            target.ApplyHealing(percentage);
        }
    }

    public void GiveTempHP(IEnumerable<ICharacter> targets, int value)
    {
        foreach (var target in targets)
        {
            target.ApplyTempHP(value);
        }
    }

    public void Mark(IEnumerable<ICharacter> targets)
    {
        throw new System.NotImplementedException();
    }
}
