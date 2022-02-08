using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private int currentLine;
    [SerializeField] public GameObject dialogueBox;

    public static DialogueManager instance;
    public bool justStarted;

    private TMP_Text dialogueText;
    private TMP_Text nameText;
    private GameObject nameBox;
    private GameObject uiCanvas;
    private string[] dialogueLines;

    private PlayerControls playerControls;

    private void Awake() {
        instance = this;
        playerControls = new PlayerControls();
        uiCanvas = GameObject.Find("UI Canvas");
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    void Start()
    {
        UpdateDialogueGameObjects();

        playerControls.Spacebar.Use.performed += _ => ShowDialogueWindow();
    }


    private void ShowDialogueWindow()
    {
        if (dialogueBox != null) {
            if (dialogueBox.activeInHierarchy)
            {
                if (!justStarted)
                {
                currentLine++;
                    if (currentLine >= dialogueLines.Length)
                    {
                        PlayerController.instance.toggleGameState();
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
                    PlayerController.instance.toggleGameState();
                }
            }
        } 
        
    }

    // Fail safe for singleton(s) scene Transitions
    private void UpdateDialogueGameObjects() {
        SceneManagement sceneManagement = uiCanvas.GetComponent<SceneManagement>();
        dialogueBox = sceneManagement.dialogueBox;
        dialogueText = sceneManagement.dialogueText.GetComponent<TMP_Text>();
        nameBox = sceneManagement.nameBox;
        nameText = sceneManagement.nameText.GetComponent<TMP_Text>();
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
