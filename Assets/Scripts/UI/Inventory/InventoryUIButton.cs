using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button thisButton;
    public GameObject invManagerObj;
    public InventoryManager invManager;
    public Text nameText;
    public Text magText;
    public Text typeText;
    public Text damText;
    public Text armText;
    public Text weightText;
    public Text valText;

    void Start()
    {
        invManagerObj = GameObject.Find("Inventory");
        invManager = invManagerObj.GetComponent<InventoryManager>();
    }

    public void InitButton(Item i)
    {
        //i.itemUIButton = thisButton;
        nameText.text = i.itemName;
        magText.text = i.rating.ToString();
        typeText.text = i.type.ToString();
        damText.text = i.rating.ToString();
        armText.text = i.rating.ToString();
        weightText.text = i.weight.ToString();
        valText.text = i.value.ToString();
    }

    public void OnPointerEnter(PointerEventData e)
    {
        var invUI = invManager.currentUI;
        invUI.CancelInvoke("DisableSidePanel");
        invUI.SetSidePanelInfo(thisButton);
    }

    public void OnPointerExit(PointerEventData e)
    {
        var invUI = invManager.currentUI;
        invUI.Invoke("DisableSidePanel", 0.20f);
    }

}
