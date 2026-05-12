using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private fields
    private Vector2 movement;

    // Inspector-available fields
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float movementSpeed = 1.0f;

    void Start()
    {
        
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
    }
}
