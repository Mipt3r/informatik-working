using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private GameObject player;
    private Points points;
    public int pointWorth;
    public int damageAmount = 1;
     
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            Death();
        }
        if (collision.gameObject.tag == "ThrownWeapon")
        {
            Death();
        }
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<playerScript>().TakeDamage(damageAmount);
        }
    }

    void Death()
    {
        Destroy(gameObject);
        points.GetPoints(pointWorth);
    }
}
