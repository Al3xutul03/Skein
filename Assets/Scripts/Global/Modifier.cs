using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Modifier
{
    private ICharacter owner;

    private int duration;
    public  int Duration { get { return duration; } }
    
    private int value;
    public  int Value { get { return value; } }
    
    private HashSet<ModifierTag> tags;
    private HashSet<AttackType> attackTypes;
    private HashSet<DefenseType> defenseTypes;

    public Modifier(ICharacter owner, int value, HashSet<ModifierTag> tags, HashSet<AttackType> attackTypes, HashSet<DefenseType> defenseTypes, int duration = -1)
    {
        this.owner = owner;
        this.duration = duration;
        this.value = value;
        this.tags = tags;
        this.attackTypes = attackTypes;
        this.defenseTypes = defenseTypes;
    }

    public Modifier(ICharacter owner, Modifier modifier)
    {
        this.owner = owner;
        value = modifier.value;
        tags = new HashSet<ModifierTag>(modifier.tags);
        attackTypes = new HashSet<AttackType>(modifier.attackTypes);
        defenseTypes = new HashSet<DefenseType>(modifier.defenseTypes);
    }

    public int AddModifier(int initial)
    {
        return initial + value;
    }

    public bool HasTag(ModifierTag tag)
    {
        return tags.Contains(tag);
    }

    public bool HasAllTags(IEnumerable<ModifierTag> tags)
    {
        var wantedTags = new List<ModifierTag>(tags.Where(tag => this.tags.Contains(tag)));
        return wantedTags.Count == tags.Count();
    }

    public bool HasAnyTags(IEnumerable<ModifierTag> tags)
    {
        var wantedTags = new List<ModifierTag>(tags.Where(tag => this.tags.Contains(tag)));
        return wantedTags.Count > 0;
    }

    public bool HasAttackType(AttackType attackType)
    {
        return attackTypes.Contains(attackType);
    }

    public bool HasDefenseType(DefenseType defenseType)
    {
        return defenseTypes.Contains(defenseType);
    }

    public void DecreaseDuration()
    {
        if (duration > 0) duration--;

        if (duration == 0) NotifyOwner();
    }

    private void NotifyOwner()
    {
        if (owner == null) return;

        owner.GetNotification(this);
    }
}

public enum ModifierTag
{
    // Where the modifier goes
    Base, Coin, CoinNumber
}
