using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBuilder : MonoBehaviour
{
    [SerializeField]
    private CharacterPrototype prototype;
    [SerializeField]
    private int ID;

    private Dictionary<CharacterPrototype, Action> builderList = new Dictionary<CharacterPrototype, Action>();

    private void Awake()
    {
        builderList[CharacterPrototype.PlayerPrototype] = PlayerPrototype;
        builderList[CharacterPrototype.EnemyPrototype] = EnemyPrototype;
    }

    public void Build()
    {
        if (prototype != CharacterPrototype.None) builderList[prototype]();
    }

    public List<Character> BuildParty()
    {
        List<Character> list = new List<Character>();

        var itemFactory = new ItemFactory();
        var abilityFactory = new AbilityFactory();

        IWeapon weapon = itemFactory.BuildPrototypeWeapon();
        IHealthComponent healthComponent = new BaseHealthComponent(10, new HashSet<AttackTag>(), new Dictionary<AttackTag, int>());
        IDefenseComponent defenseComponent = new BaseDefenseComponent(1, 2, 2, 1, 2, 2);
        ITargetComponent targetComponent = new BaseTargetComponent(2, 2, 0, 2, 2);

        Character newCharacter = new Character(0, 1, new List<IAbility>(), weapon, new List<IItem>(), healthComponent, defenseComponent, targetComponent);
        list.Add(newCharacter);

        weapon = itemFactory.BuildPrototypeWeapon();
        healthComponent = new BaseHealthComponent(8, new HashSet<AttackTag>(), new Dictionary<AttackTag, int>());
        defenseComponent = new BaseDefenseComponent(1, 1, 2, 2, 2, 2);
        targetComponent = new BaseTargetComponent(1, 1, 0, 2, 2);

        newCharacter = new Character(1, 1, new List<IAbility>(), weapon, new List<IItem>(), healthComponent, defenseComponent, targetComponent);
        list.Add(newCharacter);

        weapon = itemFactory.BuildPrototypeWeapon();
        healthComponent = new BaseHealthComponent(8, new HashSet<AttackTag>(), new Dictionary<AttackTag, int>());
        defenseComponent = new BaseDefenseComponent(1, 1, 1, 2, 2, 2);
        targetComponent = new BaseTargetComponent(1, 0, 1, 2, 2);

        newCharacter = new Character(2, 1, new List<IAbility>(), weapon, new List<IItem>(), healthComponent, defenseComponent, targetComponent);
        list.Add(newCharacter);

        return list;
    }

    private void PlayerPrototype()
    {
        this.gameObject.AddComponent(typeof(Player));
        Player newPlayer = this.gameObject.GetComponent<Player>();

        Character newCharacter = BasicCharacter();
        newPlayer.Initialize(newCharacter);
        newCharacter.Initialize(newPlayer);
        Debug.Log("Player Created");
    }

    private void EnemyPrototype()
    {
        this.gameObject.AddComponent(typeof(Enemy));
        var newEnemy = this.gameObject.GetComponent<Enemy>();
        
        Character newCharacter = BasicCharacter();
        newEnemy.Initialize(newCharacter);
        newCharacter.Initialize(newEnemy);
        Debug.Log("Enemy Created");
    }

    private Character BasicCharacter()
    {
        var itemFactory = new ItemFactory();
        var abilityFactory = new AbilityFactory();
        IWeapon weapon = itemFactory.BuildPrototypeWeapon();

        IHealthComponent healthComponent = new BaseHealthComponent(10, new HashSet<AttackTag>(), new Dictionary<AttackTag, int>());
        IDefenseComponent defenseComponent = new BaseDefenseComponent(1, 1, 1, 1, 2, 2);
        ITargetComponent targetComponent = new BaseTargetComponent(1, 1, 1, 2, 2);

        Character character = new Character(ID, 1, new List<IAbility>(), weapon, new List<IItem>(), healthComponent, defenseComponent, targetComponent);
        StrikeAbility basicStrike = abilityFactory.BuildBasicStrike(character);
        character.Abilities.Add(basicStrike);

        Debug.Log("Character Created");
        return character;
    }
}

public enum CharacterPrototype
{
    None,
    PlayerPrototype,
    EnemyPrototype
}
