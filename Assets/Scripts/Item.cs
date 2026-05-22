using UnityEngine;

public class Item : MonoBehaviour
{

    [Header ("Item Details")]
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemValue;
    [SerializeField] private Sprite itemSprite;

    [Header ("Item Type")]
    [SerializeField] private bool isConsumable;

    [Header ("Item Details")]
    [SerializeField] private bool isWeapon;
    [SerializeField] private bool isArmour;
    [SerializeField] private bool effectsHealth, effectsMana, effectsStrength;

    [Header ("Item Stats")]
    [SerializeField] private int itemPower;
    [SerializeField] private int weaponStrength;
    [SerializeField] private int armourStrength;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetItemName() => itemName;
    public string GetItemDescription() => itemDescription;
    public Sprite GetItemSprite() => itemSprite;
}
