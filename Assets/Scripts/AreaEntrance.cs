using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    private bool hasLoaded = false;

    [SerializeField] private string areaTransitionId;
    public string AreaTransitionId
    {
        get { return areaTransitionId; }
        set { areaTransitionId = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance != null && areaTransitionId == PlayerController.instance.AreaTransitionId) {
            PlayerController.instance.transform.position = transform.position;
        }


        if (UIFade.instance != null) {
            UIFade.instance.FadeFromBlack();
        } else {
            Debug.Log("UIFade instance not available: AreaEntrance.Start()");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasLoaded && UIFade.instance != null)  {
            UIFade.instance.FadeFromBlack();
            this.hasLoaded = true;
            // todo: implement a timer here and freeze player movement until the scene has faded in
            Debug.Log("UIFade.instance.FdeFromBlack called: AreaEntrance.Update()");
        }
    }
}
