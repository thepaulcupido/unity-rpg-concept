using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") ,Input.GetAxisRaw("Vertical")) * movementSpeed;
    }
}
