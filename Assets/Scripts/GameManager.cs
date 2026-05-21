using UnityEngine;

/// <summary>
/// This class is responsible for managing the overall state of the game, including tracking whether the game menu is open, whether dialogue is active, and whether the player is moving between areas.
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private bool isGameMenuOpen, isDialogueActive, isMovingBetweenAreas;
    [SerializeField] private CharacterStats[] playerStats;

    void Start()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Game Manager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameMenuOpen || isDialogueActive || isMovingBetweenAreas)
        {
            PlayerController.instance.DisableMovement();
        }
        else
        {
            PlayerController.instance.EnableMovement();
        }
    }

    // @todo - look into a modern event system to replace these methods.

    public void OpenGameMenu() => isGameMenuOpen = true;
    public void CloseGameMenu() => isGameMenuOpen = false;

    public void StartDialogue() => isDialogueActive = true;
    public void EndDialogue() => isDialogueActive = false;

    public void StartAreaTransition() => isMovingBetweenAreas = true;
    public void EndAreaTransition() => isMovingBetweenAreas = false;

    public CharacterStats[] GetPlayerStats() => playerStats;

}
