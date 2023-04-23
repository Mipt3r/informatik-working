using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeaponScript : MonoBehaviour
{

    public float jumpForce = 15;
    private Transform tf;
    private Rigidbody2D rg;
    private Transform pTf;
    private Rigidbody2D pRg;
    private Animator animation;
    public float time = 2;
    private float currentTime;
    private Vector2 returnVect;
    private float xMovement;
    private float yMovement;

    

    void Start()
    {
        currentTime = time;
        tf = GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        pTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pRg = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (animation.GetBool("returning"))
        {
            xMovement = pTf.position.x - tf.position.x;
            yMovement = pTf.position.y - tf.position.y;

            returnVect = new Vector2(xMovement, yMovement);
            
            returnVect /= (float)Math.Sqrt(Math.Pow((double)xMovement, 2) + Math.Pow((double)yMovement, 2));

            rg.velocity = returnVect * 20f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (animation.GetBool("returning"))
            {
                Destroy(gameObject);
            }
            else if (animation.GetBool("stopped"))
            {
                pRg.velocity = new Vector2(0, jumpForce);
            }
        }
        else if (collision.gameObject.tag != "Enemy" && !animation.GetBool("returning"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animation.SetBool("stopped", true);
            animation.Play("Stopped", 0, 0.2f);
        } 
    }
}
