using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeThrowScript : MonoBehaviour
{
    public Transform player;
    public GameObject Projectile;
    public float throwCooldown = 2;
    public float throwVelocity = 2;
    public float agroRange = 8;
    private float currentThrowCooldown;
    private Vector2 throwVect;


    // Start is called before the first frame update
    void Start()
    {
        currentThrowCooldown = throwCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        throwVect = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);

        if (currentThrowCooldown >= 0.1)
        {
            currentThrowCooldown -= Time.deltaTime;
        }
        else if (currentThrowCooldown < 0.1 && throwVect.magnitude < agroRange)
        {
            throwVect /= throwVect.magnitude;

            var Banana = Instantiate(Projectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Banana.GetComponent<Rigidbody2D>().velocity = throwVect * throwVelocity;
            Destroy (Banana, 3);
            currentThrowCooldown = throwCooldown;
        }
    }
}
