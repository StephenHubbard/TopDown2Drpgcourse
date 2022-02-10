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
        StartCoroutine(Fade(fadeScreen, 1));
    }
    
    public void FadeToClear()
    {
        StartCoroutine(Fade(fadeScreen, 0));
    }
    
    IEnumerator Fade(Image image, float targetAlpha)
    {
        while(image.color.a != targetAlpha) // Ask Gary a work around 
        {
            float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
}