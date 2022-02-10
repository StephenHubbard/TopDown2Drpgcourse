using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;

    public Image fadeScreen;
    public static UIFade instance;

    private bool shouldFadeToBlack;
    private bool shouldFadeFromBlack;
    
    // private IEnumerator currentCo

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        FadeToClear();
    }
    
    public void FadeToBlack() 
    {
        // StopAllCoroutines in case of overlap, stalling out the image alpha
        StopAllCoroutines();
        StartCoroutine(Fade(fadeScreen, 1));
    }
    
    public void FadeToClear()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(fadeScreen, 0));
    }
    
    IEnumerator Fade(Image image, float targetAlpha)
    {
        while(!Mathf.Approximately(image.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}