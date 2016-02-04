using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class GameMenu : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup;

    public BlurOptimized blurEffect;
    public int sampleAmount;
    public float blurSize;
    public int blurIterations;

    public Player player;

    public Animator thisAnimator;

    public bool isMenuOpen;
    public bool fading;

    void Update()
    {
        if (Input.GetButtonDown("Leave/Open Menu") && !player.isInDialogue && !fading)
        {
            if (!isMenuOpen)
            {
                StartCoroutine("FadeInMenu");
                fading = true;
                player.isInMenu = true;
            }
            else if (!thisAnimator.GetBool("inMenu")) 
            {
                StartCoroutine("FadeOutMenu");
                fading = true;
            }
        }
    }


    IEnumerator FadeOutMenu()
    {
        float t = 0f;
        float b = 0f;
        menuCanvasGroup.interactable = false;
        while (!(t >= 1.0f))
        {
            t += Time.deltaTime;
            b += 8 * Time.deltaTime;
            menuCanvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, t);
            blurEffect.blurSize = Mathf.Lerp(blurSize, 0.0f, t);
            blurEffect.blurIterations = (int)Mathf.Lerp(blurIterations, 0.0f, b);
            blurEffect.downsample = (int)Mathf.Lerp(sampleAmount, 0.0f, b);
            yield return null;
        }
        if (t >= 1f)
        {
            blurEffect.enabled = false;
            isMenuOpen = false;
            fading = false;
            player.isInMenu = false;
        }
    }

    IEnumerator FadeInMenu()
    {
        blurEffect.enabled = true;
        float t = 0f;
        blurEffect.blurIterations = blurIterations;
        blurEffect.downsample = sampleAmount;
        while (!(t >= 1.0f))
        {
            t += Time.deltaTime;
            menuCanvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, t);
            blurEffect.blurSize = Mathf.Lerp(0.0f, blurSize, t);
            yield return null;
        }
        if (t >= 1f)
        {
            menuCanvasGroup.interactable = true;
            isMenuOpen = true;
            fading = false;
        }

  
    }

}
