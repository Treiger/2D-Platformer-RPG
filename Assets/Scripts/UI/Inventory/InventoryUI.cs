using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryUI : MonoBehaviour
{
    public InventoryManager inv;

    public Button itemButton;
    public Transform contentTransform;
    public InventoryTabs inventoryTabs;
    public InventorySidePanel sidePanel;

    public InputField filter;

    public string currentInvTab;

    public int sortDirection; // -1 descending (default), 1 asecending
    public string currentSortHeading;

    public Image currentSortImage;
    //Sort Arrow Images
    public Image nameSortArrow;
    public Image typeSortArrow;
    public Image magSortArrow;
    public Image damSortArrow;
    public Image armSortArrow;
    public Image wgtSortArrow;
    public Image valSortArrow;

    public bool inventoryActivated;
	
    void Start()
    {
        SortInventory("Name");
        filter.onValueChange.AddListener(delegate { FilterList(); });
        currentInvTab = inventoryTabs.currentTab;
    }

    void Update()
    {
        if (currentSortHeading == "Type" && currentSortImage.gameObject.transform.parent.gameObject.activeInHierarchy == false)
        {
            SortInventory("Name");
        }
        if (currentInvTab == inventoryTabs.prevTab)
        {
            FilterList();
        }

        currentInvTab = inventoryTabs.currentTab;
    }


    public void SortInventory(string sortHeading)
    {
        switch (sortHeading)
        {
            case "Name":
                if (sortHeading == currentSortHeading)
                {
                    NewSortDirection();
                    UpdateItemButtonHierarchy();
                    return;
                }
                currentSortImage.gameObject.SetActive(false);
                currentSortImage = nameSortArrow;
                currentSortImage.gameObject.SetActive(true);
                sortDirection = -1;
                Comparison<Item> compnamedel = new Comparison<Item>(Item.CompareItemName);
                inv.inventory.Sort(compnamedel);
                UpdateItemButtonHierarchy();
                currentSortHeading = sortHeading;
                break;

            case "Weight":
                if (sortHeading == currentSortHeading)
                {
                    NewSortDirection();
                    UpdateItemButtonHierarchy();
                    return;
                }
                currentSortImage.gameObject.SetActive(false);
                currentSortImage = wgtSortArrow;
                currentSortImage.gameObject.SetActive(true);
                sortDirection = -1;
                Comparison<Item> compweightdel = new Comparison<Item>(Item.CompareItemWeight);
                inv.inventory.Sort(compweightdel);
                UpdateItemButtonHierarchy();
                currentSortHeading = sortHeading;
                break;

            case "Value":
                if (sortHeading == currentSortHeading)
                {
                    NewSortDirection();
                    UpdateItemButtonHierarchy();
                    return;
                }
                currentSortImage.gameObject.SetActive(false);
                currentSortImage = valSortArrow;
                currentSortImage.gameObject.SetActive(true);
                sortDirection = -1;
                Comparison<Item> compvaluedel = new Comparison<Item>(Item.CompareItemValue);
                inv.inventory.Sort(compvaluedel);
                UpdateItemButtonHierarchy();
                currentSortHeading = sortHeading;
                break;
            case "Rating":
                if (sortHeading == currentSortHeading)
                {
                    NewSortDirection();
                    UpdateItemButtonHierarchy();
                    return;
                }
                switch (inventoryTabs.currentTab)
                {
                    case "Weapons":
                        currentSortImage.gameObject.SetActive(false);
                        currentSortImage = damSortArrow;
                        currentSortImage.gameObject.SetActive(true);
                        break;
                    case "Armor":
                        currentSortImage.gameObject.SetActive(false);
                        currentSortImage = armSortArrow;
                        currentSortImage.gameObject.SetActive(true);
                        break;
                    case "Potions":
                        currentSortImage.gameObject.SetActive(false);
                        currentSortImage = magSortArrow;
                        currentSortImage.gameObject.SetActive(true);
                        break;
                }
                sortDirection = -1;
                Comparison<Item> compratingdel = new Comparison<Item>(Item.CompareItemRating);
                inv.inventory.Sort(compratingdel);
                UpdateItemButtonHierarchy();
                currentSortHeading = sortHeading;
                break;
            case "Type":
                if (sortHeading == currentSortHeading)
                {
                    NewSortDirection();
                    UpdateItemButtonHierarchy();
                    return;
                }
                if (!inventoryTabs.subTypeCat)
                {
                    currentSortImage.gameObject.SetActive(false);
                    currentSortImage = typeSortArrow;
                    currentSortImage.gameObject.SetActive(true);
                    sortDirection = -1;
                    Comparison<Item> comptypedel = new Comparison<Item>(Item.CompareItemType);
                    inv.inventory.Sort(comptypedel);
                    UpdateItemButtonHierarchy();
                    currentSortHeading = sortHeading;
                }
                else
                {
                    currentSortImage.gameObject.SetActive(false);
                    currentSortImage = typeSortArrow;
                    currentSortImage.gameObject.SetActive(true);
                    sortDirection = -1;
                    Comparison<Item> compsubtypedel = new Comparison<Item>(Item.CompareItemSubType);
                    inv.inventory.Sort(compsubtypedel);
                    UpdateItemButtonHierarchy();
                    currentSortHeading = sortHeading;
                }
                break;
        }
    }

    public void UpdateItemButtonHierarchy()
    {
        if (sortDirection == -1)
        {
            int i = 0;
            foreach (var item in inv.inventory)
            {
                item.itemUIButton.transform.SetSiblingIndex(i);
                i++;
            }
        }
        else
        {
            int i = inv.inventory.Count - 1;
            foreach (var item in inv.inventory)
            {
                item.itemUIButton.transform.SetSiblingIndex(i);
                i--;
            }
        }
    }

    public void NewSortDirection()
    {
       sortDirection = (sortDirection == 1) ? -1 : 1;
    }
    
    public void ChangeSortImageDirection(Image sortImage)
    {
        sortImage.transform.localScale = (sortDirection == 1) ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);
    }

    public void FilterList()
    {
        inventoryTabs.SetCategories(inventoryTabs.currentTab);
        foreach (Item item in inv.inventory)
        {
            var itemLower = item.itemName.ToLower();
            var filterText = filter.text.ToLower();
            if (!itemLower.Contains(filterText))
            {
                item.itemUIButton.gameObject.SetActive(false);
            }
        }
        
    }

    public void DisablePlaceholderText()
    {
        filter.placeholder.gameObject.SetActive(false);
    }

    public void EndEditManagePlaceholderText()
    {
        if (filter.text == "")
        {
            filter.placeholder.gameObject.SetActive(true);
        }
        else filter.placeholder.gameObject.SetActive(false);
    }

    public void DisableSidePanel()
    {
        sidePanel.gameObject.SetActive(false);
    }

    public void SetSidePanelInfo(Button button)
    {
        sidePanel.gameObject.SetActive(true);
        var theInv = inv.inventory;
        var i = button.transform.GetSiblingIndex();
        sidePanel.panelNameText.text = theInv[i].itemName;
        sidePanel.panelValueText.text = theInv[i].value.ToString();
        sidePanel.panelWeightText.text = theInv[i].weight.ToString();
        sidePanel.panelDescriptionText.text = theInv[i].description;

        switch(theInv[i].type.ToString())
        {
            case "Weapon":
                sidePanel.panelRatingNameText.text = "DAMAGE";
                sidePanel.panelRatingValueText.text = theInv[i].rating.ToString();
                break;
            case "Armor":
                sidePanel.panelRatingNameText.text = "ARMOR";
                sidePanel.panelRatingValueText.text = theInv[i].rating.ToString();
                break;
            case "Potion":
                sidePanel.panelRatingNameText.text = "MAGNITUDE ";
                sidePanel.panelRatingValueText.text = theInv[i].rating.ToString();
                break;
        }
    }
}
