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

    public Collider2D objectCollider;
    public Collider2D anotherCollider;

    //edited values for making the respawn mechanic work properly
    public float movementSpeed;
        public float movementJump;

    //Movement
        float speed;
        float jump;
        float moveVelocity;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        speed = movementSpeed;
        jump = movementJump;
    }
   

    //Grounded Vars
    bool isGrounded = true;
        
        void Update()
    {

        //checks if the player is touching the ground
        if (objectCollider.IsTouching(anotherCollider))
        {
           isGrounded = true;
        }
        else
        {
         isGrounded = false;
        }
           
        //Jumping
        if (Input.GetButton("Jump"))
        {
            if (isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
            }

        }

        if (lives < 0)
        {
            StartCoroutine(Dead());
        }

        IEnumerator Dead()
            {
            Debug.Log("dead");
            speed = 0;
            jump = 0;
            GetComponent<Renderer>().enabled = false;
            Player.transform.position = RespawnPoint.position;
            lives = 3;
            yield return new WaitForSeconds(DeathTime);
            speed = movementSpeed;
            jump = movementJump;
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
}