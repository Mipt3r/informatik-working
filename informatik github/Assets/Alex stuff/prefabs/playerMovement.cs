using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    //enables the player to 
    Rigidbody2D rb;

    public GameObject Player;



    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    //Movement
    public float speed;
    public float jump;
   
    float moveVelocity;

    //Grounded Vars
    bool isGrounded = true;

    public int lives = 3;
   

    void OnTriggerEnter2D(Collider2D col)
    {
        isGrounded = true;
        if (col.gameObject.tag == "Enemy")
        {
            lives--;
            if (lives > 0)
            {
                StartCoroutine(Dead());
            }
        }
        IEnumerator Dead()
        {
            Debug.Log("dead");
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(5);
            Debug.Log("respawn");
            GetComponent<Renderer>().enabled = true;
        }
    }
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
            moveVelocity = -speed;
        }
        if (Input.GetButton("Right"))
        {
            moveVelocity = speed;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);

    }
   
    //Check if Grounded
    void OnTriggerEnter2D()
    {
        
    }
}