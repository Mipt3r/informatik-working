using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    public GameObject wallTop;
    public GameObject wallBottom;
    public float speed = 2;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == wallTop)
        {
            speed = -speed;
        }

        if (collision.gameObject == wallBottom)
        {
            speed = -speed;
        }
    }
}
