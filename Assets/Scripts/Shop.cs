using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the in-game shop UI and inventory, providing the player with
/// the ability to buy and sell items. Exposes controls for opening and
/// closing the shop, switching between buy and sell views, and updating
/// the shop's available inventory at runtime.
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

  
    /// <summary>
    /// Initializes the <see cref="Shop"/> singleton instance on scene load.
    /// If an instance already exists, the duplicate is destroyed and a warning is logged.
    /// The surviving instance is marked to persist across scene loads via
    /// <see cref="Object.DontDestroyOnLoad"/>.
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
    /// This method closes the shop but setting the shop menu to inactive and calling the Game Manager's CloseShop method
    /// </summary>
    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.CloseShopMenu();
    }

    /// <summary>
    /// This method sets the buy menu to active and the sell menu to inactive.
    /// </summary>
    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
    }

    /// <summary>
    /// This method sets the sell menu to active and the buy menu to inactive.
    /// </summary>
    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
    }

    public GameObject GetShopMenu() => shopMenu;
    public bool isOpen => shopMenu.activeInHierarchy;

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
}
