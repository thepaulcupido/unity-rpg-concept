using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    [SerializeField]
    private string areaToLoad = "";

    [SerializeField]
    private string areaTransitionId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("running");
        if (otherObject.tag == "Player") {
            // load a new scene
            SceneManager.LoadScene(this.areaToLoad);
            PlayerController.instance.AreaTransitionId =this.areaTransitionId;
        }
    }
}
