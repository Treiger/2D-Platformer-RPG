using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> itemDatabase;
}

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public string type;
    public bool stackable;
    public Image itemImage;
    public GameObject itemObject;
    public float weight;
    public int value;

}


