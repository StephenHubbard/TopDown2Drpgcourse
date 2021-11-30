using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private float fpsFloat;

    // debugging purposes only
    void Update()
    {
        fpsFloat = 1 / Time.unscaledDeltaTime;
        fpsText.text = fpsFloat.ToString("F0");
    }
}
