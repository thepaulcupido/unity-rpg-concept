using UnityEngine;

/// <summary>
/// This class represents the stats of a character in the game. 
/// 
/// It includes the character's name, level, health, magic, experience points, gold, 
/// strength, defence, weapon power, armour power, max health, max magic, 
/// equipped weapon and armour, and character image.
/// 
/// It also includes methods to add experience points, check for level up, and update stats on level up.
/// </summary>
public class CharacterStats : MonoBehaviour
{

    [SerializeField] private string characterName;
    [SerializeField] private int characterLevel;
    [SerializeField] private int characterHealth;
    [SerializeField] private int characterMagic;
    [SerializeField] private int experiencePoints;
    [SerializeField] private int gold;

    [SerializeField] private int strength;
    [SerializeField] private int defence;
    [SerializeField] private int weaponPower;
    [SerializeField] private int armourPower;
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxMagic;


    [SerializeField] private string equippedWeapon = "";
    [SerializeField] private string equippedArmour = "";
    [SerializeField] private Sprite characterImage;

    [SerializeField] private int maxLevel = 100;
    [SerializeField] private int baseExperience = 1000;
    [SerializeField] private int[] expToNextLevel;
    [SerializeField] private int[] magicLevelBonusArray;
   
    void Start()
    {
        if (maxLevel < 2)
        {
            throw new System.ArgumentException("Max level must be at least 2.");
        }

        magicLevelBonusArray = new int[maxLevel];
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExperience;

        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }

        characterHealth = maxHealth;
        characterMagic = maxMagic;
    }

    /// <summary>
    /// This method adds experience points to the character and checks if they have leveled up.
    /// </summary>
    /// <param name="amount"></param>
    public void AddExperience(int amount)
    {
        experiencePoints += amount;
        if (characterLevel <= maxLevel)
        {
            CheckLevelUp();
        }
    }


    /// <summary>
    /// This method checks if the character has enough experience points to level up.
    /// </summary>
    public void CheckLevelUp()
    {
        if (characterLevel < maxLevel && experiencePoints >= expToNextLevel[characterLevel])
        {
            experiencePoints -= expToNextLevel[characterLevel];
            characterLevel++;
            UpdateStatsOnLevelUp();
        }
    }

    /// <summary>
    /// This method updates the character's stats when they level up. 
    /// </summary>
    public void UpdateStatsOnLevelUp()
    {
        maxHealth = Mathf.FloorToInt(maxHealth * 1.05f);
        characterHealth = maxHealth;

        maxMagic += magicLevelBonusArray[characterLevel];
        characterMagic = maxMagic;

        if (characterLevel % 2 == 0)
        {
            strength += 2;
        } 
        else
        {
            defence += 2;    
        }

    } 

    /// <summary>
    /// The following methods are getters for the character's stats. 
    /// </summary>
    public string GetCharacterName() => characterName;
    public int GetLevel() => characterLevel;
    public int GetCurrentHP() => characterHealth;
    public int GetMaxHP() => maxHealth;
    public int GetCurrentMP() => characterMagic;
    public int GetMaxMP() => maxMagic;
    public int GetCurrentEXP() => experiencePoints;
    public int GetExpToNextLevel() => expToNextLevel[characterLevel];

    public int GetStrength() => strength;
    public int GetDefense() => defence;
    public int GetWeaponPower() => weaponPower;
    public int GetArmorPower() => armourPower;
    public string GetWeaponName() => equippedWeapon;
    public string GetArmorName() => equippedArmour;
    public Sprite GetCharacterImage() => characterImage;
}
