using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for managing the game menu, including displaying the player's stats and toggling the menu on and off.
/// It retrieves the player stats from the GameManager and updates the UI elements accordingly when the menu is opened. 
/// It also checks for player input to toggle the menu and updates the GameManager to indicate whether the menu is open or closed.
/// </summary>
public class GameMenu : MonoBehaviour
{
    [Header ("Game Menu UI")]
    [SerializeField] private GameObject gameMenuUI;
    [SerializeField] private GameObject[] subMenuWindows;

    [Header ("Player Stats")]
    [SerializeField] private CharacterStats[] playerStats;
    [SerializeField] private Text[] nameText, mpText, hpText, expText, levelText;
    [SerializeField] private Image[] characterImage;
    [SerializeField] private Slider[] slider;
    [SerializeField] private GameObject[] characterStatsArray;

    [SerializeField] private GameObject[] StatusButtons;
    [SerializeField] private Text StatusNameText, StatusHPText, StatusMPText, StatusExpText, 
    StatusStrText, StatusDefText, StatusWpnText, StatusArmText, StatsArmPowerText, StatusWpnPowText;
    [SerializeField] private Image StatusCharacterImage;

    [Header ("Player Inventory")]
    [SerializeField] private ItemButton[] itemButtons;
    [SerializeField] private string selectedItem;
    [SerializeField] private Item activeItem;
    [SerializeField] private Text itemNameText, itemDescriptionText, useButtonText;

    [SerializeField] private GameObject itemCharacterChoiceMenu;
    [SerializeField] private Text[] itemCharacterChoiceNames;
    [SerializeField] private Text currentGoldText;


    public static GameMenu instance;

     void Awake()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Game Menu detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    void Start()
    {
        gameMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ToggleGameMenu();
        }
    }

    /// <summary>
    /// This method updates the main stats of the player characters in the game menu.
    /// It retrieves the player stats from the GameManager and updates the UI elements accordingly.
    /// </summary>
    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                nameText[i].text = playerStats[i].GetCharacterName();
                mpText[i].text = "MP: " + playerStats[i].GetCurrentMP() + "/" + playerStats[i].GetMaxMP();
                hpText[i].text = "HP: " + playerStats[i].GetCurrentHP() + "/" + playerStats[i].GetMaxHP();
                expText[i].text = "EXP: " + playerStats[i].GetCurrentEXP() + "/" + playerStats[i].GetExpToNextLevel();
                levelText[i].text = "Level: " + playerStats[i].GetLevel();
                characterImage[i].sprite = playerStats[i].GetCharacterImage();
                slider[i].maxValue = playerStats[i].GetExpToNextLevel();
                slider[i].value = playerStats[i].GetCurrentEXP();

                currentGoldText.text = GameManager.instance.GetCurrentGold() +"g";

                characterStatsArray[i].SetActive(true);
            } 
            else
            {
                characterStatsArray[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// This method toggles the game menu on and off. 
    /// If the game menu is currently active, it will be closed. 
    /// If it is currently inactive, it will be opened and the main stats will be updated.
    /// </summary>
    public void ToggleGameMenu()
    {
        if (gameMenuUI.activeSelf)
        {
            CloseAllSubMenus();
            itemCharacterChoiceMenu.SetActive(false);
        }
        else
        {
            OpenGameMenu();
        }
    }

    /// <summary>
    /// This method closes the game menu and updates the GameManager to indicate that the game menu is no longer open.
    /// </summary>
    public void CloseGameMenu()
    {
        GameManager.instance.CloseGameMenu();
        gameMenuUI.SetActive(false);

        itemCharacterChoiceMenu.SetActive(false);
    }

    /// <summary>
    /// This method opens the game menu, updates the main stats, and updates the GameManager to indicate that the game menu is open.
    /// </summary>
    public void OpenGameMenu()
    {
        UpdateMainStats();
        GameManager.instance.OpenGameMenu();
        gameMenuUI.SetActive(true);
    }

    /// <summary>
    /// This method toggles the sub-menu windows within the game menu. It takes an integer parameter (windowNumber) that indicates which sub-menu window to display.
    /// </summary>
    /// <param name="windowNumber"></param>
    public void ToggleWindow(int windowNumber)
    {
        UpdateMainStats();

        for (int i = 0; i < subMenuWindows.Length; i++)
        {
            if (i == windowNumber)
            {
                subMenuWindows[i].SetActive(!subMenuWindows[i].activeInHierarchy);
            } 
            else
            {
                subMenuWindows[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// This method closes all sub-menu windows and the main game menu. 
    /// It is called when the player wants to exit the game menu and return to gameplay.
    /// </summary>
    public void CloseAllSubMenus()
    {
        for (int i = 0; i < subMenuWindows.Length; i++)
        {
            subMenuWindows[i].SetActive(false);
        }

        CloseGameMenu();
    }

    /// <summary>
    /// This method opens the status menu within the game menu and updates the character stats displayed in the status menu.
    /// It retrieves the player stats from the GameManager and updates the UI elements accordingly.
    /// </summary>
    public void OpenStatusMenu()
    {
        UpdateMainStats();
        UpdateCharacterStats(0);
        
        for (int i = 0; i < StatusButtons.Length; i++)
        {
            StatusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            StatusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].GetCharacterName();

        }
    }

    /// <summary>
    /// This method updates the character stats displayed in the status menu of the game menu. 
    /// It takes an integer parameter (selected) that indicates which character's stats to display.
    /// </summary>
    /// <param name="selected"></param>
    public void UpdateCharacterStats(int selected)
    {
        string weaponText = playerStats[selected].GetWeaponName().ToString();
        string armourText = playerStats[selected].GetArmorName().ToString();
        int experienceToLevel = playerStats[selected].GetExpToNextLevel() - playerStats[selected].GetCurrentEXP();

        StatusNameText.text = playerStats[selected].GetCharacterName();
        StatusHPText.text = playerStats[selected].GetCurrentHP() + "/" + playerStats[selected].GetMaxHP();
        StatusMPText.text = playerStats[selected].GetCurrentMP() + "/" + playerStats[selected].GetMaxMP();
        StatusExpText.text = experienceToLevel.ToString();
        StatusStrText.text = playerStats[selected].GetStrength().ToString();
        StatusDefText.text = playerStats[selected].GetDefense().ToString();
        StatusWpnText.text = weaponText != "" ? weaponText : "None";
        StatusArmText.text = armourText != "" ? armourText: "None";
        StatsArmPowerText.text = playerStats[selected].GetArmorPower().ToString();
        StatusWpnPowText.text = playerStats[selected].GetWeaponPower().ToString();
        StatusCharacterImage.sprite = playerStats[selected].GetCharacterImage();
    }

    /// <summary>
    /// This method updates the item buttons in the game menu based on the player's inventory.
    /// It retrieves the player's inventory from the GameManager and updates the item buttons accordingly, 
    /// displaying the item sprite and amount for each item held by the player.
    /// </summary>
    public void ShowItemButtons()
    {
        GameManager.instance.SortItems();
        string[] itemsHeld = GameManager.instance.GetItemsHeld();
        int[] numberOfItemsHeld = GameManager.instance.GetNumberOfItemsHeld();
        
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].SetValue(i);

            if (itemsHeld.Length > 0 && itemsHeld[i] != "")
            {
                Item itemDetails = GameManager.instance.GetItemDetails(itemsHeld[i]);
                itemButtons[i].SetItemImage(itemDetails.GetItemSprite());
                itemButtons[i].SetItemAmountText(numberOfItemsHeld[i].ToString());
                itemButtons[i].gameObject.SetActive(true);
            } 
            else
            {
                // itemButtons[i].gameObject.SetActive(false);
                itemButtons[i].SetItemAmountText("");
                itemButtons[i].SetItemImage(null);
            }
        }
    }

    public void SetActiveItem(Item item)
    {
        activeItem = item;

        if (activeItem != null)
        {
            useButtonText.text = activeItem.isWeaponOrArmour() ? "Equip" : "Use";
            itemNameText.text = activeItem.GetItemName();
            itemDescriptionText.text = activeItem.GetItemDescription();
        }
         else
        {
            itemNameText.text = "";
            itemDescriptionText.text = "";
            useButtonText.text = "Use";
        }
    }

    public void DiscardItem()
    {
        if (activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.GetItemName());
            SetActiveItem(null);
        }
    }

    public void OpenItemCharacterChoice()
    {
        CharacterStats[] stats = GameManager.instance.GetPlayerStats();
        itemCharacterChoiceMenu.SetActive(true);

        for (int i = 0; i < itemCharacterChoiceNames.Length; i++)
        {
            itemCharacterChoiceNames[i].text = stats[i].GetCharacterName();
            itemCharacterChoiceNames[i].transform.parent.gameObject.SetActive(stats[i].gameObject.activeInHierarchy);
        }
    }

    public void CLoseItemCharacterChoice()
    {
        itemCharacterChoiceMenu.SetActive(false);
    }

    public void UseItem(int characterIndex)
    {
        activeItem.UseItem(characterIndex);
        CLoseItemCharacterChoice();
    }    
}
