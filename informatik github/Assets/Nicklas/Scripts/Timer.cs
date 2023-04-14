using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float maxTime = 60.0f;
    private Points points;
    private string timeStr;
    public int digits = 3;
    private Text ValueText;

    void Start()
    {
        maxTime += 1;
        ValueText = GameObject.FindWithTag("Timer").GetComponent<Text>();
        points = GameObject.FindWithTag("PointCounter").GetComponent<Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if (maxTime >= 0.5f)
        {
            maxTime -= Time.deltaTime;

            timeStr = "";
            while (timeStr.Length < digits - Math.Floor(maxTime).ToString().Length)
            {
                timeStr += '0';
            }

            timeStr += Math.Floor(maxTime).ToString();
        
            ValueText.text = "TIME: " + timeStr;
        }
    }

    public void LevelEnd()
    {
        points.GetPoints(Convert.ToInt32(Math.Floor(maxTime) * 10));
    }
}
