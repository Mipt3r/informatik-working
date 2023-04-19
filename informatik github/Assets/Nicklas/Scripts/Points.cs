using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Points : MonoBehaviour
{
    public int currentPoints = 0;
    public int digits = 6;
    private string pointStr;
    private Text ValueText;

    void Start()
    {
        ValueText = GameObject.FindWithTag("PointCounter").GetComponent<Text>();;
    }
    
    public void GetPoints(int newPoints)
    {
        if (currentPoints + newPoints < 0)
        {
            currentPoints = 0;
        }
        else if (currentPoints + newPoints < Math.Pow((double)10, (double)digits) - 1)
        {
            currentPoints += newPoints;
        }
        else
        {
            currentPoints = (int)Math.Pow((double)10, (double)digits) - 1;
        } 

        
        pointStr = new string('0', digits - currentPoints.ToString().Length) + currentPoints.ToString();

        
        ValueText.text = "POINTS: " + pointStr;
    }

}
