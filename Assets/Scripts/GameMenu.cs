using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuUI;
    [SerializeField] private CharacterStats[] playerStats;
    [SerializeField] private Text[] nameText, mpText, hpText, expText, levelText;
    [SerializeField] private Image[] characterImage;
    [SerializeField] private Slider[] slider;
    [SerializeField] private GameObject[] characterStatsArray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ToggleGameMenu();
        }
    }

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

                characterStatsArray[i].SetActive(true);
            } 
            else
            {
                characterStatsArray[i].SetActive(false);
            }
        }
    }

    public void ToggleGameMenu()
    {
        if (gameMenuUI.activeSelf)
        {
            GameManager.instance.CloseGameMenu();
        }
        else
        {
            UpdateMainStats();
            GameManager.instance.OpenGameMenu();
        }
        gameMenuUI.SetActive(!gameMenuUI.activeSelf);
    }
}
