using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    private Transform attackPoint;
    private Transform player;
    public GameObject hitBox;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            Destroy(GameObject.FindWithTag("Weapon"), 0F);
            Instantiate(hitBox, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).transform.parent = transform;
        }
    }
}
