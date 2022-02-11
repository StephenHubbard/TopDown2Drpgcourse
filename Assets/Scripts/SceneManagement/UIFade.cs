using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private float fadeSpeed;

    public Image fadeScreen;

    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;

    private IEnumerator fadeCo;

    void Start() {
        FadeToClear();
    }
    
    public void FadeToBlack() 
    {
        if (fadeCo != null) {
            StopCoroutine(fadeCo);
        }

        fadeCo = FadeRoutine(fadeScreen, 1);
        StartCoroutine(fadeCo);
    }
    
    public void FadeToClear()
    {
        if (fadeCo != null) {
            StopCoroutine(fadeCo);
        }
        
        fadeCo = FadeRoutine(fadeScreen, 0);
        StartCoroutine(fadeCo);
    }
    
    IEnumerator FadeRoutine(Image image, float targetAlpha)
    {
        while(!Mathf.Approximately(image.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}