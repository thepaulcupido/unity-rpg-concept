using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : AreaTransition
{

    // Private fields
    private bool loadSceneAfterFade = false;

    // Serialized fields - these can be set in the Unity Inspector
    [SerializeField] private float loadingTime = 1f;
    [SerializeField] private string areaToLoad;
    [SerializeField] private AreaEntrance areaEntrance;

    void Start()
    {
        string areaName = GetAreaTransitionName();
        areaEntrance.SetAreaTransitionName(areaName);
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            loadSceneAfterFade = true;
            UIFade.instance.StartFadeToBlack();

            string areaName = GetAreaTransitionName();
            PlayerController.instance.SetAreaTransitionName(areaName);
        }
    }

}