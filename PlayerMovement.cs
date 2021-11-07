using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;


    public Vector2 vec = new Vector2(1,1);
    public LayerMask whatIsGround;
    private float xSpeed;
    private float ySpeed;
    public bool jumping, sprinting, grounded, dashing, readyToJump = true;
    public float dashForce = 100;
    public float maxSpeed = 10;
    private float threshold = 0.01f;
    public float speed = 4500;
    public float jumpForce = 550f;
    float jumpCooldown = 0.25f;
    public float runMultiplier;
    public float counterMovement = 0.175f;
    public float maxSlopeAngle = 35f;
    public bool readyToDash = true;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
        MyInput();
    }

    private void Movement()
    {

        //Find actual velocity relative to where player is looking

        //Counteract sliding and sloppy movement

        //If holding jump && ready to jump, then jump
        if (readyToJump && jumping) Jump();

        //Set max speed
        float maxSpeed = this.maxSpeed;
        bool isGoingLeft = false;
        bool isGoingRight = false;
        if (xSpeed > 0) isGoingRight = true; else if (xSpeed < 0) isGoingLeft = true;

        if (dashing && readyToDash)
        {
            if (isGoingRight)
            {
                rb.AddForce(vec*dashForce);
                readyToDash = false;
                Invoke(nameof(ResetDash), 1f);
            }
            else if (isGoingLeft)
            {
                rb.AddForce(new Vector2(-1, 3.75f) * dashForce);
                readyToDash = false;
                Invoke(nameof(ResetDash), 1f);
            }
            else
            {
                rb.AddForce(new Vector2(0f, 5f) * dashForce);
                readyToDash = false;
                Invoke(nameof(ResetDash), 1f);
            }



        }

        //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
        if (xSpeed > 0 && rb.velocity.x > maxSpeed) xSpeed = 0;
        if (xSpeed < 0 && rb.velocity.x < -maxSpeed) xSpeed = 0;
        //Some multipliers
        float multiplier = 1f, multiplierV = 1f;

        // Movement in air
        if (!grounded)
        {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }


        //Apply forces to move player
        rb.AddForce(Vector3.right * xSpeed * speed * Time.deltaTime * multiplier * multiplierV);
    }
    private void Jump()
    {
        if (grounded && readyToJump)
        {

            readyToJump = false;

            //Add jump forces
            
            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(Vector2.up * jumpForce * 0.5f);

            //If jumping while falling, reset y velocity.
            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector2(vel.x, 0);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector2(vel.x, vel.y / 2);
            grounded = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    private void ResetDash()
    {
        readyToDash = true;
    }
    private void CounterMovement(float x)
    {
        if (!grounded || jumping) return;


        //Counter movement
        if (Mathf.Abs(rb.velocity.x) > threshold && Mathf.Abs(x) < 0.05f || (rb.velocity.x < -threshold && x > 0) || (rb.velocity.x > threshold && x < 0))
        {
            rb.AddForce(speed * Vector3.right * Time.deltaTime * -rb.velocity.x * counterMovement);
        }

    }

    private void MyInput()
    {
        xSpeed = Input.GetAxis("Horizontal");
        jumping = Input.GetKey(KeyCode.W);
        dashing = Input.GetButton("Dash");
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    /// <summary>
    /// Handle ground detection
    /// </summary>
    private void OnCollisionStay2D(Collision2D other)
    {
        if ((int)other.gameObject.layer == (int)whatIsGround)
        {
            grounded = true;
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }


}
