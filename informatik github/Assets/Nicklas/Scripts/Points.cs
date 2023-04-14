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
        if (currentPoints + newPoints < Math.Pow((double)10, (double)digits) - 1){
        currentPoints += newPoints;
        }
        else{
            currentPoints = (int)Math.Pow((double)10, (double)digits) - 1;
        } 

        pointStr = "";
        while (pointStr.Length < digits - currentPoints.ToString().Length)
        {
            pointStr += '0';
        }

        pointStr += currentPoints.ToString();
        ValueText.text = "POINTS: " + pointStr;
    }

}
