using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] public GameObject dialogueText;
    [SerializeField] public GameObject nameBox;
    [SerializeField] public GameObject nameText;
    [SerializeField] public GameObject inventoryContainer;

    private PlayerControls playerControls;


    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Start() {
        playerControls.Inventory.OpenInventoryContainer.performed += _ => OpenInventoryContainer();
    }

    private void Update() {
        
    }

    private void OpenInventoryContainer() {
        if (inventoryContainer.gameObject.activeInHierarchy == false) {
            inventoryContainer.gameObject.SetActive(true);
            PlayerController.instance.PauseGame();
        }

        else if (inventoryContainer.gameObject.activeInHierarchy == true) {
            inventoryContainer.gameObject.SetActive(false);
            PlayerController.instance.UnpauseGame();
        }
    }
}
