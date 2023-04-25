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
    private Animator animator;
    private float currentTime;
    private Vector2 returnVect;

    void Start()
    {
        tf = GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pRg = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (animator.GetBool("returning"))
        {
            returnVect = new Vector2(pTf.position.x - tf.position.x, pTf.position.y - tf.position.y);
            
            returnVect /= returnVect.magnitude;

            rg.velocity = returnVect * 20f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !animator.GetBool("returning"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animator.SetBool("stopped", true);
            animator.Play("stopped", 0, 0.2f);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("returning", true);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (animator.GetBool("returning"))
            {
                Destroy(gameObject);
            }
            else if (animator.GetBool("stopped"))
            {
                pRg.velocity = new Vector2(0, jumpForce);
            }
        }
    }
}
