using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
