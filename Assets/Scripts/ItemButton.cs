using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{

    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemAmountText;
    [SerializeField] private int itemValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        Item item;
        string[] itemsHeldByPlayer = GameManager.instance.GetItemsHeld();

        if (itemsHeldByPlayer[itemValue] != "")
        {
            item = GameManager.instance.GetItemDetails(itemsHeldByPlayer[itemValue]);
            GameMenu.instance.SetActiveItem(item);
        }
    }
}
