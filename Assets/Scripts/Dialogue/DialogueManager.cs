using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] public GameObject dialogueBox;
    [SerializeField] private GameObject nameBox;
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private int currentLine;
    [SerializeField] private GameObject uiCanvas;

    public bool justStarted;

    public static DialogueManager instance;

    void Start()
    {
        instance = this;

        uiCanvas = GameObject.Find("UI Canvas");
        UpdateDialogueGameObjects();
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
                if (Input.GetButtonUp("Fire2"))
                {

                    if (!justStarted)
                    {
                    currentLine++;

                    if (currentLine >= dialogueLines.Length)
                    {
                        dialogueBox.SetActive(false);
                        justStarted = true;
                        PlayerController.instance.canMove = true;
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
        } else {
            UpdateDialogueGameObjects();
        }
    }

    private void UpdateDialogueGameObjects() {
        SceneManagement sceneManagement = uiCanvas.GetComponent<SceneManagement>();
        dialogueBox = sceneManagement.dialogueBox;
        dialogueText = sceneManagement.dialogueText.GetComponent<TMP_Text>();
        nameBox = sceneManagement.nameBox;
        nameText = sceneManagement.nameText.GetComponent<TMP_Text>();
    }

    public void ShowDialogue(string[] newLines, bool isPerson) {
        dialogueLines = newLines;

        currentLine = 0;

        CheckIfName();

        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);

        justStarted = true;

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
