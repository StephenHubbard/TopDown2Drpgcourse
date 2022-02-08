using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private GameObject buttonUI;

    public string[] lines;
    public bool isPerson;

    private bool canActivate;
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
        playerControls.Spacebar.Use.performed += _ => OpenDialogue();
    }

    void Update()
    {
    }

    // BUG: script race condition between this and playerController if player moves in between scenes but not if starting scene is in town.
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
