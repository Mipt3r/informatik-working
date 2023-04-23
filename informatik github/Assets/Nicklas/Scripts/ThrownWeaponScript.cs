using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownWeaponScript : MonoBehaviour
{

    public float jumpForce = 15;
    private GameObject player;
    public bool returning = false;
    private Transform tf;
    private Rigidbody2D rg;
    private Transform pTf;
    private Rigidbody2D pRg;
    

    void Start()
    {
        tf = GetComponent<Transform>();
        rg = GetComponent<Rigidbody2D>();
        pTf = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pRg = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (returning)
        {
            rg.AddForce(new Vector2(pTf.position.x - tf.position.x, pTf.position.y - tf.position.y) * 5f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (collision.gameObject.tag == "Player" && !returning)
        {
            pRg.velocity = new Vector2(0, jumpForce);
        }
        if (collision.gameObject.tag == "Player" && returning)
        {
                Destroy(gameObject);
                returning = false;
        }
    }
}
