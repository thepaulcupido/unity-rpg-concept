using UnityEngine;

public class AreaEntrance : AreaTransition
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string areaName = GetAreaTransitionName();

        if (areaName == PlayerController.instance.GetAreaTransitionName())
        {
            PlayerController.instance.transform.position = transform.position;
        }
         else
        {
            Debug.LogWarning("Area transition name does not match the player's area transition name. Player will be placed at the default position.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
