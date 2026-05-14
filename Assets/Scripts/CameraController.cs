using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;

    void Start()
    {
        target = PlayerController.instance.transform;
        Debug.Log("Camera target set to player.");
        Debug.Log("player instance: " + PlayerController.instance);
        Debug.Log("player transform: " + target);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x, 
            target.position.y, 
            transform.position.z
        );
    }
}
