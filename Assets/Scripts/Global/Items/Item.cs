using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    private string name;
    public  string Name { get { return name; } }

    private string description;
    public  string Description { get { return description; } }

    private float price;
    public  float Price { get { return price; } }

    private int bulk;
    public  int Bulk { get { return bulk; } }

    private int quantity;
    public int Quantity { get { return quantity; } }

    private HashSet<ItemTag> tags;
    public HashSet<ItemTag> ItemTags { get { return tags; } }

    public Item(string name, string description, float price, int bulk, int quantity, HashSet<ItemTag> tags)
    {
        this.name = name;
        this.description = description;
        this.price = price;
        this.bulk = bulk;
        this.quantity = quantity;
        this.tags = tags;
    }
}
