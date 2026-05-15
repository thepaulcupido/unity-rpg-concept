using System;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    // Serialized fields
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float fadeToBlackThreshold = 0.99f;
    [SerializeField] private float fadeFromBlackThreshold = 0.01f;

    private bool fadeToBlack = false;
    private bool fadeFromBlack = false;


    void Awake()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of UIFade detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (fadeToBlack)
        {
            fadeImage.color = new Color(
                fadeImage.color.r, 
                fadeImage.color.g, 
                fadeImage.color.b, 
                Mathf.MoveTowards(
                    fadeImage.color.a, 
                    1f, 
                    fadeSpeed * Time.deltaTime
                )
            );

            if (fadeImage.color.a >= fadeToBlackThreshold)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            fadeImage.color = new Color(
                fadeImage.color.r, 
                fadeImage.color.g, 
                fadeImage.color.b, 
                Mathf.MoveTowards(
                    fadeImage.color.a, 
                    0f, 
                    fadeSpeed * Time.deltaTime
                )
            );

            if (fadeImage.color.a <= fadeFromBlackThreshold)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadeFromBlack = true;
        fadeToBlack = false;
    }
}
