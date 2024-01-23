using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movementSpeed;

        anim.SetFloat("MoveX", this.rigidBody.velocity.x);
        anim.SetFloat("MoveY", this.rigidBody.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            this.anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            this.anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
        
    }
}
