using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalScript : MonoBehaviour
{
    private Points points;
    private Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
        timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            points.GetPoints(Convert.ToInt32(Math.Floor(timer.maxTime) * 10));
            timer.maxTime = 0;
            StartCoroutine(End());

            IEnumerator End()
            {
                yield return new WaitForSeconds(5);
                Application.Quit();
            }
        }
    }
}
