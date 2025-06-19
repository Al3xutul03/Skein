using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Character : ICharacter
{
    private ICharacter owner;
    public ICharacter Owner {  get { return owner; } set { owner = value; } }

    private int id;
    public int ID { get { return id; } }

    private int level;
    public int Level { get { return level; } }

    private int noActions;
    public int NoActions { get { return noActions; } set { noActions = value; } }

    private int initiative;
    public int Initiative { get { return initiative; } }

    public GameObject GameObject { get { return owner.GameObject; } }

    //private TurnManager turnManager;
    //private PlayerInput playerInput;

    private List<Modifier> modifiers = new List<Modifier>();
    public List<Modifier> Modifiers { get { return modifiers; } }

    private List<IAbility> abilities;
    public List<IAbility> Abilities {  get { return abilities; } }

    private IWeapon weapon;
    public  IWeapon Weapon { get { return weapon; } }

    private List<IItem> equippedItems;
    public  List<IItem> EquippedItems { get { return equippedItems; } }

    public int MaxHP { get { return healthComponent.MaxHealth; } }
    public int CurrentHP { get { return healthComponent.CurrentHealth; } }
    public int TempHP { get { return healthComponent.TempHP; } }

    public int Meele { get { return targetComponent.Meele; } }
    public int Ranged { get { return targetComponent.Ranged; } }
    public int Casting { get { return targetComponent.Casting; } }

    public int ArmorClass { get { return defenseComponent.ArmorClass; } }
    public int Fortitude { get { return defenseComponent.Fortitude; } }
    public int Reflex { get { return defenseComponent.Reflex; } }
    public int Will { get { return defenseComponent.Will; } }

    private IHealthComponent healthComponent;
    private IDefenseComponent defenseComponent;
    private ITargetComponent targetComponent;

    public Character(int id, int level, List<IAbility> abilities, IWeapon weapon, List<IItem> equppedItems, IHealthComponent healthComponent, IDefenseComponent defenseComponent, ITargetComponent targetComponent)
    {
        this.id = id;
        this.level = level;
        this.abilities = abilities;
        this.weapon = weapon;
        this.equippedItems = equippedItems;
        this.healthComponent = healthComponent;
        this.defenseComponent = defenseComponent;
        this.targetComponent = targetComponent;

        Roller roller = new Roller(1, 2, 2);
        List<bool> coinResults = new List<bool>();
        this.initiative = roller.Roll(out coinResults);
    }

    public void Initialize(ICharacter owner) { this.owner = owner; }

    public void StartTurn()
    {
        NoActions = 3;
    }

    public void EndTurn()
    {
        Debug.Log("Id: " + this.id + " took its turn! Initiative: " + initiative);
    }

    public bool IsControllable() { return owner.IsControllable(); }

    public void Attack(IEnumerable<ICharacter> targets, AttackType attackType, DefenseType defenseType, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers)
    {
        Debug.Log($"ID:{this.id} prepares an attack!");
        var attackModifiers = modifiers.Where(modifier => modifier.HasAttackType(attackType)).ToList();
        targetComponent.Attack(targets, attackType, defenseType, attackModifiers, damages, resultModifiers);
    }

    public void Defend(DefenseType defenseType, int attackModifier, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers)
    {
        var defenseModifiers = modifiers.Where(modifier => modifier.HasDefenseType(defenseType)).ToList();
        SuccessLevel successLevel = defenseComponent.IsHit(defenseType, attackModifier, defenseModifiers);

        Debug.Log($"ID:{this.id} defends! Attack Success Level: {successLevel.ToString()}");

        healthComponent.ApplyDamage(successLevel, damages);
        var newModifier = new Modifier(this, resultModifiers[successLevel]);
        modifiers.Add(newModifier);
    }

// Delegations
    public void ApplyHealing(int value) { healthComponent.ApplyHealing(value); }
    public void ApplyHealing(float percentage) { healthComponent.ApplyHealing(percentage); }
    public void ApplyTempHP(int tempHP) { healthComponent.ApplyTempHP(tempHP); }
    public bool IsAlive() { return healthComponent.IsAlive(); }
    public void Heal(IEnumerable<ICharacter> targets, int value) { targetComponent.Heal(targets, value); }
    public void Heal(IEnumerable<ICharacter> targets, float percentage) { targetComponent.Heal(targets, percentage); }
    public void GiveTempHP(IEnumerable<ICharacter> targets, int value) { targetComponent.GiveTempHP(targets, value); }
    public void Buff(IEnumerable<ICharacter> targets, IEnumerable<Modifier> modifiers) { targetComponent.Buff(targets, modifiers); }
    public void Mark(IEnumerable<ICharacter> targets) { targetComponent.Mark(targets); }

    public int CompareTo(ICharacter obj)
    {
        if (this.initiative < obj.Initiative)
            return -1;
        if (this.initiative > obj.Initiative)
            return 1;
        return 0;
    }

    public void GetNotification(Modifier modifier)
    {
        throw new System.NotImplementedException();
    }

    public void DeleteSelf()
    {
        owner.DeleteSelf();
    }
}
