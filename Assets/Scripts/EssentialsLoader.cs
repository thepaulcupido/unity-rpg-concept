using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UIScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
