using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for managing the dialogue system in the game.
/// It handles displaying dialogue lines, updating the speaker's name, and managing the dialogue UI.
/// </summary>
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

    /// <summary>
    /// In the Start method, we initialize the dialogue system by displaying the first line of dialogue and setting up the singleton instance of the DialogueManager.
    /// We also check for multiple instances of the DialogueManager and destroy any duplicates to ensure that there is only one instance managing the dialogue system.
    /// </summary>
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

    /// <summary>
    /// In the Update method, we check if the dialogue UI is active 
    /// and if the player has released the "Fire1" button (usually left mouse button or a key).
    /// </summary>
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
                        GameManager.instance.EndDialogue();
                        currentLineIndex = 0;
                        dialogueUI.SetActive(false);
                        speakerNameUI.SetActive(false);
                        
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

    /// <summary>
    /// This method updates the dialogue line displayed in the dialogue UI.
    /// It also increments the current line index to prepare for the next line of dialogue.
    /// </summary>
    /// <param name="newLine"></param>
    public void UpdateDialogueLine(string newLine)
    {
        if (dialogueUI.activeInHierarchy)
        {
            dialogueText.text = newLine;
            currentLineIndex++;
        }
    }

    /// <summary>
    /// This method is called to display a new set of dialogue lines. 
    /// It takes an array of strings (the dialogue lines) and a boolean indicating whether the speaker is a person (which determines whether the speaker name UI should be active).
    /// </summary>
    /// <param name="newLines"></param>
    /// <param name="isPerson"></param>
    public void DisplayDialogue(string[] newLines, bool isPerson)
    {
        dialogueLines = newLines;
        ClearDialogueAndSpeakerText();
        CheckIfNameAndUpdateLine();
        dialogueText.text = dialogueLines[currentLineIndex];
        //currentLineIndex++;
        
        dialogueUI.SetActive(true);
        speakerNameUI.SetActive(isPerson);
        GameManager.instance.StartDialogue();
    }

    public GameObject DialogueUI => dialogueUI;

    /// <summary>
    /// This method checks separates the speaker's name from the dialogue line based on a specific format (lines that start with "n-" indicate a speaker's name).
    /// If the current dialogue line starts with "n-", it updates the speaker name text and increments the current line index to move to the next line of dialogue.
    /// </summary>
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

    /// <summary>
    /// This method clears the dialogue text and speaker name text in the dialogue UI.
    /// </summary>
    public void ClearDialogueAndSpeakerText()
    {
        dialogueText.text = "";
        speakerNameText.text = "";
    }
}
