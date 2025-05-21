using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter : IComparable<ICharacter>
{
    int ID { get; }
    int Level { get; }
    int NoActions { get; set; }
    int Initiative { get; }
    List<Modifier> Modifiers { get; }
    List<IAbility> Abilities { get; }
    IWeapon Weapon { get; }
    List<IItem> EquippedItems { get; }
    GameObject GameObject { get; }
    bool IsControllable();
    void StartTurn();
    void EndTurn();

    void Attack(IEnumerable<ICharacter> targets, AttackType attackType, DefenseType defenseType, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers);
    void Defend(DefenseType defenseType, int attackModifier, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers);

    void ApplyHealing(int value);
    void ApplyHealing(float percentage);
    void ApplyTempHP(int tempHP);
    bool IsAlive();

    void Heal(IEnumerable<ICharacter> targets, int value);
    void Heal(IEnumerable<ICharacter> targets, float percentage);
    void GiveTempHP(IEnumerable<ICharacter> targets, int value);
    void Buff(IEnumerable<ICharacter> targets, IEnumerable<Modifier> modifiers);
    void Mark(IEnumerable<ICharacter> targets);

    void GetNotification(Modifier modifier);
    void DeleteSelf();

    // UI Getters
    int MaxHP { get; }
    int CurrentHP { get; }
    int TempHP { get; }

    int Meele { get; }
    int Ranged { get; }
    int Casting { get; }

    int ArmorClass { get; }
    int Fortitude { get; }
    int Reflex { get; }
    int Will { get; }
}

public interface IHealthComponent
{
    int MaxHealth { get; }
    int CurrentHealth { get; }
    int TempHP { get; }
    HashSet<AttackTag> Immunities { get; }
    Dictionary<AttackTag, int> Resistances { get; }

    void ApplyDamage(SuccessLevel successLevel, Dictionary<AttackTag, int> damages);
    void ApplyHealing(int value);
    void ApplyHealing(float percentage);
    void ApplyTempHP(int tempHP);
    bool IsAlive();
}

public interface IDefenseComponent
{
    int ArmorClass { get; }
    int Fortitude { get; }
    int Reflex { get; }
    int Will { get; }

    SuccessLevel IsHit(DefenseType defenseType, int attackModifier, List<Modifier> defenseModifiers);
}

public interface ITargetComponent
{
    int Meele { get; }
    int Ranged { get; }
    int Casting { get; }

    void Attack(IEnumerable<ICharacter> targets, AttackType attackType, DefenseType defenseType, List<Modifier> attackModifiers, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers);
    void Heal(IEnumerable<ICharacter> targets, int value);
    void Heal(IEnumerable<ICharacter> targets, float percentage);
    void GiveTempHP(IEnumerable<ICharacter> targets, int value);
    void Buff(IEnumerable<ICharacter> targets, IEnumerable<Modifier> modifiers);
    void Mark(IEnumerable<ICharacter> targets);
}

public interface IItem
{
    string Name { get; }
    string Description { get; }
    float Price { get; }
    int Bulk { get; }
    int Quantity { get; }
    HashSet<ItemTag> ItemTags { get; }
}

public interface IWeapon : IItem
{
    AttackType AttackType { get; }
    int Range { get; }
    Dictionary<AttackTag, int> Damages { get; }
    HashSet<WeaponTag> WeaponTags { get; }
}

public interface IAbility
{
    public ICharacter Owner { get; }
    public string Name { get; }
    public int Level { get; }
    public string Description { get; }
    public AbilityType AbilityType { get; }
    public int ActionCost { get; }
    public int StressCost { get; }

    public void Use();
}

public enum ItemTag
{

}

public enum WeaponTag
{

}

public enum AttackTag
{
    Slashing, Bludgeoning, Piercing
}

public enum AttackType
{
    Meele, Ranged, Casting
}

public enum DefenseType
{
    ArmorClass, Fortitude, Reflex, Will
}

public enum SuccessLevel
{
    CriticalFailure, Failure, Success, CriticalSuccess
}