using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Updates UI based on event system's current selected object. 
public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private GameObject selectionBorder;
    [SerializeField] public GameObject currentSelectedItem;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Image activeSpriteUI;
    [SerializeField] public GameObject inventoryContainer;

    public enum CurrentEquippedItem { Boomerang, Bomb };
    public CurrentEquippedItem currentEquippedItem;
    public GameObject itemEquippedInv;

    private PlayerControls playerControls;
    

    protected override void Awake() {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start() {
        playerControls.Inventory.OpenInventoryContainer.performed += _ => OpenInventoryContainer();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        if (playerControls != null) {
            playerControls.Disable();
        }
    }

    private void Update() {
        UpdateDetectIfItemChange();
    }
    

    private void OpenInventoryContainer() {
        if (inventoryContainer.gameObject.activeInHierarchy == false) {
            inventoryContainer.gameObject.SetActive(true);
            PlayerController.Instance.PauseGame();
        }

        else if (inventoryContainer.gameObject.activeInHierarchy == true) {
            inventoryContainer.gameObject.SetActive(false);
            PlayerController.Instance.UnpauseGame();
        }
    }

    private void UpdateDetectIfItemChange(){
        if (currentSelectedItem != eventSystem.currentSelectedGameObject || currentSelectedItem == null) {
            currentSelectedItem = eventSystem.currentSelectedGameObject;
            selectionBorder.transform.position = currentSelectedItem.transform.position;
            activeSpriteUI.sprite = currentSelectedItem.GetComponent<Image>().sprite;
            ChangeCurrentEquippedItem();
        }
    }

    public void ChangeCurrentEquippedItem() {
        ItemDisplay thisItem = currentSelectedItem.GetComponent<ItemDisplay>();
        
        if (thisItem) { 
            if (thisItem.item.itemType == "Boomerang") {
                currentEquippedItem = CurrentEquippedItem.Boomerang;
            } else if (thisItem.item.itemType == "Bomb") {
                currentEquippedItem = CurrentEquippedItem.Bomb;
            } 

            itemEquippedInv = thisItem.item.useItemPrefab;
        } else {
            itemEquippedInv = null;
        }

    }

    private void updateSelectionBorder() {
        selectionBorder.transform.position = currentSelectedItem.transform.position;
    }

    // see EventSystemSpawner.cs
    public void SetEventSystem(EventSystem newEventSystem) {
        eventSystem = newEventSystem;
    }
}
