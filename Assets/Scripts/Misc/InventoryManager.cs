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

    [SerializeField] private GameObject[] itemSlots;
    [SerializeField] private GameObject selectionBorder;
    [SerializeField] private GameObject currentSelectedItem;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Image activeSpriteUI;

    public static InventoryManager instance;



    private void Awake() {
        Singleton();
        currentSelectedItem = itemSlots[0];
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
        if (currentSelectedItem != eventSystem.currentSelectedGameObject) {
            currentSelectedItem = eventSystem.currentSelectedGameObject;
            selectionBorder.transform.position = currentSelectedItem.transform.position;
            activeSpriteUI.sprite = currentSelectedItem.GetComponent<Image>().sprite;
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
