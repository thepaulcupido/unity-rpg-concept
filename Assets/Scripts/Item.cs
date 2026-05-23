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
    [SerializeField] private bool effectsHealth, effectsMagic, effectsStrength;

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

    public void useItem(int index)
    {
        CharacterStats[] allChararcterStats = GameManager.instance.GetPlayerStats();

        // don't naively trust the index just because it's an int.
        if (index < 0 || index >= allChararcterStats.Length)
        {
            Debug.LogError("Index provided ("+ index +") not in range of Player Character arrays.");
            return;
        }

        if (isConsumable)
        {
            if (effectsHealth)
            {
                int currentHp = allChararcterStats[index].GetCurrentHP();
                int newHp = currentHp + itemPower;
                int maxHp = allChararcterStats[index].GetMaxHP();
                if (newHp > allChararcterStats[index].GetMaxHP())
                {
                    newHp = maxHp;
                }
                allChararcterStats[index].SetCharacterHealth(newHp);
            }

            if (effectsMagic)
            {
                int currentMp = allChararcterStats[index].GetCurrentMP();
                int newMp = currentMp + itemPower;
                int maxMp = allChararcterStats[index].GetMaxMP();
                if (newMp > allChararcterStats[index].GetMaxMP())
                {
                    newMp = maxMp;
                }
                allChararcterStats[index].SetCharacterMagic(newMp);
            }

            if (effectsStrength)
            {
                int strength = allChararcterStats[index].GetStrength() + itemPower;
                allChararcterStats[index].SetCharacterStrength(strength);
            }
        } 
        else if (isWeapon)
        {
            string equippedWeapon = allChararcterStats[index].GetWeaponName();

            if (equippedWeapon != "")
            {
                GameManager.instance.AddItem(equippedWeapon);
            }

            allChararcterStats[index].SetEquippedWeapon(itemName);
            allChararcterStats[index].SetWeaponPower(weaponStrength);
        }
        else if (isArmour)
        {
            string equippedArmour = allChararcterStats[index].GetArmorName();

            if (equippedArmour != "")
            {
                GameManager.instance.AddItem(equippedArmour);
            }

            allChararcterStats[index].SetEquippedArmour(itemName);
            allChararcterStats[index].SetArmourPower(armourStrength);
        }
        

        GameManager.instance.UpdatePlayerStats(allChararcterStats);
        GameManager.instance.RemoveItem(itemName);
    }

    public string GetItemName() => itemName;
    public string GetItemDescription() => itemDescription;
    public Sprite GetItemSprite() => itemSprite;
    public bool isWeaponOrArmour() => isWeapon || isArmour;
}
