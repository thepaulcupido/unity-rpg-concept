using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a button in the inventory UI that corresponds to an item held by the player character.
/// </summary>
public class ItemButton : MonoBehaviour
{

    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private int itemValue;

    // setters for item button properties
    public void SetValue(int value) => itemValue = value;
    public void SetItemImage(Sprite newSprite)
    {
        itemImage.sprite = newSprite;
        itemImage.color = newSprite != null ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0f);
    }
    public void SetItemAmountText(string amount) => itemAmountText.text = amount;

    public void Press()
    {
        Shop shop = Shop.instance;
        GameManager gameMan = GameManager.instance;

        if (GameMenu.instance.GameMenuUI.activeInHierarchy)
        {
            Item item;
            string[] itemsHeldByPlayer = gameMan.GetItemsHeld();

            if (itemsHeldByPlayer[itemValue] != "")
            {
                item = gameMan.GetItemDetails(itemsHeldByPlayer[itemValue]);
                GameMenu.instance.SetActiveItem(item);
            }
        }

    
        if (shop.ShopMenuUI.activeInHierarchy)
        {
            if (shop.ShopBuyMenuUI.activeInHierarchy)
            {
                shop.SelectBuyItem(gameMan.GetItemDetails(shop.ItemsForSale[itemValue]));
            }
            
            if (shop.ShopSellMenuUI.activeInHierarchy)
            {
                shop.SelectSellItem(gameMan.GetItemDetails(gameMan.ItemsHeldByPlayer[itemValue]));
            }
        }
    }
}
