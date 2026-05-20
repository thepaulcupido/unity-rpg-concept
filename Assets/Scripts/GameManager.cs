using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] CharacterStats[] playerStats;

    void Start()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Game Manager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
