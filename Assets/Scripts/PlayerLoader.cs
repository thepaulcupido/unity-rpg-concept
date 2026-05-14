using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    void Start()
    {
        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }
}
