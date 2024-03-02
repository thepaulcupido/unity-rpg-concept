using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    private bool loadAfterFade;

    [SerializeField] private float loadingDelay = 10f;

    [SerializeField] private string areaToLoad = "";

    [SerializeField] private string areaTransitionId;

    [SerializeField] private AreaEntrance entrance;

    // Start is called before the first frame update
    void Start()
    {
        //entrance.AreaTransitionId = areaTransitionId;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.loadAfterFade) {
            this.loadingDelay -= Time.deltaTime;
            if (this.loadingDelay <= 0) {
                this.loadAfterFade = false;
                SceneManager.LoadScene(this.areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player") {
            PlayerController.instance.AreaTransitionId =this.areaTransitionId;
            
            this.loadAfterFade = true;
            if (UIFade.instance != null) {
                UIFade.instance.FadeToBlack();
            }
        }
    }
}
