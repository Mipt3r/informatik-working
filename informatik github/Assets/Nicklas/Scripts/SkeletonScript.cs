using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public GameObject wallLeft;
    public GameObject wallRight;
    public float speed = 2;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == wallLeft)
        {
            speed = -speed;
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        if (collision.gameObject == wallRight)
        {
            speed = -speed;
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
