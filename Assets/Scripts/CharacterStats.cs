using UnityEngine;

/// <summary>
/// This class represents the stats of a character in the game. 
/// It includes attributes such as health, strength, defence, and experience points. 
/// It also includes information about the character's level, equipped weapon and armour, and their image.
/// This class can be used to manage the character's stats and update them as the game progresses.
/// </summary>
public class CharacterStats : MonoBehaviour
{

    /// <summary>
    /// The character's name, level, health, experience points, gold, strength, 
    /// defence, weapon power, armour power, max health, max magic, equipped weapon and armour, 
    /// and character image are all defined as serialized fields.
    /// </summary>
    [SerializeField] private string characterName = "Tim";
    [SerializeField] private int characterLevel = 1;
    [SerializeField] private int characterHealth = 0;
    [SerializeField] private int experiencePoints = 0;
    [SerializeField] private int gold = 0;

    [SerializeField] private int strength = 15;
    [SerializeField] private int defence = 12;
    [SerializeField] private int weaponPower = 0;
    [SerializeField] private int armourPower = 0;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxMagic = 30;


    [SerializeField] private string equippedWeapon = "";
    [SerializeField] private string equippedArmour = "";
    [SerializeField] private Sprite characterImage;

    [SerializeField] private int maxLevel = 100;
    [SerializeField] private int baseExperience = 1000;
    [SerializeField] private int[] expToNextLevel;
   
    void Start()
    {
        if (maxLevel < 2)
        {
            throw new System.ArgumentException("Max level must be at least 2.");
        }

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExperience;

        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }
}
