using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void SetItemsForSale(string[] newItems)
    {
        itemsForSale = new string[newItems.Length];

        for (int i = 0; i < itemsForSale.Length; i++)
        {
            itemsForSale[i] = newItems[i];
        }
    }
}
