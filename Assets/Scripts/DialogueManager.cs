using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private bool recentlyOpened = true;

    public static DialogueManager instance;

    [SerializeField] private int currentLineIndex = 0;

    [SerializeField] private Text dialogueText;
    [SerializeField] private Text speakerNameText;
    [SerializeField] private string[] dialogueLines;

    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject speakerNameUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDialogueLine(dialogueLines[currentLineIndex]);
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of DialogueManager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueUI.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!recentlyOpened)
                {
                    if (currentLineIndex >= dialogueLines.Length)
                    {
                        dialogueUI.SetActive(false);
                        speakerNameUI.SetActive(false);
                        PlayerController.instance.EnableMovement();

                        currentLineIndex = 0;
                        ClearDialogueAndSpeakerText();
                        return;
                    }
                    CheckIfNameAndUpdateLine();
                    UpdateDialogueLine(dialogueLines[currentLineIndex]);
                } 
                else
                {
                    recentlyOpened = false;
                }
                
            }
            
        }
    }

    public void UpdateDialogueLine(string newLine)
    {
        if (dialogueUI.activeInHierarchy)
        {
            dialogueText.text = newLine;
            currentLineIndex++;
        }
    }

    public void DisplayDialogue(string[] newLines)
    {
        dialogueLines = newLines;
        ClearDialogueAndSpeakerText();
        CheckIfNameAndUpdateLine();
        dialogueText.text = dialogueLines[currentLineIndex];
        currentLineIndex++;
        
        dialogueUI.SetActive(true);
        speakerNameUI.SetActive(true);
        PlayerController.instance.DisableMovement();
    }

    public GameObject DialogueUI => dialogueUI;

    public void CheckIfNameAndUpdateLine()
    {
        if (currentLineIndex >= dialogueLines.Length)
        {
            return;
        }

        if (dialogueLines[currentLineIndex].StartsWith("n-"))
        {
            speakerNameText.text = dialogueLines[currentLineIndex].Replace("n-", "");
            currentLineIndex++;
        }
    }

    public void ClearDialogueAndSpeakerText()
    {
        dialogueText.text = "";
        speakerNameText.text = "";
    }
}
