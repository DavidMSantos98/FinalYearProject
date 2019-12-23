using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrototypeMovement : MonoBehaviour
{
    private Rigidbody2D PlayerRB;
    public BoxCollider2D GroundCheck;
    private LayerMask walkableLayers;
    
    //MOVEMENT VARIABLES
    
    //Horizontal Movement
    private float horizontalMovement;
    private bool facingRight;
    public float maxSpeed;

    //Vertical Movement
    public float jumpHeight;
    void Start()
    {
        walkableLayers = LayerMask.NameToLayer("Ground");
        facingRight = true;
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
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
        bool isGrounded = GroundCheck.IsTouchingLayers(LayerMask.NameToLayer("Ground"));
        if (isGrounded) { Debug.Log("TouchingGround"); }
        if (isGrounded && Input.GetAxis("Jump")>0)
        {
            Debug.Log("Jump");
            PlayerRB.AddForce(new Vector2(0, jumpHeight));
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

