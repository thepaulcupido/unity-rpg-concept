using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    private float halfCameraWidth;
    private float halfCameraHeight;
    private Vector3 bottomLeftMapLimit;
    private Vector3 topRightMapLimit;

    [SerializeField] private Transform target;
    [SerializeField] private Tilemap currentTileMap;

    void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
        
        halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        halfCameraHeight = Camera.main.orthographicSize;

        Vector3 halfCameraSize = new Vector3(halfCameraWidth, halfCameraHeight, 0);

        bottomLeftMapLimit = currentTileMap.localBounds.min + halfCameraSize;
        topRightMapLimit = currentTileMap.localBounds.max - halfCameraSize;

        PlayerController.instance.SetBounds(currentTileMap.localBounds.min, currentTileMap.localBounds.max);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x, 
            target.position.y, 
            transform.position.z
        );

        // bound the camera to the tilemap bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftMapLimit.x, topRightMapLimit.x), 
            Mathf.Clamp(transform.position.y, bottomLeftMapLimit.y, topRightMapLimit.y), 
            transform.position.z
        );
    }
}
