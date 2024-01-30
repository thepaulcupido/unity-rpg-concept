using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

        if (PlayerController.instance != null) {
            target = PlayerController.instance.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (target == null && PlayerController.instance != null) {
            target = PlayerController.instance.transform;
        } 
    }
}
