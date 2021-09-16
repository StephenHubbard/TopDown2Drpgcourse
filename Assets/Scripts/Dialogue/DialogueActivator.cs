using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private GameObject buttonUI;

    public string[] lines;

    private bool canActivate;

    public bool isPerson;
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

    void Start()
    {
        playerControls.RightClick.Use.performed += _ => OpenDialogue();
    }

    void Update()
    {
    }

    private void OpenDialogue() {
        if(canActivate && !DialogueManager.instance.dialogueBox.activeInHierarchy) {
            DialogueManager.instance.ShowDialogue(lines, isPerson);
            PlayerController.instance.canAttack = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            buttonUI.SetActive(true);
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player") {
            buttonUI.SetActive(false);
            canActivate = false;
        }
    }
}
