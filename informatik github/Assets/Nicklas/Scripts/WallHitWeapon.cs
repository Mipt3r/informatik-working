using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHitWeapon : MonoBehaviour
{

    public float jumpForce = 15;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpForce);
        }

    }
}
