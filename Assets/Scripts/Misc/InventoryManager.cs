using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public int currentRupees;
    [SerializeField] private TMP_Text rupeeText;

    public static InventoryManager instance;

    private void Awake() {
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
    }

    public void UpdateRupeeText() {
        rupeeText.text = currentRupees.ToString("D3");
    }

    public void IncreaseRupeeCount(int amount) {
        currentRupees += amount;
    }
}
