using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls fading effects on a UI element.
/// </summary>
public class UIFade : MonoBehaviour
{
    // static variables
    public static UIFade instance;

    // private variables
    private bool fadeFromBlack;
    private bool fadeToBlack = true;

    // serialized fields
    [SerializeField] private float fadeSpeed;
    [SerializeField] private Image fadeScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Set instance to this script to allow for easy access from other scripts
        instance = this;

        // Prevent this object from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if fading to black is required
        if (this.fadeToBlack)
        {
            // Adjust the alpha value of the fade screen color to simulate fading to black
            this.fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.b,
                fadeScreen.color.g,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime)
            );
            Debug.Log(fadeScreen.color.a);

            // Check if fade to black is complete
            if (fadeScreen.color.a == 1)
            {
                // Set fadeToBlack to false to stop the fade effect
                this.fadeToBlack = false;
            }
        }

        // Check if fading from black is required
        if (this.fadeFromBlack)
        {
            // Adjust the alpha value of the fade screen color to simulate fading from black
            this.fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.b,
                fadeScreen.color.g,
                Mathf.MoveTowards(fadeScreen.color.a, 0, fadeSpeed * Time.deltaTime)
            );

            // Check if fade from black is complete
            if (fadeScreen.color.a == 0)
            {
                // Set fadeFromBlack to false to stop the fade effect
                this.fadeFromBlack = false;
            }
        }
    }

    /// <summary>
    /// Fades the UI element to black.
    /// </summary>
    public void FadeToBlack()
    {
        // Set fadeToBlack to true and fadeFromBlack to false to start the fade to black effect
        this.fadeToBlack = true;
        this.fadeFromBlack = false;
    }

    /// <summary>
    /// Fades the UI element from black.
    /// </summary>
    public void FadeFromBlack()
    {
        // Set fadeFromBlack to true and fadeToBlack to false to start the fade from black effect
        this.fadeToBlack = false;
        this.fadeFromBlack = true;
    }
}