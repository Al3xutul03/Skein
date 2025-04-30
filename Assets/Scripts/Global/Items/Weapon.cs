using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    private Item item;

    public string Name { get { return item.Name; } }
    public string Description { get { return item.Description; } }
    public float Price { get { return item.Price; } }
    public int Bulk { get { return item.Bulk; } }
    public int Quantity { get { return item.Quantity; } }
    public HashSet<ItemTag> ItemTags { get { return item.ItemTags; } }


    private AttackType attackType;
    public  AttackType AttackType { get { return attackType; } }

    private int range;
    public  int Range { get { return range; } }

    private Dictionary<AttackTag, int> damages;
    public  Dictionary<AttackTag, int> Damages { get { return damages; } }

    private HashSet<WeaponTag> weaponTags;
    public  HashSet<WeaponTag> WeaponTags { get { return weaponTags; } }

    public Weapon(Item item, AttackType attackType, int range, Dictionary<AttackTag, int> damages, HashSet<WeaponTag> weaponTags)
    {
        this.item = item;
        this.attackType = attackType;
        this.range = range;
        this.damages = damages;
        this.weaponTags = weaponTags;
    }
}
