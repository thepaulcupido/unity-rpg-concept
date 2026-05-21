using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This class is responsible for controlling the camera in the game.
/// It follows the target (usually the player) while ensuring that the camera stays within the bounds of the tilemap.
/// </summary>
public class CameraController : MonoBehaviour
{
    private float halfCameraWidth;
    private float halfCameraHeight;
    private Vector3 bottomLeftMapLimit;
    private Vector3 topRightMapLimit;

    [SerializeField] private Transform target;
    [SerializeField] private Tilemap currentTileMap;

    /// <summary>
    /// In the Start method, we calculate the half width and height of the camera based on its orthographic size and aspect ratio.
    /// We then calculate the bottom left and top right limits of the camera based on the bounds of the tilemap and the half size of the camera.
    /// Finally, we set the bounds of the player controller to match the bounds of the tilemap
    /// </summary>
    void Start()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        target = FindAnyObjectByType<PlayerController>().transform;
        
        halfCameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        halfCameraHeight = Camera.main.orthographicSize;

        Vector3 halfCameraSize = new Vector3(halfCameraWidth, halfCameraHeight, 0);

        bottomLeftMapLimit = currentTileMap.localBounds.min + halfCameraSize;
        topRightMapLimit = currentTileMap.localBounds.max - halfCameraSize;

        PlayerController.instance.SetBounds(currentTileMap.localBounds.min, currentTileMap.localBounds.max);
    }

    /// <summary>
    /// In the LateUpdate method, we update the camera's position to follow the target while 
    /// ensuring that the camera stays within the bounds of the tilemap.
    /// </summary>
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
