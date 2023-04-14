using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Points : MonoBehaviour
{
    public Text ValueText;

    public int points = 0;
    private string pointStr;
    
    // Update is called once per frame
    void Update()
    {
        pointStr = "";
        while (pointStr.Length < 6 - points.ToString().Length)
        {
            pointStr += '0';
        }

        pointStr += points.ToString();
        ValueText.text = "Points: " + pointStr;
    }


}
