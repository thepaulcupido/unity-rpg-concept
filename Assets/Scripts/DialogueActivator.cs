using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for activating dialogue when the player interacts with certain game objects (such as NPCs or signs).
/// It checks for player input and proximity to the game object, and if the conditions are met, it triggers the dialogue system to display the appropriate dialogue lines.
/// </summary>
public class DialogueActivator : MonoBehaviour
{

    private bool canActivate = false;
    [SerializeField] private bool isPerson = true;
    [SerializeField] private string[] lines;


    /// <summary>
    /// In the Update method, we check if the player can activate the dialogue (i.e., they are in proximity to the game object and have pressed the "Fire1" button),
    /// and if there are dialogue lines to display. We also check if the dialogue UI is not already active to prevent overlapping dialogues. If all conditions are met, we call the DisplayDialogue method of the DialogueManager to show the dialogue lines.
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
