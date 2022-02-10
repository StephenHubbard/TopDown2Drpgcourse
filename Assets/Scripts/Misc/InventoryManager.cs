using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public int currentRupees;
    [SerializeField] private TMP_Text rupeeText;
    [SerializeField] private GameObject selectionBorder;
    [SerializeField] public GameObject currentSelectedItem;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Image activeSpriteUI;
    [SerializeField] public GameObject inventoryContainer;

    [SerializeField] public GameObject boomerangPrefabInv;
    [SerializeField] public GameObject bombPrefabInv;
    [SerializeField] public GameObject itemEquippedInv;

    public static InventoryManager instance;
    public enum CurrentEquippedItem { Boomerang, Bomb };
    public CurrentEquippedItem currentEquippedItem;


    private PlayerControls playerControls;


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

    private void Awake() {
        playerControls = new PlayerControls();

        Singleton();
    }

    private void Start() {
        playerControls.Inventory.OpenInventoryContainer.performed += _ => OpenInventoryContainer();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Update() {
        UpdateRupeeText();
        UpdateDetectIfItemChange();
    }
    

    private void Singleton() {
        if (instance == null) {
            instance = this;
        } else {
            if (instance != this) {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
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
        if (!currentSelectedItem.GetComponent<ItemDisplay>()) { return; }

        if (currentSelectedItem.GetComponent<ItemDisplay>().item.itemType == "Boomerang") {
            currentEquippedItem = CurrentEquippedItem.Boomerang;
            itemEquippedInv = boomerangPrefabInv;
        } else if (currentSelectedItem.GetComponent<ItemDisplay>().item.itemType == "Bomb") {
            currentEquippedItem = CurrentEquippedItem.Bomb;
            itemEquippedInv = bombPrefabInv;
        } 
    }

    private void updateSelectionBorder() {
        selectionBorder.transform.position = currentSelectedItem.transform.position;
    }

    public void UpdateRupeeText() {
        rupeeText.text = currentRupees.ToString("D3");
    }

    public void IncreaseRupeeCount(int amount) {
        currentRupees += amount;
    }
}
