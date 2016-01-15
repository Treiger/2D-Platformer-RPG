using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class NPCDialogue 
{
	public string[] line;
	public int choiceTrans;
}

[System.Serializable]
public class Choices
{
	public string[] choice;
	public int[] dialogueTrans;
	//public bool delete = false;
    public Choices()
    {
        choice = null;
        dialogueTrans = null;
    }
	
}	

public class Character : MonoBehaviour 
{
	public string npcName;
	public Sprite portrait;
	public Canvas dialogueMenu;
	public Canvas dialogueMenuObject;
	public Transform dContentTransform;
	public CanvasGroup dCanvasGroup;
	public DialogueManager dialogueManager;
	public NPCDialogue[] npcDialogue;
	public Choices[] choices;
	[HideInInspector]
	public bool resetPlayerDialogueStatus;
	

	void OnTriggerStay2D(Collider2D other)
	{
		if (resetPlayerDialogueStatus)
		{
			other.GetComponent<Player>().isInDialogue = false;
			resetPlayerDialogueStatus = false;
			return;
		}
		bool alreadyFading = (dialogueManager.fadeIn || dialogueManager.fadeOut);
		if (other.CompareTag("Player") && Input.GetButtonDown ("Interact"))
		{
			if (alreadyFading || dialogueManager.dialogueShowing) return;
			other.GetComponent<Player>().isInDialogue = true;
			
			if (!dialogueManager.initiatedDialogue)
			{
				dialogueMenu = Instantiate (dialogueMenuObject);
				//Find all the Components of the Canvas that Dialogue Manager needs to use.
				dContentTransform = dialogueMenu.transform.Find ("Choice View BG/Choice View/Choice Panel/Choice Panel Content");
				dCanvasGroup = dialogueMenu.GetComponent<CanvasGroup>();
				dialogueManager.npcPortrait = dialogueMenu.transform.Find ("PortraitPanel/Portrait").GetComponent<Image>();
				dialogueManager.npcDialogueDisplay = dialogueMenu.transform.FindChild("NPCDialogue").GetComponent<Text>();
				dialogueManager.npcNameDisplay = dialogueMenu.transform.FindChild("NPCName").GetComponent<Text>();
				
				dialogueManager.fadeIn = true;
				dialogueManager.dialogueShowing = true;
				if (!dialogueManager.inDialogue && !dialogueManager.inChoice)
				{
                    dialogueManager.choiceRef = choices[0];
                    dialogueManager.NPCInfoInit(gameObject);
				}
				dialogueManager.initiatedDialogue = true;
			}
			else 
			{
				dialogueMenu.enabled = true;
				dialogueManager.fadeIn = true;
				dialogueManager.dialogueShowing = true;
                dialogueManager.CancelInvoke("ResetDialogue");
			}
		}
		else if (other.CompareTag("Player") && Input.GetButtonDown ("Leave/Open Menu"))
		{	
			if (alreadyFading || !dialogueManager.dialogueShowing) return;
			dialogueManager.fadeOut = true;
			dialogueManager.dialogueShowing = false;
		}
	}

    public void SendDialogueInfo(int choiceIndex)
    {
        for (int i = 0; i < npcDialogue[choiceIndex].line.Length; i++)
        {
            dialogueManager.dialogueTree.Add(npcName + ": " + npcDialogue[choiceIndex].line[i]);
        }
        int cTrans = npcDialogue[choiceIndex].choiceTrans;
        if (cTrans != -1) dialogueManager.choiceRef = choices[choiceIndex];
        else dialogueManager.choiceRef = new Choices();
        dialogueManager.inDialogue = true;
        dialogueManager.inChoice = false;
        dialogueManager.npcDialogueDisplay.text = dialogueManager.dialogueTree[0];
    }
}

