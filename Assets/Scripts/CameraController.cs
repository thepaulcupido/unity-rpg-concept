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


    [SerializeField]
    private Tilemap sceneTileMap;


    // Start is called before the first frame update
    void Start()
    {
        this.mapLimitTopRight = this.sceneTileMap.localBounds.max;
        this.mapLimitBottomLeft = this.sceneTileMap.localBounds.min;

        if (Camera.main != null) {
            this.halfCameraHeight = Camera.main.orthographicSize;
            this.halfCameraWidth = this.halfCameraHeight * Camera.main.aspect;

            //Setting the Camera limits in the context of the assigned TileMap and Camera size
            this.mapLimitTopRight -=  new Vector3(this.halfCameraWidth, this.halfCameraHeight, 0f);
            this.mapLimitBottomLeft += new Vector3(this.halfCameraWidth, this.halfCameraHeight, 0f);
        } else {
            Debug.Log("Main camera is not tagged as 'MainCamera' in the scene.");
        }

        
        if (PlayerController.instance != null && target == null) {
            // Centering the camera on the player
            target = PlayerController.instance.transform;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keeps the camera inside the bounds of the screen
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, this.mapLimitBottomLeft.x, this.mapLimitTopRight.x), 
            Mathf.Clamp(transform.position.y, this.mapLimitBottomLeft.y, this.mapLimitTopRight.y), 
            transform.position.z
        );
    }
}
