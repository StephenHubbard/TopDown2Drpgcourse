using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    [SerializeField] private GameObject buttonUI;

    public string[] lines;

    private bool canActivate;

    public bool isPerson;

    void Start()
    {
        
    }

    void Update()
    {
        if(canActivate && Input.GetButtonDown("Fire2") && !DialogueManager.instance.dialogueBox.activeInHierarchy) {
            DialogueManager.instance.ShowDialogue(lines, isPerson);
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
