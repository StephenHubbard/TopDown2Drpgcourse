using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Put on gameobjects that can be toggled with opening the dialogue window (currently spacebar).  If isPerson isn't toggled true, the name box window will not appear.
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

    private void OpenDialogue() {
        if (canActivate) {
            if(!DialogueManager.Instance.dialogueBox.activeInHierarchy) {
                DialogueManager.Instance.ShowDialogue(lines, isPerson);
                PlayerController.Instance.canAttack = false;
                PlayerController.Instance.DialogueStopMove();
            } else {
                DialogueManager.Instance.ShowDialogueWindow();
            }
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
