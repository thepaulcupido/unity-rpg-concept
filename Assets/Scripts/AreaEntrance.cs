using UnityEngine;

/// <summary>
/// This class represents an area entrance in the game. 
/// It places the player at the correct position when they enter a new area and handles the transition effects.
/// It inherits from the AreaTransition class, which provides common functionality for area transitions.
/// </summary>
public class AreaEntrance : AreaTransition
{

    /// <summary>
    /// In the Start method, we check if the area transition name of the entrance matches the player's area transition name.
    /// If they match, we set the player's position to the position of the entrance.
    /// </summary>
    void Start()
    {
        string areaName = AreaTransitionName;
        string playerAreaName = PlayerController.instance.AreaTransitionName;

        if (areaName == playerAreaName)
        {
            PlayerController.instance.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning($"Area transition name ({areaName}) does not match the player's area transition name ({playerAreaName}) "+ 
            "Player will be placed at the default position.");
        }

        UIFade.instance.StartFadeFromBlack();
        GameManager.instance.EndAreaTransition();
    }

}