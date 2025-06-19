using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class AbilityFactory
{
    public AbilityFactory() { }

    public StrikeAbility BuildBasicStrike(ICharacter owner)
    {
        Ability ability = new Ability(
            owner: owner,
            name: "Basic Strike",
            level: 1,
            description: "A simple strike done with whatever weapon the user is carrying (or with fists if they have no weapon).",
            abilityType: AbilityType.Basic,
            actionCost: 1,
            stressCost: 0);

        return new StrikeAbility(ability, 1, 0, 0, 0);
    }

    public List<IAbility> BuildFighterAbilities(ICharacter owner)
    {
        List<IAbility> list = new List<IAbility>();

        list.Add(BuildBasicStrike(owner));

        Ability ability = new Ability(
            owner: owner,
            name: "Overhead Strike",
            level: 1,
            description: "A powerful strike done with whatever weapon the user is carrying (or with fists if they have no weapon).",
            abilityType: AbilityType.Maneuver,
            actionCost: 2,
            stressCost: 0);

        StrikeAbility strikeAbility = new StrikeAbility(ability, 1, 2, 0, 0);
        list.Add(strikeAbility);

        ability = new Ability(
            owner: owner,
            name: "Encourage",
            level: 1,
            description: "Encourage an ally with a battlecry, granting them +1 to their attacks for 1 turn.",
            abilityType: AbilityType.Maneuver,
            actionCost: 2,
            stressCost: 0);

        BuffAbility buffAbility = new BuffAbility(ability, 10, 1, 1, 0, 0);
        list.Add(buffAbility);

        return list;
    }

    public List<IAbility> BuildRogueAbilities(ICharacter owner)
    {
        List<IAbility> list = new List<IAbility>();

        list.Add(BuildBasicStrike(owner));

        Ability ability = new Ability(
            owner: owner,
            name: "Precise Strike",
            level: 1,
            description: "A focused strike with a +1 coin bonus done with whatever weapon the user is carrying (or with fists if they have no weapon).",
            abilityType: AbilityType.Maneuver,
            actionCost: 2,
            stressCost: 0);

        StrikeAbility strikeAbility = new StrikeAbility(ability, 1, 0, 1, 0);
        list.Add(strikeAbility);

        ability = new Ability(
            owner: owner,
            name: "Assasinate",
            level: 1,
            description: "A powerful strike with a +1 basic bonus and +1 bonus to the number of coins",
            abilityType: AbilityType.Maneuver,
            actionCost: 3,
            stressCost: 0);

        strikeAbility = new StrikeAbility(ability, 1, 1, 0, 1);
        list.Add(strikeAbility);

        return list;
    }

    public List<IAbility> BuildClericAbilities(ICharacter owner)
    {
        List<IAbility> list = new List<IAbility>();

        list.Add(BuildBasicStrike(owner));

        Ability ability = new Ability(
            owner: owner,
            name: "Bless",
            level: 1,
            description: "A quick prayer to your god to give one of your allies a +1 bonus to their strikes.",
            abilityType: AbilityType.Spell,
            actionCost: 1,
            stressCost: 0);

        BuffAbility buffAbility = new BuffAbility(ability, 10, 1, 1, 0, 0);
        list.Add(buffAbility);

        ability = new Ability(
            owner: owner,
            name: "Divine Will",
            level: 1,
            description: "By divine will of your god, one of your allies will gain a +1 coin bonus to their strikes on their next turn.",
            abilityType: AbilityType.Spell,
            actionCost: 2,
            stressCost: 0);

        buffAbility = new BuffAbility(ability, 10, 1, 0, 1, 0);
        list.Add(buffAbility);

        return list;
    }
}
