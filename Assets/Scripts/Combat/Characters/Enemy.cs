using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    private ICharacter character;
    public int ID { get { return character.ID; } }
    public int Level { get { return character.Level; } }
    public int NoActions { get { return character.NoActions; } set { character.NoActions = value; } }
    public int Initiative { get { return character.Initiative; } }

    private TurnManager turnManager;
    private PlayerInput playerInput;

    public GameObject GameObject { get { return this.gameObject; } }

    public List<Modifier> Modifiers { get { return character.Modifiers; } }
    public List<IAbility> Abilities { get { return character.Abilities; } }
    public IWeapon Weapon { get { return character.Weapon; } }
    public List<IItem> EquippedItems { get { return character.EquippedItems; } }

    public int MaxHP { get { return character.MaxHP; } }
    public int CurrentHP { get { return character.CurrentHP; } }
    public int TempHP { get { return character.TempHP; } }

    public int Meele { get { return character.Meele; } }
    public int Ranged { get { return character.Ranged; } }
    public int Casting { get { return character.Casting; } }

    public int ArmorClass { get { return character.ArmorClass; } }
    public int Fortitude { get { return character.Fortitude; } }
    public int Reflex { get { return character.Reflex; } }
    public int Will { get { return character.Will; } }

    public void Initialize(ICharacter character) { this.character = character; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnManager = UnityEngine.GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        playerInput = UnityEngine.GameObject.FindWithTag("PlayerInput").GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter() { playerInput.NotifyMouseEnter(this); }

    void OnMouseExit() { playerInput.NotifyMouseExit(this); }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        turnManager.ChangeTurn();
    }

    public void StartTurn()
    {
        character.StartTurn();
        StartCoroutine(EnemyTurn());
    }

    public void EndTurn() { character.EndTurn(); }

    public bool IsControllable() { return false; }

    public void Attack(IEnumerable<ICharacter> targets, AttackType attackType, DefenseType defenseType, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers)
    {
        character.Attack(targets, attackType, defenseType, damages, resultModifiers);
    }

    public void Defend(DefenseType defenseType, int attackModifier, Dictionary<AttackTag, int> damages, Dictionary<SuccessLevel, Modifier> resultModifiers)
    {
        character.Defend(defenseType, attackModifier, damages, resultModifiers);
    }

    public void ApplyHealing(int value) { character.ApplyHealing(value); }
    public void ApplyHealing(float percentage) { character.ApplyHealing(percentage); }
    public void ApplyTempHP(int tempHP) { character.ApplyTempHP(tempHP); }
    public bool IsAlive() { return character.IsAlive(); }
    public void Heal(IEnumerable<ICharacter> targets, int value) { character.Heal(targets, value); }
    public void Heal(IEnumerable<ICharacter> targets, float percentage) { character.Heal(targets, percentage); }
    public void GiveTempHP(IEnumerable<ICharacter> targets, int value) { character.GiveTempHP(targets, value); }
    public void Buff(IEnumerable<ICharacter> targets, IEnumerable<Modifier> modifiers) { character.Buff(targets, modifiers); }
    public void Mark(IEnumerable<ICharacter> targets) { character.Mark(targets); }

    public int CompareTo(ICharacter obj) { return character.CompareTo(obj); }

    public void GetNotification(Modifier modifier)
    {
        character.GetNotification(modifier);
    }

    public void DeleteSelf()
    {
        Destroy(this);
    }
}
