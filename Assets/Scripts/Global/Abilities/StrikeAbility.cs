using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAbility : IAbility
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

    private List<Modifier> modifiers;

    public StrikeAbility(Ability ability, int noTargets, int basicBonus, int coinBonus, int coinNumberBonus)
    {
        this.ability = ability;
        this.noTargets = noTargets;

        this.modifiers = new List<Modifier>()
        {
            new Modifier(Owner, basicBonus,
                new HashSet<ModifierTag>(){ ModifierTag.Base },
                new HashSet<AttackType>(){ Owner.Weapon.AttackType },
                new HashSet<DefenseType>{ DefenseType.ArmorClass }),
            new Modifier(Owner, coinBonus,
                new HashSet<ModifierTag>(){ ModifierTag.Coin },
                new HashSet<AttackType>(){ Owner.Weapon.AttackType },
                new HashSet<DefenseType>{ DefenseType.ArmorClass }),
            new Modifier(Owner, coinNumberBonus,
                new HashSet<ModifierTag>(){ ModifierTag.CoinNumber },
                new HashSet<AttackType>(){ Owner.Weapon.AttackType },
                new HashSet<DefenseType>{ DefenseType.ArmorClass })
        };
    }

    public void Use()
    {
        foreach (var modifier in modifiers) { Owner.Modifiers.Add(modifier); }
        
        Owner.Attack(targets, Owner.Weapon.AttackType, DefenseType.ArmorClass, Owner.Weapon.Damages, new Dictionary<SuccessLevel, Modifier>());

        foreach (var modifier in modifiers) { Owner.Modifiers.Remove(modifier); }
    }

    //public void SetTargets(ICharacter target)
}
