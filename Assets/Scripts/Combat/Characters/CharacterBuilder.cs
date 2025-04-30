using System;
using System.Collections.Generic;
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

        builderList[prototype]();
    }

    private void PlayerPrototype()
    {
        this.gameObject.AddComponent(typeof(Player));
        Player newPlayer = this.gameObject.GetComponent<Player>();

        ICharacter newCharacter = BasicCharacter(newPlayer);
        newPlayer.Initialize(newCharacter);
    }

    private void EnemyPrototype()
    {
        this.gameObject.AddComponent(typeof(Enemy));
        var newEnemy = this.gameObject.GetComponent<Enemy>();
        
        ICharacter newCharacter = BasicCharacter(newEnemy);
        newEnemy.Initialize(newCharacter);
    }

    private ICharacter BasicCharacter(ICharacter owner)
    {
        var itemFactory = new ItemFactory();
        IWeapon weapon = itemFactory.BuildPrototypeWeapon();

        IHealthComponent healthComponent = new BaseHealthComponent(10, new HashSet<AttackTag>(), new Dictionary<AttackTag, int>());
        IDefenseComponent defenseComponent = new BaseDefenseComponent(1, 1, 1, 1, 2, 2);
        ITargetComponent targetComponent = new BaseTargetComponent(1, 1, 1, 2, 2);

        return new Character(owner, ID, 1, new List<IAbility>(), weapon, new List<IItem>(),healthComponent, defenseComponent, targetComponent);
    }
}

public enum CharacterPrototype
{
    PlayerPrototype,
    EnemyPrototype
}
