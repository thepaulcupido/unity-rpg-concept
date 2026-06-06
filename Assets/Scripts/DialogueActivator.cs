using UnityEngine;

/// <summary>
/// This class is responsible for activating dialogue when the player interacts with certain game objects.
/// It checks for player proximity and input to trigger the dialogue, and can also set up quest activation at the end of the dialogue if specified. The dialogue lines and speaker information can be configured in the Unity Inspector.
/// </summary>
public class DialogueActivator : MonoBehaviour
{
    // private variables
    private bool canActivate = false;

    // inspector-available fields
    [Header("Dialogue Activator Settings")]
    [SerializeField] private bool isPerson = true;
    [SerializeField] private string[] lines;

    [Header("Quest Activation Settings")]
    [SerializeField] private bool shouldActivateQuestAtEndOfDialogue;
    [SerializeField] private string questToMark;
    [SerializeField] private bool markQuestComplete;


    /// <summary>
    /// In the Update method, we check if the player can activate the dialogue based on proximity and input.
    /// We then display the dialogue lines and set up quest activation if specified.
    /// </summary>
    void Update()
    {
        if (
            canActivate && 
            Input.GetButtonDown("Fire1") && 
            lines.Length > 0 && 
            !DialogueManager.instance.DialogueUI.activeInHierarchy
        ) {
            DialogueManager.instance.DisplayDialogue(lines, isPerson);
            DialogueManager.instance.ShouldActivateQuestAtEndOfDialogue(questToMark, markQuestComplete);
        }
    }

    // In the OnTriggerEnter2D and OnTriggerExit2D methods, we check for collisions with the player.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
