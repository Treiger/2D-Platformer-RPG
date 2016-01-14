using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIButtonRecede : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Animator[] otherAnimators;
    public GameMenu gameMenu;
    public CanvasGroup mainCanvasGroup;
    public GameObject particleHover;
    GameObject theParticle;
    bool isInside;
    bool fadeTransition;

    void Update()
    {
        if (!gameMenu.isMenuOpen && isInside)
        {
            fadeTransition = true;
        }
        else if (!isInside) fadeTransition = false;
       
        if (gameMenu.isMenuOpen && isInside && fadeTransition)
        {
            fadeTransition = false;
            StartCoroutine("PlayPopoutAnim");
        }

    }

	public void OnPointerEnter(PointerEventData eventData)
    {
        isInside = true;
        fadeTransition = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isInside = false;
        PlayRecedeAnim();
    }

    IEnumerator PlayPopoutAnim()
    {
        while (!mainCanvasGroup.interactable)
        {
            yield return null;
        }
        if (mainCanvasGroup.interactable)
        {
            for (int i = 0; i < otherAnimators.Length; i++)
            {
                otherAnimators[i].SetTrigger("Recede");
            }
      
        }
        if (theParticle == null)
        {
            theParticle = Instantiate(particleHover);
            theParticle.transform.SetParent(this.transform);
            theParticle.transform.localPosition = new Vector3(227.0f, 0.0f, 0.0f);
            theParticle.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            theParticle.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void PlayRecedeAnim()
    {
        for (int i = 0; i < otherAnimators.Length; i++)
        {
            otherAnimators[i].ResetTrigger("Recede");
            otherAnimators[i].SetTrigger("Normal");
        }

        if (theParticle != null)
        {
            Destroy(theParticle);
        }

    }
}
