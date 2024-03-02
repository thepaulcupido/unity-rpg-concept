using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // static variables
    public static PlayerController instance;
    
    //private variables
    private Vector3 mapLimitTopRight;
    private Vector3 mapLimitBottomLeft;
    private Vector3 boundsBuffer = new Vector3(0.25f, 0.5f, 0f);

    // private serialized fields
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private string areaTransitionId;
    public string AreaTransitionId
    {
        get { return areaTransitionId; }
        set { areaTransitionId = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Applying movement speed
        this.rigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movementSpeed;

        anim.SetFloat("MoveX", this.rigidBody.velocity.x);
        anim.SetFloat("MoveY", this.rigidBody.velocity.y);

        // Setting Last moved positon for animations
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            this.anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            this.anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
        
        // Clamping the player to the map
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, this.mapLimitBottomLeft.x, this.mapLimitTopRight.x), 
            Mathf.Clamp(transform.position.y, this.mapLimitBottomLeft.y, this.mapLimitTopRight.y), 
            transform.position.z
        );
    }

    public void SetBounds(Vector3 mapLimitTopRight, Vector3 mapLimitBottomLeft)
    {
        this.mapLimitBottomLeft = mapLimitBottomLeft;
        this.mapLimitTopRight = mapLimitTopRight;
    }
}
