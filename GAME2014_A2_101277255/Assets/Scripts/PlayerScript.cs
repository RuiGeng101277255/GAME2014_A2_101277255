/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: PlayerScript.cs
 Last Modified: November 21st, 2021
 Description: Defines the player's behaviour
 Version History: v1.01 Initial blank script and internal documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Input")]
    public Joystick joystick;
    public float sensitivity;

    [Header("Movement & Grounding")]
    public Rigidbody2D playerRB;
    public static bool jumpButtonDown;
    public float horizontalForce;
    public float verticalForce;

    public bool grounded;
    public Transform groundLevel;
    public float groundRadius;
    public LayerMask groundLayerMask;

    public AudioSource jumpSFX;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        CheckIfGrounded();
    }

    private void Move()
    {
        float x = (Input.GetAxisRaw("Horizontal") + joystick.Horizontal) * sensitivity;

        Debug.Log(x);

        if (grounded)
        {
            // Keyboard Input
            float y = (Input.GetAxisRaw("Vertical") + joystick.Vertical) * sensitivity;
            float jump = Input.GetAxisRaw("Jump") + ((jumpButtonDown) ? 1.0f : 0.0f);

            // jump activated
            if (jump > 0)
            {
                //jumpSFX.Play();
            }

            // Check for Flip

            if (x != 0)
            {
                //x = FlipAnimation(x);
                //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.RUN); // RUN State
                //state = PlayerAnimationState.RUN;
                //CreateDustTrail();
            }
            else
            {
                //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.IDLE); // IDLE State
                //state = PlayerAnimationState.IDLE;
            }

            float horizontalMoveForce = x * horizontalForce;
            float jumpMoveForce = jump * verticalForce;

            float mass = playerRB.mass * playerRB.gravityScale;


            playerRB.AddForce(new Vector2(horizontalMoveForce, jumpMoveForce) * mass);
            playerRB.velocity *= 0.99f; // scaling / stopping hack
        }
        else // Air Control
        {
            //animatorController.SetInteger("AnimationState", (int)PlayerAnimationState.JUMP); // JUMP State
            //state = PlayerAnimationState.JUMP;

            if (x != 0)
            {
                //x = FlipAnimation(x);
                //
                //float horizontalMoveForce = x * horizontalForce * airControlFactor;
                //float mass = playerRB.mass * playerRB.gravityScale;
                //
                //playerRB.AddForce(new Vector2(horizontalMoveForce, 0.0f) * mass);
            }
        }
    }


    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundLevel.position, groundRadius, Vector2.down, groundRadius, groundLayerMask);

        grounded = (hit) ? true : false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundLevel.position, groundRadius);
    }
}
