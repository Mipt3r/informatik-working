using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerScript : MonoBehaviour
{
    //enables the player to move and defines public uses like lives and gameobjects that need to be connected
    Rigidbody2D rb;
    public int lives = 3;
    public GameObject Player;
    public Transform RespawnPoint;
    //why is nothing working
    public int DeathTime;

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

        if (lives < 0)
        {
            StartCoroutine(Dead());
        }

        IEnumerator Dead()
            {
            Debug.Log("dead");
            GetComponent<Renderer>().enabled = false;
            Player.transform.position = RespawnPoint.position;
            lives = 3;
            yield return new WaitForSeconds(5);
            Debug.Log("respawn");
            GetComponent<Renderer>().enabled = true;
            
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