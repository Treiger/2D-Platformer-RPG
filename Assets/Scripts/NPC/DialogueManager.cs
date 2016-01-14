using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{	
	public GameObject choiceButton;
	public RectTransform choiceButtonRect;
	
	[HideInInspector]
	public bool dialogueShowing;
	public bool initiatedDialogue;
	
	[HideInInspector]
	public bool fadeIn;
	[HideInInspector]
	public bool fadeOut;
	
	public float fadeTime;
	float currentFadeTime;
	
	public float dialogueFadeTime;
	float currentDialogueFadeTime;
	
	[HideInInspector]
	public string npcName;
	public Text npcDialogueDisplay;
	public Text npcNameDisplay;
	[HideInInspector]
	public Character npc;
	[HideInInspector]
	public Image npcPortrait;
	
	public bool inDialogue; // player is reading dialogue
	public bool inChoice; // making choices
	public bool dialogueInitial; // flag for detecting when dialogue is first initialized
	public Choices choiceRef;
	
	
	public List<GameObject> currentChoices = new List<GameObject>(); //currentChoices to display
	public List<string> dialogueTree = new List<string>();
	
	void Update () 
	{
		if (fadeIn) FadeInMenu();
		else if (fadeOut) FadeOutMenu();

        if (initiatedDialogue && !dialogueShowing && !IsInvoking("ResetDialogue"))
        {
            Invoke("ResetDialogue", 5.0f);
            return;
        }
        
		
		if (!initiatedDialogue || !dialogueShowing) return;

		if (dialogueInitial)
		{	
			choiceRef = npc.choices[0];
			for (int i = 0; i < npc.choices[0].choice.Length; i++)
			{
				GameObject aChoice = Instantiate (choiceButton);
				aChoice.gameObject.transform.SetParent(npc.dContentTransform,false);
				aChoice.GetComponentInChildren<Text>().text = npc.choices[0].choice[i];
				Button b = aChoice.GetComponent<Button>();
				int index = i;
				b.onClick.AddListener (() => SendChoiceInfo (index));
				currentChoices.Add(aChoice);
			}
				dialogueInitial = false;
				inChoice = true;
		}
		else if (inDialogue)
		{
			if (Input.GetButtonDown ("Fire1"))
			{
				if (dialogueTree.Count != 1)
				{
					dialogueTree.RemoveAt(0);
				}
				else if (dialogueTree.Count == 1 && choiceRef.choice == null)
				{	
					foreach (var choice in currentChoices)
					{
						Destroy (choice);
					}
					currentChoices.Clear();
					dialogueTree.Clear ();
					inDialogue = false;
					inChoice = false;
					choiceRef = new Choices();
					fadeOut = true;
					dialogueShowing = false;
					npcDialogueDisplay.text = "";
					currentDialogueFadeTime = 0.0f;
					initiatedDialogue = false;
				}
				else
				{
					currentDialogueFadeTime = 0.0f;
					CreateChoices();
				}
			}
			
		}
		else if (inChoice)
		{
			if (currentDialogueFadeTime >= 0.0f)
			{
				currentDialogueFadeTime += Time.deltaTime;	
			}
			if (currentDialogueFadeTime >= dialogueFadeTime)
			{
				dialogueTree.RemoveAt(0);
				npcDialogueDisplay.text = "";
				currentDialogueFadeTime = -1.0f;
			}
			
		}
		
	}
	
	public void FadeInMenu()
	{
		currentFadeTime += fadeTime * Time.deltaTime;
		npc.dCanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, currentFadeTime); 
		if (npc.dCanvasGroup.alpha >= 1) 
		{
			fadeIn = false; 
			currentFadeTime = 0f;
		} 
	}
	public void FadeOutMenu()
	{
		currentFadeTime += fadeTime * Time.deltaTime;
		npc.dCanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, currentFadeTime); 
		if (npc.dCanvasGroup.alpha <= 0) 
		{
			fadeOut = false; 
			currentFadeTime = 0f;
			npc.resetPlayerDialogueStatus = true;
			//initiatedDialogue = false;
			if (!initiatedDialogue)
			{
				Destroy (npc.dialogueMenu.gameObject);
			}
			else 
			{
				npc.dialogueMenu.enabled = false;
			}
		} 
	}
	
	public void NPCInfoInit(GameObject character)
	{	
		npc = character.GetComponent<Character>();
		npcName =  npc.npcName;
		npcNameDisplay.text = npcName;
		npcPortrait.sprite = npc.portrait;
		for (int i = 0; i < npc.npcDialogue[0].line.Length; i++)
		{
			dialogueTree.Add (npcName + ": " + npc.npcDialogue[0].line[i]);
		}
		dialogueInitial = true;
		npcDialogueDisplay.text = dialogueTree[0];
	}
	
	public void SendChoiceInfo(int choiceIndex)
	{
		if (currentDialogueFadeTime != -1.0f)
		{
			dialogueTree.Clear();
			currentDialogueFadeTime = -1.0f;
		}
		GameObject chosenChoice = currentChoices[choiceIndex];
		foreach (var choice in currentChoices)
		{
			if (choice != chosenChoice)
			{
				choice.GetComponent<Button>().enabled = false;
				choice.GetComponent<Button>().interactable = false;
				Destroy(choice.GetComponent<Image>());
				Destroy(choice.GetComponentInChildren<Text>());
			}
			else 
			{
				choice.GetComponent<Button>().interactable = false;
			}
		}
		//currentChoices = new List<GameObject>{currentChoices[choiceIndex]};
		npc.SendDialogueInfo(choiceRef.dialogueTrans[choiceIndex]);
	}
	
	public void CreateChoices()
	{
		foreach (var choice in currentChoices)
		{
			Destroy (choice);
		}
		
		currentChoices.Clear();

		for (int i = 0; i < choiceRef.choice.Length; i++)
		{
			GameObject aChoice = Instantiate (choiceButton);
			aChoice.gameObject.transform.SetParent(npc.dContentTransform,false);
			aChoice.GetComponentInChildren<Text>().text = choiceRef.choice[i];
			Button b = aChoice.GetComponent<Button>();
			int index = i;
			b.onClick.AddListener (() => SendChoiceInfo (index));
			currentChoices.Add(aChoice);
		}
		inDialogue = false;
		inChoice = true;
	}

    public void ResetDialogue()
    {
        foreach (var choice in currentChoices)
        {
            Destroy(choice);
        }
        currentChoices.Clear();
        dialogueTree.Clear();
        inDialogue = false;
        inChoice = false;
        choiceRef = new Choices();
        fadeOut = true;
        dialogueShowing = false;
        npcDialogueDisplay.text = "";
        currentDialogueFadeTime = 0.0f;
        initiatedDialogue = false;
    }

}
