using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] private GameObject cameraContainer;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject player;


    void Start()
    {
        if (UIFade.instance == null) {
            UIFade.instance = Instantiate(uiCanvas).GetComponent<UIFade>();
        }

        if(PlayerController.instance == null) {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }

        if(CameraController.instance == null) {
            CameraController.instance = Instantiate(cameraContainer).GetComponent<CameraController>();
        }
    }

    void Update()
    {
        
    }
}
