using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 mapLimitTopRight;
    private Vector3 mapLimitBottomLeft;

    private float halfCameraHeight;
    private float halfCameraWidth;


    [SerializeField] private Tilemap sceneTileMap;


    // Start is called before the first frame update
    void Start()
    {
        
        this.sceneTileMap.CompressBounds();
        this.mapLimitTopRight = this.sceneTileMap.localBounds.max;
        this.mapLimitBottomLeft = this.sceneTileMap.localBounds.min;
  
        Debug.Log("text - " + mapLimitTopRight);
        Debug.Log("text - " + mapLimitBottomLeft);

        if (Camera.main != null) {
            this.halfCameraHeight = Camera.main.orthographicSize;
            this.halfCameraWidth = this.halfCameraHeight * Camera.main.aspect;
        } else {
            Debug.Log("Main camera is not tagged as 'MainCamera' in the scene.");
        }

        if (PlayerController.instance != null) {
            PlayerController.instance.SetBounds(this.mapLimitTopRight, this.mapLimitBottomLeft);
            target = PlayerController.instance.transform;
        } else {
            Debug.Log("Player does not exist at runtime: Start()");
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (PlayerController.instance != null && target == null) {
            target = PlayerController.instance.transform;
            PlayerController.instance.SetBounds(this.mapLimitTopRight, this.mapLimitBottomLeft);
            Debug.Log("Player bounds and Camera target set at runtime: LateUpdate()");
        }

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        
        // Keeps the camera inside the bounds of the screen
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, this.mapLimitBottomLeft.x + this.halfCameraWidth, this.mapLimitTopRight.x - this.halfCameraWidth), 
            Mathf.Clamp(transform.position.y, this.mapLimitBottomLeft.y + this.halfCameraHeight, this.mapLimitTopRight.y - this.halfCameraHeight), 
            transform.position.z
        );
    }
}
