using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{

    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private float fpsFloat;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fpsFloat = 1 / Time.unscaledDeltaTime;
        fpsText.text = fpsFloat.ToString("F0");
    }
}
