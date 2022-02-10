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
            // Can place in any scene to set the spawn point of our Hero
            if (FindObjectOfType<FountainRespawn>()) {
                clone.transform.position = FindObjectOfType<FountainRespawn>().respawnPoint.transform.position;
            }
        }

        if(CameraController.instance == null) {
            CameraController.instance = Instantiate(cameraContainer).GetComponent<CameraController>();
        }
    }

}
