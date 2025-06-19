using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class BaseHealthComponent : IHealthComponent
{
    private int maxHealth;
    public  int MaxHealth { get { return maxHealth; } }

    private int currentHealth;
    public  int CurrentHealth { get { return currentHealth; } }
    
    private int tempHP;
    public  int TempHP { get { return tempHP; } }


    private HashSet<AttackTag> immunities;
    public  HashSet<AttackTag> Immunities { get { return immunities; } }
    
    private Dictionary<AttackTag, int> resistances;
    public  Dictionary<AttackTag, int> Resistances { get { return resistances; } }

    private static readonly Dictionary<SuccessLevel, float> changeFactor = new Dictionary<SuccessLevel, float>
    {
        { SuccessLevel.CriticalSuccess, 2f },
        { SuccessLevel.Success, 1f },
        { SuccessLevel.Failure, 0.5f },
        { SuccessLevel.CriticalFailure, 0f }
    };

    public BaseHealthComponent(int maxHealth, HashSet<AttackTag> immunities, Dictionary<AttackTag, int> resistances)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.tempHP = 0;
        this.immunities = immunities;
        this.resistances = resistances;
    }

    public void ApplyDamage(SuccessLevel successLevel, Dictionary<AttackTag, int> damages)
    {
        int finalDamage = ComputeFinalDamage(successLevel, damages);

        finalDamage -= tempHP;
        if (finalDamage <= 0)
        {
            tempHP -= finalDamage + tempHP;
        }
        else
        {
            currentHealth -= finalDamage;
        }

        var attackInfo = GameObject.FindWithTag("AttackInfo").GetComponent<AttackInfo>();
        attackInfo.SetDamage(finalDamage);

        var playerInput = GameObject.FindWithTag("PlayerInput").GetComponent<PlayerInput>();
        playerInput.NotifyStrike();

        Debug.Log($"Damage taken: {finalDamage}");
    }

    public void ApplyHealing(int healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void ApplyHealing(float percentage)
    {
        currentHealth += (int) (maxHealth * percentage);
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void ApplyTempHP(int tempHP)
    {
        if (tempHP > this.tempHP) this.tempHP = tempHP;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    private int ComputeFinalDamage(SuccessLevel successLevel, Dictionary<AttackTag, int> damages)
    {
        var nonImmune = damages.Where(pair => !immunities.Contains(pair.Key));
        int finalDamage = 0;
        foreach (var damage in nonImmune)
        {
            int resistance = resistances.ContainsKey(damage.Key) ? resistances[damage.Key] : 0;
            finalDamage += damage.Value > resistance ? damage.Value - resistance : 0;
        }

        finalDamage = (int)(finalDamage * changeFactor[successLevel]);
        return finalDamage;
    }
}
