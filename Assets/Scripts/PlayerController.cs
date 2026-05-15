using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : AreaTransition
{
    // Private fields
    private bool canMove = true;
    private Vector2 movement;
    private Vector3 bottomLeftMapLimit;
    private Vector3 topRightMapLimit;

    // Inspector-available fields
    [SerializeField] private Animator playerMovementAnimator;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float movementSpeed = 1.0f;

    // Public fields
    public static PlayerController instance;

    void Start()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Player detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // @Todo replace the movement system since this version has been marked for deprecation by Unity. The new system is called the Input System and can be found in the Package Manager.
    void Update()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")
            ).normalized;
    }

    void FixedUpdate()
    {
        playerRigidBody.linearVelocity = canMove ? movement * movementSpeed : Vector2.zero;
        
        playerMovementAnimator.SetFloat("move_x", playerRigidBody.linearVelocity.x);
        playerMovementAnimator.SetFloat("move_y", playerRigidBody.linearVelocity.y);

        //@todo this is a bit of a hack to get the last movement direction for the idle animation. It would be better to use a state machine or something similar to keep track of the player's state and direction.
        if (Math.Abs(movement.x) == 1 || Math.Abs(movement.y) == 1)
        {
            if (canMove)
            {
                playerMovementAnimator.SetFloat("last_movement_x", movement.x);
                playerMovementAnimator.SetFloat("last_movement_y", movement.y);
            }
        }

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftMapLimit.x, topRightMapLimit.x), 
            Mathf.Clamp(transform.position.y, bottomLeftMapLimit.y, topRightMapLimit.y), 
            transform.position.z
        );
    }

    public void SetBounds(Vector3 bottomLeft, Vector3 topRight)
    {
        bottomLeftMapLimit = bottomLeft + new Vector3(0.5f, 0.5f, 0);
        topRightMapLimit = topRight - new Vector3(0.5f, 0.5f, 0);
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    
    public void EnableMovement()
    {
        canMove = true;
    }
}
