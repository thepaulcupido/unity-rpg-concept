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

    [SerializeField] private int itemNumberLimit = 999;

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

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Mana Potion");
            RemoveItem("Health Potion");
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
    public Item[] GetReferenceItems() => referenceItems;

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
    
    /// <summary>
    /// This method sorts the player's inventory by moving all non-empty inventory 
    /// slots to the left and all empty slots to the right.
    /// </summary>
    public void SortItems()
    {
        bool isItemAfterEmpty = true;

        while (isItemAfterEmpty == true)
        {
            isItemAfterEmpty = false;

            for (int i = 0; i < itemsHeldByPlayer.Length - 1; i++)
            {
                if (itemsHeldByPlayer[i] == "")
                {
                    itemsHeldByPlayer[i] = itemsHeldByPlayer[i + 1];
                    numberOfItemsHeldByPlayer[i] = numberOfItemsHeldByPlayer[i + 1];
                    itemsHeldByPlayer[i + 1] = "";
                    numberOfItemsHeldByPlayer[i + 1] = 0;

                    if (itemsHeldByPlayer[i] != "")
                    {
                        isItemAfterEmpty = true;
                    }
                }
            }   
        }
    }

    /// <summary>
    /// This method adds an item to the player's inventory. 
    /// It first checks if the item exists in the referenceItems array, then checks if the item is already held by the player.
    /// 
    /// If the item is already held, it increments the quantity. If the item is not held, it finds the first empty slot and adds the item there.
    /// If there are no empty slots available, it logs a warning message.
    /// </summary>
    /// <param name="itemToAdd"></param>
    public void AddItem(string itemToAdd)
    {
        
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].name == itemToAdd)
            {
                for (int j = 0; j < itemsHeldByPlayer.Length; j++)
                {
                    if (itemsHeldByPlayer[j] == itemToAdd)
                    {
                        if (numberOfItemsHeldByPlayer[j] >= itemNumberLimit)
                        {
                            Debug.LogWarning("Cannot hold more than " + itemNumberLimit + " of the same item: " + itemToAdd);
                            return;
                        }

                        numberOfItemsHeldByPlayer[j]++;
                        GameMenu.instance.ShowItemButtons();
                        return;
                    }
                }

                for (int j = 0; j < itemsHeldByPlayer.Length; j++)
                {
                    if (itemsHeldByPlayer[j] == "")
                    {
                        if (IsItemInReferenceItems(itemToAdd) == false)
                        {
                            Debug.LogWarning("Item not found in reference items: " + itemToAdd);
                            return;
                        }

                        itemsHeldByPlayer[j] = itemToAdd;
                        numberOfItemsHeldByPlayer[j] = 1;
                        GameMenu.instance.ShowItemButtons();
                        return;
                    }
                }

                Debug.LogWarning("No empty inventory slots available to add item: " + itemToAdd);
                return;
            }
        }

        Debug.LogWarning("Item not found in reference items: " + itemToAdd);
    }

    public void RemoveItem(string itemToRemove)
    {
        for (int i = 0; i < itemsHeldByPlayer.Length; i++)
        {
            if (itemsHeldByPlayer[i] == itemToRemove)
            {
                numberOfItemsHeldByPlayer[i]--;

                if (numberOfItemsHeldByPlayer[i] <= 0)
                {
                    itemsHeldByPlayer[i] = "";
                    numberOfItemsHeldByPlayer[i] = 0;
                }

                SortItems();
                GameMenu.instance.ShowItemButtons();
                return;
            }
        }

        Debug.LogWarning("Item not found in player's inventory: " + itemToRemove);
    }

    /// <summary>
    /// This method checks if a given item name exists in the referenceItems array. 
    /// It returns true if the item is found and false if it is not found.
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public bool IsItemInReferenceItems(string itemName)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].name == itemName)
            {
                return true;
            }
        }

        return false;
    }
}
