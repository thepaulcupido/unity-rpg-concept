using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuUI;


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

    public void ToggleGameMenu()
    {
        if (gameMenuUI.activeSelf)
        {
            GameManager.instance.CloseGameMenu();
        }
        else
        {
            GameManager.instance.OpenGameMenu();
        }
        gameMenuUI.SetActive(!gameMenuUI.activeSelf);
    }
}
