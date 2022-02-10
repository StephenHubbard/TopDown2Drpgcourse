using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private int currentLine;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] GameObject nameBox;

    public static DialogueManager instance;
    public bool justStarted;


    private string[] dialogueLines;

    void Start()
    {
        instance = this;

    }

    void Update()
    {
        ShowDialogueWindow();
    }

    private void ShowDialogueWindow()
    {
        if (dialogueBox != null) {
            if (dialogueBox.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (!justStarted)
                    {
                    currentLine++;
                        if (currentLine >= dialogueLines.Length)
                        {
                            dialogueBox.SetActive(false);
                            justStarted = true;
                            PlayerController.instance.canMove = true;
                            PlayerController.instance.canAttack = true;
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
            }
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
        PlayerController.instance.canMove = false;
    }

    public void CheckIfName() {
        if (dialogueLines[currentLine].StartsWith("n-")) {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}