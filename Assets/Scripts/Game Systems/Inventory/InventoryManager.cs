using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    public List<Item> inventory; // consider making static
    public ItemDataBase dataBase;
    public Button itemButton;
    public Transform invPanel;
    public InventoryUI currentUI;

    void Start()
    {
        
        Item theItem = dataBase.armorDatabase.Find(x => x.itemName == "Wooden Gauntlets");
        AddItem(theItem, 1);
        theItem = dataBase.weaponDatabase.Find(x => x.itemName == "Wooden Sword");
        AddItem(theItem, 4);
        theItem = dataBase.armorDatabase.Find(x => x.itemName == "Wooden Chestplate");
        AddItem(theItem, 30);
        theItem = dataBase.potionDatabase.Find(x => x.itemName == "Minor Health Potion");
        AddItem(theItem, 150);
    }

    public void AddItem(Item item, int quantity)
    {
        if (item.stackable)
        {
            if (inventory.Exists(x => x.itemName == item.itemName))
            {
                int index = inventory.FindIndex(x => x.itemName == item.itemName);
                inventory[index].quantity += quantity;
            }
            else
            {
                var button = Instantiate(itemButton);
                button.transform.SetParent(invPanel, false);
                inventory.Add(item.ReturnCopy());
                inventory[inventory.Count - 1].itemUIButton = button;
                inventory[inventory.Count - 1].quantity = quantity;
                button.GetComponent<InventoryUIButton>().InitButton(inventory[inventory.Count - 1]);
            }
        }
        else
        {
            for (int i = 0; i < quantity; i++)
            {
                var button = Instantiate(itemButton);
                button.transform.SetParent(invPanel, false);
                inventory.Add(item.ReturnCopy());
                inventory[inventory.Count - 1].itemUIButton = button;
                button.GetComponent<InventoryUIButton>().InitButton(inventory[inventory.Count - 1]);
            }
        }
    }

    public void RemoveItem(Item item, int amntToRemove)
    {
        if (item.stackable)
        {
            if (inventory.Exists(x => x.itemName == item.itemName))
            {
                int index = inventory.FindIndex(x => x.itemName == item.itemName);
                if (inventory[index].quantity == amntToRemove)
                {
                    Destroy(inventory[index].itemUIButton);
                    inventory.RemoveAt(index);
                }
                else
                {
                    inventory[index].quantity -= amntToRemove;
                }
            }
        }
        else
        {
            if (inventory.Exists(x => x.itemName == item.itemName))
            {
                int index = inventory.FindIndex(x => x.itemName == item.itemName);
                Destroy(inventory[index].itemUIButton);
                inventory.RemoveAt(index); // Assume that if it isn't stackable, it only contains 1
            }
        }
    }

    //Call for stackable, and want to drop only certain amount
    public void DropItem(Item item, int amntToRemove)
    {
        if (item.stackable)
        {
            if (inventory.Exists(x => x.itemName == item.itemName))
            {
                int index = inventory.FindIndex(x => x.itemName == item.itemName);
                if (inventory[index].quantity == amntToRemove)
                {
                    GameObject theItem = Instantiate(inventory[index].itemObject);
                    theItem.transform.position = player.transform.position;
                    ItemObject theItemObject = theItem.GetComponent<ItemObject>();
                    theItemObject.quantity = inventory[index].quantity;
                    theItemObject.item = inventory[index];
                    Destroy(inventory[index].itemUIButton);
                    inventory.RemoveAt(index);
                }
                else
                {
                    GameObject theItem = Instantiate(inventory[index].itemObject);
                    theItem.transform.position = player.transform.position;
                    ItemObject theItemObject = theItem.GetComponent<ItemObject>();
                    theItemObject.quantity = amntToRemove;
                    theItemObject.item = inventory[index];
                    inventory[index].quantity -= amntToRemove;
                }
            }
        }
        else
        {
            if (inventory.Exists(x => x.itemName == item.itemName))
            {
                int index = inventory.FindIndex(x => x.itemName == item.itemName);
                GameObject theItem = Instantiate(inventory[index].itemObject);
                theItem.transform.position = player.transform.position;
                ItemObject theItemObject = theItem.GetComponent<ItemObject>();
                theItemObject.quantity = inventory[index].quantity;
                theItemObject.item = inventory[index];
                Destroy(inventory[index].itemUIButton);
                inventory.RemoveAt(index); // Assume that if it isn't stackable, it only contains 1
            }
        }
    }

    public void DropItem(Item item)
    {
        if (inventory.Exists(x => x.itemName == item.itemName))
        {
            int index = inventory.FindIndex(x => x.itemName == item.itemName);
            GameObject theItem = Instantiate(inventory[index].itemObject);
            theItem.transform.position = player.transform.position;
            ItemObject theItemObject = theItem.GetComponent<ItemObject>();
            theItemObject.quantity = inventory[index].quantity;
            theItemObject.item = inventory[index];
            Destroy(inventory[index].itemUIButton);
            inventory.RemoveAt(index); // Assume that if it isn't stackable, it only contains 1
        }
    }

    //For customization later on...
    public void RenameItem(Item item, string newName)
    {
        if (inventory.Exists(x => x.itemName == item.itemName))
        {
            int index = inventory.FindIndex(x => x.itemName == item.itemName);
            inventory[index].itemName = newName;
        }
    }
}
