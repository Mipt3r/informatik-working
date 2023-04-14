using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text ValueText;
    public float targetTime = 60.0f;
    public Points points;

    // Update is called once per frame
    void Update()
    {
        if (targetTime >= 1.0f)
        {
            targetTime -= Time.deltaTime;
            ValueText.text = "Time: " + Math.Floor(targetTime);
        }
    }

    public void LevelEnd()
    {
        points.points += Convert.ToInt32(Math.Floor(targetTime) * 10);
    }
}
