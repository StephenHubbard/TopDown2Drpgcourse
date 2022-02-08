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

    public static InventoryManager instance;
    public enum CurrentEquippedItem { Boomerang, Bomb };
    public CurrentEquippedItem currentEquippedItem;

    private void Awake() {
        Singleton();
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

    private void Update() {
        UpdateRupeeText();
        UpdateDetectIfItemChange();
    }

    private void UpdateDetectIfItemChange(){
        if (currentSelectedItem != eventSystem.currentSelectedGameObject || currentSelectedItem == null) {
            if (eventSystem.currentSelectedGameObject == null) {
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            }
            currentSelectedItem = eventSystem.currentSelectedGameObject;
            selectionBorder.transform.position = currentSelectedItem.transform.position;
            activeSpriteUI.sprite = currentSelectedItem.GetComponent<Image>().sprite;
            ChangeCurrentEquippedItem();
        }
    }

    private void ChangeCurrentEquippedItem() {
        if (!currentSelectedItem.GetComponent<ItemDisplay>()) { return; }

        if (currentSelectedItem.GetComponent<ItemDisplay>().item.itemType == "Boomerang") {
            currentEquippedItem = CurrentEquippedItem.Boomerang;
        } else if (currentSelectedItem.GetComponent<ItemDisplay>().item.itemType == "Bomb") {
            currentEquippedItem = CurrentEquippedItem.Bomb;
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
