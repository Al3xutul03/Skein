using UnityEngine;

public class Ability : IAbility
{
    private ICharacter owner;
    public ICharacter Owner { get { return owner; } }

    private string name;
    public string Name { get { return name; } }

    private int level;
    public int Level { get { return level; } }

    private string description;
    public string Description { get { return description; } }

    private AbilityType abilityType;
    public AbilityType AbilityType { get { return abilityType; } }

    private int actionCost;
    public int ActionCost { get { return actionCost; } }

    private int stressCost;
    public int StressCost { get { return stressCost; } }

    public Ability(ICharacter owner, string name, int level, string description, AbilityType abilityType, int actionCost, int stressCost)
    {
        this.owner = owner;
        this.name = name;
        this.level = level;
        this.description = description;
        this.abilityType = abilityType;
        this.actionCost = actionCost;
        this.stressCost = stressCost;
    }

    public void Use()
    {
        Debug.Log(owner.ID + "used " + name + "!");
    }
}

public enum AbilityType
{
    Basic, Class, Maneuver, Spell
}