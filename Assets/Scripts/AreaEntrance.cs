using UnityEngine;

public class AreaEntrance : AreaTransition
{

    void Start()
    {
        string areaName = GetAreaTransitionName();
        string playerAreaName = PlayerController.instance.GetAreaTransitionName();

        if (areaName == playerAreaName)
        {
            PlayerController.instance.transform.position = transform.position;
        }
         else
        {
            Debug.LogWarning("Area transition name does not match the player's area transition name. Player will be placed at the default position.");
        }

        UIFade.instance.StartFadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
