using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    public ItemFactory() { }
    
    public IWeapon BuildPrototypeWeapon()
    {
        Item item = new Item("Prototype Weapon", "For testing purposes", 0, 0, 1, new HashSet<ItemTag>());
        var damages = new Dictionary<AttackTag, int>
        {
            { AttackTag.Slashing, 4 }
        };

        return new Weapon(item, AttackType.Meele, 1, damages, new HashSet<WeaponTag>());
    }
}
