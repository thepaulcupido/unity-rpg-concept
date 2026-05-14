using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : AreaTransition
{
    [SerializeField] private string areaToLoad;
    [SerializeField] private AreaEntrance areaEntrance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string areaName = GetAreaTransitionName();
        areaEntrance.SetAreaTransitionName(areaName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // GameManager.Instance.LoadArea(areaToLoad);
            string areaName = GetAreaTransitionName();
            SceneManager.LoadScene(areaToLoad);
            PlayerController.instance.SetAreaTransitionName(areaName);
        }
    }

}