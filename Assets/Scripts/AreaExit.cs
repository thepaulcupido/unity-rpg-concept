using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class represents an area exit in the game.
/// It handles the transition effects and loads the new scene when the player exits an area.
/// </summary>
public class AreaExit : AreaTransition
{

    // Private fields
    private bool loadSceneAfterFade = false;

    // Inspector-available fields
    [SerializeField] private float loadingTime = 1f;
    [SerializeField] private string areaToLoad;
    [SerializeField] private AreaEntrance areaEntrance;

    void Start()
    {
        string areaName = AreaTransitionName;
        areaEntrance.SetAreaTransitionName(areaName);
    }

    /// <summary>
    /// In the Update method, we check if the scene should be loaded after the fade effect. 
    /// If so, we count down the loading time and load the new scene when the time reaches zero.
    /// This allows us to synchronize the scene loading with the fade effect, ensuring a smooth transition for the player.
    /// </summary>
    void Update()
    {
        if (loadSceneAfterFade)
        {
            loadingTime -= Time.deltaTime;

            if (loadingTime <= 0f)
            {
                loadSceneAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    /// <summary>
    /// When the player enters the trigger collider of the area exit, this method starts the fade to black effect 
    /// and set the player's area transition name to match the exit's transition name.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loadSceneAfterFade = true;
            UIFade.instance.StartFadeToBlack();
            GameManager.instance.StartAreaTransition();

            string areaName = AreaTransitionName;
            PlayerController.instance.SetAreaTransitionName(areaName);
        }
    }

}