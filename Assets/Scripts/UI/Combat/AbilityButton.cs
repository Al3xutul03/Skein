using TMPro;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    private IAbility ability;
    public IAbility Ability { get { return ability; } }

    private TextMeshPro abilityName;

    public void Initialize(IAbility ability)
    {
        this.ability = ability;
        abilityName.text = ability.Name;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        abilityName = GetComponentInChildren<TextMeshPro>();
    }
}
