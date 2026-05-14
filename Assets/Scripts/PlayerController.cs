using System;
using UnityEngine;

public class PlayerController : AreaTransition
{
    // Private fields
    private Vector2 movement;

    // Inspector-available fields
    [SerializeField] private Animator playerMovementAnimator;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float movementSpeed = 1.0f;

    // Public fields
    public static PlayerController instance;

    void Start()
    {
        // @todo this is a bit of a hack to make the player controller a singleton. It would be better to use a proper singleton pattern or a dependency injection framework to manage the player's dependencies.

       if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        playerRigidBody.linearVelocity = movement * movementSpeed;
        playerMovementAnimator.SetFloat("move_x", playerRigidBody.linearVelocity.x);
        playerMovementAnimator.SetFloat("move_y", playerRigidBody.linearVelocity.y);


        //@todo this is a bit of a hack to get the last movement direction for the idle animation. It would be better to use a state machine or something similar to keep track of the player's state and direction.
        if (Math.Abs(movement.x) == 1 || Math.Abs(movement.y) == 1)
        {
            playerMovementAnimator.SetFloat("last_movement_x", movement.x);
            playerMovementAnimator.SetFloat("last_movement_y", movement.y);
        }
    }
}
