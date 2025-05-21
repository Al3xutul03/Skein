using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : IAbility
{
    private Ability ability;

    public ICharacter Owner { get { return ability.Owner; } }
    public string Name { get { return ability.Name; } }
    public int Level { get { return ability.Level; } }
    public string Description { get { return ability.Description; } }
    public AbilityType AbilityType { get { return ability.AbilityType; } }
    public int ActionCost { get { return ability.ActionCost; } }
    public int StressCost { get { return ability.StressCost; } }
    public int Range { get { return Owner.Weapon.Range; } }

    private List<ICharacter> targets = new List<ICharacter>();
    public List<ICharacter> Targets { get { return targets; } }

    private int noTargets;
    public int NoTargets { get { return noTargets; } }

    private Modifier buff;

    public BuffAbility(Ability ability, int noTargets, int basicBonus, int coinBonus, int coinNumberBonus)
    {
        this.ability = ability;
        this.noTargets = noTargets;

        this.buff = new Modifier(Owner, basicBonus,
            new HashSet<ModifierTag>() { ModifierTag.Base },
            new HashSet<AttackType>() { AttackType.Meele, AttackType.Ranged, AttackType.Casting },
            new HashSet<DefenseType> { });
    }

    public void Use()
    {
        Owner.Buff(targets, new List<Modifier>() {buff});
    }

    //public void SetTargets(ICharacter target)
}
