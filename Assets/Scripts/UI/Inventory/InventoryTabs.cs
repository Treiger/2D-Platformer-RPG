using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryTabs : MonoBehaviour
{
    public Color normalColor;
    public Color selectedColor;

    public InventoryManager invManager;

    public Button[] tabButtons;
    public string prevTab;
    public string currentTab;
    public bool subTypeCat;
    //Category Text
    public Text catText;
    //Heading Text
    public Text nameCat;
    public Text magCat;
    public Text typeCat;
    public Text damCat;
    public Text armCat;
    public Text weightCat;
    public Text valCat;
    
    /* Is called when a tab is clicked. It disables and enables the headings needed and loops through all buttons to disable and enable 
    text components depending on the section. 
    */
	public void SetCategories(string category)
    {
        prevTab = currentTab;
        catText.text = category;
        switch(category)
        {
            case "Favorites":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.favorited)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "All":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    item.itemUIButton.gameObject.SetActive(true);
                    var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                    buttonComponent.typeText.gameObject.SetActive(false);
                    buttonComponent.damText.gameObject.SetActive(false);
                    buttonComponent.armText.gameObject.SetActive(false);
                    buttonComponent.magText.gameObject.SetActive(false);
                    if (item.stackable && item.quantity > 1)
                    {
                        buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                    }
                    else buttonComponent.nameText.text = item.itemName;
                }
                break;
            case "Weapons":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(true);
                damCat.gameObject.SetActive(true);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = true;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type.ToString() == "Weapon")
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(true);
                        string subText;
                        if (item.subTypeDict.TryGetValue((int)item.subType, out subText))
                        {
                            buttonComponent.typeText.text = subText;
                        } else buttonComponent.typeText.text = item.type.ToString();
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                        buttonComponent.damText.gameObject.SetActive(true);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }
                break;
            case "Armor":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(true);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(true);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = true;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type.ToString() == "Armor")
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(true);
                        string subText;
                        if (item.subTypeDict.TryGetValue((int)item.subType, out subText))
                        {
                            buttonComponent.typeText.text = subText;
                        }
                        else buttonComponent.typeText.text = item.type.ToString();
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(true);
                        buttonComponent.magText.gameObject.SetActive(false);
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Potions":
                magCat.gameObject.SetActive(true);
                typeCat.gameObject.SetActive(true);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = true;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Potion)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(true);
                        string subText;
                        if (item.subTypeDict.TryGetValue((int)item.subType, out subText))
                        {
                            buttonComponent.typeText.text = subText;
                        }
                        else buttonComponent.typeText.text = item.type.ToString();
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(true);
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Scrolls":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Note)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Ingredients":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Ingredient)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Books":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Book)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Keys":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Key)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
            case "Misc":
                magCat.gameObject.SetActive(false);
                typeCat.gameObject.SetActive(false);
                damCat.gameObject.SetActive(false);
                armCat.gameObject.SetActive(false);
                weightCat.gameObject.SetActive(true);
                valCat.gameObject.SetActive(true);
                currentTab = category;
                subTypeCat = false;

                foreach (Item item in invManager.inventory)
                {
                    if (item.type == Item.Type.Misc)
                    {
                        item.itemUIButton.gameObject.SetActive(true);
                        var buttonComponent = item.itemUIButton.GetComponent<InventoryUIButton>();
                        buttonComponent.typeText.gameObject.SetActive(false);
                        buttonComponent.damText.gameObject.SetActive(false);
                        buttonComponent.armText.gameObject.SetActive(false);
                        buttonComponent.magText.gameObject.SetActive(false);
                        if (item.stackable && item.quantity > 1)
                        {
                            buttonComponent.nameText.text = item.itemName + " " + "(" + item.quantity + ")";
                        }
                        else buttonComponent.nameText.text = item.itemName;
                    }
                    else
                    {
                        item.itemUIButton.gameObject.SetActive(false);
                    }
                }

                break;
        }
    }

    public void SetButtonColors(Button excludedButton)
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            if (excludedButton == tabButtons[i])
            {
                tabButtons[i].GetComponent<Image>().color = selectedColor;
            }
            else tabButtons[i].GetComponent<Image>().color = normalColor;
        }
    }
}
