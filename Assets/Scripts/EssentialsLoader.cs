using UnityEngine;

/// <summary>
/// This class is responsible for loading essential game objects.
/// </summary>
public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIScreen;
    [SerializeField] private GameObject GameManagerObj;

    /// <summary>
    /// In the Start method, check if the essential game objects (PlayerController, UIFade, and GameManager) 
    /// are already instantiated. If not, we instantiate them and assign them to their respective singleton instances.
    /// </summary>
    void Start()
    {
        if (PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
            Debug.Log("Player instantiated.");
        }
        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
            Debug.Log("UIFade instantiated.");
        }
        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(GameManagerObj).GetComponent<GameManager>();
            Debug.Log("GameManager instantiated.");
        }
    }
}
