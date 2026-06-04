using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the in-game shop UI and inventory, 
/// allowing the player to buy and sell items with a shopkeeper NPC.
/// </summary>
public class Shop : MonoBehaviour
{
    public static Shop instance;

    [Header ("Shop Menus")]
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject buyMenu;
    [SerializeField] private GameObject sellMenu;

    [Header ("Player Information")]
    [SerializeField] private Text goldText;

    [Header ("Shop Items")]
    [SerializeField] private string[] itemsForSale;
    [SerializeField] private ItemButton[] buyItemButtons;
    [SerializeField] private ItemButton[] sellItemButtons;
    [SerializeField] private Item selectedItem;

    [Header ("Buy Items")]
    [SerializeField] private Text buyItemNameText;
    [SerializeField] private Text buyItemDescriptionText;
    [SerializeField] private Text buyItemValueText;

    [Header ("Sell Items")]
    [SerializeField] private Text sellItemNameText;
    [SerializeField] private Text sellItemDescriptionText;
    [SerializeField] private Text sellItemValueText;

    /// <summary>
    /// Initializes the <see cref="Shop"/> singleton instance on scene load.
    /// If an instance already exists, the duplicate is destroyed and a warning is logged.
    /// </summary>
    void Awake()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Shop detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Opens the shop UI and initializes it to the buy menu view.
    /// Activates the shop menu, switches to the buy menu, notifies the
    /// <see cref="GameManager"/> that the shop is open, and refreshes
    /// the displayed gold amount to reflect the player's current balance.
    /// </summary>
    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();
        GameManager.instance.OpenShopMenu();

        goldText.text = GameManager.instance.GetCurrentGold().ToString() + "g";
    }

    /// <summary>
    /// This method closes the shop but setting the shop menu to inactive and calling the Game Manager's CloseShopMenu method
    /// </summary>
    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.CloseShopMenu();
    }

    /// <summary>
    /// This method sets the buy menu to active and the sell menu to inactive.
    /// It then iterates through the shop's inventory of items for sale, retrieves the details for each item 
    /// from the Game Manager, and updates the corresponding buy item button.
    /// </summary>
    public void OpenBuyMenu()
    {
        // early exit if no buy item buttons are assigned to avoid null reference exceptions
        if (buyItemButtons == null || buyItemButtons.Length == 0)
        {
            Debug.LogWarning("No buy item buttons assigned in the shop.");
            return;
        }
 
        // activate the buy menu and deactivate the sell menu
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        // simulate pressing the first buy item button to initialize the buy item details display
        buyItemButtons[0].Press();

        // iterate through the shop's inventory of items for sale and update each buy item button to display the corresponding item
        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            if (itemsForSale[i] != "" && itemsForSale[i] != null)
            {
                Item itemDetails = GameManager.instance.GetItemDetails(itemsForSale[i]);
  
                if (itemDetails != null)
                {
                    buyItemButtons[i].SetItemImage(itemDetails.ItemSprite);
                    buyItemButtons[i].SetItemAmountText("");
                    buyItemButtons[i].SetValue(i);
                    buyItemButtons[i].gameObject.SetActive(true);
                }
            } 
            else
            {
                buyItemButtons[i].SetItemAmountText("");
                buyItemButtons[i].SetItemImage(null);
            }
        }
    }

    /// <summary>
    /// This method sets the sell menu to active and the buy menu to inactive.
    /// It then retrieves the player's current inventory from the Game Manager, sorts it, 
    /// and updates each sell item button to display the corresponding item image.
    /// </summary>
    public void OpenSellMenu()
    {
        // early exit if no sell item buttons are assigned to avoid null reference exceptions
        if (sellItemButtons == null || sellItemButtons.Length == 0)
        {
            Debug.LogWarning("No sell item buttons assigned in the shop.");
            return;
        }

        // activate the sell menu and deactivate the buy menu
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        GameManager gameMan = GameManager.instance;
    
        // sort the player's inventory to ensure empty slots are at the end of the array
        gameMan.SortItems();

        // retrieve the player's current inventory from the Game Manager
        string[] itemsHeldByPlayer = gameMan.ItemsHeldByPlayer;

        // simulate pressing the first sell item button to initialize the sell item details display
        sellItemButtons[0].Press();
        
        // iterate through the sell item buttons and update each one to display the corresponding item from the player's inventory
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].SetValue(i);

            if (gameMan.ItemsHeldByPlayer[i] != "")
            {
                string text = gameMan.NumberOfItemsHeldByPlayer[i].ToString();
                Item itemDetails = gameMan.GetItemDetails(itemsHeldByPlayer[i]);
                
                if (itemDetails != null)
                {
                    sellItemButtons[i].SetItemImage(itemDetails.ItemSprite);
                    sellItemButtons[i].SetItemAmountText(text);
                    sellItemButtons[i].gameObject.SetActive(true);
                    continue;
                }
                Debug.LogWarning("Item details not found for item: " + gameMan.ItemsHeldByPlayer[i]);
            }

            sellItemButtons[i].SetItemAmountText("");
            sellItemButtons[i].SetItemImage(null);    
        }
    }
    
    // getters for shop properties
    public string[] ItemsForSale => itemsForSale;
    public GameObject ShopMenuUI => shopMenu;
    public GameObject ShopBuyMenuUI => buyMenu;
    public GameObject ShopSellMenuUI => sellMenu;
    public bool IsOpen => shopMenu.activeInHierarchy;

    /// <summary>
    /// Replaces the shop's current inventory with a new set of items available for purchase.
    /// Performs a shallow clone of <paramref name="newItems"/>, ensuring the internal
    /// inventory is decoupled from the caller's array reference.
    /// </summary>
    /// <param name="newItems">
    /// A non-null, non-empty string array of item identifiers to stock in the shop.
    /// </param>
    public void SetItemsForSale(string[] newItems)
    {
        if (newItems == null)
        {
            throw new ArgumentNullException(nameof(newItems), "Items for sale array cannot be null.");
        }

        if (newItems.Length == 0)
        {
            throw new ArgumentException("Items for sale array cannot be empty.", nameof(newItems));
        }

        itemsForSale = (string[])newItems.Clone();
    }

    /// <summary>
    /// This method is called when the player selects an item from the shop's buy menu to view its details.
    /// </summary>
    /// <param name="item"></param>
    public void SelectBuyItem(Item item)
    {
        selectedItem = item;
        buyItemNameText.text = item.ItemName;
        buyItemDescriptionText.text = item.ItemDescription;
        buyItemValueText.text = "Value: " + item.ItemValue.ToString() + "g";
    }

    /// <summary>
    /// This method is called when the player selects an item from their inventory to sell in the shop.
    /// </summary>
    /// <param name="item"></param>
    public void SelectSellItem(Item item)
    {
        string itemValueText = Mathf.FloorToInt(item.ItemValue * 0.5f).ToString();

        selectedItem = item;
        sellItemNameText.text = item.ItemName;
        sellItemDescriptionText.text = item.ItemDescription;
        sellItemValueText.text = "Value: " + itemValueText + "g";
    }


}
