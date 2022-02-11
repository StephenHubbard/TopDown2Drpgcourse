using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] private GameObject cameraContainer;
    [SerializeField] private GameObject player;

    void Start()
    {

        if(PlayerController.Instance == null) {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();

            // Can place in any scene to set the spawn point of our hero in that scene
            if (FindObjectOfType<FountainRespawn>()) {
                clone.transform.position = FindObjectOfType<FountainRespawn>().respawnPoint.transform.position;
            }
        }

        if(CameraController.Instance == null) {
            Instantiate(cameraContainer).GetComponent<CameraController>();
        }

    }

}
