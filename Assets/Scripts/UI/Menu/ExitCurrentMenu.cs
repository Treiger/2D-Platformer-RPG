using UnityEngine;
using System.Collections;

public class ExitCurrentMenu : MonoBehaviour
{
    public CanvasGroup inventoryUI;

    public string gameMenuAnimBool;
    public string thisAnimatorBool;

    public Animator thisAnimator;
    public Animator gameMenuAnimator;
    void Update()
    {
        // Fade out inventory and Fade in menu buttons when the player exits menu
        if (Input.GetButtonDown("Leave/Open Menu"))
        {
            thisAnimator.SetBool(thisAnimatorBool, false);
            gameMenuAnimator.SetBool(gameMenuAnimBool, false);
        }
        /*
        if (thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rest") && thisAnimator.GetBool(thisAnimatorBool) == false)
        {
            this.gameObject.SetActive(false);
        }
        */
	}
}
