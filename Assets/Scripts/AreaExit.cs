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

    [SerializeField]
    private AreaEntrance entrance;

    // Start is called before the first frame update
    void Start()
    {
        //entrance.AreaTransitionId = areaTransitionId;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.tag == "Player") {
            PlayerController.instance.AreaTransitionId =this.areaTransitionId;
            SceneManager.LoadScene(this.areaToLoad);
        }
    }
}
