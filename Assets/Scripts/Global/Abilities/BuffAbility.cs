using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : IAbility
{
    private Ability ability;

    private int range;

    public ICharacter Owner { get { return ability.Owner; } }
    public string Name { get { return ability.Name; } }
    public int Level { get { return ability.Level; } }
    public string Description { get { return ability.Description; } }
    public AbilityType AbilityType { get { return ability.AbilityType; } }
    public int ActionCost { get { return ability.ActionCost; } }
    public int StressCost { get { return ability.StressCost; } }
    public int Range { get { return range; } }

    private List<ICharacter> targets = new List<ICharacter>();
    public List<ICharacter> Targets { get { return targets; } }

    private int noTargets;
    public int NoTargets { get { return noTargets; } }

    private Modifier buff;

    public BuffAbility(Ability ability, int range, int noTargets, int basicBonus, int coinBonus, int coinNumberBonus)
    {
        this.ability = ability;
        this.noTargets = noTargets;
        this.range = range;

        int bonus = 0;
        var tags = new HashSet<ModifierTag>();
        if (basicBonus != 0) { tags.Add(ModifierTag.Base); bonus = basicBonus; }
        if (coinBonus != 0) { tags.Add(ModifierTag.Coin); bonus = coinBonus; }
        if (coinNumberBonus != 0) { tags.Add(ModifierTag.CoinNumber); bonus = coinNumberBonus; }

        this.buff = new Modifier(Owner, bonus, tags,
            new HashSet<AttackType>() { AttackType.Meele, AttackType.Ranged, AttackType.Casting },
            new HashSet<DefenseType> { });
    }

    public void Use()
    {
        Owner.Buff(targets, new List<Modifier>() {buff});
        Owner.NoActions -= ability.ActionCost;
    }

    //public void SetTargets(ICharacter target)
}
