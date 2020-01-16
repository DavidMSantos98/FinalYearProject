using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrototypeMovement : MonoBehaviour
{
    private Rigidbody2D PlayerRB;
    public Collider2D GroundCheckCollider;
    private Collider2D PlayerCollider;

    public Transform spearTip;

    //MOUSE VARIABLES
    public Vector3 mousePos;
    public Transform cursor;
    
    //MOVEMENT VARIABLES

    //Horizontal Movement
    private float horizontalMovement;
    public bool facingRight;
    public float maxSpeed;

    //Vertical Movement
    public float jumpHeight;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    void Start()
    {
        PlayerCollider = GetComponent<Collider2D>();
        //GroundCheckCollider = GroundCheck.GetComponent<Collider2D>();
        facingRight = true;
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //HORIZONTAL MOVEMENT
        horizontalMovement = Input.GetAxis("Horizontal");
        PlayerRB.velocity = new Vector2(horizontalMovement * maxSpeed, PlayerRB.velocity.y);

        if (horizontalMovement > 0 && !facingRight)
        {
            flip();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            flip();
        }

        //VERTICAL MOVEMENT
        bool isGrounded = GroundCheckCollider.IsTouchingLayers(LayerMask.GetMask(new string[] { "Ground" }));
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            PlayerRB.velocity = Vector2.up * jumpHeight;
        }

        
        if (PlayerRB.velocity.y < 0)
        {
            PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }else if (PlayerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            PlayerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Update()
    {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            cursor.position = mousePos;

    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

