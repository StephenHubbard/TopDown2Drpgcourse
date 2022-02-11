using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private int currentLine;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] GameObject nameBox;

    public bool justStarted;

    private string[] dialogueLines;

    public void ShowDialogueWindow()
    {
        if (!justStarted)
        {
        currentLine++;
            if (currentLine >= dialogueLines.Length)
            {
                dialogueBox.SetActive(false);
                justStarted = true;
                PlayerController.Instance.canMove = true;
                PlayerController.Instance.canAttack = true;
            }
            else
            {
                CheckIfName();
                dialogueText.text = dialogueLines[currentLine];
            }
        }
        else
        {
            justStarted = false;
        }
    }
    
    public void ShowDialogue(string[] newLines, bool isPerson) {
        justStarted = true;
        dialogueLines = newLines;
        currentLine = 0;
        CheckIfName();
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        nameBox.SetActive(isPerson);
        PlayerController.Instance.canMove = false;
        ShowDialogueWindow();
    }

    public void CheckIfName() {
        if (dialogueLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}