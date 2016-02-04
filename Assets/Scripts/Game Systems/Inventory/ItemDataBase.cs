using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemDataBase : MonoBehaviour // consider making static
{
    public List<Item> itemDatabase;
    public List<Item> weaponDatabase;
    public List<Item> armorDatabase;
    public List<Item> potionDatabase;
    public List<Item> bookDatabase;
    public List<Item> ingredientsDatabase;
    public List<Item> keyDatabase;
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public Type type;
    public SubType subType;
    public ArmorType armorType;
    public bool stackable;
    public bool favorited;
    public Image itemImage;
    public GameObject itemObject; // the physical object in the game world
    public Button itemUIButton; // the item's representation in the inventory screen
    public float weight;
    public int value;
    public int quantity;
    public int rating; //damage, armor, or magnitude
    public Dictionary<int, string> subTypeDict = new Dictionary<int, string>();
    
    public Item()
    {
        subTypeDict.Add((int)SubType.LightArmor, "Light Armor");
        subTypeDict.Add((int)SubType.HeavyArmor, "Heavy Armor");
        subTypeDict.Add((int)SubType.OneHanded, "One Handed");
        subTypeDict.Add((int)SubType.TwoHanded, "Two Handed");
        subTypeDict.Add((int)SubType.Health, "Health");
        subTypeDict.Add((int)SubType.Mana, "Mana");
    }

    public enum Type
    {
        Misc,
        Ingredient,
        Weapon,
        Armor,
        Potion,
        Book,
        Key,
        Note
    }

    public enum SubType
    {
        OneHanded,
        TwoHanded,
        Bow,
        Dagger,
        Staff,
        LightArmor,
        HeavyArmor,
        Clothes,
        Health,
        Mana,
        Enchantment,
        Poison,
        Effect,
        None

    }

    public enum ArmorType
    {
        Gauntlets,
        Chestplate,
        Helmet,
        Boots,
        Robe,
        None
    }

    public Item ReturnCopy()
    {
        Item addItem = new Item();

        addItem.itemName = itemName;
        addItem.description = description;
        addItem.quantity = quantity;
        addItem.type = type;
        addItem.subType = subType;
        addItem.armorType = armorType;
        addItem.stackable = stackable;
        addItem.favorited = favorited;
        addItem.itemImage = itemImage;
        addItem.itemObject = itemObject;
        addItem.weight = weight;
        addItem.value = value;
        addItem.quantity = quantity;
        addItem.rating = rating;

        return addItem;
    }

    public static int CompareItemName(Item i1, Item i2)
    {
        return i1.itemName.CompareTo(i2.itemName);
    }

    public static int CompareItemType(Item i1, Item i2)
    {
        return i1.type.CompareTo(i2.type);
    }

    public static int CompareItemSubType(Item i1, Item i2)
    {
        return i1.subType.CompareTo(i2.subType);
    }

    public static int CompareItemWeight(Item i1, Item i2)
    {
        return i1.weight.CompareTo(i2.weight);
    }

    public static int CompareItemValue(Item i1, Item i2)
    {
        return i1.value.CompareTo(i2.value);
    }

    public static int CompareItemRating(Item i1, Item i2)
    {
        return i1.rating.CompareTo(i2.rating);
    }
}

// book note ingredients left 
