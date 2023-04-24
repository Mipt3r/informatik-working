using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeScript : MonoBehaviour
{
    public Transform player;
    
    void Update()
    {
        if (transform.position.x - player.position.x > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x - player.position.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
    }
}
