using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private GameObject player;
    private Points points;
    public int pointWorth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Destroy(player);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Death();
            
        }
    }

    void Death()
    {
        Destroy(gameObject);
        points.GetPoints(pointWorth);
    }
}
