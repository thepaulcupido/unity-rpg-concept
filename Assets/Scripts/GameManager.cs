using UnityEngine;

/// <summary>
/// This class is responsible for managing the overall state of the game, including tracking whether the game menu is open, whether dialogue is active, and whether the player is moving between areas.
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header ("Player Inventory")]
    [SerializeField] private string[] itemsHeldByPlayer;
    [SerializeField] private int[] numberOfItemsHeldByPlayer;
    [SerializeField] private Item[] referenceItems;

    [Header ("Game State")]
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

    /// <summary>
    /// In the Update method, we check the current game state and enable or disable player movement accordingly.
    /// </summary>
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

    /// <summary>
    /// The following methods are used to manage the game state, allowing other scripts to open and close the game menu, 
    /// start and end dialogue, and start and end area transitions. 
    /// 
    /// These methods also ensure that the player's movement is appropriately enabled or disabled based on the current game state.
    /// </summary>
    public void OpenGameMenu() => isGameMenuOpen = true;
    public void CloseGameMenu() => isGameMenuOpen = false;

    public void StartDialogue() => isDialogueActive = true;
    public void EndDialogue() => isDialogueActive = false;

    public void StartAreaTransition() => isMovingBetweenAreas = true;
    public void EndAreaTransition() => isMovingBetweenAreas = false;

    public CharacterStats[] GetPlayerStats() => playerStats;

    public int[] GetNumberOfItemsHeld() => numberOfItemsHeldByPlayer;
    public string[] GetItemsHeld() => itemsHeldByPlayer;

    /// <summary>
    /// This method takes an item name as input and searches through the referenceItems array to find a matching item.
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public Item GetItemDetails(string itemName)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].name == itemName)
            {
                return referenceItems[i];
            }
        }

        Debug.LogWarning("Item not found: " + itemName);
        return null;
    }
}
