using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovementTest : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }
    //Movement
    public float speed;
    public float jump;
   
    float moveVelocity;

    //Grounded Vars
    bool isGrounded = true;


    void Update()
    {
        //Jumping
        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
               
                isGrounded = false;
            }

        }


        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetButton("Left"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);

            moveVelocity = -speed;
        }
        if (Input.GetButton("Right"))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            moveVelocity = speed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }
   
    //Check if Grounded
    void OnTriggerEnter2D()
    {
        isGrounded = true;
    }
}